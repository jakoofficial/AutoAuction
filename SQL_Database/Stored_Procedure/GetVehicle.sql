CREATE PROCEDURE GetVehicle @id int
AS 
SELECT vehicleId, name, registrationNumber, year, newPrice, hasTowbar, engineSize, kmPerLiter, fuelType, driversLicense
FROM Vehicle
WHERE vehicleId = @id