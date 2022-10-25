CREATE view [aggregate].[BlogComment]
AS
SELECT   
      t1.[BlogCommentId],
      t1.[ParentBlogCommentId],
      t1.[BlogId],
      t1.[ApplicationUserId],
      t2.[Username],
      t1.[Content], 
      t1.[PublishDate],
      t1.[UpdateDate],
      t1.[ActiveInd]
  FROM 
   [dbo].[BlogComment] t1


 INNER JOIN dbo.ApplicationUser t2 ON 
 t1.ApplicationUserId = t2.ApplicationUserId