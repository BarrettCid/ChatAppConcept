CREATE TABLE [Chat].[ServerChannel]
(
	[ServerId] uniqueidentifier not null,
	[ChannelId] uniqueidentifier not null,
	CONSTRAINT FK_ServerChannel_Server FOREIGN KEY ([ServerId]) REFERENCES [Chat].[Server]([ServerId]),
	CONSTRAINT FK_ServerChannel_Channel FOREIGN KEY ([ChannelId]) REFERENCES [Chat].[Channel]([ChannelId]),
	CONSTRAINT [PK_ServerChannel] PRIMARY KEY CLUSTERED
	(
		ServerId ASC,
		ChannelId ASC
	)
)
