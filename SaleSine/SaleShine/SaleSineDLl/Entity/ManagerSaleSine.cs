using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using SaleSineDLl.Entity;

namespace SaleSineDLl
{
public class ManagerSaleSine
    {
        #region SQL
        const string CONNTION_STRING_NAME = "ShineSale";

        const string GET_STUDENT =@"SELECT [TZ] ,[Name],[Class],case when [IsSister]>1 then 1 else 0 end as ISSister ,Gift as ,ISGetGift as ISGift FROM " +
            "[dbo].[Students] where tz='{0}'";
        const string GET_PAYMENT = @"SELECT[Num],[TZ],[Sum],[Kind],[KindName]
                                        FROM[shineSalesByWolf].[dbo].[MoneyDonated]
                                            where TZ = '{0}'";


        const string GET_CACHE = @"SELECT [Type],[Gift],[money] FROM [dbo].[GiftType]";

        const string Get_GIFT = "SELECT  [Gift],[ISGet] FROM [dbo].[Gifts] where tz='{0}'";

        #endregion
        public Student GetStudent(string TZ)
        {
            Student value = null;

            ////קבלת הפרטים על התלמיד
            DataTable dt = Functions.ExQueryWithConnStr(string.Format(GET_STUDENT, TZ), new DalHandler().GetConnectionString(CONNTION_STRING_NAME));
                if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

               value = new DALService().CopyRecord<Student>(dt.Rows[0], typeof(Student));
            ///קבלת הפרטים על התשלום
            dt = Functions.ExQueryWithConnStr(string.Format(GET_PAYMENT, TZ), new DalHandler().GetConnectionString(CONNTION_STRING_NAME));
                if (dt != null && dt.Rows.Count > 0)
            {
                List<Payment> payments = new DALService().CopyRecords<Payment>(dt, typeof(Payment));
                value.Payments = payments;
            }
             dt= Functions.ExQueryWithConnStr(string.Format(Get_GIFT, TZ), new DalHandler().GetConnectionString(CONNTION_STRING_NAME));
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Gifts> Gift_All = new DALService().CopyRecords<Gifts>(dt, typeof(Gifts));
             value.GIFT = Gift_All;
            }

            return value;
        }


        public  List<GiftType> FillType()
        {
            List<GiftType> value = null;
            DataTable dt = Functions.ExQueryWithConnStr(GET_CACHE, new DalHandler().GetConnectionString(CONNTION_STRING_NAME));
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            value = new DALService().CopyRecords<GiftType>(dt, typeof(GiftType));

            return value;
        }


    }
}
