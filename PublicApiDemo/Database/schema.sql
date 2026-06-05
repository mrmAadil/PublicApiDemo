-- Create the database
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = 'PublicApiDemo')
BEGIN
    CREATE DATABASE PublicApiDemo;
END
GO

USE PublicApiDemo;
GO

-- Create the Countries table
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Countries]') AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[Countries]
    (
        Id INT IDENTITY(1,1) PRIMARY KEY,
        Name NVARCHAR(200) NOT NULL,
        Capital NVARCHAR(200) NOT NULL DEFAULT '',
        Region NVARCHAR(100) NOT NULL DEFAULT '',
        Subregion NVARCHAR(100) NOT NULL DEFAULT '',
        Population BIGINT NOT NULL DEFAULT 0,
        Area FLOAT NOT NULL DEFAULT 0
    );

    CREATE UNIQUE INDEX IX_Countries_Alpha3Code ON [dbo].[Countries](Alpha3Code);
END
GO