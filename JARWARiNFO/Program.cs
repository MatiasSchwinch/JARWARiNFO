using JARWARiNFO.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(setup => {
    setup.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {
        Title = "JARWAR iNFO API",
        Version = "v1",
        Contact = new OpenApiContact
        {
            Name = "Matias Schwinch",
            Email = "matias.schwinch@outlook.com",
            Url = new Uri("https://github.com/MatiasSchwinch")
        },
        Description = "Esta WebAPI al ser solicitada devuelve todos los datos recopilados relacionados con el hardware de su PC."
    });
    setup.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml"));
});
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddSingleton<HWMonitor>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
}

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));

app.Run();
