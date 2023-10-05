CREATE PROCEDURE GetBus @BusID int
AS
SELECT Bus.hasToilet, Bus.numberOfSeats, Bus.numberOfSleepingSpaces FROM Vehicle 
INNER JOIN HeavyVehicle on Vehicle.vehicleId = HeavyVehicle.vehicleId
INNER JOIN Bus on HeavyVehicle.heavyVehicleId = Bus.heavyVehicleId
WHERE Vehicle.vehicleId = @BusID
