using SalesApi.Models;

namespace SalesApi.Services
{
    public class SalesService:ISalesService
    {
        private readonly DataContext _context;
        private readonly ILogger<SalesService> _logger;
        /// <summary>
        /// Максимальное количество продаваемых маффинов
        /// </summary>
        private readonly int maxCountBuyMuffin = 3;

        /// <summary>
        /// Кол-во создаваемых маффинов
        /// </summary>
        private readonly int countCreateMuffin = 10;

        /// <summary>
        /// Срок годности в минутах
        /// </summary>
        private readonly int lifetimeMuffin = 1;

        /// <summary>
        /// Кол-во дней отчета
        /// </summary>
        private readonly int maxDayReport = 1;

        public SalesService(ILogger<SalesService> logger, DataContext dataContext)
        {
            _context = dataContext;
            _logger = logger;
        }

        public void Buy(int countMuffin)
        {
            if (countMuffin <= maxCountBuyMuffin)
            {
                var muffins = _context.Muffins.Where(m => m.DateCreate > DateTime.Now.AddMinutes(-lifetimeMuffin) && m.Status == StatusMaffin.Supplied).OrderBy(m => m.DateCreate).Take(countMuffin);
                if (muffins.Count() < maxCountBuyMuffin)
                {
                    foreach(var muffin in muffins)
                    {
                        muffin.Status = StatusMaffin.Sold;
                        _context.Muffins.Update(muffin);
                        _logger.LogInformation($"Изменен статус Id={muffin.Id} на продано");
                    }
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Нет маффинов соответсвующих условиям продажи");
                }
            }
            else
            {
                throw new Exception("Количество маффинов превышает допустимое");
            }
        }

        public void Create()
        {
            for(int i = 0; i < countCreateMuffin; i++)
            {
                Muffin muffin = new Muffin();
                _context.Muffins.Add(muffin);
                _logger.LogInformation($"Добавлена запись с датой создания{muffin.DateCreate} со статусом поставлена");
            }
            _context.SaveChanges();
        }

        public IEnumerable<Muffin> GetReport()
        {
            var muffins = _context.Muffins.Where(m => m.DateCreate <= DateTime.Now.AddMinutes(-lifetimeMuffin) && m.Status == StatusMaffin.Supplied);
            foreach (var muffin in muffins)
            {
                muffin.Status = StatusMaffin.Overdue;
                _context.Muffins.Update(muffin);
                _logger.LogInformation($"Изменен статус Id={muffin.Id} на просрочено");
            }
            _context.SaveChanges();

            return _context.Muffins.Where(m => m.DateCreate > DateTime.Now.AddDays(-maxDayReport)).OrderBy(m=>m.Status);
        }
    }
}
