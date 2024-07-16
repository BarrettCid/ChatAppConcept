CREATE TABLE [Security].[Log]
(
	[LogEntryId] int NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[UserId] int not null,
	[Action] nvarchar(50) not null,
	[Message] nvarchar(max) not null,
	[DateLogged] datetime not null,
	CONSTRAINT FK_LogEnty_User FOREIGN KEY ([UserId]) REFERENCES [Security].[User]([UserId])
)
