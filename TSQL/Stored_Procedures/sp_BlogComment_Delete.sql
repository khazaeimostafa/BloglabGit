-- Create a new stored procedure called 'BlogComment_Delete' in schema 'dbo'
-- Drop the stored procedure if it already exists
IF EXISTS (
SELECT *
    FROM INFORMATION_SCHEMA.ROUTINES
WHERE SPECIFIC_SCHEMA = N'dbo'
    AND SPECIFIC_NAME = N'BlogComment_Delete'
    AND ROUTINE_TYPE = N'PROCEDURE'
)
DROP PROCEDURE dbo.BlogComment_Delete
GO
 
CREATE PROCEDURE dbo.BlogComment_Delete
   @BlogCommentId INT 
AS

BEGIN

DROP TABLE IF EXISTS #BlogCommentSToBeDeleted;

WITH cte_BogComments AS (

    SELECT

t1.[BlogCommentId],
t1.[ParentBlogCommentId]

FROM

[dbo].[BlogComment] t1

WHERE

t1.[BlogCommentId]  = @BlogCommentId

UNION ALL
SELECT

t2.[BlogCommentId],
t2.[ParentBlogCommentId]

FROM

[dbo].[BlogComment] t2

INNER JOIN cte_BogComments t3
ON 

t3.[BlogCommentId]  = t2.[ParentBlogCommentId]

)

SELECT
        [BlogCommentId],
        [ParentBlogCommentId]
             
          INTO
          
           #BlogCommentSToBeDeleted

          FROM

          cte_BogComments;

          UPDATE t1

          SET
          t1.[ActiveInd] = CONVERT(BIT,1),
          t1.[UpdateDate] = GETDATE()

          FROM
            [dbo].[BlogComments] t1
            INNER JOIN #BlogCommentSToBeDeleted t2
             ON t1.[BlogCommentId] = t2.[BlogCommentId];

        
         
END

