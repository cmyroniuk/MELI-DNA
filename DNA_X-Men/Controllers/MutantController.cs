using DNA_X_Men.Models;
using System.Net;
using System.Web.Http;

namespace DNA_X_Men.Controllers
{
    public class MutantController : ApiController
    {
        private readonly IMutantService mutantService;

        public MutantController(IMutantService mutantService)
        {
            this.mutantService = mutantService;
        }

        // POST api/mutant
        [System.Web.Http.HttpPost]
        public IHttpActionResult Post([FromBody] DnaRequest request)
        {
            if (!this.mutantService.ValidarDNAValido(request.Dna))
            {
                return Content(HttpStatusCode.Forbidden, new { message = "No es un DNA valido" });
            }

            if (this.mutantService.ValidarDNA(request.Dna))
            {
                return Ok("DNA mutante");
            }

            return Content(HttpStatusCode.Forbidden, new { message = "DNA humano" });

        }


    }
}