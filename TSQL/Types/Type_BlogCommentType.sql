-- Create a new table called '[BlogCommentTpe]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[BlogCommentTpe]', 'U') IS NOT NULL
DROP TABLE [dbo].[BlogCommentTpe]
GO
-- Create the table in the specified schema
CREATE TYPE [dbo].[BlogCommentType] AS TABLE
(
    [BlogCommentId] INT NOT NULL  IDENTITY(1,1), -- Primary Key column
    [ParentBlogCommentId] INT NULL,
    [BlogId] INT NOT NULL,
    Content NVARCHAR(300) NOT NULL
    -- Specify more columns here
);
GO