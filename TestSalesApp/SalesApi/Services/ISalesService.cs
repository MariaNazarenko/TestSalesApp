using SalesApi.Models;

namespace SalesApi.Services
{
    public interface ISalesService
    {
        public Muffin Create();
        public MuffinReport GetReport();
        public bool Buy(int countMuffin);
    }
}
