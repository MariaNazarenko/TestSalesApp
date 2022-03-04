namespace SalesApi.Models
{
    /// <summary>
    /// Отчет по маффинам
    /// </summary>
    public class MuffinReport
    {
        /// <summary>
        /// Кол-во поставлено
        /// </summary>
        public int СountSupplied { get; set; }

        /// <summary>
        /// Кол-во продано
        /// </summary>
        public int СountSolded { get; set; }

        /// <summary>
        /// Кол-во просрочено
        /// </summary>
        public int СountOverdued { get; set; }
    }
}
