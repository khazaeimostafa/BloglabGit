-- Create a new table called '[BlogComment]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[BlogComment]', 'U') IS NOT NULL
DROP TABLE [dbo].[BlogComment]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[BlogComment]
(
    [BlogCommentId] INT NOT NULL  IDENTITY(1,1), -- Primary Key column
    [ParentBlogCommentId] INT NULL,
    [BlogId] INT NOT NULL,
    [ApplicationUserId] NVARCHAR(450) NOT NULL,
    -- [ColumnName] NVARCHAR(50) NOT NULL,
    Content NVARCHAR(300) NOT NULL,
   PublishDate DATETIME NOT NULL DEFAULT GETDATE(),
   UpdateDate DATETIME NOT NULL DEFAULT GETDATE(),
   ActiveInd BIT NOT NULL DEFAULT CONVERT(BIT ,1)
  
    -- Specify more columns here
 PRIMARY KEY(BlogCommentId),
 FOREIGN KEY(BlogId) REFERENCES Blog(BlogId),
 FOREIGN KEY(ApplicationUserId) REFERENCES ApplicationUser(ApplicationUserId)
    -- Specify more columns here
);
GO