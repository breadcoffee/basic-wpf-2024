USE [EMS]
GO
/****** Object:  Table [dbo].[Dustsensor]    Script Date: 2024-05-17 오후 2:48:56 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Dustsensor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Dev_id] [varchar](20) NULL,
	[Name] [nvarchar](40) NULL,
	[Loc] [nvarchar](100) NULL,
	[Coordx] [float] NULL,
	[Coordy] [float] NULL,
	[Ison] [bit] NULL,
	[Pm10_after] [int] NULL,
	[Pm25_after] [int] NULL,
	[State] [int] NULL,
	[Timestamp] [datetime] NULL,
	[Company_id] [varchar](50) NULL,
	[Company_name] [nvarchar](50) NULL,
 CONSTRAINT [PK_Dustsensor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
