using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class LiquidacionService : ILiquidacionService
    {
        private readonly ILiquidacionRepository liquidacionRepository;
        private readonly IPaseadorRepository paseadorRepository;
        private readonly IPaseoRepository paseoRepository;

        public LiquidacionService(ILiquidacionRepository liquidacionRepository, IPaseadorRepository paseadorRepository,
                                  IPaseoRepository paseoRepository)
        {
            this.liquidacionRepository = liquidacionRepository;
            this.paseadorRepository = paseadorRepository;
            this.paseoRepository = paseoRepository;
        }

        public async Task<LiquidacionDTO> AddAsync(LiquidacionDTO dto)
        {
            Liquidacion liquidacion = await ArmarLiquidacionAsync(0, dto, DateTime.Now);

            await liquidacionRepository.AddAsync(liquidacion);

            // Se vuelve a leer para devolverla con los nombres de paseador y perros
            Liquidacion? guardada = await liquidacionRepository.GetAsync(liquidacion.Id);
            return MapToDTO(guardada ?? liquidacion);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await liquidacionRepository.DeleteAsync(id);
        }

        public async Task<LiquidacionDTO?> GetAsync(int id)
        {
            Liquidacion? liquidacion = await liquidacionRepository.GetAsync(id);

            if (liquidacion == null)
                return null;

            return MapToDTO(liquidacion);
        }

        public async Task<IEnumerable<LiquidacionDTO>> GetAllAsync()
        {
            var liquidaciones = await liquidacionRepository.GetAllAsync();
            return liquidaciones.Select(MapToDTO).ToList();
        }

        public async Task<bool> UpdateAsync(LiquidacionDTO dto)
        {
            // Recuperar la existente para preservar la FechaAlta
            var existing = await liquidacionRepository.GetAsync(dto.Id);
            if (existing == null)
                return false;

            Liquidacion liquidacion = await ArmarLiquidacionAsync(dto.Id, dto, existing.FechaAlta);
            return await liquidacionRepository.UpdateAsync(liquidacion);
        }

        public async Task<IEnumerable<PaseoDTO>> GetPaseosPendientesAsync(int paseadorId, DateTime fechaDesde, DateTime fechaHasta)
        {
            if (fechaDesde.Date > fechaHasta.Date)
            {
                throw new ArgumentException("La fecha desde no puede ser posterior a la fecha hasta.");
            }

            var paseos = await liquidacionRepository.GetPaseosPendientesAsync(paseadorId, fechaDesde, fechaHasta);
            return paseos.Select(PaseoService.MapToDTO).ToList();
        }

        // Arma la cabecera y le agrega cada paseo: las reglas de cada línea las valida el dominio
        private async Task<Liquidacion> ArmarLiquidacionAsync(int id, LiquidacionDTO dto, DateTime fechaAlta)
        {
            if (await paseadorRepository.GetAsync(dto.PaseadorId) == null)
            {
                throw new ArgumentException($"No existe un paseador con el Id {dto.PaseadorId}.");
            }

            Liquidacion liquidacion = new Liquidacion(id, dto.PaseadorId, dto.FechaDesde, dto.FechaHasta, fechaAlta);

            foreach (int paseoId in dto.Detalles.Select(d => d.PaseoId).Distinct())
            {
                Paseo? paseo = await paseoRepository.GetAsync(paseoId);
                if (paseo == null)
                {
                    throw new ArgumentException($"No existe un paseo con el Id {paseoId}.");
                }

                // Regla de negocio: un paseo se le paga una sola vez al paseador
                int? excludeId = id == 0 ? null : id;
                if (await liquidacionRepository.PaseoEstaLiquidadoAsync(paseoId, excludeId))
                {
                    throw new ArgumentException($"El paseo {paseoId} ya está incluido en otra liquidación.");
                }

                liquidacion.AgregarPaseo(paseo);
            }

            liquidacion.ValidarQueTengaDetalles();
            return liquidacion;
        }

        private static LiquidacionDTO MapToDTO(Liquidacion liquidacion)
        {
            return new LiquidacionDTO
            {
                Id = liquidacion.Id,
                PaseadorId = liquidacion.PaseadorId,
                FechaDesde = liquidacion.FechaDesde,
                FechaHasta = liquidacion.FechaHasta,
                FechaAlta = liquidacion.FechaAlta,
                Total = liquidacion.CalcularTotal(),
                PaseadorNombre = liquidacion.Paseador != null ? $"{liquidacion.Paseador.Apellido}, {liquidacion.Paseador.Nombre}" : "",
                Detalles = liquidacion.Detalles
                    .OrderBy(d => d.Paseo?.FechaHoraInicio)
                    .Select(d => new LiquidacionDetalleDTO
                    {
                        Id = d.Id,
                        PaseoId = d.PaseoId,
                        Importe = d.Importe,
                        FechaHoraInicio = d.Paseo?.FechaHoraInicio ?? default,
                        DuracionMinutos = d.Paseo?.DuracionMinutos ?? 0,
                        PerroNombre = d.Paseo?.Perro?.Nombre ?? ""
                    })
                    .ToList()
            };
        }
    }
}
