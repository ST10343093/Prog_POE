USE [master]
GO
/****** Object:  Database [AgriEnergyConnectDB]    Script Date: 5/13/2025 12:21:46 PM ******/
CREATE DATABASE [AgriEnergyConnectDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'AgriEnergyConnectDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\AgriEnergyConnectDB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'AgriEnergyConnectDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\AgriEnergyConnectDB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [AgriEnergyConnectDB] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AgriEnergyConnectDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AgriEnergyConnectDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET  ENABLE_BROKER 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET  MULTI_USER 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [AgriEnergyConnectDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [AgriEnergyConnectDB] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [AgriEnergyConnectDB] SET QUERY_STORE = ON
GO
ALTER DATABASE [AgriEnergyConnectDB] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [AgriEnergyConnectDB]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 5/13/2025 12:21:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Products]    Script Date: 5/13/2025 12:21:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Products](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Category] [nvarchar](50) NOT NULL,
	[ProductionDate] [datetime2](7) NOT NULL,
	[Description] [nvarchar](500) NULL,
	[Price] [decimal](18, 2) NULL,
	[IsOrganic] [bit] NOT NULL,
	[ImageUrl] [nvarchar](200) NULL,
	[FarmerId] [int] NOT NULL,
 CONSTRAINT [PK_Products] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 5/13/2025 12:21:46 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Role] [nvarchar](20) NOT NULL,
	[FarmName] [nvarchar](100) NULL,
	[FarmLocation] [nvarchar](200) NULL,
	[FarmDescription] [nvarchar](500) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20250430182724_InitialCreate', N'9.0.4')
GO
SET IDENTITY_INSERT [dbo].[Products] ON 

INSERT [dbo].[Products] ([ProductId], [Name], [Category], [ProductionDate], [Description], [Price], [IsOrganic], [ImageUrl], [FarmerId]) VALUES (1, N'Organic Tomatoes', N'Vegetables', CAST(N'2025-03-15T00:00:00.0000000' AS DateTime2), N'Fresh organic tomatoes grown without pesticides', CAST(25.99 AS Decimal(18, 2)), 1, N'/images/products/tomatoes.jpg', 2)
INSERT [dbo].[Products] ([ProductId], [Name], [Category], [ProductionDate], [Description], [Price], [IsOrganic], [ImageUrl], [FarmerId]) VALUES (2, N'Free-Range Eggs', N'Dairy', CAST(N'2025-04-01T00:00:00.0000000' AS DateTime2), N'Farm fresh eggs from free-range chickens', CAST(45.50 AS Decimal(18, 2)), 1, N'/images/products/eggs.jpg', 2)
INSERT [dbo].[Products] ([ProductId], [Name], [Category], [ProductionDate], [Description], [Price], [IsOrganic], [ImageUrl], [FarmerId]) VALUES (3, N'Grass-Fed Beef', N'Meat', CAST(N'2025-03-20T00:00:00.0000000' AS DateTime2), N'Premium grass-fed beef from ethically raised cattle', CAST(180.00 AS Decimal(18, 2)), 1, N'/images/products/beef.jpg', 3)
INSERT [dbo].[Products] ([ProductId], [Name], [Category], [ProductionDate], [Description], [Price], [IsOrganic], [ImageUrl], [FarmerId]) VALUES (4, N'Honey', N'Sweeteners', CAST(N'2025-02-10T00:00:00.0000000' AS DateTime2), N'Raw, unfiltered honey from local beehives', CAST(85.75 AS Decimal(18, 2)), 1, N'/images/products/honey.jpg', 3)
INSERT [dbo].[Products] ([ProductId], [Name], [Category], [ProductionDate], [Description], [Price], [IsOrganic], [ImageUrl], [FarmerId]) VALUES (5, N'Organic Spinach', N'Vegetables', CAST(N'2025-04-05T00:00:00.0000000' AS DateTime2), N'Fresh organic spinach leaves', CAST(18.99 AS Decimal(18, 2)), 1, N'/images/products/spinach.jpg', 2)
INSERT [dbo].[Products] ([ProductId], [Name], [Category], [ProductionDate], [Description], [Price], [IsOrganic], [ImageUrl], [FarmerId]) VALUES (8, N'Farm Honey', N'Honey', CAST(N'2025-05-07T00:00:00.0000000' AS DateTime2), N'Fresh 100% Pure Honey', CAST(330.00 AS Decimal(18, 2)), 0, N'https://m.media-amazon.com/images/I/5167vl9vloL.jpg', 4)
INSERT [dbo].[Products] ([ProductId], [Name], [Category], [ProductionDate], [Description], [Price], [IsOrganic], [ImageUrl], [FarmerId]) VALUES (9, N'Chicken', N'Meat', CAST(N'2025-05-07T00:00:00.0000000' AS DateTime2), N'Fresh Farm Chicken', CAST(99.00 AS Decimal(18, 2)), 0, N'https://tillmansmeats.com/cdn/shop/products/103---Fresh-Cut-Chicken_1200x1200.jpg?v=1558567692', 5)
INSERT [dbo].[Products] ([ProductId], [Name], [Category], [ProductionDate], [Description], [Price], [IsOrganic], [ImageUrl], [FarmerId]) VALUES (10, N'Large Eggs ', N'Eggs', CAST(N'2025-05-10T00:00:00.0000000' AS DateTime2), N'Fresh Large Chicken Eggs', CAST(80.00 AS Decimal(18, 2)), 1, N'https://cdn.britannica.com/94/151894-050-F72A5317/Brown-eggs.jpg', 4)
INSERT [dbo].[Products] ([ProductId], [Name], [Category], [ProductionDate], [Description], [Price], [IsOrganic], [ImageUrl], [FarmerId]) VALUES (11, N'Beef ', N'Meat', CAST(N'2025-05-10T00:00:00.0000000' AS DateTime2), N'Fresh Cut Beef', CAST(200.00 AS Decimal(18, 2)), 0, N'https://www.allrecipes.com/thmb/Qch2UpqrMAdaLPi6WwJxPHzz6BY=/1500x0/filters:no_upscale():max_bytes(150000):strip_icc()/sous-vide-brisket-mfs-193-4x3-1-24930daf16854a9091eaff1503aac157.jpg', 6)
INSERT [dbo].[Products] ([ProductId], [Name], [Category], [ProductionDate], [Description], [Price], [IsOrganic], [ImageUrl], [FarmerId]) VALUES (13, N'Fresh Bucket Fruits', N'Fruits', CAST(N'2025-05-11T00:00:00.0000000' AS DateTime2), N'Fresh Bucket of mixed fruits', CAST(40.00 AS Decimal(18, 2)), 1, N'https://www.leonsfruitshop.co.uk/wp-content/uploads/2013/07/IMG-20220505-WA0007.jpg', 7)
SET IDENTITY_INSERT [dbo].[Products] OFF
GO
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([UserId], [Username], [Password], [FirstName], [LastName], [Email], [Role], [FarmName], [FarmLocation], [FarmDescription]) VALUES (1, N'employee1', N'qe/q/eDziqo7smNHMNmuNw==:7Qhy1FkzoCOsTMqT+y1XTCjuL1MbFYnN94hVCa3adjQ=', N'John', N'Smith', N'john.smith@agrienergy.com', N'Employee', NULL, NULL, NULL)
INSERT [dbo].[Users] ([UserId], [Username], [Password], [FirstName], [LastName], [Email], [Role], [FarmName], [FarmLocation], [FarmDescription]) VALUES (2, N'farmer1', N'9PdpFcnk1DqHaMRmyy45Aw==:whNkvqpi+wAOdvbPuJmWhZhUpZXeiclVYKgvuQQPHUM=', N'Sarah', N'Johnson', N'sarah@greenfarms.co.za', N'Farmer', N'Green Valley Farms', N'Western Cape', N'Sustainable organic farm specializing in vegetables and fruits.')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [FirstName], [LastName], [Email], [Role], [FarmName], [FarmLocation], [FarmDescription]) VALUES (3, N'farmer2', N'password123', N'David', N'Nkosi', N'david@sunrisefarm.co.za', N'Farmer', N'Sunrise Eco Farm', N'KwaZulu-Natal', N'Family-owned farm focusing on sustainable livestock and dairy production.')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [FirstName], [LastName], [Email], [Role], [FarmName], [FarmLocation], [FarmDescription]) VALUES (4, N'admin', N'5KkYEvrbXbydpbyMxDzTbQ==:5xv6LyIRCBcmGB7H43sfc/0YZbOU8U/LMzfn2y4gYT0=', N'Arshad', N'Bhula', N'arshad@gmail.com', N'Farmer', N'Arshad''s Farm', N'KwaZulu-Natal', N'We Provide only the best and most fresh farm produce ever.')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [FirstName], [LastName], [Email], [Role], [FarmName], [FarmLocation], [FarmDescription]) VALUES (5, N'sylo', N'i+hXn0KTV27leWR+r07GnA==:/fbxVuD8j4BcKoKUZmf0REBY6w9Fqp4M1hskXsGub2g=', N'jordan', N'gardiner', N'jordan@gmail.com', N'Farmer', N'Jordan''s Farm', N'Ladysmith', N'Protein Farm')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [FirstName], [LastName], [Email], [Role], [FarmName], [FarmLocation], [FarmDescription]) VALUES (6, N'cam', N'uqyWs3QWEP0ZVuvxRAMNvA==:7UuJgmt1Lr/snfbk0MJ0CdzDibJ4AijKk4WGDdIatuw=', N'Cameron', N'Chetty', N'cam@gmail.com', N'Farmer', N'Cam''s Farm', N'Western Cape', N'Beef Farm')
INSERT [dbo].[Users] ([UserId], [Username], [Password], [FirstName], [LastName], [Email], [Role], [FarmName], [FarmLocation], [FarmDescription]) VALUES (7, N'john', N'X+0NXKiPGSdawCZwL2GoaQ==:9hbySNXjl+RIeaEoXxll92+tgcWQqkljOh9zO1+ofwo=', N'john', N'doe', N'johndoe@gmail.com', N'Farmer', N'John''s Farm', N'Western Cape', N'We at John''s sell all kinds of fresh fruits, vegetables and meat.')
SET IDENTITY_INSERT [dbo].[Users] OFF
GO
/****** Object:  Index [IX_Products_FarmerId]    Script Date: 5/13/2025 12:21:46 PM ******/
CREATE NONCLUSTERED INDEX [IX_Products_FarmerId] ON [dbo].[Products]
(
	[FarmerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Products]  WITH CHECK ADD  CONSTRAINT [FK_Products_Users_FarmerId] FOREIGN KEY([FarmerId])
REFERENCES [dbo].[Users] ([UserId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Products] CHECK CONSTRAINT [FK_Products_Users_FarmerId]
GO
USE [master]
GO
ALTER DATABASE [AgriEnergyConnectDB] SET  READ_WRITE 
GO
