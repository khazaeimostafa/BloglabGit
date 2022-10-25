-- Create a new table called '[Blog]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Blog]', 'U') IS NOT NULL
DROP TABLE [dbo].[Blog]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Blog]
(
    [BlogId] INT NOT NULL IDENTITY(1,1),
    -- Primary Key column
    [ApplicationUserId] NVARCHAR(450) NOT NULL,
    [PhotoId] INT NULL,
    [Title] NVARCHAR(50) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    PublishDate DATETIME NOT NULL DEFAULT GETDATE(),
    UpdateDate DATETIME NOT NULL DEFAULT GETDATE(),
    ActiveInd BIT NOT NULL DEFAULT CONVERT(BIT ,1)

        -- Specify more columns here
     PRIMARY KEY(BlogId),
    FOREIGN KEY(ApplicationUserId) REFERENCES ApplicationUser(ApplicationUserId),
    FOREIGN KEY(photoId) REFERENCES Photo(photoId)
); 
GO