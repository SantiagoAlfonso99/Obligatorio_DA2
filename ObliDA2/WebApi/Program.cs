using ServiceFactory;
using WebApi.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddControllers(
    options => options.Filters.Add(typeof(ExceptionFilter))
);

ServicesFactory.RegisterServices(builder.Services);
ServicesFactory.RegisterDataAccess(builder.Services);
ServicesFactory.RegisterReportService(builder.Services);

var app = builder.Build();

//ServicesFactory.CreateDefaultUser(app.Services);

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