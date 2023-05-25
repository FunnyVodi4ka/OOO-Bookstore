using OOO_Bookstore.AppConnection;
using OOO_Bookstore.DatabaseConnection;
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

namespace OOO_Bookstore.Shop
{
    /// <summary>
    /// Логика взаимодействия для PageProducts.xaml
    /// </summary>
    public partial class PageProducts : Page
    {
        public PageProducts()
        {
            InitializeComponent();

            SetFilter();
            SetSort();

            listProducts.ItemsSource = SearchFilterSort();
        }

        Products[] SearchFilterSort()
        {
            List<Products> rows = AppConnect.modelDB.Products.ToList();

            return rows.ToArray();
        }

        private void SetFilter()
        {
            cbFilter.Items.Add("Выберите фильтрацию");

            foreach (var item in AppConnect.modelDB.Categories)
            {
                cbFilter.Items.Add(item.Name);
            }

            cbFilter.SelectedIndex = 0;
        }

        private void SetSort()
        {
            cbSort.Items.Add("Выберите сортировку");

            cbSort.Items.Add("По возростанию");
            cbSort.Items.Add("По убыванию");

            cbSort.SelectedIndex = 0;
        }

        private void tbSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            listProducts.ItemsSource = SearchFilterSort();
        }

        private void cbFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listProducts.ItemsSource = SearchFilterSort();
        }

        private void cbSort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listProducts.ItemsSource = SearchFilterSort();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            listProducts.ItemsSource = SearchFilterSort();
        }

        private void btnAddProduct_Click(object sender, RoutedEventArgs e)
        {
            var product = listProducts.SelectedItem as Products;

            var order = AppConnect.modelDB.Basket.FirstOrDefault(x => x.IdProduct == product.IdProduct);

            if(order == null)
            {
                Basket row = new Basket();
                row.IdProduct = product.IdProduct;
                row.Count = 1;

                AppConnect.modelDB.Basket.Add(row);
            }
            else
            {
                order.Count++;
            }
            
            AppConnect.modelDB.SaveChanges();
        }

        private void btnOrder_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageOrder());
        }
    }
}
