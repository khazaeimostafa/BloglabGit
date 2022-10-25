-- Create a new table called '[PhotoType]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[PhotoType]', 'U') IS NOT NULL
DROP TABLE [dbo].[PhotoType]
GO
-- Create the table in the specified schema
CREATE Type [dbo].[PhotoType]  AS TABLE
(
    [PublicId] NVARCHAR(50) NOT NULL,

    [ImageUrl] NVARCHAR(250) NOT NULL,


    [Description] NVARCHAR(30) NOT NULL

    -- Specify more columns here
);
GO