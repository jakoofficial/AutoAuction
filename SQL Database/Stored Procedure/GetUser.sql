ALTER PROCEDURE GetUser @username varchar(300)
AS
declare @usertype bit;

SELECT @usertype = dbo.UserTable.CorporateUser FROM dbo.UserTable WHERE UserTable.Username = @username

IF @usertype = 1 BEGIN
	SELECT * FROM UserTable 
	INNER JOIN dbo.CorporateUser ON dbo.CorporateUser.Username = UserTable.Username
	WHERE UserTable.Username = @username
END
ELSE
BEGIN
	SELECT * FROM UserTable 
	INNER JOIN dbo.PrivateUser ON dbo.PrivateUser.Username = UserTable.Username
	WHERE UserTable.Username = @username
END