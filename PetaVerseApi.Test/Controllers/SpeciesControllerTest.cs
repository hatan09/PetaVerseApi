using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PetaVerseApi.Contract;
using PetaVerseApi.Controller;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs.Mapping;
using PetaVerseApi.Repository;
using System.Threading.Tasks;
using Xunit;

namespace PetaVerseApi.Test.Controllers
{
    public class SpeciesControllerTest : IClassFixture<InjectionFixture>
    {
        private readonly IMapper _mapper;


        public SpeciesControllerTest(IMapper mapper)
        {
            //_mapper = new TestServer(new WebHostBuilder().UseStartup<TestStartup>());
            _mapper = new MapperConfiguration(mc => mc.AddProfile(new MappingProfile())).CreateMapper();
        }


        [Fact]
        public async Task GetAll_GetAllSpeciesFromDatabase()
        {
            // Arrange
            var services = new ServiceCollection();
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseInMemoryDatabase("Species");
            });
            var provider = services.BuildServiceProvider();
            var ctx = provider.GetService<ApplicationDbContext>();
            await ctx.Species.AddRangeAsync(new[]
            {
                new Species { Name = "Species 1"},
                new Species { Name = "Species 2"}
            });
            await ctx.SaveChangesAsync();

            var repo = new SpeciesRepository(ctx);

            var controller = new SpeciesController(
                new Mock<IAnimalRepository>().Object,
                repo,
                new Mock<IBreedRepository>().Object,
                _mapper
            );

            // Action
            var actionResult = await controller.GetAll();

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
        }

        //[Fact]
        //public async Task Create_ReturnOkResponse()
        //{
        //    var testSpecies = new SpeciesDTO()
        //    {
        //        Name = Guid.NewGuid().ToString()
        //    };

        //    var createdResponse = await _speciesController.Create(testExpert);
        //    var objectResult = Assert.IsType<OkObjectResult>(createdResponse);
        //    var result = Assert.IsType<SpeciesDTO>(objectResult.Value);

        //    Assert.IsType<SpeciesDTO>(result);
        //    Assert.Equal(testExpert.Name, result.Name);
        //}

        //[Fact]
        //public async Task Get_GetByIdFromDatabase()
        //{
        //    var expected = await _context.Species.AsQueryable().FirstOrDefaultAsync();
        //    var actionResult = await _speciesController.Get(expected!.Id);
        //    var objectResult = Assert.IsType<OkObjectResult>(actionResult);
        //    var result = Assert.IsType<SpeciesDTO>(objectResult.Value);

        //    Assert.NotNull(result);
        //    Assert.Equal(expected.Id, result.Id);
        //    Assert.Equal(expected.Name, result.Name);
        //}

        //[Fact]
        //public async Task Get_TypeIsNull_ReturnNotFoundResponse()
        //{
        //    var badResponse = await _speciesController.Get(0);
        //    Assert.IsType<NotFoundResult>(badResponse);
        //}

        //[Fact]
        //public async Task Delete_ExistingId_ReturnNoContent()
        //{
        //    var species = new Species
        //    {
        //        Id = _generator.Next(int.MaxValue),
        //        Name = Guid.NewGuid().ToString()
        //    };
        //    _context.Species.Add(species);
        //    await _context.SaveChangesAsync();

        //    var response = await _speciesController.Delete(species.Id);

        //    Assert.IsType<NoContentResult>(response);
        //}

        //[Fact]
        //public async Task Delete_TypeIsNull_ReturnNotFoundResponse()
        //{
        //    var badResponse = await _speciesController.Delete(0);
        //    Assert.IsType<NotFoundResult>(badResponse);
        //}
    }
}
