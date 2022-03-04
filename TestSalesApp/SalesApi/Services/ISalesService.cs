using SalesApi.Models;

namespace SalesApi.Services
{
    public interface ISalesService
    {
        public void Create();
        public IEnumerable<Muffin> GetReport();
        public void Buy(int countMuffin);

    }
}
