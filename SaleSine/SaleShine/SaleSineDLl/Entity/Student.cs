using System;
using System.Collections.Generic;
using System.Text;

namespace SaleSineDLl.Entity
{


    public class GiftType
    {
        public int Type { get; set; }

        public string Gift { get; set; }

        public Double Money { get; set; }

        public Double MoneySister { get; set; }
    }

   public  class Gifts
    {
        public int Gift { get; set; }
        public bool IsGift { get; set; }
    }

    public class Payment
    {

        public double Sum { get; set; }
        public int  Kind { get; set; }

        public string  KindName { get; set; }
    }
   public  class Student
    {
        public string TZ { get; set; }
        public string Name { get; set; }

        public string Class { get; set; }

        public bool ISSister { get; set; }


        public List<Payment > Payments { get; set; }

         public List<Gifts> GIFT { get; set; }

    }



}
