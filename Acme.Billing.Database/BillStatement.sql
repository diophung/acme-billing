CREATE TABLE [dbo].[BillStatement]
(
    [RowId] BIGINT NOT NULL PRIMARY KEY, 
    [CustomerId] UNIQUEIDENTIFIER NOT NULL, 
    [BillStatementId] UNIQUEIDENTIFIER NOT NULL, 
    [AmountDue] DECIMAL(18, 6) NOT NULL, 
    [StatementMonth] INT NOT NULL, 
    [StatementYear] INT NOT NULL, 
    [IsSent] BIT NOT NULL, 
    [GeneratedDateTime] DATETIME NOT NULL
)
