CREATE view [aggregate].[Blog]
AS
 SELECT  

 t1.[BlogId],
      t1.[ApplicationUserId],
      t2.Username,
      t1.[PhotoId],
      t1.[Title],
      t1.[Content],
      t1.[PublishDate],
      t1.[UpdateDate],
      t1.[ActiveInd]
  FROM  [dbo].[Blog] t1


 INNER JOIN dbo.ApplicationUser t2 ON 
 t1.ApplicationUserId = t2.ApplicationUserId