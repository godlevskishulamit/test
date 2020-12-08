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

        const string GET_STUDENT = @"SELECT [TZ] ,[Name],[Class],case when [IdSister]>1 then 1 else 0 end as ISSister FROM " +
            "[dbo].[Students] where tz='{0}'";
        const string GET_PAYMENT = @"SELECT [Sum],[Kind] as Kind,[KindName]
                                        FROM .[dbo].[MoneyDonated]
                                            where TZ = '{0}'";


        const string GET_CACHE = @"SELECT [Type],[Gift],[money],MoneySister FROM [dbo].[GiftType]";

        const string Get_GIFT = "SELECT  [Gift],[ISGet] as ISGift FROM [dbo].[Gifts] where tz='{0}'";


        const string AddPaymeny = @"INSERT INTO[dbo].[MoneyDonated]
        ([Num],[TZ],[Sum],[Kind],[KindName],[DateUpdate])
 values
 (
(
 select 
 isnull(count(*) ,0)+1from MoneyDonated where TZ = '{0}'
 )
,'{0}' ,{1} , {2}
 ,N'{3}', getdate())";

        const string GETPERCLASS = @"select  sum(m.sum)
from Students inner join [dbo].[MoneyDonated]m
on Students.TZ= m.TZ where Students.Class =N'{0}'";
        const string ADD_GIFT = @"INSERT INTO [dbo].[Gifts]
           ([Tz]
           ,[Gift]
           ,[ISGet])
     VALUES ('{0}','{1}',0 )";
        const string UPDATEGIFT = @"UPDATE [dbo].[Gifts] set [ISGet] = 'True'
 WHERE tz = '{0}' and gift = {1}";

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
            value.Payments = new List<Payment>();
            ///קבלת הפרטים על התשלום
            dt = Functions.ExQueryWithConnStr(string.Format(GET_PAYMENT, TZ), new DalHandler().GetConnectionString(CONNTION_STRING_NAME));
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Payment> payments = new DALService().CopyRecords<Payment>(dt, typeof(Payment));
                value.Payments = payments;
            }
            dt = Functions.ExQueryWithConnStr(string.Format(Get_GIFT, TZ), new DalHandler().GetConnectionString(CONNTION_STRING_NAME));
            value.GIFT = new List<Gifts>();
            if (dt != null && dt.Rows.Count > 0)
            {
                List<Gifts> Gift_All = new DALService().CopyRecords<Gifts>(dt, typeof(Gifts));
                value.GIFT = Gift_All;
            }

            return value;
        }


        public List<GiftType> FillType()
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


        /// <summary>
        /// Add To DB 
        /// </summary>
        /// <param name="TZ"></param>
        /// <param name="payment"></param>
        /// <returns></returns>
        public bool AddPAyment(String TZ, Payment payment)
        {
            try
            {
                Functions.ExNONQuery(string.Format(AddPaymeny, TZ, payment.Sum , payment.Kind, payment.KindName), CONNTION_STRING_NAME);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Add Gift 
        public bool AddGift(String TZ, Gifts gift)
        {
            try
            {
                 Functions.ExNONQuery(string.Format(ADD_GIFT, TZ, gift.Gift ), CONNTION_STRING_NAME);
                return true;
            }
            catch
            {
                return false;
            }
        }


        public bool UpdateGift(String TZ, Gifts gift)
        {
            try
            {
                Functions.ExNONQuery(string.Format(UPDATEGIFT , TZ, gift.Gift), CONNTION_STRING_NAME);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public double GetALlPaymenyClass(string Class)
        {
            DataTable dt = Functions.ExQueryWithConnStr(string.Format(GETPERCLASS, Class), new DalHandler().GetConnectionString(CONNTION_STRING_NAME));
            if (dt == null || dt.Rows.Count == 0)
            {
                return 0;
            }
            else
            if (dt.Rows[0][0] == DBNull.Value)
                return 0;
                return Convert.ToDouble(dt.Rows[0][0]);
        }

    }
}
