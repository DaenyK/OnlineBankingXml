using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineBanking.Admin.Lib.Model
{
    public class Operator
    {
        /// <summary>
        /// имя
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// процент за обслуживание
        /// </summary>
        public double Percent { get; set; }
        /// <summary>
        /// лого компании
        /// </summary>
        public string Logo { get; set; }
        /// <summary>
        /// префиксы оператора
        /// </summary>
       public List<Prefix> Prefixes = new List<Prefix>();
    }

    public class Prefix
    {
        /// <summary>
        /// префикс
        /// </summary>
        public int pref { get; set; }
    }
}
