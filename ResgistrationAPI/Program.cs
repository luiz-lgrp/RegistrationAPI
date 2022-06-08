using FluentValidation.AspNetCore;
using ResgistrationAPI.Models;
using ResgistrationAPI.Services;
using ResgistrationAPI.Validators;

var builder = WebApplication.CreateBuilder(args);



// configurando para pegar a string de conexão
builder.Services.Configure<CustomerDatabaseSettings>
    (builder.Configuration.GetSection("DevNetStoreDatabase"));

//colocando o customerService no Singleton para criar a interface
builder.Services.AddSingleton<CustomerServices>();



//Adicionando o Validator na aplicação
builder.Services.AddControllers()
    .AddFluentValidation(config => config.RegisterValidatorsFromAssemblyContaining<AddCustomerValidator>());


// Learn more about configuring Swagger/OpenAPI 
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

app.Run();
