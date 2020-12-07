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
