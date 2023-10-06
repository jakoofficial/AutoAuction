CREATE PROCEDURE GetPrivatePersonalCar @ID int
AS
SELECT Vehicle.vehicleId, 
PrivatePersonalCar.hasIsofixFittings
FROM Vehicle 
INNER JOIN PersonalCar on Vehicle.vehicleId = PersonalCar.vehicleId
INNER JOIN PrivatePersonalCar on PersonalCar.personalCarId = PrivatePersonalCar.personalCarId
WHERE Vehicle.vehicleId = @ID
