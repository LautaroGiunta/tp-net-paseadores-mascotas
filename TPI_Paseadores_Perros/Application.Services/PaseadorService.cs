using Domain.Model;
using Data;
using DTOs;

namespace Application.Services
{
    public class PaseadorService : IPaseadorService
    {
        private readonly IPaseadorRepository paseadorRepository;

        public PaseadorService(IPaseadorRepository paseadorRepository)
        {
            this.paseadorRepository = paseadorRepository;
        }

        public async Task<PaseadorDTO> AddAsync(PaseadorDTO dto)
        {
            // Regla de negocio: el email no puede estar duplicado
            if (await paseadorRepository.EmailExistsAsync(dto.Email))
            {
                throw new ArgumentException($"Ya existe un paseador con el email '{dto.Email}'.");
            }

            var fechaAlta = DateTime.Now;
            Paseador paseador = new Paseador(0, dto.Nombre, dto.Apellido, dto.Email,
                                             dto.Telefono, dto.Zona, dto.TarifaPorHora, fechaAlta);

            await paseadorRepository.AddAsync(paseador);

            return MapToDTO(paseador);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await paseadorRepository.DeleteAsync(id);
        }

        public async Task<PaseadorDTO?> GetAsync(int id)
        {
            Paseador? paseador = await paseadorRepository.GetAsync(id);

            if (paseador == null)
                return null;

            return MapToDTO(paseador);
        }

        public async Task<IEnumerable<PaseadorDTO>> GetAllAsync()
        {
            var paseadores = await paseadorRepository.GetAllAsync();
            return paseadores.Select(MapToDTO).ToList();
        }

        public async Task<bool> UpdateAsync(PaseadorDTO dto)
        {
            // Regla de negocio: el email no puede estar duplicado (excluyendo el propio paseador)
            if (await paseadorRepository.EmailExistsAsync(dto.Email, dto.Id))
            {
                throw new ArgumentException($"Ya existe otro paseador con el email '{dto.Email}'.");
            }

            // Recuperar el existente para preservar la FechaAlta
            var existing = await paseadorRepository.GetAsync(dto.Id);
            if (existing == null)
                return false;

            Paseador paseador = new Paseador(dto.Id, dto.Nombre, dto.Apellido, dto.Email,
                                             dto.Telefono, dto.Zona, dto.TarifaPorHora, existing.FechaAlta);
            return await paseadorRepository.UpdateAsync(paseador);
        }

        public async Task<IEnumerable<PaseadorDTO>> GetByCriteriaAsync(PaseadorCriteriaDTO criteriaDTO)
        {
            var criteria = new PaseadorCriteria(criteriaDTO.Texto);
            var paseadores = await paseadorRepository.GetByCriteriaAsync(criteria);
            return paseadores.Select(MapToDTO).ToList();
        }

        private static PaseadorDTO MapToDTO(Paseador paseador)
        {
            return new PaseadorDTO
            {
                Id = paseador.Id,
                Nombre = paseador.Nombre,
                Apellido = paseador.Apellido,
                Email = paseador.Email,
                Telefono = paseador.Telefono,
                Zona = paseador.Zona,
                TarifaPorHora = paseador.TarifaPorHora,
                FechaAlta = paseador.FechaAlta
            };
        }
    }
}
