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

        public bool Buy(int countMuffin)
        {
            if (countMuffin > 0 && countMuffin <= maxCountBuyMuffin)
            {
                var muffins = _context.Muffins.Where(m => m.DateCreate > DateTime.Now.AddMinutes(-lifetimeMuffin) && m.Status == StatusMaffin.Supplied).
                    OrderBy(m => m.DateCreate).Take(countMuffin);
                if (muffins.Count() == countMuffin)
                {
                    foreach (var muffin in muffins)
                    {
                        muffin.Status = StatusMaffin.Solded;
                        _context.Muffins.Update(muffin);
                        _logger.LogInformation($"Изменен статус Id={muffin.Id} на продано");
                    }
                    _context.SaveChanges();
                    return true;
                }
            }
            return false;
        }

        public Muffin Create()
        {
            Muffin muffin = new Muffin();
            _context.Muffins.Add(muffin);
            _context.SaveChanges();
            _logger.LogInformation($"Добавлена запись Id={muffin.Id} с датой создания {muffin.DateCreate} со статусом поставлена");
            return muffin; ;
        }

        public MuffinReport GetReport()
        {
            UpdateStatusMuffins();
            var muffins = _context.Muffins.Where(m => m.DateCreate > DateTime.Now.AddDays(-maxDayReport)).OrderBy(m => m.Status);

            MuffinReport report = new MuffinReport();
            report.СountSupplied = muffins.Count(m => m.Status == StatusMaffin.Supplied);
            report.СountSolded = muffins.Count(m => m.Status == StatusMaffin.Solded);
            report.СountOverdued = muffins.Count(m => m.Status == StatusMaffin.Overdued);

            return report;
        }

        /// <summary>
        /// Обновление статусов маффинов в БД
        /// </summary>
        private void UpdateStatusMuffins()
        {
            var muffins = _context.Muffins.Where(m => m.DateCreate <= DateTime.Now.AddMinutes(-lifetimeMuffin) && m.Status == StatusMaffin.Supplied);
            foreach (var muffin in muffins)
            {
                muffin.Status = StatusMaffin.Overdued;
                _context.Muffins.Update(muffin);
                _logger.LogInformation($"Изменен статус Id={muffin.Id} на просрочено");
            }
            _context.SaveChanges();
        }
    }
}
