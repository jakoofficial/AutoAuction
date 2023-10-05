CREATE PROCEDURE AddHeavyVehicle
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

--HeavyVehicle
@vehicleDimensionHeight float(53),
@vehicleDimensionWeight float(53),
@vehicleDimensionLenght float(53),
@heavyVehicleId int output

AS
--DECLARE @vehicleId INT
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
		EXEC AddVehicle @name, @km, @registrationNumber, @year, @newPrice, @hasTowbar,
		@engineSize, @kmPerLiter, @fuelType, @driversLicense, 
		@vehicleId = @vehicleId output;

		INSERT INTO HeavyVehicle(vehicleId, 
		vehicleDimensionHeight, vehicleDimensionWeight, vehicleDimensionLenght)
		values(@vehicleId, 
		@vehicleDimensionHeight, @vehicleDimensionWeight, @vehicleDimensionLenght)

		set @heavyVehicleId = SCOPE_IDENTITY()
		COMMIT;
    END TRY
    BEGIN CATCH
        -- We can log Error here
        ROLLBACK;
        THROW;
    END CATCH
END;
