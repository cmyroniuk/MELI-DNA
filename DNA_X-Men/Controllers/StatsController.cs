using DNA_X_Men.Models;
using System.Web.Http;

namespace DNA_X_Men.Controllers
{
    public class StatsController : ApiController
    {
        private readonly IMutantService mutantService;

        public StatsController(IMutantService mutantService)
        {
            this.mutantService = mutantService;
        }

        [System.Web.Http.HttpGet]
        public IHttpActionResult Get()
        {
             DNAStats stats = mutantService.GetDNAStats();

             return Ok(new
             {
                 count_mutant_dna = stats.CountMutantDna,
                 count_human_dna = stats.CountHumanDna,
                 ratio = stats.Ratio
             });
            
        }
    }
}