-- Create a new stored procedure called 'BlogComment_GetAll' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'BlogComment_GetAll'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.BlogComment_GetAll
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.BlogComment_GetAll
    @BlogId INT
AS
BEGIN
   
   
    SELECT 
       t1.[BlogCommentId]
      ,t1.[ParentBlogCommentId]
      ,t1.[BlogId]
      ,t1.[ApplicationUserId]
      ,t1.[Username]
      ,t1.[Content]
      ,t1.[PublishDate]
      ,t1.[UpdateDate]
       
  FROM [Blogweb].[aggregate].[BlogComment] t1
  WHERE
  t1.[BlogId] = @BlogId AND
  t1.[ActiveInd] = CONVERT(BIT,1)
ORDER BY t1.[UpdateDate] DESC

END
GO
 