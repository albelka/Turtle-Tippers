CREATE DATABASE turtle_tippers;
GO
USE turtle_tippers;
GO
CREATE TABLE [cards] (
  [id] int IDENTITY (1, 1) ,
  [name] varchar(255) ,
  [image] varchar(255) ,
  [flavor_text] varchar(255) ,
  [attack] int ,
  [defense] int ,
  [revive] int , 
 PRIMARY KEY ([id])
) ON [PRIMARY]
GO

CREATE TABLE [decks] (
  [id] int IDENTITY (1, 1) ,
  [card_id] int ,
  [player_id] int ,
  [in_hand] bit ,
  [in_play] bit ,
  [discard] bit ,
  [HP] int , 
 PRIMARY KEY ([id])
) ON [PRIMARY]
GO

CREATE TABLE [players] (
  [id] int IDENTITY (1, 1) ,
  [turtles] int ,
  [name] varchar(255) , 
 PRIMARY KEY ([id])
) ON [PRIMARY]
GO

ALTER TABLE [decks] ADD FOREIGN KEY (card_id) REFERENCES [cards] ([id]);
				
ALTER TABLE [decks] ADD FOREIGN KEY (player_id) REFERENCES [players] ([id]);