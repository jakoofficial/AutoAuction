CREATE PROCEDURE GetTruck @truckID int
AS
SELECT * FROM Vehicle 
INNER JOIN HeavyVehicle on Vehicle.vehicleId = HeavyVehicle.vehicleId
INNER JOIN Truck on HeavyVehicle.heavyVehicleId = Truck.heavyVehicleId
WHERE Vehicle.vehicleId = @truckID
