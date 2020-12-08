using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaleSineDLl.Entity;
using SaleSineDLl;
namespace ShinWin
{
    partial class Form1 : Form
    {
        public void AfrerStepOne()
        {
            if (string.IsNullOrEmpty(TZ.Text))

            {
                lblRemark.Visible = true;
                lblRemark.Text = "לא הוקש מספר זהות אין אפשרות להמשיך";
                return;
            }
            if (TZ.Text.Length < 9 || TZ.Text.Trim().Length > 9)
            {
                lblRemark.Visible = true;
                lblRemark.Text = "לא הוקש מספר זהות תקין אורך נדרש 9 תווים . אין אפשרות להמשיך";
                return;
            }

            MyStudent = new ManagerSaleSine().GetStudent(TZ.Text);

            if (MyStudent == null)
            {
                lblRemark.Visible = true;
                lblRemark.Text = "מספר זהות  לא קים במערכת נא לפנות להנהלה ";
                return;
            }

            lblRemark.Visible = false;
            lblName.Text = MyStudent.Name;
            lblSum.Text = MyStudent.Payments.Sum(f => f.Sum).ToString();
            CmbKind.SelectedIndex = 0;
            LBLSUMALL.Text = new ManagerSaleSine().GetALlPaymenyClass(MyStudent.Class).ToString();
            VesibleColumn(1);
            ShowGifts();
        }


        public void AfterAddSum()
        {
            string show = " האם את מאשרת הוספת הסכום   {0} ב {1} ";
            Payment p = new Payment() { Kind = CmbKind.SelectedIndex, KindName = CmbKind.SelectedItem.ToString(), Sum = Convert.ToDouble(TxtSum.Text) };
            DialogResult resul = MessageBox.Show(string.Format(show, p.Sum, p.KindName), "אישור סכום", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading);
            if (resul == DialogResult.No)
            {
                TxtSum.Text = "0";
                return;
            }

            bool b =new ManagerSaleSine().AddPAyment(MyStudent.TZ, p);
            if(!b)
            {

                MessageBox.Show("בעיה בהוספת הכסף נסה שוב");
            }
            if (MyStudent.Payments == null)
                MyStudent.Payments = new List<Payment>();
            MyStudent.Payments.Add(p);
            LBLSUMALL.Text = new ManagerSaleSine().GetALlPaymenyClass(MyStudent.Class).ToString();
            ShowGifts();

        }



        public void ShowGifts()
        {
            lblgift.Text = "";
            if (MyStudent.Payments == null)
                return;
            double Sum = MyStudent.Payments.Sum(f => f.Sum);
            for (int i = 0; i < Cache.Instance.Gifts.Count; i++)
            {
                if ((Sum < Cache.Instance.Gifts[i].Money && !MyStudent.ISSister) || (Sum < Cache.Instance.Gifts[i].MoneySister && MyStudent.ISSister))
                    return;
                if (MyStudent.GIFT.Count <= i)
                {
                    Gifts giftnew = new Gifts() { Gift = Cache.Instance.Gifts[i].Type, IsGift = false };
                    bool isSucssedAdd = new ManagerSaleSine().AddGift(MyStudent.TZ, giftnew);
                    if (!isSucssedAdd)
                    {
                        MessageBox.Show("לא הצליח להוסיף מתנה יש לפנות לממונה");
                        return;
                    }
                    MyStudent.GIFT.Add(giftnew);

                }
                ///אם לא קבל את המתנה
                if (!MyStudent.GIFT[i].IsGift)
                {
                    lblgift.Text += Cache.Instance.Gifts[i].Gift + " ,    ";
                    FlowGift.Visible = true;

                }
            }
        }
    }

}