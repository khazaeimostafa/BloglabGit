-- Create a new stored procedure called 'Blog_GetAllFamous' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Blog_GetAllFamous'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.Blog_GetAllFamous
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.Blog_GetAllFamous
    
AS
BEGIN
    SELECT TOP (6) t1.[BlogId],
      t1.[ApplicationUserId],
      t1.[Username],
      t1.[PhotoId],
      t1.[Title],
      t1.[Content],
      t1.[PublishDate],
      t1.[UpdateDate],
      t1.[ActiveInd]
  FROM [Blogweb].[aggregate].[Blog] t1
  
  INNER JOIN

  [Blogweb].[aggregate].[BlogComment] t2 ON t1.BlogId = t2.BlogId

    WHERE t1.ActiveInd = CONVERT(BIT,1) AND
      t2.ActiveInd = CONVERT(BIT,1)

      GROUP BY

      t1.[BlogId],
      t1.[ApplicationUserId],
      t1.[Username],
      t1.[PhotoId],
      t1.[Title],
      t1.[Content],
      t1.[PublishDate],
      t1.[UpdateDate],
      t1.[ActiveInd]

      ORDER BY

      COUNT(t2.BlogCommentId)

      DESC



END
GO
-- example to execute the stored procedure we just created
 