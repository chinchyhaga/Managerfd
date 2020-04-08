create database employee
--Problem 1
USE [employee]
GO
create table employees(
	[Employee-name] [nvarchar](50) PRIMARY KEY,
	[Street] [nvarchar](50),
	[City] [nvarchar](50) 
	)
create table works(
	[Employee-name] [nvarchar](50) PRIMARY KEY,
	[Company-name] [nvarchar](50),
	[Salary] [money]
	)
create table company(
	[Company-name] [nvarchar](50)PRIMARY KEY,
	[City] [nvarchar](50) 
	)
create table manages(
	[Employee-name] [nvarchar](50) PRIMARY KEY,
	[Manager-name] [nvarchar](50)
	)
-- Promblem 2
INSERT INTO employees
([Employee-name]
,[Street]
,[City])
      VALUES
('CHAU NHU NGOC','TRAN HUNG DAO','HO CHI MINH'),
('DUONG THANH NHU', 'HAI BA TRUNG','QUY NHON'),
('NGUYEN NGOC BAO', 'LE THANH TON', 'HO CHI MINH'),
('HO HINH DOAN','HAN MAC TU','QUY NHON'),
('QUACH NGOC DOAN CA','MAC DINH CHI', 'HO CHI MINH'),
('NGUYEN THANH BAO', 'TRAN HUNG DAO','HO CHI MINH'),
('MAC HAN','HAI BA TRUNG','QUY NHON'),
('DUONG NGOC THANH TAM','TANG BAT HO','HO CHI MINH'),
('NGUYEN HIEN DONG NGHI','MAC DINH CHI','QUY NHON'),
('PHAN THI THANH TAM','TRAN HUNG DAO','HO CHI MINH')
INSERT INTO works
([Employee-name]
,[Company-name]
,[Salary])
		VALUES
('CHAU NHU NGOC', 'First Bank Corporation',15.267),
('DUONG THANH NHU', 'THANH THONG', 4.596),
('NGUYEN NGOC BAO', '9SENSE', 10.000),
('HO HINH DOAN','THANH THONG',59.656),
('QUACH NGOC DOAN CA','First Bank Corporation', 100.166),
('NGUYEN THANH BAO', 'First Bank Corporation',15124.165),
('MAC HAN', 'THANH THONG', 8954.596),
('DUONG NGOC THANH TAM', '9SENSE', 16110.000),
('NGUYEN HIEN DONG NGHI', 'THANH THONG',1059.656),
('PHAN THI THANH TAM','First Bank Corporation', 7954.166)
INSERT INTO company
([Company-name]
,[City])
		VALUES
('ACECOOK','HO CHI MINH'),
('THANH THONG','QUY NHON'),
('9SENSE','HO CHI MINH'),
('BIG MARK','QUY NHON'),
('FGT','HO CHI MINH'),
('First Bank Corporation','HO CHI MINH')
INSERT INTO manages
([Employee-name]
,[Manager-name])
		VALUES
('CHAU NHU NGOC', 'NGUYEN THANH BAO'),
('DUONG THANH NHU', 'MAC HAN'),
('NGUYEN NGOC BAO', 'DUONG NGOC THANH TAM'),
('HO HINH DOAN','NGUYEN HIEN DONG NGHI'),
('QUACH NGOC DOAN CA', 'PHAN THI THANH TAM')
--PROBLEM 3
-- a
SELECT e.[Employee-name] AS [Name Of Employees Working For First Bank Corporation]
FROM works as w, employees as e
WHERE e.[Employee-name]=w.[Employee-name] AND w.[Company-name]='First Bank Corporation'
--b
SELECT [Employee-name] AS NAME, [City] AS CITYOFRESIDENCE
FROM employees
WHERE [Employee-name] in
(SELECT [Employee-name] FROM Works
WHERE [Company-name]='First Bank Corporation')--c
SELECT e.* 
FROM works as w, employees as e
WHERE e.[Employee-name]=w.[Employee-name] AND w.[Company-name]='First Bank Corporation' AND w.Salary>10000
--d
SELECT e.[Employee-name] AS NAME, c.*
FROM employees AS e, company AS c, works AS w
WHERE e.[Employee-name]=w.[Employee-name] AND w.[Company-name]=c.[Company-name] AND e.City=c.City
--e
SELECT e.*
FROM employees AS e, employees AS e1, manages AS m
WHERE e.[Employee-name]= m.[Employee-name] AND e1.[Employee-name] = m.[Manager-name]
and e.street=e1.street and e.city=e1.city--fSELECT *FROM worksWHERE [Company-name] != 'First Bank Corporation' --gSELECT *FROM worksWHERE Salary> (SELECT max(Salary) FROM works WHERE [Company-name] = 'First Bank Corporation') --hSELECT c1.*FROM company c, company c1WHERE c.[Company-name] =  'First Bank Corporation' AND c1.City=c.City--iSELECT *FROM worksWHERE Salary> (SELECT avg(w.Salary) FROM works w, works w1 WHERE w.[Company-name]=w1.[Company-name]) --jSELECT [Company-name], count(distinct [Employee-name]) AS [Number of Employees]
FROM Works
GROUP BY [Company-name]
HAVING count(distinct [Employee-name])>=all
( SELECT count(distinct [Employee-name]) FROM works
GROUP BY [Company-name])--kSELECT TOP 1 [Company-name] , avg(Salary) AS Payroll 
FROM Works
GROUP BY [Company-name]
ORDER BY avg(Salary) ASC
--l
SELECT [Company-name], avg(Salary) AS [Average Salary]  
FROM Works
GROUP BY [Company-name]
HAVING avg(Salary)>(SELECT avg(Salary) FROM works WHERE [Company-name]='First Bank Corporation')
--DATA
Select * from employees
Select* from works
Select * from manages
Select * from company