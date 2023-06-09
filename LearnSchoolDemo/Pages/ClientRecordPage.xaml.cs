using LearnSchoolDemo.ADOApp;
using LearnSchoolDemo.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LearnSchoolDemo.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientRecordPage.xaml
    /// </summary>
    public partial class ClientRecordPage : Page
    {
        public byte[] Image { get; set; }
        private Service Service { get; set; }

        public ClientRecordPage(Service service)
        {
            InitializeComponent();
            var clients = App.Connection.Client.ToList();

            foreach (var c in clients)
            {
                cbClient.Items.Add(c);
            }

            Service = service;
            tbTitle.Text = service.Title;
            tbDurationSeconds.Text = service.DurationInSeconds.ToString();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavClass.BackPage();
        }

        /// <summary>
        /// Добавление клиента на услугу
        /// </summary>
        private void Record_Click(object sender, RoutedEventArgs e)
        {
            if (tbTitle.Text != "" && tbDurationSeconds.Text != "" && cbClient.Text != "" && tbTime.Text != "")
            {
                var client = cbClient.SelectedItem as Client;
                string[] needTime = tbTime.Text.Split(':');
                DateTime expectedDateTime = Convert.ToDateTime(Date.SelectedDate).Add(new TimeSpan(Convert.ToInt32(needTime[0]), Convert.ToInt32(needTime[1]), 0));

                var newClientService = new ClientService()
                {
                    ClientID = client.ID,
                    ServiceID = Service.ID,
                    StartTime = expectedDateTime,
                    Comment = "",
                };

                App.Connection.ClientService.Add(newClientService);
                App.Connection.SaveChanges();
                MessageBox.Show("Клиент успешно добавлен на услугу");
                NavClass.NextPage(new NavComponentsClass("УСЛУГИ", new ServiceListPage()));
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
