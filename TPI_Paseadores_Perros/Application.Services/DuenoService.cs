using Data;
using Domain.Model;
using DTOs;
using System.Text.RegularExpressions;

namespace Application.Services
{
    public class DuenoService : IDuenoService
    {
        private readonly IDuenoRepository duenoRepository;

        public DuenoService(IDuenoRepository duenoRepository)
        {
            this.duenoRepository = duenoRepository;
        }

        public async Task<DuenoDTO> AddAsync(DuenoDTO dto)
        {
            // Validar que el teléfono solo tenga números y una longitud lógica (ej: entre 8 y 15 dígitos)
            if (!Regex.IsMatch(dto.Telefono, @"^[0-9]{8,15}$"))
            {
                throw new ArgumentException("El formato del teléfono es inválido. Debe contener entre 8 y 15 números, sin espacios ni letras.");
            }
            // Regla de negocio: el email no puede estar duplicado
            if (await duenoRepository.EmailExistsAsync(dto.Email))
            {
                throw new ArgumentException($"Ya existe un dueño con el email '{dto.Email}'.");
            }

            var fechaAlta = DateTime.Now;
            Dueno dueno = new Dueno(0, dto.Nombre, dto.Apellido, dto.Email,
                                    dto.Telefono, dto.Direccion, fechaAlta);

            await duenoRepository.AddAsync(dueno);

            return MapToDTO(dueno);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await duenoRepository.DeleteAsync(id);
        }

        public async Task<DuenoDTO?> GetAsync(int id)
        {
            Dueno? dueno = await duenoRepository.GetAsync(id);

            if (dueno == null)
                return null;

            return MapToDTO(dueno);
        }

        public async Task<IEnumerable<DuenoDTO>> GetAllAsync()
        {
            var duenos = await duenoRepository.GetAllAsync();
            return duenos.Select(MapToDTO).ToList();
        }

        public async Task<bool> UpdateAsync(DuenoDTO dto)
        {
            // Validar que el teléfono solo tenga números y una longitud lógica (ej: entre 8 y 15 dígitos)
            if (!Regex.IsMatch(dto.Telefono, @"^[0-9]{8,15}$"))
            {
                throw new ArgumentException("El formato del teléfono es inválido. Debe contener entre 8 y 15 números, sin espacios ni letras.");
            }
            // Regla de negocio: el email no puede estar duplicado (excluyendo el propio dueño)
            if (await duenoRepository.EmailExistsAsync(dto.Email, dto.Id))
            {
                throw new ArgumentException($"Ya existe otro dueño con el email '{dto.Email}'.");
            }

            // Recuperar el existente para preservar la FechaAlta
            var existing = await duenoRepository.GetAsync(dto.Id);
            if (existing == null)
                return false;

            Dueno dueno = new Dueno(dto.Id, dto.Nombre, dto.Apellido, dto.Email,
                                    dto.Telefono, dto.Direccion, existing.FechaAlta);
            return await duenoRepository.UpdateAsync(dueno);
        }

        private static DuenoDTO MapToDTO(Dueno dueno)
        {
            return new DuenoDTO
            {
                Id = dueno.Id,
                Nombre = dueno.Nombre,
                Apellido = dueno.Apellido,
                Email = dueno.Email,
                Telefono = dueno.Telefono,
                Direccion = dueno.Direccion,
                FechaAlta = dueno.FechaAlta
            };
        }
    }
}
