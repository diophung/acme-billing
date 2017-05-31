CREATE TABLE [dbo].[Report]
(
    [RowId] BIGINT NOT NULL PRIMARY KEY, 
    [CustomerId] NCHAR(255) NOT NULL, 
    [Content] NVARCHAR(MAX) NOT NULL, 
    [Month] INT NOT NULL, 
    [Year] INT NOT NULL
)
