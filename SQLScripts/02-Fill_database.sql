
INSERT INTO [dbo].[AspNetRoles] (Id, Name) 
VALUES (NEWID(),'Seller'),
	(NEWID(),'Customer');



INSERT INTO [dbo].[AspNetCategory] (Name) 
VALUES('Drinks'),
	('Desserts'),
	('Breakfast'),
	('Lunch'), 
	('Dinner'), 
	('Snacks');





DECLARE @cont INT;
SET @cont = 1;

WHILE @cont < 10
BEGIN
	INSERT INTO  [dbo].[AspNetProduct] (Name, Price, Quantity, Rating, ImageUrl, CategoryId) 
	VALUES (CONCAT('Product ', CAST(@cont AS VARCHAR)), 1200, 10, 5, CONCAT('/product', CAST(@cont AS VARCHAR), '.png'), 1);	
	SET @cont = @cont + 1;
END;

SET @cont = 1;
	    