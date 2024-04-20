using Personal.BankManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Personal.BankManagement.Domain;
public class Person
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public Guid AddressId { get; set; }
    public Address? Address { get; set; }
}

public static class PersonEndpoints
{
	public static void MapPersonEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Person").WithTags(nameof(Person));

        group.MapGet("/", async (PersonalBankManagementApiContext db) =>
        {
            return await db.Person.ToListAsync();
        })
        .WithName("GetAllPeople")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Person>, NotFound>> (Guid id, PersonalBankManagementApiContext db) =>
        {
            return await db.Person.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Person model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetPersonById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, Person person, PersonalBankManagementApiContext db) =>
        {
            var affected = await db.Person
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, person.Id)
                  .SetProperty(m => m.Name, person.Name)
                  .SetProperty(m => m.Email, person.Email)
                  .SetProperty(m => m.Phone, person.Phone)
                  .SetProperty(m => m.AddressId, person.AddressId)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdatePerson")
        .WithOpenApi();

        group.MapPost("/", async (Person person, PersonalBankManagementApiContext db) =>
        {
            db.Person.Add(person);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Person/{person.Id}",person);
        })
        .WithName("CreatePerson")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, PersonalBankManagementApiContext db) =>
        {
            var affected = await db.Person
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeletePerson")
        .WithOpenApi();
    }
}