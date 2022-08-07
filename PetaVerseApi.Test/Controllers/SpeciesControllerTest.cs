using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using PetaVerseApi.Contract;
using PetaVerseApi.Controller;
using PetaVerseApi.Core.Database;
using PetaVerseApi.Core.Entities;
using PetaVerseApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PetaVerseApi.Test.Controllers
{
    public class SpeciesControllerTest : IClassFixture<SpeciesController>
    {
        private readonly SpeciesController _speciesController;
        private readonly ApplicationDbContext _context;
        private readonly Random _generator;

        public SpeciesControllerTest(ApplicationDbContext context, IAnimalRepository animalRepository, ISpeciesRepository speciesRepository, IBreedRepository breedRepository, IMapper mapper, Random generator)
        {
            var requestMock = new Mock<HttpRequest>();
            requestMock.Setup(r => r.Scheme).Returns("https");
            requestMock.Setup(r => r.Host).Returns(new HostString("totechs-intranet-api.azurewebsites.net"));

            var httpContextMock = new Mock<HttpContext>();
            httpContextMock.Setup(c => c.Request).Returns(requestMock.Object);

            _speciesController = new SpeciesController(animalRepository, speciesRepository, breedRepository, mapper)
            {
                ControllerContext = new ControllerContext(new ActionContext(httpContextMock.Object, new RouteData(), new ControllerActionDescriptor()))
            };

            _context = context;
            _speciesController = new SpeciesController(animalRepository, speciesRepository, breedRepository, mapper);
            _generator = generator;
        }

        [Fact]
        public async Task GetAll_GetAllSpeciesFromDatabase()
        {
            var actionResult = await _speciesController.GetAll();
            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
            Assert.IsAssignableFrom<IEnumerable<SpeciesDTO>>(objectResult.Value);
        }

        [Fact]
        public async Task Get_GetByIdFromDatabase()
        {
            var expected = await _context.Species.AsQueryable().FirstOrDefaultAsync();
            var actionResult = await _speciesController.Get(expected!.Id);
            var objectResult = Assert.IsType<OkObjectResult>(actionResult);
            var result = Assert.IsType<SpeciesDTO>(objectResult.Value);

            Assert.NotNull(result);
            Assert.Equal(expected.Id, result.Id);
            Assert.Equal(expected.Name, result.Name);
        }

        [Fact]
        public async Task Get_TypeIsNull_ReturnNotFoundResponse()
        {
            var badResponse = await _speciesController.Get(0);
            Assert.IsType<NotFoundResult>(badResponse);
        }

        [Fact]
        public async Task Create_ReturnOkResponse()
        {
            var testExpert = new SpeciesDTO()
            {
                Name = Guid.NewGuid().ToString()
            };

            var createdResponse = await _speciesController.Create(testExpert);
            var objectResult = Assert.IsType<OkObjectResult>(createdResponse);
            var result = Assert.IsType<SpeciesDTO>(objectResult.Value);

            Assert.IsType<SpeciesDTO>(result);
            Assert.Equal(testExpert.Name, result.Name);
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnNoContent()
        {
            var species = new Species
            {
                Id = _generator.Next(int.MaxValue),
                Name = Guid.NewGuid().ToString()
            };
            _context.Species.Add(species);
            await _context.SaveChangesAsync();

            var response = await _speciesController.Delete(species.Id);

            Assert.IsType<NoContentResult>(response);
        }

        [Fact]
        public async Task Delete_TypeIsNull_ReturnNotFoundResponse()
        {
            var badResponse = await _speciesController.Delete(0);
            Assert.IsType<NotFoundResult>(badResponse);
        }
    }
}
