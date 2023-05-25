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
    /// Логика взаимодействия для PageOrder.xaml
    /// </summary>
    public partial class PageOrder : Page
    {
        decimal sum = 0, sumDiscount = 0;

        public PageOrder()
        {
            InitializeComponent();

            SetDeliveryPoints();

            listOrder.ItemsSource = OrderList();
        }

        Basket[] OrderList()
        {
            List<Basket> rows = AppConnect.modelDB.Basket.ToList();

            sum = 0;

            foreach (var item in AppConnect.modelDB.Basket)
            {
                sum += Decimal.Parse(item.Products.CostWithDiscount);
                decimal discount = (decimal)item.Products.Discount;
                sumDiscount += discount;
            }

            tblSum.Text = sum.ToString();
            tblSumDiscount.Text = sumDiscount.ToString();

            return rows.ToArray();
        }

        private void SetDeliveryPoints()
        {
            cbDeliveryPoints.Items.Add("Выберите фильтрацию");

            foreach (var item in AppConnect.modelDB.DeliveryPoints)
            {
                cbDeliveryPoints.Items.Add(item.Address);
            }

            cbDeliveryPoints.SelectedIndex = 0;
        }

        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            AppFrame.frameMain.Navigate(new PageProducts());
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            listOrder.ItemsSource = OrderList();
        }

        private void btnAddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Orders order = new Orders();
                order.Sum = sum;
                order.DeliveryDate = 3;
                var point = AppConnect.modelDB.DeliveryPoints.FirstOrDefault(x => x.Address == cbDeliveryPoints.SelectedItem.ToString());
                order.IdDeliveryPoint = point.IdDeliveryPoint;
                order.Surname = tbSurname.Text;
                order.Name = tbName.Text;
                order.Phone = tbPhone.Text;
                Random rnd = new Random();
                order.Code = rnd.Next(100,999).ToString();

                AppConnect.modelDB.Orders.Add(order);
                AppConnect.modelDB.SaveChanges();

                foreach (var item in AppConnect.modelDB.Basket)
                {
                    OrderProduct orderList = new OrderProduct();
                    orderList.IdProduct = item.IdProduct;
                    orderList.IdOrder = order.IdOrder;
                    orderList.Count = item.Count;
                    AppConnect.modelDB.OrderProduct.Add(orderList);
                    AppConnect.modelDB.Basket.Remove(item);
                }

                AppConnect.modelDB.SaveChanges();

                AppFrame.frameMain.Navigate(new PageProducts());
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.ToString());
            }
        }
    }
}
