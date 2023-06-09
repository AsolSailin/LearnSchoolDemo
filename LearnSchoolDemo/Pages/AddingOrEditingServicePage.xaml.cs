using LearnSchoolDemo.ADOApp;
using LearnSchoolDemo.Classes;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
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
    /// Логика взаимодействия для AddingOrEditingServicePage.xaml
    /// </summary>
    public partial class AddingOrEditingServicePage : Page
    {
        public byte[] Image { get; set; }
        private Service Service { get; set; }
        private int number = 0;

        public AddingOrEditingServicePage(Service service)
        {
            InitializeComponent();
            Service = service;
            DataContext = Service;
            Update();
        }

        private void Update()
        {
            IEnumerable<ServicePhoto> services = App.Connection.ServicePhoto;
            services = services.Where(x => x.Service.ID == Service.ID).Skip(number).Take(3);
            lvPhotoList.ItemsSource = services;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavClass.BackPage();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (tbTitle.Text != "" && tbCost.Text != "" && tbDurationSeconds.Text != "" && tbDiscount.Text != "" && Img != null)
            {
                if (Service.ID == 0)
                {
                    Service.MainImageByte = Image;
                    App.Connection.Service.AddOrUpdate(Service);
                }

                App.Connection.SaveChanges();
                MessageBox.Show("Услуга успешно сохранена");
            }
            else
            {
                MessageBox.Show("Все поля должны быть заполнены!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = sender as Button;
                var dialog = new OpenFileDialog();

                if (dialog.ShowDialog() != null)
                {
                    Image = File.ReadAllBytes(dialog.FileName);
                    imgImage.Source = new BitmapImage(new Uri(dialog.FileName));
                }
            }
            catch
            {
                MessageBox.Show("Файл не распознан! Необходимо добавить картинку!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Скролл картинок влево
        /// </summary>
        private void BackImg_Click(object sender, RoutedEventArgs e)
        {
            number--;

            if (number < 0)
                number = 0;

            Update();
        }

        /// <summary>
        /// Скролл картинок вправо
        /// </summary>
        private void NextImg_Click(object sender, RoutedEventArgs e)
        {
            number++;

            if (lvPhotoList.Items.Count < 3)
                number--;

            Update();
        }

        /// <summary>
        /// Добавление дополнительной картинки
        /// </summary>
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var btn = sender as Button;
                var dialog = new OpenFileDialog();

                if (dialog.ShowDialog() != null)
                {
                    Image = File.ReadAllBytes(dialog.FileName);

                    var newServicePhoto = new ServicePhoto()
                    {
                        Service = Service,
                        PhotoByte = Image
                    };
                    App.Connection.ServicePhoto.Add(newServicePhoto);
                    App.Connection.SaveChanges();
                    lvPhotoList.ItemsSource = null;
                    lvPhotoList.ItemsSource = Service.ServicePhoto.ToList();
                }
            }
            catch
            {
                MessageBox.Show("Файл не распознан! Необходимо добавить картинку!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (lvPhotoList.SelectedItem as ServicePhoto != null)
            {
                if (MessageBox.Show("Вы действительно хотите удалить картинку?", "",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question) == MessageBoxResult.Yes)
                {
                    App.Connection.ServicePhoto.Remove(lvPhotoList.SelectedItem as ServicePhoto);
                    App.Connection.SaveChanges();
                    lvPhotoList.ItemsSource = null;
                    lvPhotoList.ItemsSource = Service.ServicePhoto.ToList();
                }
            }
            else
            {
                MessageBox.Show("Выберите картинку и нажмите кнопку!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
