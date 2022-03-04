using Microsoft.AspNetCore.Mvc;
using SalesApi.Models;
using SalesApi.Services;

namespace SalesApi.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class MuffinController : ControllerBase
    {
        private readonly ISalesService _salesService;

        public MuffinController(ISalesService salesService)
        {
            _salesService = salesService;
        }

        /// <summary>
        /// Покупка мафиинов
        /// </summary>
        /// <param name="countMuffin">Кол-во маффинов</param>
        /// <returns>200 - успешно. 400 - ошибка</returns>
        [HttpPost]
        public ActionResult BuyMaffin(int countMuffin)
        {
            try
            {
                _salesService.Buy(countMuffin);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Создать маффины
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("Сreate")]
        public ActionResult CreateMuffin()
        {
            _salesService.Create();
            return Ok();
        }


        /// <summary>
        /// Получение отчета
        /// </summary>
        /// <returns>Список мафиинов</returns>
        [HttpGet]
        [Route("Report")]
        public IEnumerable<Muffin> GetReport()
        {
            var result = _salesService.GetReport();
            return result;
        }
    }
}