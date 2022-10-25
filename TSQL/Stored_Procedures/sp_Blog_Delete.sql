-- Create a new stored procedure called 'Blog_Delete' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Blog_Delete'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.Blog_Delete
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.Blog_Delete
    @BlogId INT
     
AS
BEGIN
    -- body of the stored procedure
    UPDATE [dbo].[BlogComment]
    SET [ActiveInd] = CONVERT(BIT,0)
    WHERE [blogId] = @BlogId;
    UPDATE [dbo].[Blog]
    SET
    [PhotoId] = NULL,
    [ActiveInd] = CONVERT(BIT,0)
    WHERE
       [blogId] = @BlogId;




END
GO
-- example to execute the stored procedure we just created
 