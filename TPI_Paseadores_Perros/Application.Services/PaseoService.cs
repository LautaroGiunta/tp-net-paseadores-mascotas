using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class PaseoService : IPaseoService
    {
        private readonly IPaseoRepository paseoRepository;
        private readonly IPaseadorRepository paseadorRepository;
        private readonly IPerroRepository perroRepository;

        public PaseoService(IPaseoRepository paseoRepository, IPaseadorRepository paseadorRepository,
                            IPerroRepository perroRepository)
        {
            this.paseoRepository = paseoRepository;
            this.paseadorRepository = paseadorRepository;
            this.perroRepository = perroRepository;
        }

        public async Task<PaseoDTO> AddAsync(PaseoDTO dto)
        {
            // Regla de negocio: no se puede agendar un paseo en el pasado
            if (dto.FechaHoraInicio < DateTime.Now)
            {
                throw new ArgumentException("No se puede agendar un paseo en una fecha y hora que ya pasó.");
            }

            Paseador paseador = await ValidarPaseadorAsync(dto.PaseadorId);
            await ValidarPerroAsync(dto.PerroId);

            // Regla de negocio: el paseador no puede tener dos paseos que se pisen
            if (await paseoRepository.ExisteSolapamientoAsync(dto.PaseadorId, dto.FechaHoraInicio, dto.DuracionMinutos))
            {
                throw new ArgumentException("El paseador ya tiene otro paseo agendado en ese horario.");
            }

            // El precio no lo carga el usuario: se calcula con la tarifa vigente y queda congelado
            decimal precioTotal = CalcularPrecio(paseador.TarifaPorHora, dto.DuracionMinutos);

            var fechaAlta = DateTime.Now;
            Paseo paseo = new Paseo(0, dto.PaseadorId, dto.PerroId, dto.FechaHoraInicio,
                                    dto.DuracionMinutos, precioTotal, fechaAlta);

            await paseoRepository.AddAsync(paseo);

            return MapToDTO(paseo);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await paseoRepository.DeleteAsync(id);
        }

        public async Task<PaseoDTO?> GetAsync(int id)
        {
            Paseo? paseo = await paseoRepository.GetAsync(id);

            if (paseo == null)
                return null;

            return MapToDTO(paseo);
        }

        public async Task<IEnumerable<PaseoDTO>> GetAllAsync()
        {
            var paseos = await paseoRepository.GetAllAsync();
            return paseos.Select(MapToDTO).ToList();
        }

        public async Task<bool> UpdateAsync(PaseoDTO dto)
        {
            Paseador paseador = await ValidarPaseadorAsync(dto.PaseadorId);
            await ValidarPerroAsync(dto.PerroId);

            // Al modificar, el propio paseo se excluye para que no choque consigo mismo
            if (await paseoRepository.ExisteSolapamientoAsync(dto.PaseadorId, dto.FechaHoraInicio, dto.DuracionMinutos, dto.Id))
            {
                throw new ArgumentException("El paseador ya tiene otro paseo agendado en ese horario.");
            }

            // Recuperar el existente para preservar la FechaAlta
            var existing = await paseoRepository.GetAsync(dto.Id);
            if (existing == null)
                return false;

            decimal precioTotal = CalcularPrecio(paseador.TarifaPorHora, dto.DuracionMinutos);

            Paseo paseo = new Paseo(dto.Id, dto.PaseadorId, dto.PerroId, dto.FechaHoraInicio,
                                    dto.DuracionMinutos, precioTotal, existing.FechaAlta);
            return await paseoRepository.UpdateAsync(paseo);
        }

        public async Task<IEnumerable<PaseoDTO>> GetByCriteriaAsync(PaseoCriteriaDTO criteriaDTO)
        {
            var criteria = new PaseoCriteria(criteriaDTO.PaseadorId, criteriaDTO.PerroId,
                                             criteriaDTO.FechaDesde, criteriaDTO.FechaHasta);
            var paseos = await paseoRepository.GetByCriteriaAsync(criteria);
            return paseos.Select(MapToDTO).ToList();
        }

        private async Task<Paseador> ValidarPaseadorAsync(int paseadorId)
        {
            Paseador? paseador = await paseadorRepository.GetAsync(paseadorId);
            if (paseador == null)
            {
                throw new ArgumentException($"No existe un paseador con el Id {paseadorId}.");
            }
            return paseador;
        }

        private async Task ValidarPerroAsync(int perroId)
        {
            if (await perroRepository.GetAsync(perroId) == null)
            {
                throw new ArgumentException($"No existe un perro con el Id {perroId}.");
            }
        }

        private static decimal CalcularPrecio(decimal tarifaPorHora, int duracionMinutos)
        {
            return Math.Round(tarifaPorHora * duracionMinutos / 60m, 2);
        }

        private static PaseoDTO MapToDTO(Paseo paseo)
        {
            return new PaseoDTO
            {
                Id = paseo.Id,
                PaseadorId = paseo.PaseadorId,
                PerroId = paseo.PerroId,
                FechaHoraInicio = paseo.FechaHoraInicio,
                DuracionMinutos = paseo.DuracionMinutos,
                PrecioTotal = paseo.PrecioTotal,
                FechaAlta = paseo.FechaAlta
            };
        }
    }
}
