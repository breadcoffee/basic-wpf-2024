USE [EMS]
GO
/****** Object:  Table [dbo].[LibraryItem]    Script Date: 2024-05-17 오후 2:46:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LibraryItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[주요_매출원] [nvarchar](50) NULL,
	[휴무일] [nvarchar](50) NULL,
	[개점일_개관일] [nvarchar](50) NOT NULL,
	[운영시간] [nvarchar](100) NULL,
	[이름] [nvarchar](50) NULL,
	[전화번호] [nvarchar](50) NULL,
	[주소] [nvarchar](100) NULL,
	[웹사이트] [nvarchar](100) NULL,
	[상세주소] [nvarchar](50) NULL,
	[대표자_운영주체] [nvarchar](50) NULL,
	[총면적_연면적_m2_] [nvarchar](50) NULL,
	[구분] [nvarchar](50) NULL,
	[좌석수] [int] NULL,
	[경도] [float] NULL,
	[보유도서량] [nvarchar](50) NULL,
	[E_mail] [nvarchar](50) NULL,
	[위도] [float] NULL,
	[공간유형] [nvarchar](50) NULL,
	[Objectid] [int] NULL,
 CONSTRAINT [PK_LibraryItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
