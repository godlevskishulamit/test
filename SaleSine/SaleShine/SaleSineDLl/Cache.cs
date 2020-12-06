using System;
using SaleSineDLl.Entity;
using System.Collections.Generic;
namespace SaleSineDLl
{

    public class Cache
    {


        private static readonly Lazy<Cache> _instance = new Lazy<Cache>(() => new Cache());
        public static Cache Instance
        {
            get
            {
                return _instance.Value;
            }
        }
        public List<GiftType> Gifts { get; set; }


        private Cache()
        {
            Gifts = new ManagerSaleSine().FillType();
        }
    }
}
