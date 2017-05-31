CREATE TABLE [dbo].[Report]
(
    [RowId] BIGINT NOT NULL PRIMARY KEY, 
    [Content] NVARCHAR(MAX) NOT NULL, 
    [Month] INT NOT NULL, 
    [Year] INT NOT NULL, 
    [ReportId] UNIQUEIDENTIFIER NOT NULL
)
