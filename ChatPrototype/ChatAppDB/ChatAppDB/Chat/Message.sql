CREATE TABLE [Chat].[Message]
(
	[MessageId] INT NOT NULL IDENTITY(1,1) PRIMARY KEY,
	[ChannelId] UNIQUEIDENTIFIER NOT NULL,
	[UserId] INT NOT NULL,
	[MessageData] NVARCHAR(MAX),
	[DateCreated] DATETIME NOT NULL,
	CONSTRAINT FK_Message_Channel FOREIGN KEY ([ChannelId]) REFERENCES [Chat].[Channel]([ChannelId]),
	CONSTRAINT FK_Message_User FOREIGN KEY ([UserId]) REFERENCES [Security].[User]([UserId])
)
