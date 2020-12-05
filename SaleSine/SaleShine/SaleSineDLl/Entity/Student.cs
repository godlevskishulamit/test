using System;
using System.Collections.Generic;
using System.Text;

namespace SaleSineDLl.Entity
{

    public class Payment 
    {

        public double PAyment { get; set; }
        public int  Kind { get; set; }

    }
   public  class Student
    {
        public string TZ { get; set; }
        public string Name { get; set; }

        public string Class { get; set; }

        public bool ISSister { get; set; }


        public List<Payment > Payments { get; set; }


        public bool ISGift { get; set; }

        public string TypeGift { get; set; }
    }


    
}
