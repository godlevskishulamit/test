USE [shineSalesByWolf]
GO
/****** Object:  Table [dbo].[Gifts]    Script Date: 06/12/2020 13:08:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Gifts](
	[Tz] [nvarchar](9) NOT NULL,
	[Gift] [int] NOT NULL,
	[ISGet] [bit] NULL,
 CONSTRAINT [PK_Gifts] PRIMARY KEY CLUSTERED 
(
	[Tz] ASC,
	[Gift] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GiftType]    Script Date: 06/12/2020 13:08:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GiftType](
	[Type] [int] IDENTITY(1,1) NOT NULL,
	[Gift] [nvarchar](100) NULL,
	[Money] [decimal](18, 0) NULL,
	[MoneySister] [decimal](18, 0) NULL,
 CONSTRAINT [PK_GiftType] PRIMARY KEY CLUSTERED 
(
	[Type] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MoneyDonated]    Script Date: 06/12/2020 13:08:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MoneyDonated](
	[Num] [int] NOT NULL,
	[TZ] [nvarchar](9) NOT NULL,
	[Sum] [decimal](18, 2) NULL,
	[Kind] [int] NULL,
	[KindName] [nvarchar](50) NULL,
	[DateUpdate] [datetime] NULL,
 CONSTRAINT [PK_MoneyDonated] PRIMARY KEY CLUSTERED 
(
	[Num] ASC,
	[TZ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Students]    Script Date: 06/12/2020 13:08:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Students](
	[TZ] [nvarchar](9) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Class] [nvarchar](50) NULL,
	[IdSister] [int] NULL,
	[NumClass] [int] NULL,
 CONSTRAINT [PK_Students] PRIMARY KEY CLUSTERED 
(
	[TZ] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Gifts]  WITH CHECK ADD  CONSTRAINT [FK_Gifts_GiftType] FOREIGN KEY([Gift])
REFERENCES [dbo].[GiftType] ([Type])
GO
ALTER TABLE [dbo].[Gifts] CHECK CONSTRAINT [FK_Gifts_GiftType]
GO
ALTER TABLE [dbo].[Gifts]  WITH CHECK ADD  CONSTRAINT [FK_Gifts_Students] FOREIGN KEY([Tz])
REFERENCES [dbo].[Students] ([TZ])
GO
ALTER TABLE [dbo].[Gifts] CHECK CONSTRAINT [FK_Gifts_Students]
GO
ALTER TABLE [dbo].[MoneyDonated]  WITH CHECK ADD  CONSTRAINT [FK_MoneyDonated_MoneyDonated] FOREIGN KEY([TZ])
REFERENCES [dbo].[Students] ([TZ])
GO
ALTER TABLE [dbo].[MoneyDonated] CHECK CONSTRAINT [FK_MoneyDonated_MoneyDonated]
GO
