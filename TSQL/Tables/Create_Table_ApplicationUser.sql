-- Create a new table called '[ApplicationUser]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[ApplicationUser]', 'U') IS NOT NULL
DROP TABLE [dbo].[ApplicationUser]
GO
-- Create the table in the specified schema
CREATE TABLE [dbo].[ApplicationUser]
(
    [ApplicationUserId] nvarchr(450) NOT NULL IDENTITY(1,1), -- Primary Key column
    [Username] NVARCHAR(20) NOT NULL,
    [NormalizedUsername] NVARCHAR(256) NOT NULL,
    [Email] NVARCHAR(256) NOT NULL,
    [NormalizedEmail] NVARCHAR(50) NOT NULL,
    [Fulname] NVARCHAR(40)   NULL,
    [PasswordHash] NVARCHAR(MAX) NOT NULL,
    PRIMARY KEY(ApplicationUserId)
    -- Specify more columns here
);

-- Create a nonclustered index with or without a unique constraint
-- Or create a clustered index on table '[ApplicationUser]' in schema '[dbo]' in database '[DatabaseName]'
CREATE  INDEX ApplicationUser_NormalizedUsername ON [Blogweb].[dbo].[ApplicationUser] ([NormalizedUsername] ) /*Change sort order as needed*/

CREATE  INDEX ApplicationUser_NormalizedEmail ON [Blogweb].[dbo].[ApplicationUser] ([NormalizedEmail] ) /*Change sort order as needed*/

GO

-- Select rows from a Table or View '[ApplicationUser] chema '[dbo]'


SELECT Username, NormalizedUsername, Email FROM [dbo].[ApplicationUser]  /* add search conditions here */
GO
GO

