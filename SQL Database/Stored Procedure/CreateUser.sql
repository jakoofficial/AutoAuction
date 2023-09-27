CREATE PROCEDURE CreateUser @username varchar(300), @password varchar(max)
AS
EXEC ('CREATE LOGIN ' + @username + ' WITH PASSWORD = ''' + @password+''', CHECK_POLICY = OFF; ')

EXEC ('CREATE USER ' + @username + ' FOR login ' + @username+';') 

EXEC ('GRANT CONTROL ON  Auction TO ' + @username)
EXEC ('GRANT CONTROL ON  Bus TO ' + @username)
EXEC ('GRANT CONTROL ON  HeavyVehicle TO ' + @username)
EXEC ('GRANT CONTROL ON  PersonalCar TO ' + @username)
EXEC ('GRANT CONTROL ON  PrivatePersonalCar TO ' + @username)
EXEC ('GRANT CONTROL ON  ProfessionalPersonalCar TO ' + @username)
EXEC ('GRANT CONTROL ON  Truck TO ' + @username)
EXEC ('GRANT CONTROL ON  Vehicle TO ' + @username)
EXEC ('GRANT CONTROL ON  BidHistory TO ' + @username)
EXEC ('GRANT EXEC TO ' + @username)