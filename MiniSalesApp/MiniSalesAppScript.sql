USE [master]
GO
/****** Object:  Database [MiniSalesApp]    Script Date: 11/09/2022 02:13:28 ص ******/
CREATE DATABASE [MiniSalesApp]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MiniSalesApp', FILENAME = N'D:\database\MiniSalesApp.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MiniSalesApp_log', FILENAME = N'D:\database\MiniSalesApp_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 COLLATE SQL_Latin1_General_CP1_CI_AS
GO
ALTER DATABASE [MiniSalesApp] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MiniSalesApp].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MiniSalesApp] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MiniSalesApp] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MiniSalesApp] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MiniSalesApp] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MiniSalesApp] SET ARITHABORT OFF 
GO
ALTER DATABASE [MiniSalesApp] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MiniSalesApp] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MiniSalesApp] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MiniSalesApp] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MiniSalesApp] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MiniSalesApp] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MiniSalesApp] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MiniSalesApp] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MiniSalesApp] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MiniSalesApp] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MiniSalesApp] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MiniSalesApp] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MiniSalesApp] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MiniSalesApp] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MiniSalesApp] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MiniSalesApp] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MiniSalesApp] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MiniSalesApp] SET RECOVERY FULL 
GO
ALTER DATABASE [MiniSalesApp] SET  MULTI_USER 
GO
ALTER DATABASE [MiniSalesApp] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MiniSalesApp] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MiniSalesApp] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MiniSalesApp] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [MiniSalesApp] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'MiniSalesApp', N'ON'
GO
/****** Object:  Login [NT SERVICE\Winmgmt]    Script Date: 11/09/2022 02:13:28 ص ******/
CREATE LOGIN [NT SERVICE\Winmgmt] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLWriter]    Script Date: 11/09/2022 02:13:28 ص ******/
CREATE LOGIN [NT SERVICE\SQLWriter] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT SERVICE\SQLSERVERAGENT]    Script Date: 11/09/2022 02:13:28 ص ******/
CREATE LOGIN [NT SERVICE\SQLSERVERAGENT] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT Service\MSSQLSERVER]    Script Date: 11/09/2022 02:13:28 ص ******/
CREATE LOGIN [NT Service\MSSQLSERVER] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [NT AUTHORITY\SYSTEM]    Script Date: 11/09/2022 02:13:28 ص ******/
CREATE LOGIN [NT AUTHORITY\SYSTEM] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/****** Object:  Login [MOHAMMED-KHALIL\ttgkp]    Script Date: 11/09/2022 02:13:28 ص ******/
CREATE LOGIN [MOHAMMED-KHALIL\ttgkp] FROM WINDOWS WITH DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english]
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyTsqlExecutionLogin##]    Script Date: 11/09/2022 02:13:28 ص ******/
CREATE LOGIN [##MS_PolicyTsqlExecutionLogin##] WITH PASSWORD=N'ÜT3!¬(dõ¡àÅâVdï?Ði#uá¾', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyTsqlExecutionLogin##] DISABLE
GO
/* For security reasons the login is created disabled and with a random password. */
/****** Object:  Login [##MS_PolicyEventProcessingLogin##]    Script Date: 11/09/2022 02:13:28 ص ******/
CREATE LOGIN [##MS_PolicyEventProcessingLogin##] WITH PASSWORD=N'©/X=~£Ç
úm²YCzÊâaÄãøòåq=-Á', DEFAULT_DATABASE=[master], DEFAULT_LANGUAGE=[us_english], CHECK_EXPIRATION=OFF, CHECK_POLICY=ON
GO
ALTER LOGIN [##MS_PolicyEventProcessingLogin##] DISABLE
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\Winmgmt]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLWriter]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT SERVICE\SQLSERVERAGENT]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [NT Service\MSSQLSERVER]
GO
ALTER SERVER ROLE [sysadmin] ADD MEMBER [MOHAMMED-KHALIL\ttgkp]
GO
USE [MiniSalesApp]
GO
/****** Object:  Table [dbo].[Bank]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bank](
	[BankId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Serial] [int] NOT NULL,
	[StartAmount] [decimal](12, 4) NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Bank] PRIMARY KEY CLUSTERED 
(
	[BankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BankPayment]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankPayment](
	[BankPaymentId] [int] IDENTITY(1,1) NOT NULL,
	[Serial] [int] NOT NULL,
	[IsSupplierPayment] [bit] NOT NULL,
	[SupplierId] [int] NULL,
	[Description] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount] [decimal](12, 4) NOT NULL,
	[BankId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_BankPayment] PRIMARY KEY CLUSTERED 
(
	[BankPaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BankRecivement]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BankRecivement](
	[BankRecivementId] [int] IDENTITY(1,1) NOT NULL,
	[Serial] [int] NOT NULL,
	[IsCustomerRecivement] [bit] NOT NULL,
	[CustomerId] [int] NULL,
	[Description] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount] [decimal](12, 4) NOT NULL,
	[BankId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_BankRecivement] PRIMARY KEY CLUSTERED 
(
	[BankRecivementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Bill]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Bill](
	[BillId] [int] IDENTITY(1,1) NOT NULL,
	[SupplierId] [int] NOT NULL,
	[Serial] [int] NOT NULL,
	[Total] [decimal](12, 4) NOT NULL,
	[Discount] [decimal](12, 4) NOT NULL,
	[TotalAfterDiscount] [decimal](12, 4) NOT NULL,
	[Discription] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Bill] PRIMARY KEY CLUSTERED 
(
	[BillId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[BillDetail]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BillDetail](
	[BillDetailId] [int] IDENTITY(1,1) NOT NULL,
	[BillId] [int] NOT NULL,
	[MaterialId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[PurchasePrice] [decimal](12, 4) NOT NULL,
 CONSTRAINT [PK_BillDetail] PRIMARY KEY CLUSTERED 
(
	[BillDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Customer]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[CustomerId] [int] IDENTITY(1,1) NOT NULL,
	[Serial] [int] NOT NULL,
	[Name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Phone] [nvarchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Balance] [decimal](12, 4) NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[CustomerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Invoice]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice](
	[InvoiceId] [int] IDENTITY(1,1) NOT NULL,
	[Serial] [int] NOT NULL,
	[CustomerId] [int] NOT NULL,
	[Total] [decimal](12, 4) NOT NULL,
	[Discount] [decimal](12, 4) NOT NULL,
	[TotalAfterDiscount] [decimal](12, 4) NOT NULL,
	[Discription] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_Invoice] PRIMARY KEY CLUSTERED 
(
	[InvoiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[InvoiceDetail]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvoiceDetail](
	[InvoiceDetailId] [int] IDENTITY(1,1) NOT NULL,
	[InvoiceId] [int] NOT NULL,
	[MaterialId] [int] NOT NULL,
	[Quantity] [int] NOT NULL,
	[SellPrice] [decimal](12, 4) NOT NULL,
 CONSTRAINT [PK_InvoiceDetail] PRIMARY KEY CLUSTERED 
(
	[InvoiceDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Material]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Material](
	[MaterialId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Code] [int] NOT NULL,
	[SellPrice] [decimal](12, 4) NOT NULL,
	[PurchasePrice] [decimal](12, 4) NOT NULL,
 CONSTRAINT [PK_Material] PRIMARY KEY CLUSTERED 
(
	[MaterialId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreDaily]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreDaily](
	[StoreDailyId] [int] IDENTITY(1,1) NOT NULL,
	[Serial] [int] NOT NULL,
	[StartAmount] [decimal](12, 4) NOT NULL,
	[EndAmount] [decimal](12, 4) NOT NULL,
	[StartDate] [datetime] NOT NULL,
 CONSTRAINT [PK_StoreDaily] PRIMARY KEY CLUSTERED 
(
	[StoreDailyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StorePayment]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StorePayment](
	[StorePaymentId] [int] IDENTITY(1,1) NOT NULL,
	[Serial] [int] NOT NULL,
	[IsSupplierPayment] [bit] NOT NULL,
	[SupplierId] [int] NULL,
	[Description] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount] [decimal](12, 4) NOT NULL,
	[StoreDailyId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_StorePayment] PRIMARY KEY CLUSTERED 
(
	[StorePaymentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[StoreRecivement]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[StoreRecivement](
	[StoreRecivementId] [int] IDENTITY(1,1) NOT NULL,
	[Serial] [int] NOT NULL,
	[IsCustomerRecivement] [bit] NOT NULL,
	[CustomerId] [int] NULL,
	[Description] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Amount] [decimal](12, 4) NOT NULL,
	[StoreDailyId] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
 CONSTRAINT [PK_StoreRecivement] PRIMARY KEY CLUSTERED 
(
	[StoreRecivementId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Supplier]    Script Date: 11/09/2022 02:13:28 ص ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Supplier](
	[SupplierId] [int] IDENTITY(1,1) NOT NULL,
	[Serial] [int] NOT NULL,
	[Name] [nvarchar](200) COLLATE SQL_Latin1_General_CP1_CI_AS NOT NULL,
	[Phone] [nchar](50) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Address] [nvarchar](500) COLLATE SQL_Latin1_General_CP1_CI_AS NULL,
	[Balance] [decimal](12, 4) NOT NULL,
 CONSTRAINT [PK_Supplier] PRIMARY KEY CLUSTERED 
(
	[SupplierId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
ALTER TABLE [dbo].[BankPayment]  WITH CHECK ADD  CONSTRAINT [FK_BankPayment_Bank] FOREIGN KEY([BankId])
REFERENCES [dbo].[Bank] ([BankId])
GO
ALTER TABLE [dbo].[BankPayment] CHECK CONSTRAINT [FK_BankPayment_Bank]
GO
ALTER TABLE [dbo].[BankPayment]  WITH CHECK ADD  CONSTRAINT [FK_BankPayment_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[BankPayment] CHECK CONSTRAINT [FK_BankPayment_Supplier]
GO
ALTER TABLE [dbo].[BankRecivement]  WITH CHECK ADD  CONSTRAINT [FK_BankRecivement_Bank] FOREIGN KEY([BankId])
REFERENCES [dbo].[Bank] ([BankId])
GO
ALTER TABLE [dbo].[BankRecivement] CHECK CONSTRAINT [FK_BankRecivement_Bank]
GO
ALTER TABLE [dbo].[BankRecivement]  WITH CHECK ADD  CONSTRAINT [FK_BankRecivement_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[BankRecivement] CHECK CONSTRAINT [FK_BankRecivement_Customer]
GO
ALTER TABLE [dbo].[Bill]  WITH CHECK ADD  CONSTRAINT [FK_Bill_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[Bill] CHECK CONSTRAINT [FK_Bill_Supplier]
GO
ALTER TABLE [dbo].[BillDetail]  WITH CHECK ADD  CONSTRAINT [FK_BillDetail_Bill] FOREIGN KEY([BillId])
REFERENCES [dbo].[Bill] ([BillId])
GO
ALTER TABLE [dbo].[BillDetail] CHECK CONSTRAINT [FK_BillDetail_Bill]
GO
ALTER TABLE [dbo].[Invoice]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[Invoice] CHECK CONSTRAINT [FK_Invoice_Customer]
GO
ALTER TABLE [dbo].[InvoiceDetail]  WITH CHECK ADD  CONSTRAINT [FK_InvoiceDetail_Invoice] FOREIGN KEY([InvoiceId])
REFERENCES [dbo].[Invoice] ([InvoiceId])
GO
ALTER TABLE [dbo].[InvoiceDetail] CHECK CONSTRAINT [FK_InvoiceDetail_Invoice]
GO
ALTER TABLE [dbo].[StorePayment]  WITH CHECK ADD  CONSTRAINT [FK_StorePayment_StoreDaily] FOREIGN KEY([StoreDailyId])
REFERENCES [dbo].[StoreDaily] ([StoreDailyId])
GO
ALTER TABLE [dbo].[StorePayment] CHECK CONSTRAINT [FK_StorePayment_StoreDaily]
GO
ALTER TABLE [dbo].[StorePayment]  WITH CHECK ADD  CONSTRAINT [FK_StorePayment_Supplier] FOREIGN KEY([SupplierId])
REFERENCES [dbo].[Supplier] ([SupplierId])
GO
ALTER TABLE [dbo].[StorePayment] CHECK CONSTRAINT [FK_StorePayment_Supplier]
GO
ALTER TABLE [dbo].[StoreRecivement]  WITH CHECK ADD  CONSTRAINT [FK_StoreRecivement_Customer] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([CustomerId])
GO
ALTER TABLE [dbo].[StoreRecivement] CHECK CONSTRAINT [FK_StoreRecivement_Customer]
GO
ALTER TABLE [dbo].[StoreRecivement]  WITH CHECK ADD  CONSTRAINT [FK_StoreRecivement_StoreDaily] FOREIGN KEY([StoreDailyId])
REFERENCES [dbo].[StoreDaily] ([StoreDailyId])
GO
ALTER TABLE [dbo].[StoreRecivement] CHECK CONSTRAINT [FK_StoreRecivement_StoreDaily]
GO
USE [master]
GO
ALTER DATABASE [MiniSalesApp] SET  READ_WRITE 
GO
