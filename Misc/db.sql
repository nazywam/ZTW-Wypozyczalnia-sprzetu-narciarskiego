CREATE TABLE Categories (ID int(10) NOT NULL AUTO_INCREMENT, Name varchar(30) NOT NULL, PricePerDay numeric(19, 19), PRIMARY KEY (ID), INDEX (ID));
CREATE TABLE Customers (ID int(10) NOT NULL AUTO_INCREMENT, FirstName varchar(255) NOT NULL, LastName varchar(255) NOT NULL, Address varchar(255) NOT NULL, PhoneNumber int(10) NOT NULL, 
IdentyficationNumber varchar(20) NOT NULL UNIQUE, PRIMARY KEY (ID));
CREATE TABLE Employees (ID int(10) NOT NULL AUTO_INCREMENT, FirstName varchar(30) NOT NULL, LastName varchar(30) NOT NULL, Address varchar(255) NOT NULL, PhoneNumber int(10) NOT NULL, Salary numeric(19, 
19), Login varchar(30) NOT NULL, Password varchar(30) NOT NULL, PermissionLevel int(5) NOT NULL, EmploymentDate date NOT NULL, PRIMARY KEY (ID));
CREATE TABLE Items (ID int(10) NOT NULL AUTO_INCREMENT, CategoryID int(10) NOT NULL, ManufacturerID varchar(30) NOT NULL, ModelName varchar(255) NOT NULL, `Size` varchar(10) NOT NULL, Avaiable 
varchar(1), Purchase_date date, PRIMARY KEY (ID));
CREATE TABLE Orders (ID int(10) NOT NULL AUTO_INCREMENT, PaymentID int(10) NOT NULL, EmployeeID int(10) NOT NULL, CustomerID int(10) NOT NULL, Date_Rented timestamp NULL, Date_Return timestamp NULL, 
Comment varchar(255), PRIMARY KEY (ID));
CREATE TABLE Payments (ID int(10) NOT NULL AUTO_INCREMENT, PaymentDate date NOT NULL, Amount numeric(19, 19) NOT NULL, Currency varchar(3) NOT NULL, PRIMARY KEY (ID));
CREATE TABLE Rented_Items (ID int(10) NOT NULL AUTO_INCREMENT, OrderID int(10) NOT NULL, ItemID int(10) NOT NULL, PRIMARY KEY (ID));

ALTER TABLE Rented_Items ADD CONSTRAINT FKRented_Ite582137 FOREIGN KEY (ItemID) REFERENCES Items (ID);
ALTER TABLE Rented_Items ADD CONSTRAINT FKRented_Ite43910 FOREIGN KEY (OrderID) REFERENCES Orders (ID);
ALTER TABLE Orders ADD CONSTRAINT FKOrders911524 FOREIGN KEY (CustomerID) REFERENCES Customers (ID);
ALTER TABLE Orders ADD CONSTRAINT FKOrders418421 FOREIGN KEY (EmployeeID) REFERENCES Employees (ID);
ALTER TABLE Orders ADD CONSTRAINT FKOrders875725 FOREIGN KEY (PaymentID) REFERENCES Payments (ID);
ALTER TABLE Items ADD CONSTRAINT FKItems983394 FOREIGN KEY (CategoryID) REFERENCES Categories (ID);

