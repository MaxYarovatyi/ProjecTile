var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddCors();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "API", Version = "v1" });
});

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1"));
}

app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseRouting();
// app.UseCors(builder =>
// {
//     builder.WithOrigins("http://localhost:4200", "https://localhost:4200")
//     .AllowAnyHeader()
//     .AllowAnyMethod();
// });
app.UseHttpsRedirection();
app.UseStaticFiles();


// app.UseAuthentication();
// app.UseAuthorization();


app.MapControllers();

app.Run();
