namespace SalesApi.Models
{
    public enum StatusMaffin
    {
        /// <summary>
        /// Поставлена
        /// </summary>
        Supplied,

        /// <summary>
        /// Продана
        /// </summary>
        Sold,

        /// <summary>
        /// Просрочена
        /// </summary>
        Overdue
    }
    public class Muffin
    {
        /// <summary>
        /// Идентиикатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Дата производства
        /// </summary>
        public DateTime DateCreate { get; set; } = DateTime.Now;

        /// <summary>
        /// Состояние булочки
        /// </summary>
        public StatusMaffin Status { get; set; } = StatusMaffin.Supplied;

    }
}
