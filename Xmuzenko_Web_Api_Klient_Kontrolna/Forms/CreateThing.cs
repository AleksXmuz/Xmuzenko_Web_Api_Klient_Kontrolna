using Newtonsoft.Json;
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
using Xmuzenko_Web_Api_Klient_Kontrolna.Forms.UserControls;
using Xmuzenko_Web_Api_Klient_Kontrolna.Models;

namespace Xmuzenko_Web_Api_Klient_Kontrolna.Forms
{
    public partial class CreateThing : Form
    {
        private int Id { get; set; }
        private string Name { get; set; }
        /// <summary>
        /// Инициализация компонентов окна
        /// </summary>
        /// <param name="id">айди дела</param>
        /// <param name="name">имя дела</param>
        public CreateThing(int id, string name)
        {
            InitializeComponent();
            Id = id;
            Name = name;

            label6.Text = $"Дело: {name}";

            LoadThing(Id);
        }

        private async void button2_Click(object sender, EventArgs e)
        {

            var newThing = new CreateThingRequesModel
            {
                IdTask = Id,
                Description = richTextBox1.Text,
                Priority = comboBox1.Text,
                DateOfEnd = dateTimePicker1.Value
            };

            await Task.Run(() =>
            {
                var jsonString = JsonConvert.SerializeObject(newThing);

                var client = new RestClient($"{Program.Url}Thing?idTask={Id}");
                client.Timeout = -1;

                var request = new RestRequest(Method.POST);
                request.AddParameter("application/json", jsonString, ParameterType.RequestBody);

                var response = client.Execute(request);
            });

            LoadThing(Id);
        }


        private async void LoadThing(int id)
        {
            await Task.Run(() =>
            {
                var client = new RestClient($"{Program.Url}Thing/getAllThings?taskId={id}");
                client.Timeout = -1;

                var request = new RestRequest(Method.GET);

                var response = client.Execute(request);
                if (response.Content.Length == 0) LoadThing(id);

                var model = JsonConvert.DeserializeObject<List<ThingRequestModel>>(response.Content);

                FillPanel(model);
            });
        }

        private object FillPanel(List<ThingRequestModel> model)
        {
            return Invoke(new Action(() =>
            {
                foreach (var item in model)
                {
                    var thingItem = new ThingItem(item.Description, item.Priority, item.DateOfEnd);

                    flowLayoutPanel1.Controls.Add(thingItem);
                }

            }));
        }
    }
}
