USE [shineSalesByWolf]
delete from dbo.GiftType 
GO
SET IDENTITY_INSERT [dbo].[GiftType] ON 
GO
INSERT [dbo].[GiftType] ([Type], [Gift], [Money], [MoneySister]) VALUES (1, N'מטריה', CAST(130 AS Decimal(18, 0)), CAST(110 AS Decimal(18, 0)))
GO
INSERT [dbo].[GiftType] ([Type], [Gift], [Money], [MoneySister]) VALUES (2, N'בלוטוס', CAST(260 AS Decimal(18, 0)), CAST(240 AS Decimal(18, 0)))
GO
INSERT [dbo].[GiftType] ([Type], [Gift], [Money], [MoneySister]) VALUES (3, N'תכשיט גודפילד', CAST(390 AS Decimal(18, 0)), CAST(370 AS Decimal(18, 0)))
GO
INSERT [dbo].[GiftType] ([Type], [Gift], [Money], [MoneySister]) VALUES (4, N'מצעים', CAST(600 AS Decimal(18, 0)), CAST(600 AS Decimal(18, 0)))
GO
INSERT [dbo].[GiftType] ([Type], [Gift], [Money], [MoneySister]) VALUES (5, N'מצעים', CAST(850 AS Decimal(18, 0)), CAST(850 AS Decimal(18, 0)))
GO
INSERT [dbo].[GiftType] ([Type], [Gift], [Money], [MoneySister]) VALUES (6, N'מצעים', CAST(1100 AS Decimal(18, 0)), CAST(1100 AS Decimal(18, 0)))
GO
INSERT [dbo].[GiftType] ([Type], [Gift], [Money], [MoneySister]) VALUES (7, N'מצעים', CAST(1350 AS Decimal(18, 0)), CAST(1350 AS Decimal(18, 0)))
GO
SET IDENTITY_INSERT [dbo].[GiftType] OFF
GO
