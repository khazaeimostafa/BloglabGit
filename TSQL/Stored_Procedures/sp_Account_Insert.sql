-- Create a new stored procedure called 'Account_Insert' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Account_Insert'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.Account_Insert
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.Account_Insert
    @Account AccountType READONLY
-- add more stored procedure parameters here
AS
BEGIN
    INSERT INTO 
    [dbo].[ApplicationUser]
    (     [Username]
      ,[NormalizedUsername]
      ,[Email]
      ,[NormalizedEmail]
      ,[Fulname]
      ,[PasswordHash] )
    SELECT
          [Username]
      ,[NormalizedUsername]
      ,[Email]
      ,[NormalizedEmail]
      ,[Fulname]
      ,[PasswordHash]
    

    FROM @Account;
    SELECT CAST (SCOPE_IDENTITY() AS INT)

    
END
GO
-- example to execute the stored procedure we just created
