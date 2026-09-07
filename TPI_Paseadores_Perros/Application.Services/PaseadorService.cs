using Data;
using Domain.Model;
using DTOs;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class PaseadorService : IPaseadorService
    {
        private readonly IPaseadorRepository paseadorRepository;
        private readonly IPaseoRepository paseoRepository;

        public PaseadorService(IPaseadorRepository paseadorRepository, IPaseoRepository paseoRepository)
        {
            this.paseadorRepository = paseadorRepository;
            this.paseoRepository = paseoRepository;
        }

        public async Task<PaseadorDTO> AddAsync(PaseadorDTO dto)
        {
            // Validar que el teléfono solo tenga números y una longitud lógica (ej: entre 8 y 15 dígitos)
            if (!Regex.IsMatch(dto.Telefono, @"^[0-9]{8,15}$"))
            {
                throw new ArgumentException("El formato del teléfono es inválido. Debe contener entre 8 y 15 números, sin espacios ni letras.");
            }
            // Regla de negocio: el email no puede estar duplicado
            if (await paseadorRepository.EmailExistsAsync(dto.Email))
            {
                throw new ArgumentException($"Ya existe un paseador con el email '{dto.Email}'.");
            }
            // Validar que la tarifa sea un valor lógico
            if (dto.TarifaPorHora <= 0)
            {
                throw new ArgumentException("La tarifa por hora debe ser mayor a cero.");
            }
            var fechaAlta = DateTime.Now;
            Paseador paseador = new Paseador(0, dto.Nombre, dto.Apellido, dto.Email,
                                             dto.Telefono, dto.Zona, dto.TarifaPorHora, fechaAlta);

            await paseadorRepository.AddAsync(paseador);

            return MapToDTO(paseador);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Regla de negocio: no se puede borrar un paseador que tenga paseos agendados
            if ((await paseoRepository.GetByCriteriaAsync(new PaseoCriteria(id, null, null, null))).Any())
            {
                throw new ArgumentException("No se puede eliminar el paseador porque tiene paseos registrados.");
            }

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
            // Validar que el teléfono solo tenga números y una longitud lógica (ej: entre 8 y 15 dígitos)
            if (!Regex.IsMatch(dto.Telefono, @"^[0-9]{8,15}$"))
            {
                throw new ArgumentException("El formato del teléfono es inválido. Debe contener entre 8 y 15 números, sin espacios ni letras.");
            }

            // Regla de negocio: el email no puede estar duplicado (excluyendo el propio paseador)
            if (await paseadorRepository.EmailExistsAsync(dto.Email, dto.Id))
            {
                throw new ArgumentException($"Ya existe otro paseador con el email '{dto.Email}'.");
            }
            // Validar que la tarifa sea un valor lógico
            if (dto.TarifaPorHora <= 0)
            {
                throw new ArgumentException("La tarifa por hora debe ser mayor a cero.");
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
