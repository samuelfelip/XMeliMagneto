using Microsoft.AspNetCore.Mvc;
using Meli.Application.Services;
using System.Net;
using Meli.Presentation.API.Model;

namespace Meli.Presentation.API.Controllers
{
    [Route("api/mutant")]
    [ApiController]
    public class MutantController : ControllerBase
    {
        #region Members & Properties

        private readonly IDNAService _DNAService;

        #endregion

        #region Constructors

        public MutantController(IDNAService DNAService)
        {
            _DNAService = DNAService;
        }

        #endregion

        #region Actions



        [HttpPost]
        [Route("")]
        public async Task<IActionResult> Post([FromBody] DNAReq dna)
        {
            try
            {
                var isMutant = await _DNAService.FindMutant(dna.Dna);
                if (isMutant) { return Ok(); }
                return Forbid();
            }
            catch (Exception) { return Forbid(); }
        }



        #endregion
    }
}