using Personal.BankManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Personal.BankManagement.Domain;
public class Bank
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}


public static class BankEndpoints
{
	public static void MapBankEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/Bank").WithTags(nameof(Bank));

        group.MapGet("/", async (PersonalBankManagementApiContext db) =>
        {
            return await db.Bank.ToListAsync();
        })
        .WithName("GetAllBanks")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<Bank>, NotFound>> (Guid id, PersonalBankManagementApiContext db) =>
        {
            return await db.Bank.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is Bank model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBankById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, Bank bank, PersonalBankManagementApiContext db) =>
        {
            var affected = await db.Bank
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, bank.Id)
                  .SetProperty(m => m.Name, bank.Name)
                  .SetProperty(m => m.Description, bank.Description)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBank")
        .WithOpenApi();

        group.MapPost("/", async (Bank bank, PersonalBankManagementApiContext db) =>
        {
            db.Bank.Add(bank);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/Bank/{bank.Id}",bank);
        })
        .WithName("CreateBank")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, PersonalBankManagementApiContext db) =>
        {
            var affected = await db.Bank
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBank")
        .WithOpenApi();
    }
}
