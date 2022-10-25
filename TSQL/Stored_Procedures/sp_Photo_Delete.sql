-- Create a new stored procedure called 'Photo_Delete' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Photo_Delete'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.Photo_Delete
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.Photo_Delete
    @PhotoId INT
AS
BEGIN
     DELETE FROM [dbo].[Photo] WHERE [PhotoId] = @PhotoId
END
 