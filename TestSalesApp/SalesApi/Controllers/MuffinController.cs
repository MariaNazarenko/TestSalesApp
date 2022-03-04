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
        /// ������� ��������
        /// </summary>
        /// <param name="countMuffin">���-�� ��������</param>
        /// <returns>200 - �������. 400 - ������</returns>
        [HttpPost]
        public ActionResult BuyMaffin(int countMuffin)
        {
            if (_salesService.Buy(countMuffin)== true)
                return Ok();
            else 
                return BadRequest("��� �������� �������������� �������� �������");
        }

        /// <summary>
        /// ������� �������
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("�reate")]
        public ActionResult CreateMuffin()
        {
            _salesService.Create();
            return Ok();
        }


        /// <summary>
        /// ��������� ������
        /// </summary>
        /// <returns>������ ��������</returns>
        [HttpGet]
        [Route("Report")]
        public MuffinReport GetReport()
        {
            return _salesService.GetReport();
        }
    }
}