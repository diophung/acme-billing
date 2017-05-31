CREATE TABLE [dbo].[BillStatement]
(
    [RowId] BIGINT NOT NULL PRIMARY KEY, 
    [CustomerId] UNIQUEIDENTIFIER NOT NULL, 
    [BillStatementId] UNIQUEIDENTIFIER NOT NULL, 
    [AmountDue] DECIMAL(18, 6) NOT NULL, 
    [Month] INT NOT NULL, 
    [Year] INT NOT NULL
)
