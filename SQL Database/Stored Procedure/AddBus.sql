CREATE PROCEDURE AddBus
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
@heavyVehicleId int output,

--bus
@numberOfSeats int,
@numberOfSleepingSpaces int,
@hasToilet bit,
@busId int output

AS
--DECLARE @vehicleId INT
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
		EXEC AddHeavyVehicle @name, @km, @registrationNumber, @year, @newPrice, @hasTowbar,
		@engineSize, @kmPerLiter, @fuelType, @driversLicense, 0,
		@vehicleDimensionHeight, @vehicleDimensionWeight, @vehicleDimensionLenght,
		@heavyVehicleId = @heavyVehicleId OUTPUT;

		INSERT INTO Bus(heavyVehicleId, numberOfSeats, numberOfSleepingSpaces, hasToilet)
		values(@heavyVehicleId, @numberOfSeats, @numberOfSleepingSpaces, @hasToilet)

		set @busId = SCOPE_IDENTITY()
		COMMIT;
    END TRY
    BEGIN CATCH
        -- We can log Error here
        ROLLBACK;
        THROW;
    END CATCH
END;
