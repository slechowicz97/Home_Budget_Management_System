using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HBM.Entities
{
    public class ImportantShopping
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public string Place { get; set; }
        public DateTime DateStart { get; set; }
        public DateTime DateStop { get; set; }

        public virtual List<ShoppingPicture> ShoppingPictures { get; set; }

    }
}
