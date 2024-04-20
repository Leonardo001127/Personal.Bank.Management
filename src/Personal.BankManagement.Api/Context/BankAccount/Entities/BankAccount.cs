using Personal.BankManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Personal.BankManagement.Domain;

public class BankAccount
{
    public Guid Id { get; set; }


    public Guid BankId { get; set; }
    public virtual Bank Bank { get; set; }

    public Guid PersonId { get; set; }
    public virtual Person Person { get; set; }
    public string? BankAccountGoal { get; set; }
}


public static class BankAccountEndpoints
{
	public static void MapBankAccountEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/BankAccount").WithTags(nameof(BankAccount));

        group.MapGet("/", async (PersonalBankManagementApiContext db) =>
        {
            return await db.BankAccount.ToListAsync();
        })
        .WithName("GetAllBankAccounts")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<BankAccount>, NotFound>> (Guid id, PersonalBankManagementApiContext db) =>
        {
            return await db.BankAccount.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is BankAccount model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetBankAccountById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, BankAccount bankAccount, PersonalBankManagementApiContext db) =>
        {
            var affected = await db.BankAccount
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, bankAccount.Id)
                  .SetProperty(m => m.BankId, bankAccount.BankId)
                  .SetProperty(m => m.PersonId, bankAccount.PersonId)
                  .SetProperty(m => m.BankAccountGoal, bankAccount.BankAccountGoal)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateBankAccount")
        .WithOpenApi();

        group.MapPost("/", async (BankAccount bankAccount, PersonalBankManagementApiContext db) =>
        {
            db.BankAccount.Add(bankAccount);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/BankAccount/{bankAccount.Id}",bankAccount);
        })
        .WithName("CreateBankAccount")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, PersonalBankManagementApiContext db) =>
        {
            var affected = await db.BankAccount
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteBankAccount")
        .WithOpenApi();
    }
}