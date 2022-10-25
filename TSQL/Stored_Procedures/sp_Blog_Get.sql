-- Create a new stored procedure called 'Blog_Get' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Blog_Get'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.Blog_Get
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.Blog_Get
    @BlogId INT
    
AS
BEGIN
     
   SELECT   [BlogId]
      ,[ApplicationUserId]
      ,[PhotoId]
      ,[Title]
      ,[Content]
      ,[PublishDate]
      ,[UpdateDate]
      ,[ActiveInd]
  FROM [Blogweb].[aggregate].[Blog] t1 WHERE   
  t1.[BlogId] = @BlogId AND
  t1.ActiveInd =CONVERT(BIT,1)
END
GO
 