-- Create a new stored procedure called 'Blog_Upserrt' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'Blog_Upsert'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.Blog_Upsert
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE dbo.Blog_Upsert
    
    @Blog BlogType READONLY,
    @ApplicationUserId NVARCHAR(450)

AS
BEGIN
        MERGE INTO Blog TARGET
        USING(
            SELECT BlogId,
            @ApplicationUserId [ApplicationUserId],
            Title,
            Content,
            PhotoId
            FROM
            @Blog
        ) AS SOURCE
        ON(
            TARGET.BlogId = SOURCE.BlogId AND TARGET.ApplicationUserId = SOURCE.ApplicationUserId
        )
        WHEN MATCHED THEN
        UPDATE SET
        TARGET.[Title] = SOURCE.[Title],
        TARGET.[Content] = SOURCE.[Content],
        TARGET.[PhotoId] = SOURCE.[PhotoId],
        TARGET.[UpdateDate] = GETDATE()

        WHEN NOT MATCHED BY TARGET THEN
        INSERT(

                [ApplicationUserId],
                [Title],
                [Content],
                [PhotoId]

        )
        VALUES(
            SOURCE.[ApplicationUserId],
                SOURCE.[Title],
                SOURCE.[Content],
                SOURCE.[PhotoId]

        );
       

        END
        GO
        