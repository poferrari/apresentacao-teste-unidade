CREATE DATABASE SqlOrder
GO

USE SqlOrder
GO

CREATE TABLE dbo.Customer(
	Id INT IDENTITY(1,1) NOT NULL,
	Name VARCHAR(255) NOT NULL,	
	Email VARCHAR(255) NOT NULL,	
	CONSTRAINT PK_Customer PRIMARY KEY (Id)
)
GO

SET IDENTITY_INSERT dbo.Customer ON;  
GO  

INSERT INTO dbo.Customer (Id, Name, Email) VALUES (1, 'João Vitor', 'joao.silva@email.com');
INSERT INTO dbo.Customer (Id, Name, Email) VALUES (2, 'Maria ', 'maria@email.com');
INSERT INTO dbo.Customer (Id, Name, Email) VALUES (3, 'Fernanda', 'fernanda@email.com');
INSERT INTO dbo.Customer (Id, Name, Email) VALUES (4, 'Antonio Ferreira', 'antonio.ferreira@email.com');
INSERT INTO dbo.Customer (Id, Name, Email) VALUES (5, 'Ana Clara', 'ana.clara@email.com');

SET IDENTITY_INSERT dbo.Customer OFF;  
GO  

CREATE TABLE dbo.Product(
	Id INT IDENTITY(1,1) NOT NULL,
	Name VARCHAR(255) NOT NULL,	
	Price NUMERIC (10,2) NOT NULL,
	CONSTRAINT PK_Product PRIMARY KEY (Id)
)
GO

SET IDENTITY_INSERT dbo.Product ON;  
GO  

INSERT INTO dbo.Product (Id, Name, Price) VALUES (1, 'X-Salada', '25.75');
INSERT INTO dbo.Product (Id, Name, Price) VALUES (2, 'Batata Frita', '7.00');
INSERT INTO dbo.Product (Id, Name, Price) VALUES (3, 'X-Egg', '32.40');
INSERT INTO dbo.Product (Id, Name, Price) VALUES (4, 'Burguer do Bão', '37.13');
INSERT INTO dbo.Product (Id, Name, Price) VALUES (5, 'Coca Cola', '4.00');
INSERT INTO dbo.Product (Id, Name, Price) VALUES (6, 'Guaraná', '3.70');

SET IDENTITY_INSERT dbo.Product OFF;  
GO  

CREATE TABLE dbo.PromoCode(
	Id INT IDENTITY(1,1) NOT NULL,
	Code VARCHAR(150) NOT NULL,
	Value NUMERIC (10,2) NOT NULL,
	ExpireDate DATETIME2 NOT NULL,	
	CONSTRAINT PK_PromoCode PRIMARY KEY (Id)
)
GO

SET IDENTITY_INSERT dbo.PromoCode ON;  
GO  

INSERT INTO dbo.PromoCode (Id, Code, Value, ExpireDate) VALUES (1, '50OFF', '50.00', '2021-04-29');
INSERT INTO dbo.PromoCode (Id, Code, Value, ExpireDate) VALUES (2, '20OFF', '20.00', '2021-04-28');
INSERT INTO dbo.PromoCode (Id, Code, Value, ExpireDate) VALUES (3, '5OFF', '5.00', '2021-04-27');
INSERT INTO dbo.PromoCode (Id, Code, Value, ExpireDate) VALUES (4, '10OFF', '10.00', '2021-04-21');

SET IDENTITY_INSERT dbo.PromoCode OFF;  
GO  

CREATE TABLE dbo.[Order](
	Id INT IDENTITY(1,1) NOT NULL,
	CustomerId INT NOT NULL,
	Code VARCHAR(150) NOT NULL,
	Date DATETIME2 NOT NULL,	
	SubTotal NUMERIC (10,2) NOT NULL,
	Discount NUMERIC (10,2) NOT NULL,
	DeliveryFee NUMERIC (10,2) NOT NULL,
	Total NUMERIC (10,2) NOT NULL,
	CONSTRAINT PK_Order PRIMARY KEY (Id),
	CONSTRAINT FK_Customer FOREIGN KEY (CustomerId) REFERENCES dbo.Customer(Id),
)
GO

CREATE TABLE dbo.OrderProduct(
	Id INT IDENTITY(1,1) NOT NULL,
	OrderId INT NOT NULL,
	ProductId INT NOT NULL,	
	ProductValue NUMERIC (10,2) NOT NULL,
	Amount INT NOT NULL,		
	Total NUMERIC (10,2) NOT NULL,
	CONSTRAINT PK_OrderProduct PRIMARY KEY (Id),
	CONSTRAINT FK_Order FOREIGN KEY (OrderId) REFERENCES dbo.[Order](Id),
	CONSTRAINT FK_Product FOREIGN KEY (ProductId) REFERENCES dbo.Product(Id)
)
GO
