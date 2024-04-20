using Personal.BankManagement.Api.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.OpenApi;
using Microsoft.AspNetCore.Http.HttpResults;
namespace Personal.BankManagement.Domain;

public class TransactionalHistory
{
    public Guid Id { get; set; }
    public Guid BankAccountId { get; set; }
    public int Value{get;set;}
    public TransactionType TransactionType{get;set;}
    public DateTime TransactionDate{get;set;}
    public string? Description{get;set;}
    public string Currency{get;set;}
    public bool IsExecuted{get;set;}
     
    public virtual BankAccount BankAccount{get;set;}
    
}

public enum TransactionType : byte
{
    Deposit,
    Withdraw
}


public static class TransactionalHistoryEndpoints
{
	public static void MapTransactionalHistoryEndpoints (this IEndpointRouteBuilder routes)
    {
        var group = routes.MapGroup("/api/TransactionalHistory").WithTags(nameof(TransactionalHistory));

        group.MapGet("/", async (PersonalBankManagementApiContext db) =>
        {
            return await db.TransactionalHistory.ToListAsync();
        })
        .WithName("GetAllTransactionalHistories")
        .WithOpenApi();

        group.MapGet("/{id}", async Task<Results<Ok<TransactionalHistory>, NotFound>> (Guid id, PersonalBankManagementApiContext db) =>
        {
            return await db.TransactionalHistory.AsNoTracking()
                .FirstOrDefaultAsync(model => model.Id == id)
                is TransactionalHistory model
                    ? TypedResults.Ok(model)
                    : TypedResults.NotFound();
        })
        .WithName("GetTransactionalHistoryById")
        .WithOpenApi();

        group.MapPut("/{id}", async Task<Results<Ok, NotFound>> (Guid id, TransactionalHistory transactionalHistory, PersonalBankManagementApiContext db) =>
        {
            var affected = await db.TransactionalHistory
                .Where(model => model.Id == id)
                .ExecuteUpdateAsync(setters => setters
                  .SetProperty(m => m.Id, transactionalHistory.Id)
                  .SetProperty(m => m.BankAccountId, transactionalHistory.BankAccountId)
                  .SetProperty(m => m.Value, transactionalHistory.Value)
                  .SetProperty(m => m.TransactionType, transactionalHistory.TransactionType)
                  .SetProperty(m => m.TransactionDate, transactionalHistory.TransactionDate)
                  .SetProperty(m => m.Description, transactionalHistory.Description)
                  .SetProperty(m => m.Currency, transactionalHistory.Currency)
                  .SetProperty(m => m.IsExecuted, transactionalHistory.IsExecuted)
                  );
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("UpdateTransactionalHistory")
        .WithOpenApi();

        group.MapPost("/", async (TransactionalHistory transactionalHistory, PersonalBankManagementApiContext db) =>
        {
            db.TransactionalHistory.Add(transactionalHistory);
            await db.SaveChangesAsync();
            return TypedResults.Created($"/api/TransactionalHistory/{transactionalHistory.Id}",transactionalHistory);
        })
        .WithName("CreateTransactionalHistory")
        .WithOpenApi();

        group.MapDelete("/{id}", async Task<Results<Ok, NotFound>> (Guid id, PersonalBankManagementApiContext db) =>
        {
            var affected = await db.TransactionalHistory
                .Where(model => model.Id == id)
                .ExecuteDeleteAsync();
            return affected == 1 ? TypedResults.Ok() : TypedResults.NotFound();
        })
        .WithName("DeleteTransactionalHistory")
        .WithOpenApi();
    }
}