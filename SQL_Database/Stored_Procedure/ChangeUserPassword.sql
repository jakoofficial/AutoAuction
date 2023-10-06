CREATE PROCEDURE ChangeUserPassword 
    @username NVARCHAR(128),
    @oldpassword NVARCHAR(MAX),
    @newpassword NVARCHAR(MAX),
    @success INT OUTPUT -- Will be 1 if succed and 0 if not.
AS
BEGIN
    DECLARE @sql NVARCHAR(MAX);

    IF EXISTS (
        SELECT 1
        FROM sys.sql_logins
        WHERE name = @username
          AND PWDCOMPARE(@oldpassword, password_hash) = 1
    )
    BEGIN
        SET @sql = 'ALTER LOGIN ' + QUOTENAME(@username) + ' WITH PASSWORD = ' + QUOTENAME(@newpassword, '''');
        BEGIN TRY
            EXEC sp_executesql @sql;
            SET @success = 1;
        END TRY
        BEGIN CATCH
            SET @success = 0;
        END CATCH;
    END
    ELSE
    BEGIN
        SET @success = 0;
    END
END;