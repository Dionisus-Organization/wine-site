Declare @JSON varchar(max)
SELECT @JSON=BulkColumn
FROM OPENROWSET (BULK 'C:\study\GroupProject\result\data.json', SINGLE_CLOB) import
insert into wineDB
select * 
from openjson (@JSON)
with
(
    [Wine_Id]          INT,
    [Wine]             varchar (50),
    [Wine_Slug]        varchar (50),
    [Appellation]      varchar (20),
    [Appellation_Slug] varchar (20),
    [Color]            varchar (10),
    [Wine_Type]        varchar (10),
    [Region]           varchar (10),
    [Country]          varchar (10),
    [Classification]   varchar (10),
    [Vintage]          varchar (5) ,
    [Date]             varchar (10),
    [Is_Primeurs]      BIT       ,
    [Score]            REAL      ,
    [Confidence_Index] varchar (5) ,
    [Journalist_Count] INT       ,
    [Lwin]             varchar (20),
    [Lwin_11]          varchar (20)
)