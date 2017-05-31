CREATE TABLE [dbo].[BillingSummary]
(
    [RowId] BIGINT NOT NULL PRIMARY KEY, 
    [BillingSummaryId] UNIQUEIDENTIFIER NOT NULL, 
    [NumberOfInvoiceSent] INT NOT NULL, 
    [TotalAmountDueBilled] DECIMAL(18, 6) NOT NULL, 
    [DateTimeStamp] NCHAR(10) NOT NULL
)
