CREATE PROCEDURE GetPersonalCar @ID int
AS
SELECT Vehicle.vehicleId, 
PersonalCar.numberOfSeats,
PersonalCar.trunkDimensionsHeight,
PersonalCar.trunkDimensionsWidth,
PersonalCar.trunkDimensionsDepth
FROM Vehicle 
INNER JOIN PersonalCar on Vehicle.vehicleId = PersonalCar.vehicleId
WHERE Vehicle.vehicleId = @ID
