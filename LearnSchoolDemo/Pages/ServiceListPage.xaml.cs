using LearnSchoolDemo.ADOApp;
using LearnSchoolDemo.Classes;
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
    /// Логика взаимодействия для ServiceListPage.xaml
    /// </summary>
    public partial class ServiceListPage : Page
    {
        private readonly List<Service> _services;
        private List<Service> _sorted;
        private Predicate<Service> _filterQuery = x => true;
        private Func<Service, object> _sortQuery = x => x.ID;

        public ServiceListPage()
        {
            InitializeComponent();

            if (NavClass.isAuth)
            {
                btnNext.Content = "Выйти";
                btnAddService.Visibility = Visibility.Visible;
                btnAddClient.Visibility = Visibility.Visible;
            }
            else
            {
                btnNext.Content = "Войти";
                btnAddService.Visibility = Visibility.Collapsed;
                btnAddClient.Visibility = Visibility.Collapsed;
            }

            _services = App.Connection.Service.ToList();
            lvServiceList.ItemsSource = App.Connection.Service.ToList();

            cbSort.ItemsSource = SortingClass.Methods;
            cbFilter.ItemsSource = FilteringClass.Methods;
            cbSort.SelectedIndex = 0;
            cbFilter.SelectedIndex = 0;

            UpdateRecordsCount();
        }

        private void AddingService_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass("ДАННЫЕ УСЛУГИ", new AddingOrEditingServicePage(new Service())));
        }

        private void Edit_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Service service = button.DataContext as Service;
            NavClass.NextPage(new NavComponentsClass("ДАННЫЕ УСЛУГИ", new AddingOrEditingServicePage(service)));
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Button button = sender as Button;
                Service service = button.DataContext as Service;
                if (MessageBox.Show("Вы действительно хотите удалить услугу?", "",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.Connection.Service.Remove(service);
                    App.Connection.SaveChanges();
                    MessageBox.Show("Услуга успешно удалена");
                    NavigationService.Navigate(new ServiceListPage());
                }
            }
            catch
            {
                MessageBox.Show("Данная услуга не может быть удалена", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void AddingClient_Click(object sender, RoutedEventArgs e)
        {
            if (lvServiceList.SelectedItem as Service != null)
            {
                NavClass.NextPage(new NavComponentsClass("ЗАПИСЬ КЛИЕНТА НА УСЛУГУ", new ClientRecordPage(lvServiceList.SelectedItem as Service)));
            }
            else
            {
                MessageBox.Show("Выберите услугу и нажмите кнопку!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FilteringSelect(object sender, SelectionChangedEventArgs e)
        {
            switch (cbFilter.SelectedIndex)
            {
                case 0:
                    _filterQuery = x => true;
                    break;
                case 1:
                    _filterQuery = x => x.Discount >= 0 && x.Discount < 5;
                    break;
                case 2:
                    _filterQuery = x => x.Discount >= 5 && x.Discount < 15;
                    break;
                case 3:
                    _filterQuery = x => x.Discount >= 15 && x.Discount < 30;
                    break;
                case 4:
                    _filterQuery = x => x.Discount >= 30 && x.Discount < 70;
                    break;
                case 5:
                    _filterQuery = x => x.Discount >= 70 && x.Discount < 100;
                    break;
                default:
                    _filterQuery = x => true;
                    break;
            }
            FilterAndSort();
        }

        private void SortingSelect(object sender, SelectionChangedEventArgs e)
        {
            switch (cbSort.SelectedIndex)
            {
                case 0:
                    _sortQuery = x => x.ID;
                    break;
                case 1:
                    _sortQuery = x => x.CostDiscountMethod;
                    break;
                case 2:
                    _sortQuery = x => -x.CostDiscountMethod;
                    break;
            }
            FilterAndSort();
        }

        private void SearchChanged(object sender, TextChangedEventArgs e)
        {
            Search();
        }

        private void Search()
        {
            lvServiceList.ItemsSource = _sorted
                .Where(x => string.Join(" ", x.Title, x.Description).ToLower()
                .Contains(tbSearch.Text.ToLower()))
                .ToList();
            UpdateRecordsCount();
        }

        private void FilterAndSort()
        {
            _sorted = _services.Where(x => _filterQuery(x)).OrderBy(x => _sortQuery(x)).ToList();
            lvServiceList.ItemsSource = _services.Where(x => _filterQuery(x)).OrderBy(x => _sortQuery(x)).ToList();
            if (tbSearch.Text != "")
                Search();

            UpdateRecordsCount();
        }

        private void UpdateRecordsCount()
        {
            tbCount.Text = $"{lvServiceList.Items.Count} из {_services.Count()}";
        }

        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if ((sender as Button).Content.ToString() == "Войти")
                NavClass.NextPage(new NavComponentsClass("ВВОД КОДА", new CodePage()));
            else
            {
                NavClass.isAuth = false;
                NavClass.NextPage(new NavComponentsClass("УСЛУГИ", new ServiceListPage()));
            }
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            var path = @"C:\Users\Руслана\source\repos\LearnSchoolDemoWPF\LearnSchoolDemoWPF";

            foreach (var item in App.Connection.Service.ToArray().Where(i => !string.IsNullOrWhiteSpace(i.MainImagePath)))
            {
                var fullPath = path + item.MainImagePath;
                var byteImage = File.ReadAllBytes(fullPath);
                item.MainImageByte = byteImage;
            }

            App.Connection.SaveChanges();
        }

        private void ClientService_Click(object sender, RoutedEventArgs e)
        {
            NavClass.NextPage(new NavComponentsClass("ЗАПИСИ КЛИЕНТОВ", new ClientServiceListPage()));
        }
    }
}
