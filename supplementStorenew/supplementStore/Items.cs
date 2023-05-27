using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace supplementStore
{
  public  class Items
    {
        public int id { get; set; }
        public string name { get; set; }
        public double buyPrice { get; set; }
        public double sellPrice { get; set; }
        public string description { get; set; }

        public double quntity { get; set; }
    }
}
