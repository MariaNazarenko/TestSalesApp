using System.ComponentModel.DataAnnotations.Schema;

namespace SalesApi.Models
{
    /// <summary>
    /// Статус, который может быть маффин
    /// </summary>
    public enum StatusMaffin
    {
        /// <summary>
        /// Поставлена
        /// </summary>
        Supplied,

        /// <summary>
        /// Продана
        /// </summary>
        Solded,

        /// <summary>
        /// Просрочена
        /// </summary>
        Overdued
    }

    public class Muffin
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        [Column("id")]
        public int Id { get; set; }

        /// <summary>
        /// Дата производства
        /// </summary>
        [Column("datecreate")]
        public DateTime DateCreate { get; set; } = DateTime.Now;

        /// <summary>
        /// Состояние булочки
        /// </summary>
        [Column("status")]
        public StatusMaffin Status { get; set; } = StatusMaffin.Supplied;

    }
}
