using Microsoft.AspNetCore.Mvc;
using Meli.Application.Services;
using System.Net;
using Meli.Presentation.API.Model;
using System.Diagnostics.CodeAnalysis;

namespace Meli.Presentation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatsController : ControllerBase
    {
        #region Members & Properties

        private readonly IDNAService _DNAService;

        #endregion

        #region Constructors

        public StatsController(IDNAService DNAService)
        {
            _DNAService = DNAService;
        }

        #endregion

        #region Actions
        [HttpGet]
        [Route("")]
        [ExcludeFromCodeCoverage]
        public async Task<IActionResult> GetAll()
        {
            var stats = await _DNAService.GetStats();
            return Ok(stats);
        }
        #endregion
    }
}