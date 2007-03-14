USE [Custom]
GO
/****** 对象:  Table [dbo].[Images]    脚本日期: 03/14/2007 18:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Images](
	[Id] [bigint] NOT NULL,
	[Image] [image] NULL,
	[TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** 对象:  Table [dbo].[PdaState]    脚本日期: 03/14/2007 18:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PdaState](
	[Pda] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NOT NULL,
	[LastUpdate] [datetime] NULL,
	[IpAddr] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
 CONSTRAINT [PK_State] PRIMARY KEY CLUSTERED 
(
	[Pda] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** 对象:  Table [dbo].[Shows]    脚本日期: 03/14/2007 18:12:28 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Shows](
	[Id] [bigint] NOT NULL,
	[Pda] [nvarchar](50) COLLATE Chinese_PRC_CI_AS NULL,
	[ImageId] [bigint] NULL,
	[TimeStamp] [datetime] NULL,
 CONSTRAINT [PK_Shows] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
