using RestSharp;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Xmuzenko_Web_Api_Klient_Kontrolna.Forms
{
    /// <summary>
    /// Класс Дела
    /// </summary>
    public partial class TaskItem : UserControl
    {
        private int Id { get; set; }
        private string Name { get; set; }

        /// <summary>
        /// Инициализация компонентов класса 
        /// </summary>
        /// <param name="id">айди дела</param>
        /// <param name="name">имя дела</param>
        public TaskItem(int id, string name)
        {
            InitializeComponent();
            Id = id;
            Name = name;
            this.label1.Text = $"Айди: {id}";
            this.label2.Text = $"Название: {name}";
            
        }
        /// <summary>
        /// Получение ссилки на панель
        /// </summary>
        /// <returns>Возврат ссилки</returns>
        public FlowLayoutPanel Get()
        {
            var panel = flowLayoutPanel1;
            return panel;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var seeThing = new CreateThing(Id, Name);
            seeThing.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            var client = new RestClient($"{Program.Url}controller/removeTask?id={this.Id}");
            client.Timeout = -1;

            var request = new RestRequest(Method.POST);

            var response = client.Execute(request);

            TaskList.GetInstance().RefreshItem();
        }
    }
}
