﻿/*
Deployment script for ChatAppDB

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "ChatAppDB"
:setvar DefaultFilePrefix "ChatAppDB"
:setvar DefaultDataPath "D:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\"
:setvar DefaultLogPath "D:\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\"

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
PRINT N'Creating Schema [Chat]...';


GO
CREATE SCHEMA [Chat]
    AUTHORIZATION [dbo];


GO
PRINT N'Creating Table [Chat].[Server]...';


GO
CREATE TABLE [Chat].[Server] (
    [ServerId]    UNIQUEIDENTIFIER NOT NULL,
    [Name]        VARCHAR (50)     NOT NULL,
    [Description] VARCHAR (255)    NULL,
    [DateCreated] DATETIME         NOT NULL,
    PRIMARY KEY CLUSTERED ([ServerId] ASC)
);


GO
PRINT N'Creating Table [Chat].[Channel]...';


GO
CREATE TABLE [Chat].[Channel] (
    [ChannelId] UNIQUEIDENTIFIER NOT NULL,
    [Name]      VARCHAR (50)     NULL,
    PRIMARY KEY CLUSTERED ([ChannelId] ASC)
);


GO
PRINT N'Creating Table [Chat].[ChannelMessage]...';


GO
CREATE TABLE [Chat].[ChannelMessage] (
    [Id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [Chat].[Message]...';


GO
CREATE TABLE [Chat].[Message] (
    [Id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [Chat].[UserServer]...';


GO
CREATE TABLE [Chat].[UserServer] (
    [Id] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);


GO
PRINT N'Creating Table [Chat].[ServerChannel]...';


GO
CREATE TABLE [Chat].[ServerChannel] (
    [ServerChannelId] INT              IDENTITY (1, 1) NOT NULL,
    [ServerId]        UNIQUEIDENTIFIER NOT NULL,
    [ChannelId]       UNIQUEIDENTIFIER NOT NULL,
    PRIMARY KEY CLUSTERED ([ServerChannelId] ASC)
);


GO
PRINT N'Creating Foreign Key [Chat].[FK_ServerChannel_Server]...';


GO
ALTER TABLE [Chat].[ServerChannel] WITH NOCHECK
    ADD CONSTRAINT [FK_ServerChannel_Server] FOREIGN KEY ([ServerId]) REFERENCES [Chat].[Server] ([ServerId]);


GO
PRINT N'Creating Foreign Key [Chat].[FK_ServerChannel_Channel]...';


GO
ALTER TABLE [Chat].[ServerChannel] WITH NOCHECK
    ADD CONSTRAINT [FK_ServerChannel_Channel] FOREIGN KEY ([ChannelId]) REFERENCES [Chat].[Channel] ([ChannelId]);


GO
PRINT N'Checking existing data against newly created constraints';


GO
USE [$(DatabaseName)];


GO
ALTER TABLE [Chat].[ServerChannel] WITH CHECK CHECK CONSTRAINT [FK_ServerChannel_Server];

ALTER TABLE [Chat].[ServerChannel] WITH CHECK CHECK CONSTRAINT [FK_ServerChannel_Channel];


GO
PRINT N'Update complete.';


GO
