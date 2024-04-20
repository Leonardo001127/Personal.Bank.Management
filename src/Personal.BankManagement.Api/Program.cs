using Personal.BankManagement.CrossCutting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Personal.BankManagement.Api.Data;
using Personal.BankManagement.Api;
using Personal.BankManagement.Domain;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<PersonalBankManagementApiContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("PersonalBankManagementApiContext") ?? throw new InvalidOperationException("Connection string 'PersonalBankManagementApiContext' not found."));
    
});

// Add services to the container.

builder.Services.AddInfraestructure(builder.Configuration);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

 




// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapBankEndpoints();

app.MapBankAccountEndpoints();

app.MapPersonEndpoints();

app.MapTransactionalHistoryEndpoints();

app.MapUserEndpoints();

app.Run();
