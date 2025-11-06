namespace AgendaApi.Models
{
    /// <summary>
    /// Modelo de entrada para crear o actualizar un contacto.
    /// </summary>
    public record ContactoInput(string Nombre, string? Email, string? Telefono);
}
