CREATE PROCEDURE AddProfessionalPersonalCar
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
@personalCarId int output,

--PrivatePersonalCar
@loadCapacity float(53),
@hasSafetyBar bit,
@professionalPersonalCarId int output

AS
BEGIN
	BEGIN TRY
		BEGIN TRANSACTION;
		EXEC AddPersonalCar @name, @km, @registrationNumber, @year, @newPrice, @hasTowbar,
		@engineSize, @kmPerLiter, @fuelType, @driversLicense, 0,
		@numberOfSeats, @trunkDimensionsHeight, @trunkDimensionsWidth, @trunkDimensionsDepth,
		@personalCarId = @personalCarId OUTPUT;

		INSERT INTO ProfessionalPersonalCar(personalCarId, loadCapacity, hasSafetyBar)
		values(@personalCarId, @loadCapacity, @hasSafetyBar)

		set @professionalPersonalCarId = SCOPE_IDENTITY()
		COMMIT;
    END TRY
    BEGIN CATCH
        -- We can log Error here
        ROLLBACK;
        THROW;
    END CATCH
END;