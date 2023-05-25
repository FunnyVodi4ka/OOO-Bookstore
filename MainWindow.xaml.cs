using OOO_Bookstore.AppConnection;
using OOO_Bookstore.DatabaseConnection;
using OOO_Bookstore.Shop;
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

namespace OOO_Bookstore
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                AppConnect.modelDB = new OOO_BookstoreEntities();
                AppFrame.frameMain = frmMain;

                ClassBasket basket = new ClassBasket();
                basket.ClearBasket();

                AppFrame.frameMain.Navigate(new PageProducts());
            }
            catch
            {
                MessageBox.Show("Приложение на техническом обслуживание, повторите попытку позже");
            }
        }
    }
}
