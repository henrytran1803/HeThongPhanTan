USE master
GO

CREATE DATABASE EmployeeManagement
GO

USE EmployeeManagement
GO

CREATE TABLE Employee(
	EmployeeID INT IDENTITY(1, 1),
	FullName NVARCHAR(100),
	PhoneNo CHAR(15) UNIQUE,
	Birthday DATE,
	Adress NVARCHAR(100),
	CONSTRAINT PK_Employee PRIMARY KEY (EmployeeID)
)
GO

CREATE TABLE History(
	EmployeeID INT,
	UpdateTime DATETIME,
	SiteUpdate NVARCHAR(100),
	ContentUpdate NTEXT,
	CONSTRAINT PK_History PRIMARY KEY (EmployeeID, UpdateTime),
	CONSTRAINT FK_History_Employee FOREIGN KEY (EmployeeID) REFERENCES Employee(EmployeeID)
)
GO
