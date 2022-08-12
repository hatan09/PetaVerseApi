using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using PetaVerseApi.Contract;
using PetaVerseApi.Controller;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;
using PetaVerseApi.Interfaces;
using PetaVerseApi.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PetaVerseApi.Test.Controllers
{
    public class SpeciesControllerTest : IClassFixture<InjectionFixture>
    {
        private readonly IMapper? _mapper;
        private readonly ApplicationDbContext? _context;
        private readonly ISpeciesRepository? _speciesRepository;

        public SpeciesControllerTest(InjectionFixture injectionFixture)
        {
            _mapper = injectionFixture.ServiceProvider?.GetService<IMapper>();
            _context = injectionFixture.ServiceProvider?.GetService<ApplicationDbContext>();

            // DI
            _speciesRepository = new SpeciesRepository(_context!);
        }


        [Fact]
        public async Task GetAll_GetAllSpeciesFromDatabase()
        {
            // Arrange
            await _context!.Species.AddRangeAsync(new[]
            {
                new Species { Name = Guid.NewGuid().ToString()},
                new Species { Name = Guid.NewGuid().ToString()}
            });
            await _context.SaveChangesAsync();

            var repo = new SpeciesRepository(_context);

            var controller = new SpeciesController(
                new Mock<IAnimalRepository>().Object,
                repo,
                new Mock<IBreedRepository>().Object,
                _mapper!,
                new Mock<IExcelHandlerService>().Object
            );

            // Action
            var actionResult = await controller.GetAll();

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.NotNull(objectResult.Value);
            Assert.IsAssignableFrom<IEnumerable<SpeciesDTO>>(objectResult.Value);
        }


        [Fact]
        public async Task Create_ReturnOkResponse()
        {
            // Arrange
            var testSpecies = new SpeciesDTO()
            {
                Name = Guid.NewGuid().ToString()
            };

            var controller = new SpeciesController(
                new Mock<IAnimalRepository>().Object,
                _speciesRepository!,
                new Mock<IBreedRepository>().Object,
                _mapper!,
                new Mock<IExcelHandlerService>().Object
            );

            // Action
            var createdResponse = await controller.Create(testSpecies);

            // Assert
            var objectResult = Assert.IsType<CreatedAtActionResult>(createdResponse);
            var result = Assert.IsType<SpeciesDTO>(objectResult.Value);

            Assert.IsType<SpeciesDTO>(result);
        }


        [Fact]
        public async Task Get_GetByIdFromDatabase()
        {
            // Arrange
            await _context!.Species.AddRangeAsync(new[]
            {
                new Species { Name = Guid.NewGuid().ToString()},
                new Species { Name = Guid.NewGuid().ToString()}
            });
            await _context.SaveChangesAsync();

            var expected = await _context!.Species.AsQueryable().FirstOrDefaultAsync();
            var controller = new SpeciesController(
                new Mock<IAnimalRepository>().Object,
                _speciesRepository!,
                new Mock<IBreedRepository>().Object,
                _mapper!,
                new Mock<IExcelHandlerService>().Object
            );

            // Action
            var actionResult = await controller.Get(expected!.Id);

            // Assert
            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
            var result = Assert.IsType<SpeciesDTO>(objectResult.Value);

            Assert.NotNull(result);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);
        }


        [Fact]
        public async Task Get_TypeIsNull_ReturnNotFoundResponse()
        {
            // Arrange
            await _context!.Species.AddRangeAsync(new[]
            {
                new Species { Name = Guid.NewGuid().ToString()},
                new Species { Name = Guid.NewGuid().ToString()}
            });
            await _context.SaveChangesAsync();

            var controller = new SpeciesController(
                new Mock<IAnimalRepository>().Object,
                _speciesRepository!,
                new Mock<IBreedRepository>().Object,
                _mapper!,
                new Mock<IExcelHandlerService>().Object
            );

            // Action
            var badResponse = await controller.Get(0);

            Assert.IsType<NotFoundResult>(badResponse);
        }


        [Fact]
        public async Task Delete_ExistingId_ReturnNoContent_ItemRemoved()
        {
            // Arrange
            await _context!.Species.AddRangeAsync(new[]
            {
                new Species { Name = Guid.NewGuid().ToString()},
                new Species { Name = Guid.NewGuid().ToString()}
            });
            await _context.SaveChangesAsync();

            var candidate = await _context!.Species.AsQueryable().FirstOrDefaultAsync();
            var controller = new SpeciesController(
                new Mock<IAnimalRepository>().Object,
                _speciesRepository!,
                new Mock<IBreedRepository>().Object,
                _mapper!,
                new Mock<IExcelHandlerService>().Object
            );

            // Action
            var response = await controller.Delete(candidate!.Id);
            var notFoundResponse = await controller.Get(candidate!.Id);

            Assert.IsType<NoContentResult>(response);
            Assert.IsType<NotFoundResult>(notFoundResponse);
        }


        [Fact]
        public async Task Delete_TypeIsNull_ReturnNotFoundResponse()
        {
            // Arrange
            await _context!.Species.AddRangeAsync(new[]
            {
                new Species { Name = Guid.NewGuid().ToString()},
                new Species { Name = Guid.NewGuid().ToString()}
            });
            await _context.SaveChangesAsync();

            var controller = new SpeciesController(
                new Mock<IAnimalRepository>().Object,
                _speciesRepository!,
                new Mock<IBreedRepository>().Object,
                _mapper!,
                new Mock<IExcelHandlerService>().Object
            );

            // Action
            var badResponse = await controller.Delete(0);

            Assert.IsType<NotFoundResult>(badResponse);
        }
    }
}
