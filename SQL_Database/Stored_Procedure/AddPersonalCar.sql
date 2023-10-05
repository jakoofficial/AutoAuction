CREATE PROCEDURE AddPersonalCar
--@vehicle
@Name varchar(max), 
@km float(53),
@registrationNumber varchar(max),
@year int,
@newPrice decimal(18, 2),
@hasTowbar bit,
@engineSize float(53),
@kmPerLiter float(53),
@fuelType int,
@driversLicense int,
@vehicleId INT output,

--personalcar
@numberOfSeats int,
@trunkDimensionsHeight float(53),
@trunkDimensionsWidth float(53),
@trunkDimensionsDepth float(53),
@personalCarId int output

AS
--DECLARE @vehicleId INT
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
		EXEC AddVehicle @name, @km, @registrationNumber, @year, @newPrice, @hasTowbar,
		@engineSize, @kmPerLiter, @fuelType, @driversLicense, 
		@vehicleId = @vehicleId output;

		INSERT INTO PersonalCar(vehicleId, numberOfSeats, trunkDimensionsHeight,
		trunkDimensionsWidth, trunkDimensionsDepth)
		values(@vehicleId, @numberOfSeats,
		@trunkDimensionsHeight, @trunkDimensionsWidth, @trunkDimensionsDepth)

		set @personalCarId = SCOPE_IDENTITY()
		COMMIT;
    END TRY
    BEGIN CATCH
        -- We can log Error here
        ROLLBACK;
        THROW;
    END CATCH
END;
