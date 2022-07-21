using Microsoft.EntityFrameworkCore;
using Receipt_Micro_Service.DataAccess;
using Receipt_Micro_Service.Repisotory;
using Receipt_Micro_Service.Repisotory.Interfaces;
using Receipt_Micro_Service.Services;
using Receipt_Micro_Service.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ReceiptDbContext>(
    options => {
        options.UseNpgsql(builder.Configuration.GetConnectionString("AppDb"));
    }, ServiceLifetime.Transient
);
builder.Services.AddScoped<IReceiptRepository,ReceiptRepository>();
builder.Services.AddScoped<IReceiptService, ReceiptService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
