CREATE PROCEDURE UpdateBalance @userName varchar(300), @balance decimal(18,2)
AS

IF NOT EXISTS(SELECT Username FROM UserTable WHERE Username = @username)
BEGIN 
    ;THROW 99001, 'User not found', 1;
END

UPDATE UserTable
SET Balance = @balance
WHERE UserTable.Username = @userName