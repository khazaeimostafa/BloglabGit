-- Create a new stored procedure called 'Blog_GetAll' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Blog_GetAll'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.Blog_GetAll
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.Blog_GetAll
    @Offset INT,
    @PageSize INT
-- add more stored procedure parameters here
AS
BEGIN
    
   SELECT    [BlogId]
      ,[ApplicationUserId]
      ,[Username]
      ,[PhotoId]
      ,[Title]
      ,[Content]
      ,[PublishDate]
      ,[UpdateDate]
      ,[ActiveInd]
  FROM [Blogweb].[aggregate].[Blog] t1
  
  WHERE t1.[ActiveInd] = CONVERT(BIT,1)
  ORDER BY t1.[BlogId]
  OFFSET @Offset ROWS
  FETCH NEXT @PageSize ROWS ONLY;



END
GO
-- example to execute the stored procedure we just created
EXECUTE dbo.Blog_GetAll 1 /*value_for_param1*/, 2 /*value_for_param2*/
GO