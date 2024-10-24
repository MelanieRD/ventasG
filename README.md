USE [master]
GO
/****** Object:  Database [ventasBDD]    Script Date: 24/10/2024 14:24:58 ******/
CREATE DATABASE [ventasBDD]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ventasBDD', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ventasBDD.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ventasBDD_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\ventasBDD_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [ventasBDD] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ventasBDD].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ventasBDD] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ventasBDD] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ventasBDD] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ventasBDD] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ventasBDD] SET ARITHABORT OFF 
GO
ALTER DATABASE [ventasBDD] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ventasBDD] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ventasBDD] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ventasBDD] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ventasBDD] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ventasBDD] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ventasBDD] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ventasBDD] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ventasBDD] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ventasBDD] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ventasBDD] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ventasBDD] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ventasBDD] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ventasBDD] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ventasBDD] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ventasBDD] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ventasBDD] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ventasBDD] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [ventasBDD] SET  MULTI_USER 
GO
ALTER DATABASE [ventasBDD] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ventasBDD] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ventasBDD] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ventasBDD] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ventasBDD] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ventasBDD] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ventasBDD] SET QUERY_STORE = ON
GO
ALTER DATABASE [ventasBDD] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [ventasBDD]
GO
/****** Object:  Table [dbo].[Company_TB]    Script Date: 24/10/2024 14:24:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Company_TB](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[description] [varchar](200) NULL,
 CONSTRAINT [PK_Company_TB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Employee_TB]    Script Date: 24/10/2024 14:24:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_TB](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fullName] [varchar](150) NULL,
	[CompanyId] [int] NULL,
 CONSTRAINT [PK_Employee_TB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Invoice_TB]    Script Date: 24/10/2024 14:24:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Invoice_TB](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Orderid] [int] NOT NULL,
	[state] [varchar](10) NULL,
	[delivery_date] [date] NULL,
 CONSTRAINT [PK_Invoice_TB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order_TB]    Script Date: 24/10/2024 14:24:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order_TB](
	[Orderid] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](150) NULL,
	[EmployeeId] [int] NULL,
	[state] [varchar](10) NULL,
	[TotalValue] [int] NULL,
	[id_OrderDetail] [int] NULL,
 CONSTRAINT [PK_Order_TB] PRIMARY KEY CLUSTERED 
(
	[Orderid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[OrderDetails_TB]    Script Date: 24/10/2024 14:24:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[OrderDetails_TB](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Productid] [int] NULL,
	[orderid] [int] NULL,
 CONSTRAINT [PK_OrderDetail_TB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Product_TB]    Script Date: 24/10/2024 14:24:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Product_TB](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](100) NULL,
	[price] [int] NULL,
	[Companyid] [int] NULL,
 CONSTRAINT [PK_Product_TB] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[User_TB]    Script Date: 24/10/2024 14:24:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User_TB](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NOT NULL,
	[PasswordHash] [varchar](250) NOT NULL,
 CONSTRAINT [PK_User_TB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employee_TB]  WITH CHECK ADD  CONSTRAINT [FK_Employee_TB_Company_TB] FOREIGN KEY([CompanyId])
REFERENCES [dbo].[Company_TB] ([id])
GO
ALTER TABLE [dbo].[Employee_TB] CHECK CONSTRAINT [FK_Employee_TB_Company_TB]
GO
ALTER TABLE [dbo].[Invoice_TB]  WITH CHECK ADD  CONSTRAINT [FK_Invoice_TB_Order_TB] FOREIGN KEY([Orderid])
REFERENCES [dbo].[Order_TB] ([Orderid])
GO
ALTER TABLE [dbo].[Invoice_TB] CHECK CONSTRAINT [FK_Invoice_TB_Order_TB]
GO
ALTER TABLE [dbo].[Order_TB]  WITH CHECK ADD  CONSTRAINT [FK_Order_TB_Employee_TB1] FOREIGN KEY([EmployeeId])
REFERENCES [dbo].[Employee_TB] ([id])
GO
ALTER TABLE [dbo].[Order_TB] CHECK CONSTRAINT [FK_Order_TB_Employee_TB1]
GO
ALTER TABLE [dbo].[OrderDetails_TB]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_TB_Order_TB] FOREIGN KEY([orderid])
REFERENCES [dbo].[Order_TB] ([Orderid])
GO
ALTER TABLE [dbo].[OrderDetails_TB] CHECK CONSTRAINT [FK_OrderDetail_TB_Order_TB]
GO
ALTER TABLE [dbo].[OrderDetails_TB]  WITH CHECK ADD  CONSTRAINT [FK_OrderDetail_TB_Product_TB] FOREIGN KEY([Productid])
REFERENCES [dbo].[Product_TB] ([id])
GO
ALTER TABLE [dbo].[OrderDetails_TB] CHECK CONSTRAINT [FK_OrderDetail_TB_Product_TB]
GO
ALTER TABLE [dbo].[Product_TB]  WITH CHECK ADD  CONSTRAINT [FK_Product_TB_Company_TB] FOREIGN KEY([Companyid])
REFERENCES [dbo].[Company_TB] ([id])
GO
ALTER TABLE [dbo].[Product_TB] CHECK CONSTRAINT [FK_Product_TB_Company_TB]
GO
USE [master]
GO
ALTER DATABASE [ventasBDD] SET  READ_WRITE 
GO
