CREATE TABLE Categories (ID int(10) NOT NULL AUTO_INCREMENT, Name varchar(30), PRIMARY KEY (ID));
CREATE TABLE Customers (ID int(10) NOT NULL AUTO_INCREMENT, FirstName varchar(30) NOT NULL, LastName varchar(30) NOT NULL, Address varchar(255) NOT NULL, PhoneNumber varchar(16) NOT NULL, IdentificationNumber varchar(20) NOT NULL 
UNIQUE, PRIMARY KEY (ID));
CREATE TABLE Employees (ID int(10) NOT NULL AUTO_INCREMENT, FirstName varchar(30) NOT NULL, LastName varchar(30) NOT NULL, Address varchar(255) NOT NULL, PhoneNumber varchar(16) NOT NULL, Salary numeric(19, 0), Login varchar(30) 
NOT NULL, Password varchar(30) NOT NULL, PermissionLevel int(5) NOT NULL, EmploymentDate date NOT NULL, PRIMARY KEY (ID));
CREATE TABLE Items (ID int(10) NOT NULL AUTO_INCREMENT, CategoryID int(10) NOT NULL, ManufacturerID int(10) NOT NULL, ModelName varchar(255) NOT NULL, `Size` numeric(19, 0) NOT NULL, PricePerDay numeric(19, 0) NOT NULL, Available 
varchar(1), PurchaseDate date, PRIMARY KEY (ID));
CREATE TABLE Manufacturers (ID int(10) NOT NULL AUTO_INCREMENT, Name varchar(255), PRIMARY KEY (ID));
CREATE TABLE Orders (ID int(10) NOT NULL AUTO_INCREMENT, PaymentID int(10) NOT NULL, EmployeeID int(10) NOT NULL, CustomerID int(10) NOT NULL, DateRented date, DateReturned date, Comment varchar(255), PRIMARY KEY (ID));
CREATE TABLE Payments (ID int(10) NOT NULL AUTO_INCREMENT, PaymentDate date NOT NULL, Amount numeric(19, 0) NOT NULL, Currency varchar(3) NOT NULL, PRIMARY KEY (ID));
CREATE TABLE RentedItems (ID int(10) NOT NULL AUTO_INCREMENT, OrderID int(10) NOT NULL, ItemsD int(10) NOT NULL, PRIMARY KEY (ID));
ALTER TABLE Orders ADD CONSTRAINT FKOrders418421 FOREIGN KEY (EmployeeID) REFERENCES Employees (ID);
ALTER TABLE Orders ADD CONSTRAINT FKOrders911524 FOREIGN KEY (CustomerID) REFERENCES Customers (ID);
ALTER TABLE Orders ADD CONSTRAINT FKOrders875725 FOREIGN KEY (PaymentID) REFERENCES Payments (ID);
ALTER TABLE RentedItems ADD CONSTRAINT FKRentedItem979035 FOREIGN KEY (OrderID) REFERENCES Orders (ID);
ALTER TABLE RentedItems ADD CONSTRAINT FKRentedItem481434 FOREIGN KEY (ItemsD) REFERENCES Items (ID);
ALTER TABLE Items ADD CONSTRAINT FKItems983394 FOREIGN KEY (CategoryID) REFERENCES Categories (ID);
ALTER TABLE Items ADD CONSTRAINT FKItems216782 FOREIGN KEY (ManufacturerID) REFERENCES Manufacturers (ID);

