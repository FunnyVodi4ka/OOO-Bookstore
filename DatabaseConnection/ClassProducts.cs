using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOO_Bookstore.DatabaseConnection
{
    public partial class Products
    {
        public string CorrectPhoto
        {
            get
            {
                if(File.Exists(System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\ProductImages\\" + Photo))
                {
                    return System.AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\ProductImages\\" + Photo;
                }
                else
                {
                    return "/Resources/AppImages/DefaultPicture.png";
                }
            }
        }

        public string CorrectBackground
        {
            get
            {
                if(InStock <= 3)
                {
                    return "Gray";
                }
                else
                {
                    return "White";
                }
            }
        }

        public string CostWithDiscount
        {
            get
            {
                return Decimal.Subtract(Cost, (decimal)Discount).ToString();
            }
        }

        public string CorrectInStockLine
        {
            get
            {
                if(InStock == 0)
                {
                    return "Strikethrough";
                }
                else
                {
                    return "None";
                }
            }
        }
    }
}
