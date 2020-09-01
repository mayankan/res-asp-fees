USE [master]
GO
/****** Object:  Database [RAINBOW]    Script Date: 02-09-2016 12:13:04 PM ******/
CREATE DATABASE [RAINBOW]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'RAINBOW', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\RAINBOW.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'RAINBOW_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.MSSQLSERVER\MSSQL\DATA\RAINBOW_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [RAINBOW] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [RAINBOW].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [RAINBOW] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [RAINBOW] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [RAINBOW] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [RAINBOW] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [RAINBOW] SET ARITHABORT OFF 
GO
ALTER DATABASE [RAINBOW] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [RAINBOW] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [RAINBOW] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [RAINBOW] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [RAINBOW] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [RAINBOW] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [RAINBOW] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [RAINBOW] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [RAINBOW] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [RAINBOW] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [RAINBOW] SET  DISABLE_BROKER 
GO
ALTER DATABASE [RAINBOW] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [RAINBOW] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [RAINBOW] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [RAINBOW] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [RAINBOW] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [RAINBOW] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [RAINBOW] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [RAINBOW] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [RAINBOW] SET  MULTI_USER 
GO
ALTER DATABASE [RAINBOW] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [RAINBOW] SET DB_CHAINING OFF 
GO
ALTER DATABASE [RAINBOW] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [RAINBOW] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [RAINBOW]
GO
/****** Object:  Table [dbo].[Left Fees]    Script Date: 02-09-2016 12:13:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Left Fees](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[Month] [date] NOT NULL,
	[AdmissionFee] [bigint] NOT NULL,
	[TutionFee] [bigint] NOT NULL,
	[RefreshmentAccFee] [bigint] NOT NULL,
	[LabFee] [bigint] NOT NULL,
	[ProjectFee] [bigint] NOT NULL,
	[AnnualCharges] [bigint] NOT NULL,
	[AdminCharges] [bigint] NOT NULL,
	[SmartClassCharges] [bigint] NOT NULL,
	[ComputerFeeYearly] [bigint] NOT NULL,
	[ComputerFeeMonthly] [bigint] NOT NULL,
	[DevelopmentChargesYearly] [bigint] NOT NULL,
	[TotalFee] [bigint] NOT NULL,
	[DateCreated] [date] NOT NULL,
	[DateModified] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Left Fees] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Payment Details]    Script Date: 02-09-2016 12:13:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Payment Details](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[StudentId] [int] NOT NULL,
	[ModeOfPayment] [varchar](max) NOT NULL,
	[LeftFeesAmount] [bigint] NOT NULL,
	[LeftfeesId] [int] NOT NULL,
	[DateCreated] [date] NOT NULL,
	[DateModified] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
	[isPaid] [bit] NOT NULL,
 CONSTRAINT [PK_Payment Details] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Student]    Script Date: 02-09-2016 12:13:04 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Student](
	[Id] [int] IDENTITY(0,1) NOT NULL,
	[AdmissionNo] [int] NOT NULL,
	[Name] [varchar](max) NOT NULL,
	[FathersName] [varchar](max) NOT NULL,
	[MothersName] [varchar](max) NOT NULL,
	[Class] [varchar](50) NOT NULL,
	[Section] [varchar](50) NOT NULL,
	[MobileNumber] [bigint] NOT NULL,
	[Gender] [bit] NOT NULL,
	[Address] [varchar](max) NOT NULL,
	[DateCreated] [date] NOT NULL,
	[DateModified] [date] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_User_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[Payment Details]  WITH CHECK ADD  CONSTRAINT [FK_Payment Details_Left Fees] FOREIGN KEY([LeftfeesId])
REFERENCES [dbo].[Left Fees] ([Id])
GO
ALTER TABLE [dbo].[Payment Details] CHECK CONSTRAINT [FK_Payment Details_Left Fees]
GO
ALTER TABLE [dbo].[Payment Details]  WITH CHECK ADD  CONSTRAINT [FK_Payment Details_User] FOREIGN KEY([StudentId])
REFERENCES [dbo].[Student] ([Id])
GO
ALTER TABLE [dbo].[Payment Details] CHECK CONSTRAINT [FK_Payment Details_User]
GO
USE [master]
GO
ALTER DATABASE [RAINBOW] SET  READ_WRITE 
GO
