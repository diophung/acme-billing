/*
The database must have a MEMORY_OPTIMIZED_DATA filegroup
before the memory optimized object can be created.

The bucket count should be set to about two times the 
maximum expected number of distinct values in the 
index key, rounded up to the nearest power of two.
*/

CREATE TABLE [dbo].[Customer]
(
    [RowId] INT NOT NULL PRIMARY KEY NONCLUSTERED HASH WITH (BUCKET_COUNT = 131072), 
    [CustomerId] UNIQUEIDENTIFIER NOT NULL, 
    [Name] NCHAR(100) NOT NULL, 
    [Email] NCHAR(100) NOT NULL, 
    [IsActive] BIT NOT NULL, 
    [Address] NCHAR(100) NOT NULL, 
    [City] NCHAR(100) NOT NULL, 
    [State] NCHAR(2) NOT NULL, 
    [Zip] NCHAR(5) NOT NULL 
) WITH (MEMORY_OPTIMIZED = ON)