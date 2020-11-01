Declare @JSON varchar(max)
SELECT @JSON=BulkColumn
FROM OPENROWSET (BULK 'C:\study\GroupProject\result\data3.json', SINGLE_CLOB) import
insert into Wine
select * 
from openjson (@JSON)
with
(
    [Wine_Id]          INT,
    [Wine]             NCHAR (50),
    [Wine_Slug]        NCHAR (50),
    [Appellation]      NCHAR (20),
    [Appellation_Slug] NCHAR (20),
    [Color]            NCHAR (10),
    [Wine_Type]        NCHAR (10),
    [Region]           NCHAR (10),
    [Country]          NCHAR (10),
    [Classification]   NCHAR (10),
    [Vintage]          NCHAR (5) ,
    [Date]             NCHAR (10),
    [Is_Primeurs]      BIT       ,
    [Score]            REAL      ,
    [Confidence_Index] NCHAR (5) ,
    [Journalist_Count] INT       ,
    [Lwin]             NCHAR (20),
    [Lwin_11]          NCHAR (20)
)