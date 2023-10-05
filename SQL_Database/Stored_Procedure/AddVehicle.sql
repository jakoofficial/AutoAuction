CREATE PROCEDURE AddVehicle 
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

@vehicleId INT OUTPUT

AS
INSERT INTO Vehicle(name, km, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, driversLicense)
VALUES(@Name, @km, @registrationNumber, @year, @newPrice, @hasTowbar, @engineSize, @kmPerLiter, @fuelType, @driversLicense)

SET @vehicleId = SCOPE_IDENTITY() 
SELECT @vehicleId AS vehicleId