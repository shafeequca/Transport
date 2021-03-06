USE [Transport]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDriver]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblDriver](
	[DriverId] [int] IDENTITY(1,1) NOT NULL,
	[DriverName] [varchar](50) NULL,
	[DriverAddress] [varchar](500) NULL,
	[DriverNumber] [varchar](50) NULL,
 CONSTRAINT [PK_tblDriver] PRIMARY KEY CLUSTERED 
(
	[DriverId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblParty]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblParty](
	[PartyId] [int] IDENTITY(1,1) NOT NULL,
	[PartyName] [varchar](50) NULL,
	[PartyAddress] [varchar](500) NULL,
	[PartyNumber] [varchar](50) NULL,
	[Location] [varchar](100) NULL,
	[Kilometer] [decimal](18, 2) NULL,
	[DriverBata20] [decimal](18, 2) NULL,
	[DriverBata40] [decimal](18, 2) NULL,
	[CleanerBata20] [decimal](18, 2) NULL,
	[CleanerBata40] [decimal](18, 2) NULL,
	[Rate20] [decimal](18, 2) NULL,
	[Rate40] [decimal](18, 2) NULL,
 CONSTRAINT [PK__tblParty__07020F21] PRIMARY KEY CLUSTERED 
(
	[PartyId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblTrip]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblTrip](
	[TripId] [int] IDENTITY(1,1) NOT NULL,
	[PartyId] [int] NULL,
	[TripDate] [datetime] NULL,
	[VehType] [varchar](50) NULL,
	[VehicleId] [int] NULL,
	[DriverID] [int] NULL,
	[CleanerID] [int] NULL,
	[TripRate] [decimal](18, 2) NULL,
	[DriverBata] [decimal](18, 2) NULL,
	[CleanerBata] [decimal](18, 2) NULL,
	[Advance] [decimal](18, 2) NULL,
	[Expense] [decimal](18, 2) NULL,
	[Address] [varchar](500) NULL,
	[ContactNumber] [varchar](50) NULL,
 CONSTRAINT [PK_tblTrip] PRIMARY KEY CLUSTERED 
(
	[TripId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblDiesel]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblDiesel](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[VehId] [int] NULL,
	[Qty] [decimal](18, 2) NULL,
	[Rate] [decimal](18, 2) NULL,
	[Total] [decimal](18, 2) NULL,
 CONSTRAINT [PK_tblDiesel] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblExpense]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblExpense](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[Date] [datetime] NULL,
	[VehId] [int] NULL,
	[Expense] [varchar](500) NULL,
	[Amount] [decimal](18, 2) NULL,
 CONSTRAINT [PK_tblExpense] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVehicle]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVehicle](
	[VehId] [int] IDENTITY(1,1) NOT NULL,
	[VehicleName] [varchar](50) NULL,
	[VehicleNumber] [varchar](50) NULL,
	[VehicleType] [varchar](50) NULL,
 CONSTRAINT [PK_tblVehicle] PRIMARY KEY CLUSTERED 
(
	[VehId] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
