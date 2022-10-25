-- Create a new table called '[Photo]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[Photo]', 'U') IS NOT NULL
DROP TABLE [dbo].[Photo]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[Photo]
(
    [PhotoId] INT NOT NULL IDENTITY(1,1), -- Primary Key column
    [ApplicationUserId] NVARCHAR(450) NOT NULL,
    [PublicId] NVARCHAR(50) NOT NULL,
    ImageUrl NVARCHAR(250) NOT NULL,
    [Description] NVARCHAR(30) Not NULL,
    PublishDate DATETIME NOT NULL DEFAULT GETDATE(),
    UpdateDate DATETIME NOT NULL DEFAULT GETDATE(),
    PRIMARY KEY(photoId),
    FOREIGN KEY(ApplicationUserId) REFERENCES ApplicationUser(ApplicationUserId)
    -- Specify more columns here
);
GO 