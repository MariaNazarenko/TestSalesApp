using Microsoft.AspNetCore.Mvc;
using SalesApi.Models;

namespace SalesApi.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class MuffinController : ControllerBase
    {
        private readonly ILogger<MuffinController> _logger;

        public MuffinController(ILogger<MuffinController> logger)
        {
            _logger = logger;
        }


        [HttpPost]
        public ActionResult BuyMaffin()
        {
            return Ok();
        }

        [HttpGet]
        [Route("Report")]
        public List<Muffin> GetReport()
        {
            var muffinList = new List<Muffin>();
            var muffin = new Muffin { Id = 1};
            var muffin2 = new Muffin { Id = 2};
            muffinList.Add(muffin);
            muffinList.Add(muffin2);

            return muffinList;
        }
    }
}