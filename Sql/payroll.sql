--create database
create database PayXpert

--change the database
use PayXpert

--create table Employee
create table Employee(
EmployeeID int primary key,
FirstName varchar(20),
LastName varchar(20),
DateOfBirth date,
Gender varchar(15),
Email varchar(50),
PhoneNumber int,
Address varchar(100),
Position varchar(50),
JoiningDate date,
TerminationDate date)

--create table payroll
create table Payroll(
PayrollID int primary key,
EmployeeID int,
PayPeriodStartDate date,
PayPeriodEndDate date,
BasicSalary int,
OvertimePay int,
Deductions int,
NetSalary int,
Foreign key(EmployeeID) references Employee(EmployeeID))

--create Tax table
create table Tax(
TaxID int primary key,
EmployeeID int,
TaxYear int,
TaxableIncome int,
TaxAmount int,
Foreign key(EmployeeID) references Employee(EmployeeID))

--create FinancialRecord table
create table FinancialRecord(
RecordID int primary key,
EmployeeID int,
RecordDate date,
Description varchar(100),
Amount int,
RecordType varchar(20),
Foreign key(EmployeeID) references Employee(EmployeeID))

--drop table as i did not give identity
drop table FinancialRecord

--create FinancialRecord table
create table FinancialRecord(
RecordID int identity primary key,
EmployeeID int,
RecordDate date,
Description varchar(100),
Amount int,
RecordType varchar(20),
Foreign key(EmployeeID) references Employee(EmployeeID))


-- Insert values into Employee table
INSERT INTO Employee (EmployeeID, FirstName, LastName, DateOfBirth, Gender, Email, PhoneNumber, Address, Position, JoiningDate, TerminationDate)
VALUES (100, 'John', 'Doe', '1990-05-15', 'Male', 'john.doe@example.com', 1234567890, 'Chennai', 'Manager', '2020-01-01', NULL),
(101, 'Jane', 'Smith', '1985-08-20', 'Female', 'jane.smith@example.com', 9876543210, 'Chennai', 'Sales Associate', '2021-03-10', NULL),
(102, 'Michael', 'Johnson', '1988-12-03', 'Male', 'michael.johnson@example.com', 5551234567, 'namakkal', 'Accountant', '2019-07-15', NULL),
(103, 'Emily', 'Brown', '1995-04-28', 'Female', 'emily.brown@example.com', 1112223333, 'Trichy', 'Developer', '2022-02-20', NULL),
(104, 'David', 'Wilson', '1992-11-10', 'Male', 'david.wilson@example.com', 7778889999, 'Trichy', 'HR Manager', '2020-10-05', NULL)

--AS I HAVE GIVEN INT FOR NUMBER
ALTER TABLE Employee
ALTER COLUMN PhoneNumber Bigint

-- Insert values into Payroll table
INSERT INTO Payroll (PayrollID, EmployeeID, PayPeriodStartDate, PayPeriodEndDate, BasicSalary, OvertimePay, Deductions, NetSalary)
VALUES (123, 100, '2022-05-01', '2022-05-15', 5000, 500, 200, 5300),
(456, 101, '2022-05-01', '2022-05-15', 4000, 300, 150, 4150),
(789, 102, '2022-05-01', '2022-05-15', 4500, 400, 180, 4320),
(012, 103, '2022-05-01', '2022-05-15', 5500, 600, 250, 5850),
(345, 104, '2022-05-01', '2022-05-15', 4800, 350, 160, 4990)

-- Insert values into Tax table
INSERT INTO Tax (TaxID, EmployeeID, TaxYear, TaxableIncome, TaxAmount)
VALUES (1, 100, 2022, 60000, 8000),
(2, 101, 2022, 45000, 6000),
(3, 102, 2022, 52000, 7000),
(4, 103, 2022, 65000, 9000),
(5, 104, 2022, 58000, 7800)

-- Insert values into FinancialRecord table
INSERT INTO FinancialRecord (EmployeeID, RecordDate, Description, Amount, RecordType)
VALUES (100, '2022-05-10', 'Bonus Payment', 1000, 'Credit'),
(101, '2022-05-12', 'Travel Expenses', 300, 'Debit'),
(102, '2022-05-13', 'Training Course Fee', 200, 'Debit'),
(103, '2022-05-14', 'Project Bonus', 800, 'Credit'),
(104, '2022-05-15', 'Medical Reimbursement', 150, 'Debit')


--displaying values
select * from Employee;

select * from FinancialRecord;

select * from Payroll;

select * from Tax;


SELECT * FROM Payroll Where EmployeeID = 100 and YEAR(PayPeriodStartDate) = 2022 and YEAR(PayPeriodEndDate) = 2022;

SELECT * FROM 
    Employee as E
INNER JOIN 
    FinancialRecord as F ON E.EmployeeID = F.EmployeeID
Where E.EmployeeID=100;