-- =======================================================
-- Create Stored Procedure to Create and update Users.
-- =======================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE SaveUsers(
	@Id INT,
	@Name VARCHAR(100),
	@BirthDate DATE,
	@Gender CHAR(1)
)
AS
BEGIN
	IF @Id IS NULL OR @Id = 0
		INSERT INTO Users (Name, BirthDate, Gender)
		VALUES(@Name, @BirthDate, @Gender);
	ELSE
		UPDATE Users
		SET Name = @Name,
			BirthDate = @BirthDate,
			Gender = @Gender
		WHERE Id = @Id;	
END
GO