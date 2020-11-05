using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Domain
{
    public class Country
    {
        public int Id { get; set; }
        public string NameEn { get; set; }
        public string NameEs { get; set; }
        public string ISO2 { get; set; }
        public string ISO3 { get; set; }
    }
}
