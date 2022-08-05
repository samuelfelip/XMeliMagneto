using Meli.App.Services.Model;
using Meli.Application.Services;
using Meli.Presentation.API.Controllers;
using Meli.Presentation.API.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Meli.Magneto.Test
{
    public class MutantTest
    {
        [Fact]
        public async Task MutantController_OK200()
        {
            //Arrange
            var todoServices = new Mock<IDNAService>();
            var controller = new MutantController(todoServices.Object);
            var newTodo = CreateMutant();
            todoServices.Setup(t=>t.FindMutant(newTodo.Dna)).ReturnsAsync(true);

            // Act
            var okResult = await controller.Post(newTodo);
            // Assert

            Assert.IsType<OkResult>(okResult);
        }
        

        [Fact]
        public async Task MutantController_Forbiden403()
        {
            //Arrange
            var todoServices = new Mock<IDNAService>();
            var controller = new MutantController(todoServices.Object);
            var newTodo = CreateHuman();
            todoServices.Setup(t => t.FindMutant(newTodo.Dna)).ReturnsAsync(false);

            // Act
            var forbidResult = await controller.Post(newTodo);

            // Assert

            Assert.IsType<ForbidResult>(forbidResult);
        }
        
        
        public static DNAReq CreateMutant()
        {
            return new DNAReq
            {
                Dna = new string[] { "ATGCGA", "CAGTGC", "TTATGT", "AGAAGG", "CCCCTA", "TCACTG" }
            };
        }
        public static DNAReq CreateHuman()
        {
            return new DNAReq
            {
                Dna = new string[] { "ATGCGA", "CAGTGC", "TTATTT", "AGACGG", "GCGTCA", "TCACTG" }
            };
        }
    }
}