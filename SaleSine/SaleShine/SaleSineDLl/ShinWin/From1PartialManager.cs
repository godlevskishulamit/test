using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaleSineDLl;
namespace ShinWin
{
    partial class Form1
    {

        SaleSineDLl.Entity.Student student;

        public void createCB()
        {

            int point1 = 59;
            int point2 = 64;
           
            for (int i = 0; i < Cache.Instance.Gifts.Count ; i++)
            { 

            
             System.Windows.Forms.CheckBox CB= new System.Windows.Forms.CheckBox();
           
            CB.BackColor = System.Drawing.Color.Black;
            CB.Font = new System.Drawing.Font("Lucida Fax", 36F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            CB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(183)))), ((int)(((byte)(103)))));
            CB.Location = new System.Drawing.Point(point1 , (i+1)*point2 );
            CB.Name = "CB"+i.ToString ();
            CB.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            CB.Size = new System.Drawing.Size(311, 59);
            CB.TabIndex = 19;
            CB.Text = Cache.Instance.Gifts[i].Gift ;
                CB.UseVisualStyleBackColor = false;

            PnelStudent.Controls.Add(CB);
                lChekBox.Add(CB);
        }
        }

        public void AftertextTzStudents(string txtStudents)
        {
           
            student = new ManagerSaleSine().GetStudent(txtStudents);
            if(student==null)
            {
             lblR.Text = "מספר זהות לא תקין";
                return;
            }
            lblR.Text = "";
            lblStudent.Text = student.Name;
            bool Nogift = true;
            PnelStudent.Visible = true;
            for (int i = 0; i < Cache.Instance.Gifts.Count  ; i++)
            {
                lChekBox[i].Visible = false;
                lChekBox[i].Text = Cache.Instance.Gifts[i].Gift;
                    if(student.GIFT.Count -1>= i)
                {
                    if (!student.GIFT[i].IsGift)
                    {
                        lChekBox[i].Visible = true;
                        Nogift = false;
                    }
                }
                     
            }
            lbno.Visible = Nogift;
        }


        }

    
}
