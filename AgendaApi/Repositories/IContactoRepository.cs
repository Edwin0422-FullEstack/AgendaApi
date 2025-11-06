using AgendaApi.Models;

namespace AgendaApi.Repositories
{
    public interface IContactoRepository
    {
        // Usamos 'Task' por si en el futuro esto va a una base de datos real (asíncrono)
        // Aunque nuestra implementación en memoria será síncrona, el contrato es asíncrono.

        Task<Contacto?> CreateAsync(ContactoInput input);
        Task<IEnumerable<Contacto>> GetAllAsync();
        Task<Contacto?> GetByIdAsync(int id);
        Task<Contacto?> UpdateAsync(int id, ContactoInput input);
        Task<Contacto?> DeleteAsync(int id);
    }
}
