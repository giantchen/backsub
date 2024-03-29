USE [Custom]
GO
/****** 对象:  Table [dbo].[Images]    脚本日期: 03/17/2007 20:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Image] [image] NULL,
	[TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** 对象:  Table [dbo].[PdaState]    脚本日期: 03/17/2007 20:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PdaState](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[DeviceId] [nvarchar](255) COLLATE Chinese_PRC_CI_AS NULL,
	[Pda] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[LastUpdate] [datetime] NULL,
	[IpAddr] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[Owner] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_PdaState_Owner]  DEFAULT (N'无名氏'),
	[Unit] [nvarchar](100) COLLATE Chinese_PRC_CI_AS NULL CONSTRAINT [DF_PdaState_Unit]  DEFAULT (N'未知'),
 CONSTRAINT [PK_PdaState] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** 对象:  Table [dbo].[Sends]    脚本日期: 03/17/2007 20:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sends](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[ImageId] [bigint] NULL,
	[TimeStamp] [datetime] NULL,
	[Message] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
	[Pdas] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Sends] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** 对象:  Table [dbo].[Shows]    脚本日期: 03/17/2007 20:42:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shows](
	[Id] [bigint] IDENTITY(1,1) NOT NULL,
	[Pda] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[ImageId] [bigint] NOT NULL,
	[TimeStamp] [datetime] NOT NULL,
	[Message] [nvarchar](500) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_Shows] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
