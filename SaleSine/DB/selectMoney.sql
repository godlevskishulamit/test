select Students.TZ as 'תעודת זהות'
,Students.Name as 'שם'
,Students.Class as 'כיתה' ,
MoneyDonated.Num as 'מספר תשלום' ,
MoneyDonated.Sum as 'סכום' 
,MoneyDonated.DateUpdate as 'תאריך הוספה' 
from MoneyDonated inner join Students 
on MoneyDonated.TZ =Students.TZ ;
