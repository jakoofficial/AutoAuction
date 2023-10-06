CREATE PROCEDURE GetUser @username varchar(300)
AS
SELECT * FROM dbo.UserTable WHERE UserTable.Username = @username
