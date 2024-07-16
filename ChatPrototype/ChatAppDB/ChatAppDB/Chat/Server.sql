CREATE TABLE [Chat].[Server]
(
	[ServerId] uniqueidentifier NOT NULL PRIMARY KEY,
	[Name] varchar(50) not null,
	[Description] varchar(255) null,
	[DateCreated] DateTime not null
)
