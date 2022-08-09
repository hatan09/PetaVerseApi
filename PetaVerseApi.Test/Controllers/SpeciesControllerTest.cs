using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PetaVerseApi.Contract;
using PetaVerseApi.Controller;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;
using PetaVerseApi.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PetaVerseApi.Test.Controllers
{
    public class SpeciesControllerTest : IClassFixture<InjectionFixture>
    {
        private readonly IMapper? _mapper;
        private readonly ApplicationDbContext? _context;

        public SpeciesControllerTest(InjectionFixture injectionFixture)
        {
            _mapper = injectionFixture.ServiceProvider?.GetService<IMapper>();
            _context = injectionFixture.ServiceProvider?.GetService<ApplicationDbContext>();
        }


        [Fact]
        public async Task GetAll_GetAllSpeciesFromDatabase()
        {
            // Arrange
            await _context.Species.AddRangeAsync(new[]
            {
                new Species { Name = "Species 1"},
                new Species { Name = "Species 2"}
            });
            await _context.SaveChangesAsync();

            var repo = new SpeciesRepository(_context);

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
            Assert.IsAssignableFrom<IEnumerable<SpeciesDTO>>(objectResult.Value);
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
