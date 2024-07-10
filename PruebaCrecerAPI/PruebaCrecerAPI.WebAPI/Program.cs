using PruebaCrecerAPI.BLL.Implementations;
using PruebaCrecerAPI.BLL.Interfaces;
using PruebaCrecerAPI.DAL;
using PruebaCrecerAPI.DAL.Implementations;
using PruebaCrecerAPI.DAL.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.Configure<ConnectionStrings>(builder.Configuration.GetSection("ConnectionStrings"));

builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IEmpresaRepository, EmpresaRepository>();

builder.Services.AddScoped<IEmpresaService, EmpresaService>();
builder.Services.AddScoped<IDepartamentoService, DepartamentoService>();

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
