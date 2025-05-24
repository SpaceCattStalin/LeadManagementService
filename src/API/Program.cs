using Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddInfrastructureServices();


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGatewayDev", policy =>
    {
        policy.WithOrigins("https://localhost:7149")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowGatewayProduction", policy =>
    {
        policy.WithOrigins("http://localhost:8080")
         .AllowAnyHeader()
         .AllowAnyMethod();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseCors("AllowGatewayDev");
}
else
{
    app.UseCors("AllowGatewayProduction");
}
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
