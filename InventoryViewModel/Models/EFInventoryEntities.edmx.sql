
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/22/2017 23:15:25
-- Generated from EDMX file: F:\HomeProject\Inven_Management\InventoryViewModel\Models\EFInventoryEntities.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [InventoryManagement];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Employee]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Employee];
GO
IF OBJECT_ID(N'[dbo].[EnumCountry]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnumCountry];
GO
IF OBJECT_ID(N'[dbo].[EnumDistric]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnumDistric];
GO
IF OBJECT_ID(N'[dbo].[EnumDivision]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnumDivision];
GO
IF OBJECT_ID(N'[dbo].[Organizations]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Organizations];
GO
IF OBJECT_ID(N'[dbo].[ProductBrands]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductBrands];
GO
IF OBJECT_ID(N'[dbo].[ProductCategorys]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductCategorys];
GO
IF OBJECT_ID(N'[dbo].[ProductColor]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductColor];
GO
IF OBJECT_ID(N'[dbo].[ProductDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductDetails];
GO
IF OBJECT_ID(N'[dbo].[Products]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Products];
GO
IF OBJECT_ID(N'[dbo].[ProductSizes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductSizes];
GO
IF OBJECT_ID(N'[dbo].[ProductTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ProductTypes];
GO
IF OBJECT_ID(N'[dbo].[Purchases]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Purchases];
GO
IF OBJECT_ID(N'[dbo].[PurcheaseDetails]', 'U') IS NOT NULL
    DROP TABLE [dbo].[PurcheaseDetails];
GO
IF OBJECT_ID(N'[dbo].[Sales]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Sales];
GO
IF OBJECT_ID(N'[dbo].[Suppliers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Suppliers];
GO
IF OBJECT_ID(N'[dbo].[UOM]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UOM];
GO
IF OBJECT_ID(N'[dbo].[UserRoles]', 'U') IS NOT NULL
    DROP TABLE [dbo].[UserRoles];
GO
IF OBJECT_ID(N'[dbo].[Users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Users];
GO
IF OBJECT_ID(N'[dbo].[ZoneOrAreas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ZoneOrAreas];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(20)  NULL,
    [Name] nvarchar(200)  NULL,
    [CountryId] int  NULL,
    [DivisionId] int  NULL,
    [DistrictId] int  NULL,
    [Mobile] nvarchar(20)  NULL,
    [PermanentAddress] nvarchar(500)  NULL,
    [PresentAddress] nvarchar(500)  NULL,
    [PABX] nvarchar(100)  NULL,
    [Email] nvarchar(100)  NULL,
    [FAX] nvarchar(100)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [CreatedAt] nvarchar(50)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(50)  NULL,
    [LastUpdateAt] nvarchar(50)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- Creating table 'Employees'
CREATE TABLE [dbo].[Employees] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BranchId] int  NOT NULL,
    [Code] nvarchar(20)  NULL,
    [Name] nvarchar(50)  NULL,
    [Salutation_E] nvarchar(200)  NULL,
    [MiddleName] nvarchar(200)  NULL,
    [LastName] nvarchar(200)  NULL,
    [CountryId] int  NOT NULL,
    [DivisionId] int  NOT NULL,
    [DistrictId] int  NOT NULL,
    [Mobile] nvarchar(20)  NULL,
    [PermanentAddress] nvarchar(500)  NULL,
    [PresentAddress] nvarchar(500)  NULL,
    [PABX] nvarchar(100)  NULL,
    [Email] nvarchar(100)  NULL,
    [FAX] nvarchar(100)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(20)  NULL,
    [CreatedAt] nvarchar(50)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(50)  NULL,
    [LastUpdateAt] nvarchar(50)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- Creating table 'EnumCountries'
CREATE TABLE [dbo].[EnumCountries] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NOT NULL,
    [IsArchive] bit  NOT NULL,
    [CreatedBy] nvarchar(50)  NOT NULL,
    [CreatedAt] nvarchar(50)  NOT NULL,
    [CreatedFrom] nvarchar(50)  NOT NULL,
    [LastUpdateBy] nvarchar(50)  NOT NULL,
    [LastUpdateAt] nvarchar(50)  NOT NULL,
    [LastUpdateFrom] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'EnumDistrics'
CREATE TABLE [dbo].[EnumDistrics] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [CountryId] int  NOT NULL,
    [DivisionId] int  NOT NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NOT NULL,
    [IsArchive] bit  NOT NULL,
    [CreatedBy] nvarchar(50)  NOT NULL,
    [CreatedAt] nvarchar(50)  NOT NULL,
    [CreatedFrom] nvarchar(50)  NOT NULL,
    [LastUpdateBy] nvarchar(50)  NOT NULL,
    [LastUpdateAt] nvarchar(50)  NOT NULL,
    [LastUpdateFrom] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'EnumDivisions'
CREATE TABLE [dbo].[EnumDivisions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(200)  NULL,
    [CountryId] int  NOT NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [CreatedAt] nvarchar(50)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(50)  NULL,
    [LastUpdateAt] nvarchar(50)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- Creating table 'Organizations'
CREATE TABLE [dbo].[Organizations] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(20)  NULL,
    [Name] nvarchar(200)  NULL,
    [CountryId] int  NULL,
    [DivisionId] int  NULL,
    [DistrictId] int  NULL,
    [Mobile] nvarchar(20)  NULL,
    [PermanentAddress] nvarchar(500)  NULL,
    [PresentAddress] nvarchar(500)  NULL,
    [PABX] nvarchar(100)  NULL,
    [Email] nvarchar(100)  NULL,
    [FAX] nvarchar(100)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NOT NULL,
    [CreatedAt] nvarchar(50)  NOT NULL,
    [CreatedFrom] nvarchar(50)  NOT NULL,
    [LastUpdateBy] nvarchar(50)  NOT NULL,
    [LastUpdateAt] nvarchar(50)  NOT NULL,
    [LastUpdateFrom] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'ProductBrands'
CREATE TABLE [dbo].[ProductBrands] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(20)  NULL,
    [Name] nvarchar(200)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [CreatedAt] nvarchar(50)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(50)  NULL,
    [LastUpdateAt] nvarchar(50)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- Creating table 'ProductCategorys'
CREATE TABLE [dbo].[ProductCategorys] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(20)  NULL,
    [Name] nvarchar(200)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [CreatedAt] nvarchar(50)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(50)  NULL,
    [LastUpdateAt] nvarchar(50)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- Creating table 'ProductColors'
CREATE TABLE [dbo].[ProductColors] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(20)  NULL,
    [Name] nvarchar(200)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(20)  NULL,
    [CreatedAt] nvarchar(14)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(20)  NULL,
    [LastUpdateAt] nvarchar(14)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- Creating table 'ProductDetails'
CREATE TABLE [dbo].[ProductDetails] (
    [Id] int  NOT NULL,
    [ProductId] int  NULL,
    [UOMId] int  NULL,
    [ProductBrandId] int  NULL,
    [ProductCatagoriesId] int  NULL,
    [ProductColorId] int  NULL,
    [ProductSizeId] int  NULL,
    [ProductTypeId] int  NULL,
    [SupplierId] int  NULL,
    [Quantity] decimal(18,2)  NULL,
    [UnitPrice] decimal(18,2)  NULL,
    [OpenQuantity] decimal(18,2)  NULL,
    [MinimumStock] int  NULL,
    [OtherCost] decimal(18,2)  NULL,
    [Discount] decimal(18,2)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(20)  NULL,
    [CreatedAt] nvarchar(14)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(20)  NULL,
    [LastUpdateAt] nvarchar(14)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- Creating table 'Products'
CREATE TABLE [dbo].[Products] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(20)  NULL,
    [Name] nvarchar(50)  NULL,
    [UOMId] int  NULL,
    [ProductBrandId] int  NULL,
    [ProductCatagoriesId] int  NULL,
    [ProductColorId] int  NULL,
    [ProductSizeId] int  NULL,
    [ProductTypeId] int  NULL,
    [SupplierId] int  NULL,
    [MinimumStock] int  NULL,
    [OtherCost] decimal(18,2)  NULL,
    [Discount] decimal(18,2)  NULL,
    [UnitePrice] decimal(18,2)  NULL,
    [Quantity] decimal(18,2)  NULL,
    [OpeningQuantity] decimal(18,2)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(20)  NULL,
    [CreatedAt] nvarchar(14)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(20)  NULL,
    [LastUpdateAt] nvarchar(14)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- Creating table 'ProductSizes'
CREATE TABLE [dbo].[ProductSizes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(20)  NULL,
    [Name] nvarchar(200)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [CreatedAt] nvarchar(50)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(50)  NULL,
    [LastUpdateAt] nvarchar(50)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- Creating table 'ProductTypes'
CREATE TABLE [dbo].[ProductTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(20)  NULL,
    [Name] nvarchar(200)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [CreatedAt] nvarchar(50)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(50)  NULL,
    [LastUpdateAt] nvarchar(50)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- Creating table 'Purchases'
CREATE TABLE [dbo].[Purchases] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(20)  NULL,
    [InvoiecNo] nvarchar(200)  NULL,
    [ProductId] int  NULL,
    [SupplierId] int  NULL,
    [EmployeeId] int  NULL,
    [Discount] decimal(18,2)  NULL,
    [Slup] decimal(18,2)  NULL,
    [SlupPrice] decimal(18,2)  NULL,
    [UnitePrice] decimal(18,2)  NULL,
    [Quantity] int  NULL,
    [TotalPaid] decimal(18,2)  NULL,
    [TotalPrice] decimal(18,2)  NULL,
    [GrandTotal] decimal(18,2)  NULL,
    [Datetimes] nvarchar(50)  NULL,
    [OpeningQuantity] decimal(18,2)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NOT NULL,
    [CreatedAt] nvarchar(50)  NOT NULL,
    [CreatedFrom] nvarchar(50)  NOT NULL,
    [LastUpdateBy] nvarchar(50)  NOT NULL,
    [LastUpdateAt] nvarchar(50)  NOT NULL,
    [LastUpdateFrom] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'PurcheaseDetails'
CREATE TABLE [dbo].[PurcheaseDetails] (
    [Id] int  NOT NULL,
    [ProductId] int  NULL,
    [SupplierId] int  NULL,
    [EmployeeId] int  NULL,
    [Code] nvarchar(20)  NULL,
    [InvoiecNo] nvarchar(200)  NULL,
    [Discount] decimal(18,2)  NULL,
    [Slup] decimal(18,2)  NULL,
    [UnitePrice] decimal(18,2)  NULL,
    [Quantity] decimal(18,1)  NULL,
    [TotalPaid] decimal(18,2)  NULL,
    [TotalPrice] decimal(18,2)  NULL,
    [GrandTotal] decimal(18,2)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NOT NULL,
    [CreatedAt] nvarchar(50)  NOT NULL,
    [CreatedFrom] nvarchar(50)  NOT NULL,
    [LastUpdateBy] nvarchar(50)  NOT NULL,
    [LastUpdateAt] nvarchar(50)  NOT NULL,
    [LastUpdateFrom] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Sales'
CREATE TABLE [dbo].[Sales] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(20)  NULL,
    [InvoiecNo] nvarchar(200)  NULL,
    [ProductId] int  NULL,
    [CustomerId] int  NULL,
    [EmployeeId] int  NULL,
    [Discount] decimal(18,2)  NULL,
    [Slup] decimal(18,2)  NULL,
    [UnitePrice] decimal(18,2)  NULL,
    [Quantity] int  NULL,
    [TotalPaid] decimal(18,2)  NULL,
    [TotalPrice] decimal(18,2)  NULL,
    [GrandTotal] decimal(18,2)  NULL,
    [Datetimes] nvarchar(50)  NULL,
    [OpeningQuantity] decimal(18,2)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NOT NULL,
    [CreatedAt] nvarchar(50)  NOT NULL,
    [CreatedFrom] nvarchar(50)  NOT NULL,
    [LastUpdateBy] nvarchar(50)  NOT NULL,
    [LastUpdateAt] nvarchar(50)  NOT NULL,
    [LastUpdateFrom] nvarchar(50)  NOT NULL
);
GO

-- Creating table 'Suppliers'
CREATE TABLE [dbo].[Suppliers] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BranchId] int  NOT NULL,
    [Code] nvarchar(20)  NULL,
    [Name] nvarchar(50)  NULL,
    [Salutation_E] nvarchar(200)  NULL,
    [MiddleName] nvarchar(200)  NULL,
    [LastName] nvarchar(200)  NULL,
    [CountryId] int  NULL,
    [DivisionId] int  NULL,
    [DistrictId] int  NULL,
    [Mobile] nvarchar(20)  NULL,
    [PermanentAddress] nvarchar(500)  NULL,
    [PresentAddress] nvarchar(500)  NULL,
    [PABX] nvarchar(100)  NULL,
    [Email] nvarchar(100)  NULL,
    [FAX] nvarchar(100)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [CreatedAt] nvarchar(50)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(50)  NULL,
    [LastUpdateAt] nvarchar(50)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- Creating table 'UOMs'
CREATE TABLE [dbo].[UOMs] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(20)  NOT NULL,
    [Name] nvarchar(200)  NOT NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [CreatedAt] nvarchar(50)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(50)  NULL,
    [LastUpdateAt] nvarchar(50)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- Creating table 'UserRoles'
CREATE TABLE [dbo].[UserRoles] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [BranchId] int  NOT NULL,
    [UserInfoId] nvarchar(128)  NULL,
    [RoleInfoId] nvarchar(128)  NULL,
    [IsArchived] bit  NOT NULL
);
GO

-- Creating table 'Users'
CREATE TABLE [dbo].[Users] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FullName] nchar(100)  NULL,
    [UserName] nchar(100)  NULL,
    [Email] nchar(100)  NULL,
    [LogId] nchar(50)  NULL,
    [Password] nchar(50)  NULL,
    [VerificationCode] nchar(20)  NULL,
    [BranchId] int  NULL,
    [EmployeeId] nvarchar(50)  NULL,
    [IsAdmin] bit  NULL,
    [Remember] bit  NOT NULL,
    [IsActive] bit  NULL,
    [IsVerified] bit  NULL,
    [IsArchived] bit  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [CreatedAt] nvarchar(50)  NULL,
    [CreatedFrom] varchar(50)  NULL,
    [LastUpdateBy] nvarchar(50)  NULL,
    [LastUpdateAt] nvarchar(50)  NULL,
    [LastUpdateFrom] varchar(50)  NULL
);
GO

-- Creating table 'ZoneOrAreas'
CREATE TABLE [dbo].[ZoneOrAreas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Code] nvarchar(20)  NULL,
    [Name] nvarchar(200)  NULL,
    [Description] nchar(10)  NULL,
    [Remarks] nvarchar(500)  NULL,
    [IsActive] bit  NULL,
    [IsArchive] bit  NULL,
    [CreatedBy] nvarchar(50)  NULL,
    [CreatedAt] nvarchar(50)  NULL,
    [CreatedFrom] nvarchar(50)  NULL,
    [LastUpdateBy] nvarchar(50)  NULL,
    [LastUpdateAt] nvarchar(50)  NULL,
    [LastUpdateFrom] nvarchar(50)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Employees'
ALTER TABLE [dbo].[Employees]
ADD CONSTRAINT [PK_Employees]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EnumCountries'
ALTER TABLE [dbo].[EnumCountries]
ADD CONSTRAINT [PK_EnumCountries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EnumDistrics'
ALTER TABLE [dbo].[EnumDistrics]
ADD CONSTRAINT [PK_EnumDistrics]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EnumDivisions'
ALTER TABLE [dbo].[EnumDivisions]
ADD CONSTRAINT [PK_EnumDivisions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Organizations'
ALTER TABLE [dbo].[Organizations]
ADD CONSTRAINT [PK_Organizations]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductBrands'
ALTER TABLE [dbo].[ProductBrands]
ADD CONSTRAINT [PK_ProductBrands]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductCategorys'
ALTER TABLE [dbo].[ProductCategorys]
ADD CONSTRAINT [PK_ProductCategorys]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductColors'
ALTER TABLE [dbo].[ProductColors]
ADD CONSTRAINT [PK_ProductColors]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductDetails'
ALTER TABLE [dbo].[ProductDetails]
ADD CONSTRAINT [PK_ProductDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Products'
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [PK_Products]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductSizes'
ALTER TABLE [dbo].[ProductSizes]
ADD CONSTRAINT [PK_ProductSizes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ProductTypes'
ALTER TABLE [dbo].[ProductTypes]
ADD CONSTRAINT [PK_ProductTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Purchases'
ALTER TABLE [dbo].[Purchases]
ADD CONSTRAINT [PK_Purchases]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'PurcheaseDetails'
ALTER TABLE [dbo].[PurcheaseDetails]
ADD CONSTRAINT [PK_PurcheaseDetails]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Sales'
ALTER TABLE [dbo].[Sales]
ADD CONSTRAINT [PK_Sales]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Suppliers'
ALTER TABLE [dbo].[Suppliers]
ADD CONSTRAINT [PK_Suppliers]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UOMs'
ALTER TABLE [dbo].[UOMs]
ADD CONSTRAINT [PK_UOMs]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'UserRoles'
ALTER TABLE [dbo].[UserRoles]
ADD CONSTRAINT [PK_UserRoles]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Users'
ALTER TABLE [dbo].[Users]
ADD CONSTRAINT [PK_Users]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ZoneOrAreas'
ALTER TABLE [dbo].[ZoneOrAreas]
ADD CONSTRAINT [PK_ZoneOrAreas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------