-- Create a new stored procedure called 'Photo_GET' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Photo_GET'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.Photo_GET
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.Photo_GET
    @PhotoId INT
AS
BEGIN
    SELECT  
       t1.[PhotoId]
      ,t1.[ApplicationUserId]
      ,t1.[PublicId]
      ,t1.[ImageUrl]
      ,t1.[Description]
      ,t1.[PublishDate]
      ,t1.[UpdateDate]
  FROM [Blogweb].[dbo].[Photo] t1
  WHERE t1.[PhotoId] = @PhotoId

END
GO
 