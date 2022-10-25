-- Create a new table called '[BlogType]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[BlogType]', 'U') IS NOT NULL
DROP TABLE [dbo].[BlogType]
GO
-- Create the table in the specified schema
CREATE TYPE [dbo].[BlogType] AS TABLE
(
 
   [BlogId] INT NOT NULL ,  -- Primary Key column  
   [Title] NVARCHAR(50) NOT NULL,
   [Content] NVARCHAR(MAX) NOT NULL,
   [PhotoId] INT NULL

    -- Specify more columns here
);
GO