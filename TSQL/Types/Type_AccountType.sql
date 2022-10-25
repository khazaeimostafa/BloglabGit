-- Create a new table called '[AccountType]' in schema '[dbo]'
-- Drop the table if it already exists
IF OBJECT_ID('[dbo].[AccountType]', 'U') IS NOT NULL
DROP TYPE [dbo].[AccountType]
GO
-- Create the table in the specified schema
CREATE Type [dbo].[AccountType] AS TABLE
(
    
  
    [Username] NVARCHAR(20) NOT NULL,
    [NormalizedUsername] NVARCHAR(256) NOT NULL,
    [Email] NVARCHAR(256) NOT NULL,
    [NormalizedEmail] NVARCHAR(30) NOT NULL,
    [Fulname] NVARCHAR(40)   NULL,
    [PasswordHash] NVARCHAR(MAX) NOT NULL

);
GO