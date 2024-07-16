﻿CREATE TABLE [Security].[User]
(
	[UserId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[EmailAddress] VARCHAR(50) UNIQUE NOT NULL,
	[Password] NVARCHAR(MAX) NOT NULL,
	[DateCreated] DATETIME NOT NULL,
	[DateModified] DATETIME NULL,
)