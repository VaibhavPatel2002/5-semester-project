/*---------------------------------------------------Create DataBase------------------------------------------------------------- */

CREATE DATABASE MusicSchool

/*-------------------------------------------Use Database To do CRUD Operation--------------------------------------------------  */

use MusicSchool

/*------------------------------------------------------Teachers----------------------------------------------------------------- */


/*-----------------Create Table Of Teachers------------ */

create table TeachersTb1(
TNum int primary key IDENTITY (500,1),
TName varchar(50),
TDOB date,
TPhone varchar(50),
TQualification varchar(50),
TGender varchar(50),
TAdd varchar(50),   
);

/*-----------------Insert Data in Teachers Table Using Store Procedure ------------ */

create procedure sp_InsertDataOfTeacher
@nm varchar(50),
@dt date,
@phone varchar(50),
@qul varchar(50),
@gen varchar(50),
@add varchar(50)
as
begin
insert into TeachersTb1
(TName,
TDOB,
TPhone,
TQualification,
TGender,
TAdd
)
values
(@nm,
@dt,
@phone,
@qul,
@gen,
@add
);
end

/*----------------------------------- Select Data from Teacher to show in GridView using Store Procedure---------------------------- */

create procedure sp_dataofTeacher
as
begin
select * from TeachersTb1
end

/*------------------------------Update Records of Teachers using Store Procedure------------------------------------------------ */

create procedure sp_EditDataOfTeachers
@no int,
@nm varchar(50),
@dt date,
@phone varchar(50),
@qul varchar(50),
@gen varchar(50),
@add varchar(50)
as
begin
UPDATE TeachersTb1 set TName=@nm,TDOB=@dt,TPhone=@phone,TQualification=@qul,TGender=@gen,TAdd=@add where TNum=@no; 
end

/*-------------------------Delete Records of Teacher Table using Store Procedure------------------------------------------------*/

create procedure sp_DeleteDataofTeachers
@no int
as
begin
delete from TeachersTb1 where TNum=@no;
end

/*-------------------------------------------------------------------Course-------------------------------------------------- */

/*-----------------Create Table Of Course------------ */

create table CourseTb1(
CNum int primary key IDENTITY (1,1),
CName varchar(50),
CTId int,
CTName varchar(50),
CPrice int,
CDuration int,
constraint FK1 foreign key (CTId)
references TeachersTb1 (TNum)
);

/*-----------------Insert Data in Course Table Using Store Procedure ------------ */

create procedure sp_InsertDatainCourse
@cnm varchar(50),
@cid int,
@ctnm varchar(50),
@cprc int,
@cdur int
as
begin
insert into CourseTb1
(CName,
CTId,
CTName,
CPrice,
CDuration
)
values
(@cnm,
@cid,
@ctnm,
@cprc,
@cdur
);
end

/*----------------------------------- Select Data from Teacher to show in GridView using Store Procedure---------------------------- */

create procedure sp_dataofCourse
as
begin
select * from CourseTb1
end

/*------------------------------Update Records of Teachers using Store Procedure------------------------------------------------ */

create procedure sp_EditDataOfCourse
@cno int,
@cnm varchar(50),
@cid int,
@ctnm varchar(50),
@cprc int,
@cdur int
as
begin
UPDATE CourseTb1 set CName=@cnm,CTId=@cid,CTName=@ctnm,CPrice=@cprc,CDuration=@cdur where CNum=@cno; 
end

/*-------------------------Delete Records of Teacher Table using Store Procedure------------------------------------------------*/

create procedure sp_DeleteDataofCourse
@cno int
as
begin
delete from CourseTb1 where CNum=@cno;
end

/*------------------------------------------------ Get teachers For Teacher table----------------------------------- */

create procedure sp_getTeacher
as 
begin
select TNum from TeachersTb1
end

/*----------------------------------------------- Featch Teacher Name from TeachersTb1 Table ------------------------------------*/

create procedure sp_FecthTeacherName
@cid int
as 
begin
select * from TeachersTb1 where TNum=@cid;
end

/* -----------------Create Table Of Student or Learners ------------ */

create table StudentTb1(
Stnum int primary key IDENTITY (1,1),
StName varchar(50),
StDob date,
StAddress varchar(50),
StPhone varchar(50),
StCourse int,
StCName varchar(50),
StGender varchar(50),
StRemarks varchar(100),
constraint FK2 foreign key (StCourse)
references CourseTb1 (CNum)
);

/*-----------------Insert Data in Student Table Using Store Procedure ------------ */

create procedure sp_InsertDatainStudent
@Snm varchar(50),
@SDob date,
@SAdd varchar(50),
@Sphone varchar(50),
@Scur int,
@SCnm varchar(50),
@SGen varchar(50),
@SRmrk varchar(100)
as
begin
insert into StudentTb1
(StName,
StDob,
StAddress,
StPhone,
StCourse,
StCName,
StGender,
StRemarks
)
values
(@Snm,
@SDob,
@SAdd,
@Sphone,
@Scur,
@SCnm,
@SGen,
@SRmrk
);
end

/*----------------------------------- Select Data from Student to show in GridView using Store Procedure ---------------------------- */

create procedure sp_DataofStudent
as
begin
select * from StudentTb1
end

/*------------------------------Update Records of Teachers using Store Procedure------------------------------------------------ */

create procedure sp_EditDataOfStudent
@Stno int,
@Snm varchar(50),
@SDob date,
@SAdd varchar(50),
@Sphone varchar(50),
@Scur int,
@SCnm varchar(50),
@SGen varchar(50),
@SRmrk varchar(100)
as
begin
UPDATE StudentTb1 set StName=@Snm,StDob=@SDob,StAddress=@SAdd,StPhone=@Sphone,StCourse=@Scur,StCName=@SCnm,StGender=@SGen,StRemarks=@SRmrk where StNum=@Stno; 
end

/*-------------------------Delete Records of Student Table using Store Procedure------------------------------------------------*/

create procedure sp_DeleteDataofStudent
@Stno int
as
begin
delete from StudentTb1 where StNum=@Stno;
end

/*--------------------------------Get Course from CoursesTb1 Table in StudentTb1--------------------------------------------*/

create procedure sp_GetCourseinStudentTB
as
begin
select CNum from CourseTb1
end

/*----------------------------------------------- Featch Course Name from CourseTb1 Table ------------------------------------*/

create procedure sp_FecthCourseName
@cno int
as 
begin
select * from CourseTb1 where CNum=@cno
end

/*-------------------------------------------------------------------Fees-------------------------------------------------- */

/*-----------------Create Table Of Fees------------ */

create table FeeTb1(
FNum int primary key IDENTITY (100,1),
FStudId int,
FStudName varchar(50),
FCourseId int,
FCourseName varchar(50),
FDate date,
FAmount int
constraint FK5 foreign key (FStudId)
references StudentTb1 (Stnum),
constraint FK6 foreign key (FCourseId) 
references CourseTb1 (CNum)
);

/*------------------------------------------------------insert data in to FeeTb1 Table-------------------------------------- */

create procedure sp_insertintoFees
@FSid int,
@FSnm varchar(50),
@FCid int,
@FCnm varchar(50),
@Fdate date,
@Famt int
as 
begin
insert into FeeTb1
(FStudId,
FStudName,
FCourseId,
FCourseName,
FDate,
FAmount
)
values
(@FSid,
@FSnm,
@FCid,
@FCnm,
@Fdate,
@Famt
);
end

/*---------------------------------------------------------GetDataofFees-------------------------------------------------------*/

create procedure sp_getdataofFees
as 
begin
select * from FeeTb1
end


