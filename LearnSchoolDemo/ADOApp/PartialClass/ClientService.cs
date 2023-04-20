using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LearnSchoolDemo.ADOApp
{
    public partial class ClientService
    {
        public DateTime TimeRemainsMethod
        {
            get
            {
                return new DateTime() + (StartTime - DateTime.Now);
            }
        }
        public string TimeRemainsStringMethod
        {
            get
            {
                return $"{TimeRemainsMethod.Day - 1}д {TimeRemainsMethod.Hour}ч {TimeRemainsMethod.Minute}м";
            }
        }
    }
}
