USE AutoAuction
GO
CREATE TABLE Vehicle(
vehicleId INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
name VARCHAR(max) NOT NULL,
km FLOAT NOT NULL,
registrationNumber VARCHAR(max) NOT NULL,
year INT NOT NULL,
newPrice DECIMAL NOT NULL,
hasTowbar BIT NOT NULL,
engineSize FLOAT NOT NULL,
kmPerLiter FLOAT NOT NULL,
fuelType INT NOT NULL,
driversLicense INT NOT NULL
);

USE AutoAuction
GO
CREATE TABLE PersonalCar (
    personalCarId INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    vehicleId INT NOT NULL,
    numberOfSeats INT NOT NULL,
    trunkDimensionsHeight FLOAT NOT NULL,
    trunkDimensionsWidth FLOAT NOT NULL,
    trunkDimensionsDepth FLOAT NOT NULL,
    FOREIGN KEY (vehicleId) REFERENCES Vehicle (vehicleId) ON DELETE CASCADE
);

USE AutoAuction
GO
CREATE TABLE ProfessionalPersonalCar (
    professionalPersonalCarId INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    personalCarId INT NOT NULL,
    hasSafetyBar INT NOT NULL,
    loadCapacity FLOAT NOT NULL,
    FOREIGN KEY (personalCarId) REFERENCES PersonalCar (personalCarId) ON DELETE CASCADE
);

USE AutoAuction
GO
CREATE TABLE PrivatePersonalCar (
    privatePersonalCarId INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    personalCarId INT NOT NULL,
    hasIsofixFittings BIT NOT NULL,
    FOREIGN KEY (personalCarId) REFERENCES PersonalCar (personalCarId) ON DELETE CASCADE
);

USE AutoAuction
GO
CREATE TABLE HeavyVehicle (
    heavyVehicleId INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    vehicleId INT NOT NULL,
    vehicleDimensionHeight FLOAT NOT NULL,
    vehicleDimensionWeight FLOAT NOT NULL,
    vehicleDimensionLenght FLOAT NOT NULL,
    FOREIGN KEY (vehicleId) REFERENCES Vehicle (vehicleId) ON DELETE CASCADE
);

USE AutoAuction
GO
CREATE TABLE Bus (
    busId INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    heavyVehicleId INT NOT NULL,
    numberOfSeats INT NOT NULL,
    numberOfSleepingSpaces INT NOT NULL,
    hasToilet BIT NOT NULL,
    FOREIGN KEY (heavyVehicleId) REFERENCES HeavyVehicle (heavyVehicleId) ON DELETE CASCADE
);


USE AutoAuction
GO
CREATE TABLE Truck (
    truckId INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
    heavyVehicleId INT NOT NULL,
    LoadCapacity FLOAT NOT NULL,
    FOREIGN KEY (heavyVehicleId) REFERENCES HeavyVehicle (heavyVehicleId) ON DELETE CASCADE
);


USE AutoAuction
GO
CREATE TABLE UserTable (
    Username VARCHAR(300) NOT NULL PRIMARY KEY,
    CorporateUser BIT NOT NULL, -- 1 if user is a corporate user
    Credit DECIMAL,
    Balance DECIMAL NOT NULL, 
	CHECK (
        CorporateUser=0 AND Balance >= 0 OR
        CorporateUser=1 AND 0 <= Credit+Balance
    )
);

USE AutoAuction
GO
CREATE TABLE PrivateUser (
    Username VARCHAR(300) NOT NULL,
    CPRNumber INT NOT NULL UNIQUE CHECK (len(CPRNumber)=10),
    FOREIGN KEY (Username) REFERENCES UserTable (Username) ON DELETE CASCADE
);

USE AutoAuction
GO
CREATE TABLE CorporateUser (
    Username VARCHAR(300) NOT NULL,
    CVRNumber INT NOT NULL UNIQUE CHECK (len(CVRNumber)=8),
    FOREIGN KEY (Username) REFERENCES UserTable (Username) ON DELETE CASCADE
);

USE AutoAuction
DROP TABLE IF EXISTS dbo.Auction
GO

CREATE TABLE dbo.Auction(
	auctionId int IDENTITY(1,1) Primary key,
	auctionNumber int unique not null,
	minimumPrice decimal(18,0) not null,
	standingBid decimal(18,0) not null,
	vehicleId int not null,
	seller varchar(max) not null,
	buyer varchar(max),
);