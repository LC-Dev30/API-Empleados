using Infraestructura.ConfiguracionApp;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(policy =>
{
    policy.AddDefaultPolicy(option =>
    {
        option.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });
});

//instacias de aplicacion
var config = new AppConfig();
config.ConfigurarServiciosContainer(builder.Services, builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
