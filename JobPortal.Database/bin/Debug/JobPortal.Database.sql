﻿/*
Deployment script for JobPortal.Database_2

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "JobPortal.Database_2"
:setvar DefaultFilePrefix "JobPortal.Database_2"
:setvar DefaultDataPath "C:\Users\junaid tariq\AppData\Local\Microsoft\VisualStudio\SSDT\EAD_GroupProject"
:setvar DefaultLogPath "C:\Users\junaid tariq\AppData\Local\Microsoft\VisualStudio\SSDT\EAD_GroupProject"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Altering [dbo].[AspNetUsers]...';


GO
ALTER TABLE [dbo].[AspNetUsers] ALTER COLUMN [FullName] NVARCHAR (50) NULL;

ALTER TABLE [dbo].[AspNetUsers] ALTER COLUMN [ProfileImage] VARBINARY (MAX) NULL;

ALTER TABLE [dbo].[AspNetUsers] ALTER COLUMN [Role] NVARCHAR (50) NULL;


GO
PRINT N'Creating [dbo].[ContactUs]...';


GO
CREATE TABLE [dbo].[ContactUs] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [Email]       NVARCHAR (256) NOT NULL,
    [Subject]     NVARCHAR (256) NOT NULL,
    [Message]     NVARCHAR (MAX) NOT NULL,
    [Name]        NVARCHAR (256) NOT NULL,
    [IsResponded] BIT            NOT NULL,
    [MessageDate] DATETIME2 (7)  NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating unnamed constraint on [dbo].[ContactUs]...';


GO
ALTER TABLE [dbo].[ContactUs]
    ADD DEFAULT 0 FOR [IsResponded];


GO
PRINT N'Creating unnamed constraint on [dbo].[ContactUs]...';


GO
ALTER TABLE [dbo].[ContactUs]
    ADD DEFAULT GETDATE() FOR [MessageDate];


GO
PRINT N'Creating [dbo].[AddMessageToContactUs]...';


GO
CREATE PROCEDURE [dbo].[AddMessageToContactUs]
	 @email nvarchar(256)
	,@subject nvarchar(256)
	,@message nvarchar(max)
	,@name nvarchar(256)

AS

INSERT INTO [ContactUs]([Email], [Subject], [Message], [Name])
VALUES(@email, @subject, @message, @name);
GO
-- Refactoring step to update target server with deployed transaction logs

IF OBJECT_ID(N'dbo.__RefactorLog') IS NULL
BEGIN
    CREATE TABLE [dbo].[__RefactorLog] (OperationKey UNIQUEIDENTIFIER NOT NULL PRIMARY KEY)
    EXEC sp_addextendedproperty N'microsoft_database_tools_support', N'refactoring log', N'schema', N'dbo', N'table', N'__RefactorLog'
END
GO
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = 'bc8c8848-872d-49a4-9386-8c1f8d204a5f')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('bc8c8848-872d-49a4-9386-8c1f8d204a5f')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '81d2ee4d-4d87-4e95-a859-2d49592859ff')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('81d2ee4d-4d87-4e95-a859-2d49592859ff')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '2265b443-6083-460c-85cc-ec714554713e')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('2265b443-6083-460c-85cc-ec714554713e')

GO

GO
PRINT N'Update complete.';


GO