use coreSP;

create proc AddEmp
@Name varchar(100),
@Salary decimal(9,2)
as
begin

insert into emps(Name,Salary) values(@Name,@Salary);
end


CREATE PROCEDURE GetAllEmps
AS
BEGIN
    SELECT * from emps;
END


select * from emps;
drop proc GetAllEmps;
exec AddEmp 'vaishnav',2000;