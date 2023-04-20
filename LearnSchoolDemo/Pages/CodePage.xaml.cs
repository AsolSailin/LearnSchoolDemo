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

namespace LearnSchoolDemo.Pages
{
    /// <summary>
    /// Логика взаимодействия для CodePage.xaml
    /// </summary>
    public partial class CodePage : Page
    {
        public CodePage()
        {
            InitializeComponent();
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavClass.BackPage();
        }

        /// <summary>
        /// Переход на аккаунт админимтратора
        /// </summary>
        private void Next_Click(object sender, RoutedEventArgs e)
        {
            if (tbCode.Text != "")
            {
                if (tbCode.Text == "0000")
                {
                    NavClass.isAuth = true;
                    MessageBox.Show("Вы вошли от имени администратора");
                    NavClass.NextPage(new NavComponentsClass("УСЛУГИ", new ServiceListPage()));
                }
                else
                {
                    MessageBox.Show("Неверный код доступа!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Пожалуйста, введите код доступа!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
