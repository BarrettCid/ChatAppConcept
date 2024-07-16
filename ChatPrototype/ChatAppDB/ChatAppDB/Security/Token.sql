CREATE TABLE [Security].[Token]
(
	[TokenId] uniqueidentifier NOT NULL PRIMARY KEY,
	[UserId] int not null unique,
	[IssueDate] datetime not null,
	CONSTRAINT FK_Token_User FOREIGN KEY ([UserId]) REFERENCES [Security].[User]([UserId])
)
