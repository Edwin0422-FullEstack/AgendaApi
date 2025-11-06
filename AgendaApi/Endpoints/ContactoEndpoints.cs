using AgendaApi.Models;
using AgendaApi.Repositories;

namespace AgendaApi.Endpoints
{
    public static class ContactoEndpoints
    {
        // 'this IEndpointRouteBuilder app' es un método de extensión
        // Nos permite hacer app.MapContactoEndpoints() en Program.cs
        public static void MapContactoEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/contactos"); // Agrupamos rutas bajo /contactos

            // CREATE
            group.MapPost("/", async (IContactoRepository repo, ContactoInput input) =>
            {
                var contacto = await repo.CreateAsync(input);
                if (contacto == null)
                {
                    return Results.BadRequest("No se pudo agregar el contacto.");
                }
                // Usamos el nombre "GetContactoById" para la ruta de 'Created'
                return Results.CreatedAtRoute("GetContactoById", new { id = contacto.Id }, contacto);
            });

            // READ (Todos)
            group.MapGet("/", async (IContactoRepository repo) =>
            {
                var contactos = await repo.GetAllAsync();
                return Results.Ok(contactos);
            });

            // READ (Uno)
            group.MapGet("/{id:int}", async (IContactoRepository repo, int id) =>
            {
                var contacto = await repo.GetByIdAsync(id);
                return contacto != null ? Results.Ok(contacto) : Results.NotFound();
            })
            .WithName("GetContactoById"); // El nombre que usamos en MapPost

            // UPDATE
            group.MapPut("/{id:int}", async (IContactoRepository repo, int id, ContactoInput input) =>
            {
                var contacto = await repo.UpdateAsync(id, input);
                return contacto != null ? Results.Ok(contacto) : Results.NotFound();
            });

            // DELETE
            group.MapDelete("/{id:int}", async (IContactoRepository repo, int id) =>
            {
                var contacto = await repo.DeleteAsync(id);
                return contacto != null ? Results.Ok(contacto) : Results.NotFound();
            });
        }
    }
}
