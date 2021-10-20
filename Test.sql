CREATE DATABASE Test;

USE Test
GO
CREATE TABLE Products (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    ProductName VARCHAR (50),
    AddedDate DATETIME,
    Quantity INT
);

USE Test
GO
CREATE TABLE Users (
    Id INT PRIMARY KEY IDENTITY (1, 1),
    Username VARCHAR (50),
    Password VARCHAR (50)
);


INSERT INTO Products(ProductName,AddedDate,Quantity) VALUES ('Product1','2021-10-20 13:00:00',100);
INSERT INTO Products(ProductName,AddedDate,Quantity) VALUES ('Product2','2021-10-20 14:00:00',125);
INSERT INTO Products(ProductName,AddedDate,Quantity) VALUES ('Product3','2021-10-20 15:00:00',548);
INSERT INTO Products(ProductName,AddedDate,Quantity) VALUES ('Product4','2021-10-20 16:00:00',652);
INSERT INTO Products(ProductName,AddedDate,Quantity) VALUES ('Product5','2021-10-20 17:00:00',173);
INSERT INTO Products(ProductName,AddedDate,Quantity) VALUES ('Product6','2021-10-20 18:00:00',689);
INSERT INTO Products(ProductName,AddedDate,Quantity) VALUES ('Product7','2021-10-20 19:00:00',322);
INSERT INTO Products(ProductName,AddedDate,Quantity) VALUES ('Product8','2021-10-20 20:00:00',265);

INSERT INTO Users(Username,Password) VALUES('admin','11');
