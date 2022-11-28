# Database Challenge

At Careview we use c# entity framework quite a lot. While Entity framework is a great tool to work with databases, sometimes we do have to make use the power of SQL server

You are tasked with converting a c# linq query to sql server stored procedure

Below is the c# code
```cs
        List<Data> GetData(string SortField, bool Ascending, bool? MutlipleServices, decimal? TotalValueLessThanAmount)
        {
            String sql = "SELECT dbo.Client.FirstName + ' ' +dbo.Client.LastName as ClientName, COUNT(dbo.Services.ServiceName) AS NoOfService, SUM(dbo.Services.Cost) AS TotalCost FROM dbo.Client INNER JOIN         dbo.Services ON dbo.Client.ClientID = dbo.Services.ClientID GROUP BY dbo.Client.FirstName, dbo.Client.LastName";
            if (MutlipleServices == true)
            {
                sql = sql + " HAVING(COUNT(dbo.Services.ServiceName) > 1)";
            }
            else if (MutlipleServices == false)
            {
                sql = sql + " HAVING(COUNT(dbo.Services.ServiceName) = 1)";
            }
            //else if MutlipleServices ==null then do nothing
            if (TotalValueLessThanAmount != null)
            {
                if (MutlipleServices == null)
                    sql = sql + " HAVING(SUM(dbo.Services.Cost) < " + TotalValueLessThanAmount.Value + ")";
                else
                    sql = sql + " and(SUM(dbo.Services.Cost) < " + TotalValueLessThanAmount.Value + ")";
            }
            if (SortField == "ClientName")
            {
                sql = sql + " ORDER BY dbo.Client.FirstName, dbo.Client.LastName";
                if (!Ascending)
                {
                    sql = sql + " dec";
                }
            }
            else if (SortField == "NoOfService")
            {
                sql = sql + " ORDER BY NoOfService";
                if (!Ascending)
                {
                    sql = sql + " dec";
                }
            }
            else if (SortField == "ServiceName")
            {
                sql = sql + " ORDER BY ServiceName";
                if (!Ascending)
                {
                    sql = sql + " dec";
                }
                return ExecuteSQLToData(sql);
            }
        }
```

You can use these scripts to create the tables
```sql
CREATE TABLE [dbo].[Client](
      [ClientID] [int] IDENTITY(1,1) NOT NULL,
      [FirstName] [nvarchar](255) NOT NULL,
      [LastName] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Client] PRIMARY KEY CLUSTERED
(
      [ClientID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
INSERT INTO [dbo].[Client]
          ([FirstName]
          ,[LastName])
    VALUES
          ('Andrew'
          ,'Smith')
GO
INSERT INTO [dbo].[Client]
          ([FirstName]
          ,[LastName])
    VALUES
          ('Catherine'
          ,'Jones')
Go
INSERT INTO [dbo].[Client]
          ([FirstName]
          ,[LastName])
    VALUES
          ('George'
          ,'Brisbane')
CREATE TABLE [dbo].[Services](
      [ServiceID] [int] IDENTITY(1,1) NOT NULL,
      [ClientID] [int] NOT NULL,
      [ServiceName] [nvarchar](255) NOT NULL,
      [Cost] [money] NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED
(
      [ServiceID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
USE [delete]
GO
INSERT INTO [dbo].[Services]
          ([ClientID]
          ,[ServiceName]
          ,[Cost])
    VALUES
          (3,
          'Daily Support'
          ,20000)
GO
INSERT INTO [dbo].[Services]
          ([ClientID]
          ,[ServiceName]
          ,[Cost])
    VALUES
          (2,
          'Daily Support'
          ,30000)
GO
INSERT INTO [dbo].[Services]
          ([ClientID]
          ,[ServiceName]
          ,[Cost])
    VALUES
          (3,
          'Daily Support'
          ,40000)
GO
INSERT INTO [dbo].[Services]
          ([ClientID]
          ,[ServiceName]
          ,[Cost])
    VALUES
          (1,
          'Community Access'
          ,48000)
GO
INSERT INTO [dbo].[Services]
          ([ClientID]
          ,[ServiceName]
          ,[Cost])
    VALUES
          (2,
          'Community Access'
          ,30000)
GO
INSERT INTO [dbo].[Services]
          ([ClientID]
          ,[ServiceName]
          ,[Cost])
    VALUES
          (3,
          'Community Access'
          ,25000)
GO

```
please provide the implementation of the the stored procedure 

``` sql
Create or ALTER proc [dbo].[spReportData]
(
      @sortField nvarchar(128) = null,
      @Ascending varchar(4) = null,
      @MutlipleServices bit = null,
      @TotalValueLessThanAmount money= null
) as
```