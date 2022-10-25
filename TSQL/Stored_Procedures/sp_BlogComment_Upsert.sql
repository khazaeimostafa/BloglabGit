-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.BlogComment_Upsert
    @BlogComment BlogCommentType READONLY,
    @ApplicationUserId NVARCHAR(450)
AS
BEGIN
    MERGE INTO dbo.BlogComment TARGET
    USING(
        SELECT
        [BlogCommentId]
       ,[ParentBlogCommentId]
       ,[BlogId]
       ,[Content],
        @ApplicationUserId [ApplicationUserId]

        FROM @BlogComment
    )AS Source
    ON(
            TARGET.[BlogCommentId] = SOURCE.[BlogCommentId] AND TARGET.[ApplicationUserId] = SOURCE.[ApplicationUserId]

    )
    WHEN MATCHED  THEN
        UPDATE SET
         TARGET.[Content] = SOURCE.[Content],
         TARGET.[UpdateDate] = GETDATE()
         WHEN NOT MATCHED BY TARGET THEN
         INSERT(

        [ParentBlogCommentId]
       ,[BlogId]
       ,[Content]
       ,  [ApplicationUserId]

        )
        VALUES(
                SOURCE.[ParentBlogCommentId],
                SOURCE.[BlogId],
                SOURCE.[Content],
               SOURCE.[ApplicationUserId]

        );
     
END
GO
