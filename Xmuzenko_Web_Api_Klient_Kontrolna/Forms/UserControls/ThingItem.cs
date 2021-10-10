using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xmuzenko_Web_Api_Klient_Kontrolna.Forms.UserControls
{   
    /// <summary>
    /// Класс задачи
    /// </summary>
    public partial class ThingItem : UserControl
    {
        /// <summary>
        /// Инициализация компонентов задачи
        /// </summary>
        /// <param name="description">Описание</param>
        /// <param name="priority">Приоритет</param>
        /// <param name="date">Дата окончания</param>
        public ThingItem(string description, string priority, DateTime date)
        {
            InitializeComponent();
            label1.Text = $"Описание: {description}" ;
            label2.Text = $"Приоритет: {priority}";
            label3.Text = $"Дата окончания: {date}";
        }
    }
}
