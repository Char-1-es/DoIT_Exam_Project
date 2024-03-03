using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Task_3
{
    public class User
    {
        public string Username { get; set; }
        public int Score { get; set; }
        public override string ToString()
        {
            return $"{Username} {Score}";
        }
    }
}
