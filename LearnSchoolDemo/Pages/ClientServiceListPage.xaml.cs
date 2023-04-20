using LearnSchoolDemo.Classes;
using System;
using System.Collections.Generic;
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
using System.Windows.Threading;

namespace LearnSchoolDemo.Pages
{
    /// <summary>
    /// Логика взаимодействия для ClientServiceListPage.xaml
    /// </summary>
    public partial class ClientServiceListPage : Page
    {
        private DispatcherTimer _timer;

        public ClientServiceListPage()
        {
            InitializeComponent();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            RefreshService();
        }
        private void RefreshService()
        {
            lvClientServiceList.ItemsSource = null;
            lvClientServiceList.ItemsSource = App.Connection.ClientService.ToList().Where(z => (z.StartTime.Year == DateTime.Today.Year && z.StartTime.Month == DateTime.Today.Month && z.StartTime.Day == DateTime.Today.Day && z.StartTime.Hour >= DateTime.Now.Hour && z.StartTime.Minute >= DateTime.Now.Minute) ||
            (z.StartTime.Year == DateTime.Today.Year && z.StartTime.Month == DateTime.Today.Month && z.StartTime.Day == DateTime.Now.AddDays(1).Day));
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            _timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(30)
            };
            _timer.Tick += TimerTick;
            _timer.Start();
            RefreshService();
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            _timer = null;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavClass.BackPage();
        }
    }
}
