
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 05/29/2019 01:13:54
-- Generated from EDMX file: C:\Users\Priya\Downloads\COMP5348-Group-Assignment-integrate_message_services_jai\COMP5348-Group-Assignment\BookStore.Entities\BookStore.Business.Entities\BookStoreEntityModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [BookStore];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_OrderOrderItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderItems] DROP CONSTRAINT [FK_OrderOrderItem];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_CustomerOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomerLoginCredential]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Users] DROP CONSTRAINT [FK_CustomerLoginCredential];
GO
IF OBJECT_ID(N'[dbo].[FK_OrderItemBook]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[OrderItems] DROP CONSTRAINT [FK_OrderItemBook];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole_User]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_User];
GO
IF OBJECT_ID(N'[dbo].[FK_UserRole_Role]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[UserRole] DROP CONSTRAINT [FK_UserRole_Role];
GO
IF OBJECT_ID(N'[dbo].[FK_DeliveryOrder]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Deliveries] DROP CONSTRAINT [FK_DeliveryOrder];
GO
IF OBJECT_ID(N'[dbo].[FK_Orders_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Orders_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_WStock_ToTable_1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WStocks] DROP CONSTRAINT [FK_WStock_ToTable_1];
GO
IF OBJECT_ID(N'[dbo].[FK_WStock_ToTable]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[WStocks] DROP CONSTRAINT [FK_WStock_ToTable];
GO
IF OBJECT_ID(N'[dbo].[FK_BookWStock_Book]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookWStock] DROP CONSTRAINT [FK_BookWStock_Book];
GO
IF OBJECT_ID(N'[dbo].[FK_BookWStock_WStock]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BookWStock] DROP CONSTRAINT [FK_BookWStock_WStock];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[Deliveries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Deliveries];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[OrderItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[OrderItems];
GO
IF OBJECT_ID(N'[dbo].[LoginCredentials]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LoginCredentials];
GO
IF OBJECT_ID(N'[dbo].[Books]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Books];
GO
IF OBJECT_ID(N'[dbo].[Roles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Roles];
GO
IF OBJECT_ID(N'[dbo].[Warehouses]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Warehouses];
GO
IF OBJECT_ID(N'[dbo].[WBooks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WBooks];
GO
IF OBJECT_ID(N'[dbo].[WStocks]', 'U') IS NOT NULL
    DROP TABLE [dbo].[WStocks];
GO
IF OBJECT_ID(N'[dbo].[UserRole]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRole];
GO
IF OBJECT_ID(N'[dbo].[BookWStock]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BookWStock];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Address] nvarchar(max)  NOT NULL,
    [Email] nvarchar(max)  NOT NULL,
    [Revision] binary(8)  NULL,
    [BankAccountNumber] int  NOT NULL,
    [LoginCredential_Id] int  NOT NULL
);
GO

-- Creating table 'Deliveries'
CREATE TABLE [dbo].[Deliveries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Status] int  NOT NULL,
    [DestinationAddress] nvarchar(max)  NOT NULL,
    [ExternalDeliveryIdentifier] uniqueidentifier  NOT NULL,
    [SourceAddress] nvarchar(max)  NOT NULL,
    [Order_Id] int  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Total] float  NULL,
    [OrderDate] datetime  NOT NULL,
    [Warehouse] nvarchar(max)  NULL,
    [Store] nvarchar(max)  NULL,
    [OrderNumber] uniqueidentifier  NOT NULL,
    [Warehouse_Id] int  NOT NULL,
    [Customer_Id] int  NOT NULL
);
GO

-- Creating table 'OrderItems'
CREATE TABLE [dbo].[OrderItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Quantity] int  NOT NULL,
    [OrderOrderItem_OrderItem_Id] int  NOT NULL,
    [Book_Id] int  NOT NULL
);
GO

-- Creating table 'LoginCredentials'
CREATE TABLE [dbo].[LoginCredentials] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [UserName] nvarchar(30)  NOT NULL,
    [EncryptedPassword] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Books'
CREATE TABLE [dbo].[Books] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] nvarchar(max)  NOT NULL,
    [Author] nvarchar(max)  NOT NULL,
    [Genre] nvarchar(max)  NOT NULL,
    [Price] float  NOT NULL,
    [Quantity] int  NOT NULL
);
GO

-- Creating table 'Roles'
CREATE TABLE [dbo].[Roles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Warehouses'
CREATE TABLE [dbo].[Warehouses] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(max)  NOT NULL
);
GO

-- Creating table 'WBooks'
CREATE TABLE [dbo].[WBooks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(max)  NOT NULL,
    [Author] varchar(max)  NOT NULL,
    [Genre] varchar(max)  NOT NULL
);
GO

-- Creating table 'WStocks'
CREATE TABLE [dbo].[WStocks] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [quantity] int  NULL,
    [WBook_id] int  NOT NULL,
    [Warehouse_id] int  NOT NULL
);
GO

-- Creating table 'UserRole'
CREATE TABLE [dbo].[UserRole] (
    [User_Id] int  NOT NULL,
    [Roles_Id] int  NOT NULL
);
GO

-- Creating table 'BookWStock'
CREATE TABLE [dbo].[BookWStock] (
    [Books_Id] int  NOT NULL,
    [WStocks_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Deliveries'
ALTER TABLE [dbo].[Deliveries]
ADD CONSTRAINT [PK_Deliveries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [PK_OrderItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LoginCredentials'
ALTER TABLE [dbo].[LoginCredentials]
ADD CONSTRAINT [PK_LoginCredentials]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Books'
ALTER TABLE [dbo].[Books]
ADD CONSTRAINT [PK_Books]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Roles'
ALTER TABLE [dbo].[Roles]
ADD CONSTRAINT [PK_Roles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Warehouses'
ALTER TABLE [dbo].[Warehouses]
ADD CONSTRAINT [PK_Warehouses]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WBooks'
ALTER TABLE [dbo].[WBooks]
ADD CONSTRAINT [PK_WBooks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'WStocks'
ALTER TABLE [dbo].[WStocks]
ADD CONSTRAINT [PK_WStocks]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [User_Id], [Roles_Id] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [PK_UserRole]
    PRIMARY KEY CLUSTERED ([User_Id], [Roles_Id] ASC);
GO

-- Creating primary key on [Books_Id], [WStocks_Id] in table 'BookWStock'
ALTER TABLE [dbo].[BookWStock]
ADD CONSTRAINT [PK_BookWStock]
    PRIMARY KEY CLUSTERED ([Books_Id], [WStocks_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [OrderOrderItem_OrderItem_Id] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_OrderOrderItem]
    FOREIGN KEY ([OrderOrderItem_OrderItem_Id])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderOrderItem'
CREATE INDEX [IX_FK_OrderOrderItem]
ON [dbo].[OrderItems]
    ([OrderOrderItem_OrderItem_Id]);
GO

-- Creating foreign key on [Customer_Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_CustomerOrder]
    FOREIGN KEY ([Customer_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerOrder'
CREATE INDEX [IX_FK_CustomerOrder]
ON [dbo].[Orders]
    ([Customer_Id]);
GO

-- Creating foreign key on [LoginCredential_Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [FK_CustomerLoginCredential]
    FOREIGN KEY ([LoginCredential_Id])
    REFERENCES [dbo].[LoginCredentials]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomerLoginCredential'
CREATE INDEX [IX_FK_CustomerLoginCredential]
ON [dbo].[Users]
    ([LoginCredential_Id]);
GO

-- Creating foreign key on [Book_Id] in table 'OrderItems'
ALTER TABLE [dbo].[OrderItems]
ADD CONSTRAINT [FK_OrderItemBook]
    FOREIGN KEY ([Book_Id])
    REFERENCES [dbo].[Books]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_OrderItemBook'
CREATE INDEX [IX_FK_OrderItemBook]
ON [dbo].[OrderItems]
    ([Book_Id]);
GO

-- Creating foreign key on [User_Id] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_User]
    FOREIGN KEY ([User_Id])
    REFERENCES [dbo].[Users]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Roles_Id] in table 'UserRole'
ALTER TABLE [dbo].[UserRole]
ADD CONSTRAINT [FK_UserRole_Role]
    FOREIGN KEY ([Roles_Id])
    REFERENCES [dbo].[Roles]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_UserRole_Role'
CREATE INDEX [IX_FK_UserRole_Role]
ON [dbo].[UserRole]
    ([Roles_Id]);
GO

-- Creating foreign key on [Order_Id] in table 'Deliveries'
ALTER TABLE [dbo].[Deliveries]
ADD CONSTRAINT [FK_DeliveryOrder]
    FOREIGN KEY ([Order_Id])
    REFERENCES [dbo].[Orders]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_DeliveryOrder'
CREATE INDEX [IX_FK_DeliveryOrder]
ON [dbo].[Deliveries]
    ([Order_Id]);
GO

-- Creating foreign key on [Warehouse_Id] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Orders_ToTable]
    FOREIGN KEY ([Warehouse_Id])
    REFERENCES [dbo].[Warehouses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Orders_ToTable'
CREATE INDEX [IX_FK_Orders_ToTable]
ON [dbo].[Orders]
    ([Warehouse_Id]);
GO

-- Creating foreign key on [Warehouse_id] in table 'WStocks'
ALTER TABLE [dbo].[WStocks]
ADD CONSTRAINT [FK_WStock_ToTable_1]
    FOREIGN KEY ([Warehouse_id])
    REFERENCES [dbo].[Warehouses]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WStock_ToTable_1'
CREATE INDEX [IX_FK_WStock_ToTable_1]
ON [dbo].[WStocks]
    ([Warehouse_id]);
GO

-- Creating foreign key on [WBook_id] in table 'WStocks'
ALTER TABLE [dbo].[WStocks]
ADD CONSTRAINT [FK_WStock_ToTable]
    FOREIGN KEY ([WBook_id])
    REFERENCES [dbo].[WBooks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_WStock_ToTable'
CREATE INDEX [IX_FK_WStock_ToTable]
ON [dbo].[WStocks]
    ([WBook_id]);
GO

-- Creating foreign key on [Books_Id] in table 'BookWStock'
ALTER TABLE [dbo].[BookWStock]
ADD CONSTRAINT [FK_BookWStock_Book]
    FOREIGN KEY ([Books_Id])
    REFERENCES [dbo].[Books]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [WStocks_Id] in table 'BookWStock'
ALTER TABLE [dbo].[BookWStock]
ADD CONSTRAINT [FK_BookWStock_WStock]
    FOREIGN KEY ([WStocks_Id])
    REFERENCES [dbo].[WStocks]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_BookWStock_WStock'
CREATE INDEX [IX_FK_BookWStock_WStock]
ON [dbo].[BookWStock]
    ([WStocks_Id]);
GO

SET IDENTITY_INSERT [dbo].[Warehouses] ON
INSERT INTO [dbo].[Warehouses] ([Id], [Name]) VALUES (1, N'Warehouse 3')
INSERT INTO [dbo].[Warehouses] ([Id], [Name]) VALUES (2, N'Warehouse 7')
INSERT INTO [dbo].[Warehouses] ([Id], [Name]) VALUES (3, N'Warehouse 3')
INSERT INTO [dbo].[Warehouses] ([Id], [Name]) VALUES (4, N'Warehouse 4')
INSERT INTO [dbo].[Warehouses] ([Id], [Name]) VALUES (5, N'Warehouse 2')
INSERT INTO [dbo].[Warehouses] ([Id], [Name]) VALUES (6, N'Warehouse 6')
INSERT INTO [dbo].[Warehouses] ([Id], [Name]) VALUES (7, N'Warehouse 8')
INSERT INTO [dbo].[Warehouses] ([Id], [Name]) VALUES (8, N'Neutral Bay')
INSERT INTO [dbo].[Warehouses] ([Id], [Name]) VALUES (9, N'Warehouse 9')
INSERT INTO [dbo].[Warehouses] ([Id], [Name]) VALUES (10, N'Warehouse 5')
INSERT INTO [dbo].[Warehouses] ([Id], [Name]) VALUES (11, N'Warehouse 1')
SET IDENTITY_INSERT [dbo].[Warehouses] OFF

SET IDENTITY_INSERT [dbo].[WBooks] ON
INSERT INTO [dbo].[WBooks] ([Id], [Title], [Author], [Genre]) VALUES (1, N'Pride and Prejudice', N'Jane Austen', N'Fiction')
INSERT INTO [dbo].[WBooks] ([Id], [Title], [Author], [Genre]) VALUES (2, N'Title 3', N'Author 3', N'Genre 3')
INSERT INTO [dbo].[WBooks] ([Id], [Title], [Author], [Genre]) VALUES (3, N'Title 5', N'Author 5', N'Genre 5')
INSERT INTO [dbo].[WBooks] ([Id], [Title], [Author], [Genre]) VALUES (4, N'Title 9', N'Author 9', N'Genre 9')
INSERT INTO [dbo].[WBooks] ([Id], [Title], [Author], [Genre]) VALUES (5, N'Grape Expectations', N'Charles Dickens', N'Fiction')
INSERT INTO [dbo].[WBooks] ([Id], [Title], [Author], [Genre]) VALUES (6, N'Title 1', N'Author 1', N'Genre 1')
INSERT INTO [dbo].[WBooks] ([Id], [Title], [Author], [Genre]) VALUES (7, N'Title 2', N'Author 2', N'Genre 2')
INSERT INTO [dbo].[WBooks] ([Id], [Title], [Author], [Genre]) VALUES (8, N'Title 4', N'Author 4', N'Genre 4')
INSERT INTO [dbo].[WBooks] ([Id], [Title], [Author], [Genre]) VALUES (9, N'Title 7', N'Author 7', N'Genre 7')
INSERT INTO [dbo].[WBooks] ([Id], [Title], [Author], [Genre]) VALUES (10, N'Title 6', N'Author 6', N'Genre 6')
INSERT INTO [dbo].[WBooks] ([Id], [Title], [Author], [Genre]) VALUES (11, N'Title 8', N'Author 8', N'Genre 8')
SET IDENTITY_INSERT [dbo].[WBooks] OFF

SET IDENTITY_INSERT [dbo].[WStocks] ON
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (1, 17, 1, 2)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (2, 17, 2, 2)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (3, 10, 3, 2)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (4, 5, 4, 2)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (7, 5, 5, 2)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (8, 5, 6, 2)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (9, 3, 7, 2)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (10, 5, 8, 2)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (11, 6, 9, 2)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (12, 6, 10, 2)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (13, 5, 11, 2)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (16, 2, 1, 1)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (17, 3, 2, 1)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (18, 3, 3, 1)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (19, 3, 4, 1)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (20, 3, 5, 1)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (21, 10, 1, 3)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (22, 10, 2, 3)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (23, 10, 3, 3)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (24, 10, 6, 3)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (25, 10, 7, 3)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (26, 10, 8, 3)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (27, 10, 9, 3)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (28, 10, 10, 3)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (29, 10, 11, 3)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (30, 2, 1, 4)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (31, 2, 2, 4)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (32, 2, 3, 4)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (33, 2, 4, 4)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (34, 2, 5, 4)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (35, 2, 10, 4)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (36, 2, 11, 4)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (37, 3, 6, 5)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (38, 3, 7, 5)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (39, 3, 8, 5)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (40, 3, 9, 5)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (41, 4, 10, 5)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (42, 4, 11, 5)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (43, 10, 1, 6)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (44, 10, 2, 6)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (45, 10, 3, 6)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (46, 10, 4, 6)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (47, 10, 5, 6)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (48, 10, 6, 6)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (49, 10, 7, 6)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (50, 10, 8, 6)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (51, 10, 9, 6)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (52, 10, 10, 6)
INSERT INTO [dbo].[WStocks] ([Id], [quantity], [WBook_id], [Warehouse_id]) VALUES (53, 10, 11, 6)
SET IDENTITY_INSERT [dbo].[WStocks] OFF

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------