-- =======================================================
-- Create Stored Procedure to Get Users
-- =======================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE GetUsers
AS
BEGIN
	SELECT u.Id, u.Name, u.BirthDate, u.Gender
	FROM Users u
END
GO