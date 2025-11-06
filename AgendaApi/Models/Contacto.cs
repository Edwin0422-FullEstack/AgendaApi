namespace AgendaApi.Models
{
    /// <summary>
    /// Representa un contacto en la agenda.
    /// </summary>
    public record Contacto(int Id, string Nombre, string? Email, string? Telefono);
}
