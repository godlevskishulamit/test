update   shineSalesByWolf.dbo.Students 
set class ='י13'
where tz ='328518386'

go 


update dbo.Students 
set
class =replace(class,' ','')
go
update dbo.Students 
set
class =replace(class,'"','')

go
update dbo.Students 
set
class =replace(class,'''','')



update   shineSalesByWolf.dbo.Students 
set class ='יד12'
where class like('%/%')










