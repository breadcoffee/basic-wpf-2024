USE [EMS]
GO
/****** Object:  Table [dbo].[MovieItem]    Script Date: 2024-05-17 오후 2:48:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MovieItem](
	[Id] [int] NOT NULL,
	[Title] [nvarchar](300) NOT NULL,
	[Original_Title] [nvarchar](300) NOT NULL,
	[Release_Date] [char](10) NOT NULL,
	[Original_Language] [varchar](10) NOT NULL,
	[Adult] [bit] NULL,
	[Popularity] [float] NOT NULL,
	[Vote_Average] [float] NOT NULL,
	[Vote_Count] [int] NOT NULL,
	[Poster_Path] [varchar](300) NULL,
	[Overview] [ntext] NULL,
	[Reg_Date] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
