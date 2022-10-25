-- Create a new stored procedure called 'Photo_Insert' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Photo_Insert'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.Photo_Insert
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.Photo_Insert
     @Photo PhotoType READONLY,
     @ApplicationUserId NVARCHAR(450)

AS
BEGIN
  
  INSERT INTO [dbo].[Photo]

 (
        [ApplicationUserId],
        [PublicId],
        [ImageUrl],
        [Description] 
 )
 SELECT
        @ApplicationUserId,
        [PublicId],
        [ImageUrl],
        [Description] 

        FROM @Photo;

SELECT  CAST(SCOPE_IDENTITY() AS INT)
END
GO
 