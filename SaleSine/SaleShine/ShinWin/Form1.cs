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
        List<CheckBox> lChekBox = new List<CheckBox>();
        public Form1()
        {
            InitializeComponent();
            lblRemark.Visible = false;
            VesibleColumn(0);
            createCB();
           // lChekBox.Add(CB1);
           // lChekBox.Add(cb2);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(TZ.Text==ShinWin.Properties.Settings.Default.ManagerPAssword )
            {
                PnelStudent.Visible = false;
                VesibleColumn(2);
            }
            AfrerStepOne();

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
            FlowGift.Visible = false;
            TxtSum.Text = "";
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
            BTNOK.Enabled = false;
            AfterAddSum();
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
            if (String.IsNullOrEmpty(TxtSum.Text))
                return;
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

      
        private void BtnPrev_Click(object sender, EventArgs e)
        {
            MyStudent = null;
            TZ.Text = "";
            VesibleColumn(0);
        }

        private void lblName_Click(object sender, EventArgs e)
        {

        }

        private void btnstudent_Click(object sender, EventArgs e)
        {
            VesibleColumn(0);
        }

        private void textBox1_Validated(object sender, EventArgs e)
        {
           if(txtStudents.Text .Length !=9)
            {
                lblR.Text = "מספר זהות לא תקין";
            }
        }

        private void BtnRec_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < lChekBox.Count ; i++)
            {
                if (lChekBox[i].Visible && lChekBox[i].Checked)
                    new SaleSineDLl.ManagerSaleSine().UpdateGift(student.TZ, student.GIFT[i]);
            }
            txtStudents.Text = "";
            PnelStudent.Visible = false;
            
        }

        private void label16_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            AftertextTzStudents(txtStudents.Text);
        }
    }
}
