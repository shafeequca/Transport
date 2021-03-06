USE [Transport]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDriverRegistration]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE GetDriverRegistration
END
GO

CREATE PROCEDURE [dbo].[GetDriverRegistration]
	@Search VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT * FROM tblDriver 
	WHERE	DriverName LIKE '%'+@Search+'%'
	
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DriverRegistration]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE DriverRegistration
END
GO
CREATE PROCEDURE [dbo].[DriverRegistration]
	@DriverId INT=0,
	@DriverName VARCHAR(50)='',
	@DriverAddress VARCHAR(500)='',
	@DriverNumber VARCHAR(50)='',
	@IsDelete INT=0
AS
BEGIN
	SET NOCOUNT ON;

    IF @DriverId=0
		INSERT INTO tblDriver(DriverName,DriverAddress,DriverNumber)
		VALUES (@DriverName,@DriverAddress,@DriverNumber)
	ELSE
	BEGIN
		IF @IsDelete=0 
		BEGIN
			UPDATE	tblDriver
			SET		DriverName=@DriverName
					,DriverAddress=@DriverAddress
					,DriverNumber=@DriverNumber
			WHERE	DriverId=@DriverId
		END
		ELSE
		BEGIN
			DELETE FROM tblDriver
			WHERE	DriverId=@DriverId
		END
	END
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PartyRegistration]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE PartyRegistration
END
GO
CREATE PROCEDURE [dbo].[PartyRegistration]
	@PartyId INT=0,
	@PartyName VARCHAR(50)='',
	@PartyAddress VARCHAR(500)='',
	@PartyNumber VARCHAR(50)='',
	@Location VARCHAR(100)='',
	@Kilometer DECIMAL(18,2)=0,
	@DriverBata20 DECIMAL(18,2)=0,
	@CleanerBata20 DECIMAL(18,2)=0,
	@DriverBata40 DECIMAL(18,2)=0,
	@CleanerBata40 DECIMAL(18,2)=0,
	@Rate20 DECIMAL(18,2)=0,
	@Rate40 DECIMAL(18,2)=0,
	@IsDelete INT=0
AS
BEGIN
	SET NOCOUNT ON;

    IF @PartyId=0
		INSERT INTO tblParty
					(PartyName
					,PartyAddress
					,PartyNumber
					,Location
					,Kilometer
					,DriverBata20
					,CleanerBata20
					,DriverBata40
					,CleanerBata40
					,Rate20
					,Rate40)
		VALUES (@PartyName
				,@PartyAddress
				,@PartyNumber
				,@Location
				,@Kilometer
				,@DriverBata20
				,@CleanerBata20
				,@DriverBata40
				,@CleanerBata40
				,@Rate20
				,@Rate40)
	ELSE
	BEGIN
		IF @IsDelete=0 
		BEGIN
			UPDATE	tblParty
			SET		PartyName=@PartyName
					,PartyAddress=@PartyAddress
					,PartyNumber=@PartyNumber
					,Location=@Location
					,Kilometer=@Kilometer
					,DriverBata20=@DriverBata20
					,CleanerBata20=@CleanerBata20
					,DriverBata40=@DriverBata40
					,CleanerBata40=@CleanerBata40
					,Rate20=@Rate20
					,Rate40=@Rate40
			WHERE	PartyId=@PartyId
		END
		ELSE
		BEGIN
			DELETE FROM tblParty
			WHERE	PartyId=@PartyId
		END
	END
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetPartyRegistration]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE GetPartyRegistration
END
GO
CREATE PROCEDURE [dbo].[GetPartyRegistration]
	@Search VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT [PartyId]
		,[Location] + '-' + [PartyName]
		,[PartyName]
		,[PartyAddress]
		,[PartyNumber]
		,[Location]
		,[Kilometer]
		,[DriverBata20]
		,[DriverBata40]
		,[CleanerBata20]
		,[CleanerBata40]
		,[Rate20]
		,[Rate40] 
	FROM tblParty 
	WHERE	PartyName LIKE '%'+@Search+'%'
			OR Location LIKE '%'+@Search+'%'
	ORDER BY 3
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTripRegistration]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE GetTripRegistration
END
GO
CREATE PROCEDURE [dbo].[GetTripRegistration]
	@Search VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT [TripId]
		,p.[PartyName] + '-' + p.[Location]
		,[VehType]
		,[Address]
		,[ContactNumber]
		,[VehicleId]
		,p.partyId
		,[TripRate]
		,[DriverId]
		,[CleanerId]
		,[DriverBata]
		,[CleanerBata]
		,[Advance]
		,[Expense]
		,convert(varchar, t.TripDate, 105) TripDate
	FROM tblParty p INNER JOIN tblTrip t
	ON	p.PartyId=t.PartyId
	WHERE	p.PartyName LIKE '%'+@Search+'%'
			OR p.Location LIKE '%'+@Search+'%'
	ORDER BY [TripDate],tripid desc
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDriverBata]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE GetDriverBata
END
GO
CREATE PROCEDURE [dbo].[GetDriverBata]
	@DriverId INT,
	@DateFrom DATETIME,
	@DateTo	DATETIME
AS
BEGIN
	SET NOCOUNT ON;

    SELECT Location+'-'+PartyName AS Destination,convert(varchar, t.TripDate, 105) TripDate,T.DriverBata,T.CleanerBata,T.Advance,T.Expense,(((T.DriverBata+T.CleanerBata)-T.Advance)+T.Expense) AS [Balance Bata] FROM tblTrip T INNER JOIN tblVehicle v
ON T.VehicleId=v.VehId
INNER JOIN tblParty p
on t.PartyId=p.PartyId
 WHERE DriverID=@driverID AND TripDate BETWEEN DATEADD(dd, DATEDIFF(dd, 0, @DateFrom), 0) 
								AND DATEADD(dd, DATEDIFF(dd, 0, @DateTo), 0)
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetVehicleKm]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE GetVehicleKm
END
GO
CREATE PROCEDURE [dbo].[GetVehicleKm]
	@VehId INT,
	@DateFrom DATETIME,
	@DateTo	DATETIME
AS
BEGIN
	SET NOCOUNT ON;
	SET @DateTo=DATEADD(dd,1,@DateTo)
    SELECT	SUM(p.Kilometer) AS Km
	FROM	tblTrip t INNER JOIN tblParty p
			on t.PartyId=p.partyid
	WHERE	t.VehicleId=@VehID 
			AND TripDate BETWEEN DATEADD(dd, DATEDIFF(dd, 0, @DateFrom), 0) 
								AND DATEADD(dd, DATEDIFF(dd, 0, @DateTo), 0)
END

GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDriverBataSummary]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE GetDriverBataSummary
END
GO
CREATE PROCEDURE [dbo].[GetDriverBataSummary]
	@DriverId INT,
	@DateFrom DATETIME,
	@DateTo	DATETIME
AS
BEGIN
	SET NOCOUNT ON;
	SET @DateTo=DATEADD(dd,1,@DateTo)
    SELECT SUM(T.DriverBata) DriverBata,SUM(T.CleanerBata) CleanerBata,SUM(T.Advance) Advance,SUM(T.Expense) Expense,SUM((((T.DriverBata+T.CleanerBata)-T.Advance)+T.Expense)) AS Balance FROM tblTrip T INNER JOIN tblVehicle v
	ON T.VehicleId=v.VehId
	INNER JOIN tblParty p
	on t.PartyId=p.PartyId
	 WHERE DriverID=@driverID AND TripDate BETWEEN DATEADD(dd, DATEDIFF(dd, 0, @DateFrom), 0) 
									AND DATEADD(dd, DATEDIFF(dd, 0, @DateTo), 0)
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetProfitAndLoss]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE GetProfitAndLoss
END
GO

CREATE PROCEDURE [dbo].[GetProfitAndLoss]
	@VehId INT,
	@DateFrom DATETIME,
	@DateTo	DATETIME
AS
BEGIN
	SET NOCOUNT ON;
	SET @DateTo=DATEADD(dd,1,@DateTo)
    SELECT	Location+'-'+PartyName AS Destination
			,convert(varchar, t.TripDate, 105) Date
			,ISNULL(T.TripRate,0) AS Rate
			,ISNULL(T.DriverBata,0)+ISNULL(T.CleanerBata,0) AS Bata
			,ISNULL(T.Expense,0) As Expense
			,ISNULL(T.TripRate,0)-ISNULL(T.DriverBata,0)-ISNULL(T.CleanerBata,0)-ISNULL(T.Expense,0) AS Balance
	FROM	tblTrip T INNER JOIN tblVehicle v
				ON T.VehicleId=v.VehId
			INNER JOIN tblParty p
				on t.PartyId=p.PartyId
	WHERE	T.VehicleId=@VehID 
			AND TripDate BETWEEN DATEADD(dd, DATEDIFF(dd, 0, @DateFrom), 0) 
								AND DATEADD(dd, DATEDIFF(dd, 0, @DateTo), 0)
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetTripSummary]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE GetTripSummary
END
GO
CREATE PROCEDURE [dbo].[GetTripSummary]
	@VehId INT,
	@DateFrom DATETIME,
	@DateTo	DATETIME
AS
BEGIN
	SET NOCOUNT ON;
	SET @DateTo=DATEADD(dd,1,@DateTo)
    SELECT	SUM(ISNULL(T.TripRate,0)) AS TripRate
			,SUM(ISNULL(T.DriverBata,0)+ISNULL(T.CleanerBata,0)) As Bata
			,SUM(ISNULL(T.Expense,0)) AS Expense
			,SUM(ISNULL(T.TripRate,0))-SUM(ISNULL(T.DriverBata,0)+ISNULL(T.CleanerBata,0))-SUM(ISNULL(T.Expense,0)) AS Balance
	FROM	tblTrip T INNER JOIN tblVehicle v
				ON T.VehicleId=v.VehId
	WHERE	T.VehicleId=@VehID 
			AND TripDate BETWEEN DATEADD(dd, DATEDIFF(dd, 0, @DateFrom), 0) 
								AND DATEADD(dd, DATEDIFF(dd, 0, @DateTo), 0)
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[TripRegistration]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE TripRegistration
END
GO
CREATE PROCEDURE [dbo].[TripRegistration]
	@TripId INT=0,
	@PartyId INT=0,
	@TripDate DATETIME=null,
	@Vehtype VARCHAR(10)='',
@Address VARCHAR(500)='',
@ContactNumber VARCHAR(50)='',
	@VehicleId INT=0,
	@DriverId INT=0,
	@CleanerId INT=0,
	@TripRate DECIMAL(18,2)=0,
	@DriverBata DECIMAL(18,2)=0,
	@CleanerBata DECIMAL(18,2)=0,
	@Advance DECIMAL(18,2)=0,
	@Expense DECIMAL(18,2)=0,
	@IsDelete INT=0
AS
BEGIN
	SET NOCOUNT ON;

    IF @TripId=0
		INSERT INTO tblTrip
					(PartyId,
					TripDate,
					Vehtype,
					VehicleId,
					DriverId,
					CleanerId,
					TripRate,
					DriverBata,
					CleanerBata,
					Advance,
					Expense,
					Address,
					ContactNumber
)
		VALUES		(@PartyId,
					@TripDate,
					@Vehtype,
					@VehicleId,
					@DriverId,
					@CleanerId,
					@TripRate,
					@DriverBata,
					@CleanerBata,
					@Advance,
					@Expense,
					@Address,
					@ContactNumber
)
	ELSE
	BEGIN
		IF @IsDelete=0 
		BEGIN
			UPDATE	tblTrip
			SET		PartyId=@PartyId,
					TripDate=@TripDate,
					Vehtype=@Vehtype,
					VehicleId=@VehicleId,
					DriverId=@DriverId,
					CleanerId=@CleanerId,
					TripRate=@TripRate,
					DriverBata=@DriverBata,
					CleanerBata=@CleanerBata,
					Advance=@Advance,
					Expense=@Expense,
					ContactNumber=@ContactNumber,
					Address=@Address

			WHERE	TripId=@TripId
		END
		ELSE
		BEGIN
			DELETE FROM tblTrip
			WHERE	TripId=@TripId
		END
	END
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[DieselEntry]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE DieselEntry
END
GO

CREATE PROCEDURE [dbo].[DieselEntry]
	@id INT=0,
	@Date DATETIME=null,
	@VehId INT=0,
	@Qty DECIMAL(18,2)=0,
	@Rate DECIMAL(18,2)=0,
	@Total DECIMAL(18,2)=0,
	@IsDelete INT=0
AS
BEGIN
	SET NOCOUNT ON;

    IF @id=0
		INSERT INTO tblDiesel(Date,VehId,Qty,Rate,Total)
		VALUES (@Date,@VehId,@Qty,@Rate,@Total)
	ELSE
	BEGIN
		IF @IsDelete=0 
		BEGIN
			UPDATE	tblDiesel
			SET		VehId=@VehId
					,Qty=@Qty
					,Rate=@Rate
					,Total=@Total
			WHERE	id=@id
		END
		ELSE
		BEGIN
			DELETE FROM tblDiesel
			WHERE	id=@id
		END
	END
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDieselEntry]') AND type in (N'P', N'PC'))

BEGIN
	DROP PROCEDURE GetDieselEntry
END
GO
CREATE PROCEDURE [dbo].[GetDieselEntry]
	@Search VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT id
		,convert(varchar, d.Date, 105) Date
		,V.VehicleNumber
		,V.vehId
		,Qty
		,Rate
		,Total
	FROM tblDiesel d INNER JOIN tblVehicle v
			ON d.VehId=v.VehId
	WHERE	v.VehicleNumber LIKE '%'+@Search+'%'
	ORDER BY 1 DESC
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetDieselSummary]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE GetDieselSummary
END
GO
CREATE PROCEDURE [dbo].[GetDieselSummary]
	@VehId INT,
	@DateFrom DATETIME,
	@DateTo	DATETIME
AS
BEGIN
	SET NOCOUNT ON;
	SET @DateTo=DATEADD(dd,1,@DateTo)
    SELECT	SUM(Qty) AS Qty
			,SUM(Total) AS Total
	FROM	tblDiesel
	WHERE	VehId=@VehID 
			AND Date BETWEEN DATEADD(dd, DATEDIFF(dd, 0, @DateFrom), 0) 
								AND DATEADD(dd, DATEDIFF(dd, 0, @DateTo), 0)
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ExpenseEntry]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE ExpenseEntry
END
GO
CREATE PROCEDURE [dbo].[ExpenseEntry]
	@id INT=0,
	@Date DATETIME=null,
	@VehId INT=0,
	@Expense VARCHAR(500)='',
	@Amount DECIMAL(18,2)=0,
	@IsDelete INT=0
AS
BEGIN
	SET NOCOUNT ON;

    IF @id=0
		INSERT INTO tblExpense(Date,VehId,Expense,Amount)
		VALUES (@Date,@VehId,@Expense,@Amount)
	ELSE
	BEGIN
		IF @IsDelete=0 
		BEGIN
			UPDATE	tblExpense
			SET		VehId=@VehId
					,Expense=@Expense
					,Amount=@Amount
			WHERE	id=@id
		END
		ELSE
		BEGIN
			DELETE FROM tblExpense
			WHERE	id=@id
		END
	END
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetExpenseEntry]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE GetExpenseEntry
END
GO
CREATE PROCEDURE [dbo].[GetExpenseEntry]
	@Search VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT id
		,convert(varchar, d.Date, 105) Date
		,V.VehicleNumber
		,V.vehId
		,Expense
		,Amount
	FROM tblExpense d INNER JOIN tblVehicle v
			ON d.VehId=v.VehId
	WHERE	v.VehicleNumber LIKE '%'+@Search+'%'
	ORDER BY 1 DESC
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VehicleRegistration]') AND type in (N'P', N'PC'))

BEGIN
	DROP PROCEDURE VehicleRegistration
END
GO
CREATE PROCEDURE [dbo].[VehicleRegistration]
	@VehId INT=0,
	@VehicleName VARCHAR(50)='',
	@VehicleNumber VARCHAR(50)='',
	@VehicleType VARCHAR(10)='',
	@IsDelete INT=0
AS
BEGIN
	SET NOCOUNT ON;

    IF @VehId=0
		INSERT INTO tblVehicle(VehicleName,VehicleNumber,VehicleType)
		VALUES (@VehicleName,@VehicleNumber,@VehicleType)
	ELSE
	BEGIN
		IF @IsDelete=0 
		BEGIN
			UPDATE	tblVehicle
			SET		VehicleName=@VehicleName
					,VehicleNumber=@VehicleNumber
					,VehicleType=@VehicleType
			WHERE	VehId=@VehId
		END
		ELSE
		BEGIN
			DELETE FROM tblVehicle
			WHERE	VehId=@VehId
		END
	END
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetVehicleRegistration]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE GetVehicleRegistration
END
GO
CREATE PROCEDURE [dbo].[GetVehicleRegistration]
	@Search VARCHAR(50)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT * FROM tblVehicle 
	WHERE	VehicleName LIKE '%'+@Search+'%'
			OR VehicleNumber LIKE '%'+@Search+'%'
	
END
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetVehicleByType]') AND type in (N'P', N'PC'))
BEGIN
	DROP PROCEDURE GetVehicleByType
END
GO
CREATE PROCEDURE [dbo].[GetVehicleByType]
	@Type VARCHAR(10)
AS
BEGIN
	SET NOCOUNT ON;

    SELECT VehId,VehicleNumber+' ('+VehicleName+')' as Vehicle FROM tblVehicle 
	WHERE	VehicleType=@Type
	
END
