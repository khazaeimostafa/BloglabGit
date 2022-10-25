-- Create a new stored procedure called 'Blog_GetByUserId' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Blog_GetByUserId'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.Blog_GetByUserId
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.Blog_GetByUserId
     
     @ApplicationUserId NVARCHAR(450)
     

AS
BEGIN
   
   SELECT   
       t1.[BlogId]
      ,t1.[ApplicationUserId]
      ,t1.[Username]
      ,t1.[PhotoId]
      ,t1.[Title]
      ,t1.[Content]
      ,t1.[PublishDate]
      ,t1.[UpdateDate]
      ,t1.[ActiveInd]
  FROM [Blogweb].[aggregate].[Blog] t1
  WHERE
  t1.[ApplicationUserId] = @ApplicationUserId AND
  t1.[ActiveInd] = CONVERT(BIT,1)

END
 