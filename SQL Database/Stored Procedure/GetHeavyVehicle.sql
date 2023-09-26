CREATE PROCEDURE GetHeavyVehicle @ID int
AS
SELECT Vehicle.vehicleId, 
HeavyVehicle.vehicleDimensionHeight, 
HeavyVehicle.vehicleDimensionLenght,
HeavyVehicle.vehicleDimensionWeight
FROM Vehicle 
INNER JOIN HeavyVehicle on Vehicle.vehicleId = HeavyVehicle.vehicleId
WHERE Vehicle.vehicleId = @ID
