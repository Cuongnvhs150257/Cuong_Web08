using MISA.WEB08.AMIS.BL;
using MISA.WEB08.AMIS.DL;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(p => p.AddPolicy("corsapp", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));


builder.Services.AddControllers().ConfigureApiBehaviorOptions(option =>
{
    option.SuppressModelStateInvalidFilter = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dependency Injection
builder.Services.AddScoped(typeof(IBaseBL<>), typeof(BaseBL<>));
builder.Services.AddScoped(typeof(IBaseDL<>), typeof(BaseDL<>));

builder.Services.AddScoped<IUnitBL, UnitBL>();
builder.Services.AddScoped<IUnitDL, UnitDL>();
builder.Services.AddScoped<IEmployeeBL, EmployeeBL>();
builder.Services.AddScoped<IEmployeeDL, EmployeeDL>();
builder.Services.AddScoped<IProductBL, ProductBL>();
builder.Services.AddScoped<IProductDL, ProductDL>();
builder.Services.AddScoped<ISupplyBL, SupplyBL>();
builder.Services.AddScoped<ISupplyDL, SupplyDL>();
builder.Services.AddScoped<IUnitCalculateBL, UnitCalculateBL>();
builder.Services.AddScoped<IUnitCalculateDL, UnitCalculateDL>();
builder.Services.AddScoped<IWarehouseBL, WarehouseBL>();
builder.Services.AddScoped<IWarehouseDL, WarehouseDL>();

DataContext.MySqlConnectionString = builder.Configuration.GetConnectionString("MySqlConnectionString");



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("corsapp");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
