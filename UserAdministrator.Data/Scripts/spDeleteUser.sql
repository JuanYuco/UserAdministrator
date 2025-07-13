-- =======================================================
-- Create Stored Procedure to Delete users
-- =======================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE DeleteUsers(
	@Id INT
)
AS
BEGIN
	DELETE FROM Users
	WHERE Id = @Id;
END
GO