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
            if (TZ.Text.Length < 9)
            {
                lblRemark.Visible = true;
                lblRemark.Text = "לא הוקש מספר זהות תקין אורך נדרש 9 תווים . אין אפשרות להמשיך";
                return;
            }
          
            Student student = new ManagerSaleSine().GetStudent(TZ.Text);

            if (student == null)
            {
                lblRemark.Visible = true;
                lblRemark.Text = "מספר זהות  לא קים במערכת נא לפנות להנהלה ";
                return;
            }

            lblRemark.Visible = false;
            VesibleColumn(1);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

       private void VesibleColumn(int num)
        {

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
    }
}
