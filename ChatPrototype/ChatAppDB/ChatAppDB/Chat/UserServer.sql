CREATE TABLE [Chat].[UserServer]
(
	[UserId] int not null,
	[ServerId] uniqueidentifier not null,
	CONSTRAINT FK_UserServer_User FOREIGN KEY ([UserId]) REFERENCES [Security].[User]([UserId]),
	CONSTRAINT FK_UserServer_Server FOREIGN KEY ([ServerId]) REFERENCES [Chat].[Server]([ServerId]),
	CONSTRAINT [PK_UserServer] PRIMARY KEY CLUSTERED
	(
		UserId ASC,
		ServerId ASC
	)
)
