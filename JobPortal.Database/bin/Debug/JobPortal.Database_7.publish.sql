﻿/*
Deployment script for JobPortal-DB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "JobPortal-DB"
:setvar DefaultFilePrefix "JobPortal-DB"
:setvar DefaultDataPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\"

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
PRINT N'The following operation was generated from a refactoring log file 81d2ee4d-4d87-4e95-a859-2d49592859ff, 2265b443-6083-460c-85cc-ec714554713e';

PRINT N'Rename [dbo].[AspNetUsers].[LockoutEndDate] to LockoutEnd';


GO
EXECUTE sp_rename @objname = N'[dbo].[AspNetUsers].[LockoutEndDate]', @newname = N'LockoutEnd', @objtype = N'COLUMN';


GO
-- Refactoring step to update target server with deployed transaction logs
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '81d2ee4d-4d87-4e95-a859-2d49592859ff')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('81d2ee4d-4d87-4e95-a859-2d49592859ff')
IF NOT EXISTS (SELECT OperationKey FROM [dbo].[__RefactorLog] WHERE OperationKey = '2265b443-6083-460c-85cc-ec714554713e')
INSERT INTO [dbo].[__RefactorLog] (OperationKey) values ('2265b443-6083-460c-85cc-ec714554713e')

GO

GO
PRINT N'Update complete.';


GO
