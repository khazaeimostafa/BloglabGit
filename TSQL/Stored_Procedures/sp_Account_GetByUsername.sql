-- Create a new stored procedure called 'Account_GetByUsername' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Account_GetByUsername'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.Account_GetByUsername
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.Account_GetByUsername
    @NormalizedUsername  NVARCHAR(256)
 
-- add more stored procedure parameters here
AS
BEGIN
    -- body of the stored procedure
   SELECT   [ApplicationUserId]
      ,[Username]
      ,[NormalizedUsername]
      ,[Email]
      ,[NormalizedEmail]
      ,[Fulname]
      ,[PasswordHash]
  FROM [Blogweb].[dbo].[ApplicationUser] t1
  WHERE t1.NormalizedUsername = @NormalizedUsername
END
GO
-- example to execute the stored procedure we just created
EXECUTE dbo.Account_GetByUsername 1 /*value_for_param1*/, 2 /*value_for_param2*/
GO