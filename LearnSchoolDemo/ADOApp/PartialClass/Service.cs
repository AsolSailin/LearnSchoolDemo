using LearnSchoolDemo.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace LearnSchoolDemo.ADOApp
{
    public partial class Service
    {
        public Visibility VisibilityMethod
        {
            get
            {
                if (NavClass.isAuth)
                    return Visibility.Visible;
                else
                    return Visibility.Collapsed;
            }
        }

        public decimal CostDiscountMethod
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return Cost;
                else
                    return Cost - (decimal)Cost * (decimal)Discount / 100;
            }
        }

        public string StrDiscountMethod
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return "";
                else
                    return $"* скидка {Discount}%";
            }
        }

        public string CostDurationMethod
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return $"{Cost:F} рублей за {DurationInSeconds / 60} минут";
                else
                    return $"{(double)Cost - (double)Cost * (double)Discount / 100:F} рублей за {DurationInSeconds / 60} минут";
            }
        }

        public Visibility DiscountVisibilityMethod
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }

        public string ColorDisMethod
        {
            get
            {
                if (Discount == 0 || Discount == null)
                    return "#ffffff";
                else
                    return "#D1FFD1";
            }
        }
    }
}
