using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace LearnSchoolDemo.Classes
{
    public class NavComponentsClass
    {
        public string PageTitle { get; set; }
        public Page Page { get; set; }
        public NavComponentsClass(string pageTitle, Page page)
        {
            PageTitle = pageTitle;
            Page = page;
        }
    }
}
