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
    public partial class TaskList : Form
    {

        private static TaskList _instance;
        /// <summary>
        /// Иницыализацыя компонентов
        /// </summary>
        public TaskList()
        {
            InitializeComponent();
            _instance = this;
        }
        /// <summary>
        /// Получение ссилки екземпляра
        /// </summary>
        /// <returns>Возврат ссилки</returns>
        public static TaskList GetInstance()
        {
            return _instance ??= new TaskList();
        }
        /// <summary>
        /// Обновление полей в окне после получения данных
        /// </summary>
        public void RefreshItem()
        {
            listBox1.Items.Clear();
            flowLayoutPanel1.Controls.Clear();
            textBox1.Text = "";
            TasksLoad();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (listBox1.Items.Count != 0) RefreshItem();
            else TasksLoad();
        }

        private async void button2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                label2.Text = "Имя пустое или только пробел";
                return;
            }

            var newTask = new CreateTaskRequestModel
            {
                Name = textBox1.Text
            };

            await Task.Run(() =>
            {
                var jsonString = JsonConvert.SerializeObject(newTask);

                var client = new RestClient($"{Program.Url}controller");
                client.Timeout = -1;

                var request = new RestRequest(Method.POST);
                request.AddParameter("application/json", jsonString, ParameterType.RequestBody);

                var response = client.Execute(request);
            });

            label2.Text = "Название дела";
            RefreshItem();
        }

        private async void TasksLoad()
        {
            await Task.Run(() =>
            {
                var client = new RestClient($"{Program.Url}controller/");
                client.Timeout = -1;

                var request = new RestRequest(Method.GET);

                var response = client.Execute(request);
                if (response.Content.Length == 0) TasksLoad();

                var model = JsonConvert.DeserializeObject<List<TaskRequestModel>>(response.Content);

                FillListBox(model);
            });
        }

        private object FillListBox(List<TaskRequestModel> model)
        {
            return Invoke(new Action(() =>
            {
                foreach (var item in model)
                {
                    var taskItem = new TaskItem(item.Id, item.Name);

                    listBox1.Items.Add(item.Name);

                    flowLayoutPanel1.Controls.Add(taskItem);

                    var client2 = new RestClient($"{Program.Url}Thing/getAllThings?taskId={item.Id}");
                    client2.Timeout = -1;

                    var request2 = new RestRequest(Method.GET);

                    var response2 = client2.Execute(request2);
                    if (response2.Content.Length == 0) TasksLoad();

                    var model2 = JsonConvert.DeserializeObject<List<ThingRequestModel>>(response2.Content);

                    FillControl(item, taskItem, model2);
                }

                label1.Text = $"Количество Дел: {model.Count}";

            }));
        }

        private void FillControl(TaskRequestModel item, TaskItem taskItem, List<ThingRequestModel> model2)
        {
            Invoke(new Action(() =>
            {
                
                foreach (var item2 in model2)
                {
                    if (item.Id == item2.IdTask)
                    {
                        var thingItem = new ThingItem(item2.Description, item2.Priority, item2.DateOfEnd);

                        taskItem.Get().Controls.Add(thingItem);
                    }
                }
            }));
        }
    }
}
