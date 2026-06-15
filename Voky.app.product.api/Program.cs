using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Voky.app.product.api.Data;
using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

builder.Services.AddScoped<DbProductService>();
builder.Services.AddScoped<DbMainSupplierService>();
builder.Services.AddScoped<DbLifecycleService>();
builder.Services.AddScoped<DbSupplierCurrencyService>();
builder.Services.AddScoped<DbCurrencyEndPriceService>();
builder.Services.AddScoped<DbQuestionGroupService>();
builder.Services.AddScoped<DbQuestionService>();
builder.Services.AddScoped<DbQuestionChoiceService>();
builder.Services.AddScoped<DbPriceMatrixService>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<MainSupplierService>();
builder.Services.AddScoped<LifecycleService>();
builder.Services.AddScoped<SupplierCurrencyService>();
builder.Services.AddScoped<CurrencyEndPriceService>();
builder.Services.AddScoped<QuestionGroupService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<QuestionChoiceService>();
builder.Services.AddScoped<PriceMatrixService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
