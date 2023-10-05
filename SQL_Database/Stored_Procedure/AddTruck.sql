CREATE PROCEDURE AddTruck
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

--Truck
@loadCapacity float(53),
@truckId int output

AS
--DECLARE @vehicleId INT
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
		EXEC AddHeavyVehicle @name, @km, @registrationNumber, @year, @newPrice, @hasTowbar,
		@engineSize, @kmPerLiter, @fuelType, @driversLicense, 0,
		@vehicleDimensionHeight, @vehicleDimensionWeight, @vehicleDimensionLenght,
		@heavyVehicleId = @heavyVehicleId OUTPUT;

		INSERT INTO Truck(heavyVehicleId, loadCapacity)
		values(@heavyVehicleId, @loadCapacity)

		set @truckId = SCOPE_IDENTITY()
		COMMIT;
    END TRY
    BEGIN CATCH
        -- We can log Error here
        ROLLBACK;
        THROW;
    END CATCH
END;
