using AgendaApi.Models;
using System.Collections.Concurrent;

namespace AgendaApi.Repositories
{
    public class ContactoRepository : IContactoRepository
    {
        // --- Nuestro almacén en memoria se muda aquí ---
        private static readonly ConcurrentDictionary<int, Contacto> _contactos = new();
        private static int _nextId = 0;

        // Convertimos los métodos a 'Task' para cumplir con la interfaz
        // Usamos Task.FromResult para devolver un resultado síncrono como si fuera asíncrono

        public Task<Contacto?> CreateAsync(ContactoInput input)
        {
            var id = Interlocked.Increment(ref _nextId);
            var contacto = new Contacto(id, input.Nombre, input.Email, input.Telefono);

            if (_contactos.TryAdd(contacto.Id, contacto))
            {
                return Task.FromResult<Contacto?>(contacto);
            }
            return Task.FromResult<Contacto?>(null); // Falló la creación
        }

        public Task<IEnumerable<Contacto>> GetAllAsync()
        {
            var todos = _contactos.Values.OrderBy(c => c.Nombre);
            return Task.FromResult(todos.AsEnumerable());
        }

        public Task<Contacto?> GetByIdAsync(int id)
        {
            _contactos.TryGetValue(id, out var contacto);
            return Task.FromResult(contacto);
        }

        public Task<Contacto?> UpdateAsync(int id, ContactoInput input)
        {
            if (!_contactos.ContainsKey(id))
            {
                return Task.FromResult<Contacto?>(null); // No encontrado
            }

            var contactoActualizado = new Contacto(id, input.Nombre, input.Email, input.Telefono);
            _contactos[id] = contactoActualizado;

            return Task.FromResult<Contacto?>(contactoActualizado);
        }

        public Task<Contacto?> DeleteAsync(int id)
        {
            _contactos.TryRemove(id, out var contactoEliminado);
            return Task.FromResult(contactoEliminado);
        }
    }
}
