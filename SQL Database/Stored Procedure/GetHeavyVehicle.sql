CREATE PROCEDURE GetHeavyVehicle @ID int
AS
SELECT * FROM Vehicle 
INNER JOIN HeavyVehicle on Vehicle.vehicleId = HeavyVehicle.vehicleId
WHERE Vehicle.vehicleId = @ID
