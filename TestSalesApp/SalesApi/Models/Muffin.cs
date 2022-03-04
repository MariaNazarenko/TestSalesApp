using System.ComponentModel.DataAnnotations.Schema;

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
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Дата производства
        /// </summary>
        /// 
        [Column("datecreate")]
        public DateTime DateCreate { get; set; } = DateTime.Now;

        /// <summary>
        /// Состояние булочки
        /// </summary>
        [Column("status")]
        public StatusMaffin Status { get; set; } = StatusMaffin.Supplied;

    }
}
