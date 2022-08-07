using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PetaVerseApi.Contract;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs.Mapping;
using PetaVerseApi.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace PoC.CostsHub.UnitTests
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            var generator = new Random();
            
            services.AddSingleton(generator);

            services.AddScoped<DbNameProvider>();

            var defaultCostsHubData = GetDefaultData(generator);

            services.AddScoped(sp =>
            {
                var dbName = sp.GetRequiredService<DbNameProvider>().Name;
                var option = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: dbName).Options;
                var context = new ApplicationDbContext(option);
                context.Database.EnsureCreated();
                context.AddRange(defaultCostsHubData);
                context.SaveChanges();
                return context;
            });

            //services.AddScoped(sp =>
            //{
            //    var emailSenderMock = new Mock<IEmailSender>();
            //    emailSenderMock.Setup(s => s.SendAsync(It.IsAny<EmailModel>())).Verifiable();
            //    return emailSenderMock;
            //});

            services.AddScoped<IAnimalRepository, AnimalRepository>();
            services.AddScoped<ISpeciesRepository, SpeciesRepository>();
            services.AddScoped<IBreedRepository, BreedRepository>();


            //services.AddSingleton(new Mock<IStringLocalizer<MenuService>>().Object);
            //services.AddSingleton(new Mock<IStringLocalizer<PageService>>().Object);
            //services.AddSingleton(new Mock<IAssetManager>().Object);
            //services.Configure<AppSettings>(settings => { });
            //services.AddSingleton<IUser>(new CurrentUser(id: Guid.NewGuid().ToString()));

            //services.AddScoped<IUnitOfWork, UnitOfWork>();
            //services.AddScoped<IMenuService, MenuService>();
            //services.AddScoped<IPageService, PageService>();

            services.AddSingleton(new MapperConfiguration(mc =>
            {
                mc.AddProfile<MappingProfile>();
            }).CreateMapper());
        }

        private IEnumerable<object> GetDefaultData(Random generator)
        {
            var species = new Species
            {
                Id = generator.Next(int.MaxValue),
                Name = Guid.NewGuid().ToString()
            };

            return (
                new object[] { species }
                );
        }
    }
    
}

class DbNameProvider
    {
        public string Name { get; } = $"petaverse-{Guid.NewGuid()}";
    }
