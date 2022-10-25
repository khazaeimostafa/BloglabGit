SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- Create the stored procedure in the specified schema
CREATE PROCEDURE [dbo].[Photo_GetByUserId]
    @ApplicationUserId NVARCHAR(450)
AS
BEGIN
    

    SELECT  
       t1.[PhotoId]
      ,t1.[ApplicationUserId]
      ,t1.[PublicId]
      ,t1.[ImageUrl]
      ,t1.[Description]
      ,t1.[PublishDate]
      ,t1.[UpdateDate]
  FROM [Blogweb].[dbo].[Photo] t1
  WHERE t1.[ApplicationUserId] = @ApplicationUserId


END
GO
