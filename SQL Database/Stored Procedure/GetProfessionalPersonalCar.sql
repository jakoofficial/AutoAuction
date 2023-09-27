CREATE PROCEDURE GetProfessionalPersonalCar @ID int
AS
SELECT Vehicle.vehicleId, 
ProfessionalPersonalCar.loadCapacity,
ProfessionalPersonalCar.hasSafetyBar
FROM Vehicle 
INNER JOIN PersonalCar on Vehicle.vehicleId = PersonalCar.vehicleId
INNER JOIN ProfessionalPersonalCar on PersonalCar.personalCarId = ProfessionalPersonalCar.personalCarId
WHERE Vehicle.vehicleId = @ID
