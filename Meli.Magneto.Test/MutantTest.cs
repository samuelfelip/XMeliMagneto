using Meli.App.Services.Model;
using Meli.Application.Services;
using Meli.Presentation.API.Controllers;
using Meli.Presentation.API.Model;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.Protected;
using System.Net;
using System.Net.Http;
using System.Threading;
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

            var iActionResult = Assert.IsType<OkObjectResult>(okResult);
            Assert.True((bool)iActionResult.Value);
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
            var okResult = await controller.Post(newTodo);

            // Assert

            var iActionResult = Assert.IsType<OkObjectResult>(okResult);
            Assert.True(iActionResult.StatusCode == 403);
        }
        
        [Fact]
        public async Task StatsController_GetStats200()
        {
            //Arrange
            var todoServices = new Mock<IDNAService>();
            var controller = new StatsController(todoServices.Object);

            // Act
            var actionResult = await controller.GetAll();
            var okResult = actionResult as Stats;

            // Assert          
            Assert.NotNull(okResult);
        }

        //[Fact]
        //public async Task GivenMockedHandler_WhenRunningMain_ThenHandlerResponds()
        //{
        //    var mockedProtected = _msgHandler.Protected();
        //    var setupApiRequest = mockedProtected.Setup<Task<HttpResponseMessage>>(
        //        "SendAsync",
        //        ItExpr.IsAny<HttpRequestMessage>(),
        //        ItExpr.IsAny<CancellationToken>()
        //        );
        //    var apiMockedResponse =
        //        setupApiRequest.ReturnsAsync(new HttpResponseMessage()
        //        {
        //            StatusCode = HttpStatusCode.OK,
        //            Content = new StringContent("mocked API response")
        //        });
        //}

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