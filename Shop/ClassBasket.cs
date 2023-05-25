using OOO_Bookstore.AppConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_Bookstore.Shop
{
    class ClassBasket
    {
        public void ClearBasket()
        {
            foreach(var item in AppConnect.modelDB.Basket)
            {
                AppConnect.modelDB.Basket.Remove(item);
            }

            AppConnect.modelDB.SaveChanges();
        }
    }
}
