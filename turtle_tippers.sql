USE [turtle_tippers]
GO
/****** Object:  Table [dbo].[cards]    Script Date: 12/21/2016 8:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[cards](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[image] [varchar](255) NULL,
	[flavor_text] [varchar](255) NULL,
	[attack] [int] NULL,
	[defense] [int] NULL,
	[revive] [int] NULL,
	[tier] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[decks]    Script Date: 12/21/2016 8:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[decks](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[card_id] [int] NULL,
	[player_id] [int] NULL,
	[in_hand] [bit] NULL,
	[in_play] [bit] NULL,
	[discard] [bit] NULL,
	[HP] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[players]    Script Date: 12/21/2016 8:23:17 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[players](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[turtles] [int] NULL,
	[name] [varchar](255) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET IDENTITY_INSERT [dbo].[cards] ON 

INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (1, N'squirrel', N'/../Content/img/squirrel.png', N'I may be small, but I''m wiry!', 1, 1, 0, 2)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (2, N'deer', N'/../Content/img/deer.png', N'...', 2, 1, 0, 2)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (3, N'bear', N'/../Content/img/bear.png', N'Mama Bear loves her turtles!', 2, 4, 0, 1)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (4, N'dung beetle', N'/../Content/img/dungbeetle.gif', N'Smells bad to other animals, but not to turtles.', 1, 1, 1, 2)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (5, N'honey badger', N'/../Content/img/honey-badger.png', N'Honey badger don''t care.', 4, 1, 0, 1)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (6, N'snake', N'/../Content/img/snake.png', N'Don''t tread on me.', 1, 2, 0, 2)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (7, N'sloth', N'/../Content/img/sloth.png', N'Zzzz... zzzz...', 0, 3, 0, 2)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (8, N'fox', N'/../Content/img/fox.png', N'Crazy like a fox!', 2, 3, 0, 2)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (9, N'duck', N'/../Content/img/duck.png', N'Quack, quack!', 1, 1, 0, 2)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (10, N'cougar', N'/../Content/img/cougar.png', N'Silently deadly.', 4, 2, 0, 1)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (11, N'pineapple', N'/../Content/img/pineapple.png', N'So juicy sweet! Revives one turtle.', 0, 0, 1, 2)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (12, N'strawberry', N'/../Content/img/strawberry.png', N'The seeds will get stuck in your teeth. Revives one turtle.', 0, 0, 1, 2)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (13, N'lettuce', N'/../Content/img/lettuce.png', N'Nom, nom, nom! Revives two turtles.', 0, 0, 2, 2)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (14, N'blueberry', N'/../Content/img/blueberry.png', N'Little blue bundles of joy! Revives one turtle.', 0, 0, 1, 2)
INSERT [dbo].[cards] ([id], [name], [image], [flavor_text], [attack], [defense], [revive], [tier]) VALUES (15, N'multi-vitamin ', N'/../Content/img/vitamin.png', N'Eat it!! Revives three turtles.', 0, 0, 3, 1)
SET IDENTITY_INSERT [dbo].[cards] OFF
SET IDENTITY_INSERT [dbo].[decks] ON 

INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (121, 12, 5, 1, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (122, 9, 5, 1, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (123, 9, 5, 0, 1, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (124, 3, 5, 1, 0, 0, 4)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (125, 13, 5, 1, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (126, 6, 5, 0, 0, 0, 2)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (127, 2, 5, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (128, 8, 5, 0, 0, 0, 3)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (129, 15, 5, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (130, 12, 5, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (131, 9, 5, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (132, 5, 5, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (133, 10, 5, 0, 0, 0, 2)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (134, 13, 5, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (135, 6, 5, 0, 0, 0, 2)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (136, 3, 5, 0, 0, 0, 4)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (137, 3, 5, 0, 0, 0, 4)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (138, 13, 5, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (139, 12, 5, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (140, 4, 5, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (141, 3, 5, 0, 0, 0, 4)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (142, 7, 5, 0, 0, 0, 3)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (143, 13, 5, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (144, 6, 5, 0, 0, 0, 2)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (145, 6, 5, 0, 0, 0, 2)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (146, 9, 5, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (147, 15, 5, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (148, 6, 5, 0, 0, 0, 2)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (149, 6, 5, 0, 0, 0, 2)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (150, 10, 5, 0, 0, 0, 2)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (151, 6, 6, 1, 0, 0, 2)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (152, 6, 6, 0, 0, 0, 2)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (153, 7, 6, 0, 0, 0, 3)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (154, 4, 6, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (155, 2, 6, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (156, 7, 6, 0, 0, 0, 3)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (157, 3, 6, 0, 0, 0, 4)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (158, 2, 6, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (159, 13, 6, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (160, 2, 6, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (161, 3, 6, 0, 0, 0, 4)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (162, 15, 6, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (163, 15, 6, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (164, 5, 6, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (165, 4, 6, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (166, 9, 6, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (167, 14, 6, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (168, 15, 6, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (169, 1, 6, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (170, 7, 6, 0, 0, 0, 3)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (171, 6, 6, 0, 0, 0, 2)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (172, 15, 6, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (173, 12, 6, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (174, 12, 6, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (175, 15, 6, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (176, 12, 6, 0, 0, 0, 0)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (177, 6, 6, 0, 0, 0, 2)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (178, 8, 6, 0, 0, 0, 3)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (179, 5, 6, 0, 0, 0, 1)
INSERT [dbo].[decks] ([id], [card_id], [player_id], [in_hand], [in_play], [discard], [HP]) VALUES (180, 8, 6, 0, 0, 0, 3)
SET IDENTITY_INSERT [dbo].[decks] OFF
SET IDENTITY_INSERT [dbo].[players] ON 

INSERT [dbo].[players] ([id], [turtles], [name]) VALUES (5, 5, N'player1')
INSERT [dbo].[players] ([id], [turtles], [name]) VALUES (6, 5, N'player2')
SET IDENTITY_INSERT [dbo].[players] OFF
