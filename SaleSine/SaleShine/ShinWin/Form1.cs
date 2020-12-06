using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using SaleSineDLl;
using SaleSineDLl.Entity;

namespace ShinWin
{
    public partial class Form1 : Form
    {
        Student MyStudent = null; 
        public Form1()
        {
            InitializeComponent();
            lblRemark.Visible = false;
            VesibleColumn(0);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            AfrerStepOne();
        }

        private void AfrerStepOne()
        {
            if (string.IsNullOrEmpty(TZ.Text))

            {
                lblRemark.Visible = true;
                lblRemark.Text = "לא הוקש מספר זהות אין אפשרות להמשיך";
                return;
            }
            if (TZ.Text.Length < 9|| TZ.Text.Trim().Length>9)
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
            CmbKind.SelectedIndex = 1;
            LBLSUMALL.Text = new ManagerSaleSine().GetALlPaymenyClass(MyStudent.Class).ToString ();
            VesibleColumn(1);
            ShowGifts();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }



        /// <summary>
        /// תצוגה פקדים 
        /// </summary>
        /// <param name="num"></param>
       private void VesibleColumn(int num)
        {
            lblRemark.Text = "";
            for (int i = 0; i < 4; i++)
            {
                if (i == num)
                {
                    this.TableLyaut.ColumnStyles[i].SizeType = SizeType.Percent;
                    this.TableLyaut.ColumnStyles[i].Width = 100;
                  
                }
                else
                {
                    this.TableLyaut.ColumnStyles[i].SizeType = SizeType.Percent;
                    this.TableLyaut.ColumnStyles[i].Width = 0;
                  
                    
                    
                }    
            }
           
        }


        //אחרי הוספת סכום
        private void BTNOK_Click(object sender, EventArgs e)
        {
            AfterAddSum();
        }

        private  void AfterAddSum()
        {
            string show = " האם את מאשרת הוספת הסכום   {0} בצורה הזו{1} ";
            Payment p = new Payment() { Kind = CmbKind.SelectedIndex, KindName = CmbKind.SelectedItem.ToString(), Sum =Convert.ToDouble ( TxtSum.Text) };
            DialogResult resul = MessageBox.Show(string.Format(show, p .Sum,p.KindName), "אישור סכום", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1, MessageBoxOptions.RtlReading );
            if (resul == DialogResult.No)
            {
                TxtSum.Text = "0";
                return;
            }

            new ManagerSaleSine().AddPAyment(MyStudent.TZ, p);
            if (MyStudent.Payments == null)
                MyStudent.Payments = new List<Payment>();
            MyStudent.Payments.Add(p);

        }



        public void ShowGifts()
        {
            lblgift.Text = "";
            if (MyStudent.Payments == null)
                return;
            double Sum = MyStudent.Payments.Sum(f => f.Sum);
            for (int i = 0; i < Cache.Instance.Gifts .Count; i++)
            {
                if (Sum < Cache.Instance.Gifts[i].Money|| (Sum <Cache.Instance.Gifts[i].MoneySister&& MyStudent.ISSister ))
                    return;
                if (MyStudent.GIFT.Count <= i)
                {
                  Gifts giftnew =new Gifts() { Gift = Cache.Instance.Gifts[i].Type, IsGift = false };
                    bool isSucssedAdd = new ManagerSaleSine().AddGift(MyStudent.TZ, giftnew);
                    if(!isSucssedAdd )
                    {
                        MessageBox.Show("לא הצליח להוסיף מתנה יש לפנות לממונה");
                        return;
                    }
                    MyStudent.GIFT.Add(giftnew);

                }
                ///אם לא קבל את המתנה
                if(!MyStudent.GIFT[i].IsGift )
                {
                    lblgift.Text += Cache.Instance.Gifts[i].Gift+"     ";
                    FlowGift.Visible = true;

                }
            }
        }

        private void LBLGift1_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void TxtSum_Validated(object sender, EventArgs e)
        {
            decimal d;
            if (decimal.TryParse(TxtSum.Text, out d))
            {
                //valid 
            }
            else
            {
                //invalid
                MessageBox.Show("סכומים יכולים להיות רק מספר");
                return;
            }

        }

        private void TxtSum_TextChanged(object sender, EventArgs e)
        {
            decimal d;
            if (decimal.TryParse(TxtSum.Text, out d))
            {
                //valid 
            }
            else
            {
                //invalid
                MessageBox.Show("סכומים יכולים להיות רק מספר");
                return;
            }
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BtnPrev_Click(object sender, EventArgs e)
        {
            MyStudent = null;
            TZ.Text = "";
            VesibleColumn(0);
        }
    }
}
