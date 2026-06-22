using Voky.app.product.api.Data;
using Voky.app.product.api.Data.Services;
using Voky.app.product.api.Services;
using Voky.Shared.Visma.Database.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddVismaDatabase<VismaDbContext>(builder.Configuration, options => new VismaDbContext(options));

builder.Services.AddScoped<DbProductService>();
builder.Services.AddScoped<DbMainSupplierService>();
builder.Services.AddScoped<DbQuestionGroupService>();
builder.Services.AddScoped<DbQuestionService>();
builder.Services.AddScoped<DbQuestionChoiceService>();
builder.Services.AddScoped<DbQuestionTypeService>();
builder.Services.AddScoped<DbTagService>();
builder.Services.AddScoped<DbCategoryService>();
builder.Services.AddScoped<DbCurrencyService>();
builder.Services.AddScoped<DbPriceMatrixService>();

builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<MainSupplierService>();
builder.Services.AddScoped<QuestionGroupService>();
builder.Services.AddScoped<QuestionService>();
builder.Services.AddScoped<QuestionChoiceService>();
builder.Services.AddScoped<QuestionTypeService>();
builder.Services.AddScoped<TagService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CurrencyService>();
builder.Services.AddScoped<PriceMatrixService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
