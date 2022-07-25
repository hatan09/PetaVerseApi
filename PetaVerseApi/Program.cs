using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Contract;
using PetaVerseApi.Repository;
using PetaVerseApi.DTOs.Mapping;
using PetaVerseApi.AppSettings;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("LocalConnection")));
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo { Title = "PetaVerseApi", Version = "v1" }));
builder.Services.Configure<AzureStorageConfig>(builder.Configuration.GetSection("AzureStorageConfig"));
builder.Services.AddControllers();

builder.Services.AddScoped<IUserRepository, UserRepository>()
                .AddScoped<IBreedRepository, BreedRepository>()
                .AddScoped<IAnimalRepository, AnimalRepository>()
                .AddScoped<IStatusRepository, StatusRepository>()
                .AddScoped<ISpeciesRepository, SpeciesRepository>()
                .AddScoped<ISheddingRepository, SheddingRepository>()
                .AddScoped<IPetShortsRepository, PetShortsRepository>()
                .AddScoped<IUserAnimalRepository, UserAnimalRepository>()
                .AddScoped<ITemperamentRepository, TemperamentRepository>()
                .AddScoped<IPetaverseMediaRepository, PetaverseMediaRepository>()
                .AddScoped<IAnimalPetaverseMediaRepository, AnimalPetaverseMediaRepository>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("ClientPermission", policy =>
    {
        policy.AllowAnyHeader()
              .AllowAnyMethod()
              .WithOrigins("http://localhost:7094")
              .AllowCredentials();
    });
});

var mapperConfig = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile()));
IMapper mapper = mapperConfig.CreateMapper();
builder.Services.AddSingleton(mapper);


var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseDeveloperExceptionPage();
    app.UseHsts();
}

app.UseSwagger();

app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "PetaVerseApi v1"));

app.UseHttpsRedirection();

app.UseCors("ClientPermission");

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => endpoints.MapControllers());

app.Run();
