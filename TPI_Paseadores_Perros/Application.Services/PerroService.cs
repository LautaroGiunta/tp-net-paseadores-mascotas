using Data;
using Domain.Model;
using DTOs;

namespace Application.Services
{
    public class PerroService : IPerroService
    {
        private readonly IPerroRepository perroRepository;
        private readonly IDuenoRepository duenoRepository;
        private readonly IPaseoRepository paseoRepository;

        public PerroService(IPerroRepository perroRepository, IDuenoRepository duenoRepository,
                            IPaseoRepository paseoRepository)
        {
            this.perroRepository = perroRepository;
            this.duenoRepository = duenoRepository;
            this.paseoRepository = paseoRepository;
        }

        public async Task<PerroDTO> AddAsync(PerroDTO dto)
        {
            // Regla de negocio: el dueño referenciado tiene que existir
            if (await duenoRepository.GetAsync(dto.DuenoId) == null)
            {
                throw new ArgumentException($"No existe un dueño con el Id {dto.DuenoId}.");
            }

            var fechaAlta = DateTime.Now;
            Perro perro = new Perro(0, dto.DuenoId, dto.Nombre, dto.Raza, dto.Edad, fechaAlta);

            await perroRepository.AddAsync(perro);

            return MapToDTO(perro);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            // Regla de negocio: no se puede borrar un perro que tenga paseos registrados
            if (await paseoRepository.PerroTienePaseosAsync(id))
            {
                throw new ArgumentException("No se puede eliminar el perro porque tiene paseos registrados.");
            }

            return await perroRepository.DeleteAsync(id);
        }

        public async Task<PerroDTO?> GetAsync(int id)
        {
            Perro? perro = await perroRepository.GetAsync(id);

            if (perro == null)
                return null;

            return MapToDTO(perro);
        }

        public async Task<IEnumerable<PerroDTO>> GetAllAsync()
        {
            var perros = await perroRepository.GetAllAsync();
            return perros.Select(MapToDTO).ToList();
        }

        public async Task<bool> UpdateAsync(PerroDTO dto)
        {
            // Regla de negocio: el dueño referenciado tiene que existir
            if (await duenoRepository.GetAsync(dto.DuenoId) == null)
            {
                throw new ArgumentException($"No existe un dueño con el Id {dto.DuenoId}.");
            }

            // Recuperar el existente para preservar la FechaAlta
            var existing = await perroRepository.GetAsync(dto.Id);
            if (existing == null)
                return false;

            Perro perro = new Perro(dto.Id, dto.DuenoId, dto.Nombre, dto.Raza, dto.Edad, existing.FechaAlta);
            return await perroRepository.UpdateAsync(perro);
        }

        public async Task<IEnumerable<PerroDTO>> GetByDuenoAsync(int duenoId)
        {
            var perros = await perroRepository.GetByDuenoAsync(duenoId);
            return perros.Select(MapToDTO).ToList();
        }

        private static PerroDTO MapToDTO(Perro perro)
        {
            return new PerroDTO
            {
                Id = perro.Id,
                DuenoId = perro.DuenoId,
                Nombre = perro.Nombre,
                Raza = perro.Raza,
                Edad = perro.Edad,
                FechaAlta = perro.FechaAlta
            };
        }
    }
}
