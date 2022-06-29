USE master
GO
IF NOT EXISTS (
   SELECT name
   FROM sys.databases
   WHERE name = N'PraksaDB'
)
CREATE DATABASE [PraksaDB]
GO

USE [PraksaDB]
IF OBJECT_ID('dbo.Cocktail', 'U') IS NOT NULL
DROP TABLE dbo.Cocktail
GO

CREATE TABLE dbo.Cocktail
(
	Cocktail_ID uniqueidentifier not null primary key,
	Name [nvarchar](50)  not null,
	Price [float] not null
);
GO

IF OBJECT_ID('dbo.Ingredient', 'U') IS NOT NULL
DROP TABLE dbo.Ingredient
GO

CREATE TABLE dbo.Ingredient
(
	Ingredient_ID uniqueidentifier not null primary key,
	Name [nvarchar](50)  not null,
	Color [nvarchar](25)
);
GO

IF OBJECT_ID('dbo.Cocktail_Ingredient_Junction', 'U') IS NOT NULL
DROP TABLE dbo.Cocktail_Ingredient_Junction
GO

CREATE TABLE dbo.Cocktail_Ingredient_Junction
(
	Cocktail_ID uniqueidentifier constraint cocktail_fk foreign key references dbo.Cocktail(Cocktail_ID),
	Ingredient_ID uniqueidentifier constraint ingredient_fk foreign key references dbo.Ingredient(Ingredient_ID),
	constraint cocktail_ingredient_pk primary key(Cocktail_ID, Ingredient_ID)
);
GO


insert into dbo.Cocktail (Cocktail_ID, Name, Price) values 
(NEWID(), 'Martini', 9.5),
(NEWID(), 'Bloody Mary', 8),
(NEWID(), 'White Russian', 10),
(NEWID(), 'Old Fashioned', 10)
GO

select * from dbo.Cocktail;

insert into dbo.Ingredient (Ingredient_ID, Name, Color) values
(NEWID(), 'Whiskey', 'Brown'),
(NEWID(), 'Vodka', null),
(NEWID(), 'Gin', null),
(NEWID(), 'Vermouth', 'White/Red'),
(NEWID(), 'Olive', 'Green'),
(NEWID(), 'Tomato Juice', 'Red'),
(NEWID(), 'Coffee Liqueur', 'Brown'),
(NEWID(), 'Cream', 'White'),
(NEWID(), 'Angostura Bitters', 'Red'),
(NEWID(), 'Sugar', 'White')
GO

select * from dbo.Ingredient;

insert into dbo.Cocktail_Ingredient_Junction (Cocktail_ID, Ingredient_ID) values
((select dbo.Cocktail.Cocktail_ID from dbo.Cocktail where dbo.Cocktail.Name = 'Old Fashioned'), (select dbo.Ingredient.Ingredient_ID from dbo.Ingredient where dbo.Ingredient.Name = 'Sugar')),
((select dbo.Cocktail.Cocktail_ID from dbo.Cocktail where dbo.Cocktail.Name = 'Old Fashioned'), (select dbo.Ingredient.Ingredient_ID from dbo.Ingredient where dbo.Ingredient.Name = 'Whiskey')),
((select dbo.Cocktail.Cocktail_ID from dbo.Cocktail where dbo.Cocktail.Name = 'Old Fashioned'), (select dbo.Ingredient.Ingredient_ID from dbo.Ingredient where dbo.Ingredient.Name = 'Angostura Bitters'))
GO

insert into dbo.Cocktail_Ingredient_Junction (Cocktail_ID, Ingredient_ID) values
((select dbo.Cocktail.Cocktail_ID from dbo.Cocktail where dbo.Cocktail.Name = 'White Russian'), (select dbo.Ingredient.Ingredient_ID from dbo.Ingredient where dbo.Ingredient.Name = 'Vodka')),
((select dbo.Cocktail.Cocktail_ID from dbo.Cocktail where dbo.Cocktail.Name = 'White Russian'), (select dbo.Ingredient.Ingredient_ID from dbo.Ingredient where dbo.Ingredient.Name = 'Cream')),
((select dbo.Cocktail.Cocktail_ID from dbo.Cocktail where dbo.Cocktail.Name = 'White Russian'), (select dbo.Ingredient.Ingredient_ID from dbo.Ingredient where dbo.Ingredient.Name = 'Coffee Liqueur'))
GO

insert into dbo.Cocktail_Ingredient_Junction (Cocktail_ID, Ingredient_ID) values
((select dbo.Cocktail.Cocktail_ID from dbo.Cocktail where dbo.Cocktail.Name = 'Martini'), (select dbo.Ingredient.Ingredient_ID from dbo.Ingredient where dbo.Ingredient.Name = 'Gin')),
((select dbo.Cocktail.Cocktail_ID from dbo.Cocktail where dbo.Cocktail.Name = 'Martini'), (select dbo.Ingredient.Ingredient_ID from dbo.Ingredient where dbo.Ingredient.Name = 'Vermouth')),
((select dbo.Cocktail.Cocktail_ID from dbo.Cocktail where dbo.Cocktail.Name = 'Martini'), (select dbo.Ingredient.Ingredient_ID from dbo.Ingredient where dbo.Ingredient.Name = 'Olive'))
GO

insert into dbo.Cocktail_Ingredient_Junction (Cocktail_ID, Ingredient_ID) values
((select dbo.Cocktail.Cocktail_ID from dbo.Cocktail where dbo.Cocktail.Name = 'Bloody Mary'), (select dbo.Ingredient.Ingredient_ID from dbo.Ingredient where dbo.Ingredient.Name = 'Vodka')),
((select dbo.Cocktail.Cocktail_ID from dbo.Cocktail where dbo.Cocktail.Name = 'Bloody Mary'), (select dbo.Ingredient.Ingredient_ID from dbo.Ingredient where dbo.Ingredient.Name = 'Tomato Juice'))
GO

select * from dbo.Cocktail_Ingredient_Junction;

select dbo.Cocktail.Name, count(*) as "Number of ingredients"
from dbo.Cocktail join dbo.Cocktail_Ingredient_Junction on (dbo.Cocktail.Cocktail_ID = dbo.Cocktail_Ingredient_Junction.Cocktail_ID)
	join dbo.Ingredient on (dbo.Cocktail_Ingredient_Junction.Ingredient_ID = dbo.Ingredient.Ingredient_ID) group by dbo.Cocktail.Name having count(*) >= 3;

select * from dbo.Cocktail where dbo.Cocktail.Name like 'M%' or dbo.Cocktail.Name like 'W%';


