USE [UserAdministrator]
GO
/****** Object:  User [UserAdmin]    Script Date: 12/07/2025 12:36:47 p. m. ******/
CREATE USER [UserAdmin] FOR LOGIN [UserAdmin] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [UserAdmin]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [UserAdmin]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [UserAdmin]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [UserAdmin]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [UserAdmin]
GO
ALTER ROLE [db_datareader] ADD MEMBER [UserAdmin]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [UserAdmin]
GO
ALTER ROLE [db_denydatareader] ADD MEMBER [UserAdmin]
GO
ALTER ROLE [db_denydatawriter] ADD MEMBER [UserAdmin]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/07/2025 12:36:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[BirthDate] [date] NOT NULL,
	[Gender] [char](1) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [CK_Gender] CHECK  (([GENDER]='F' OR [GENDER]='M'))
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [CK_Gender]
GO
/****** Object:  StoredProcedure [dbo].[DeleteUsers]    Script Date: 12/07/2025 12:36:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[DeleteUsers](
	@Id INT
)
AS
BEGIN
	DELETE FROM Users
	WHERE Id = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[GetUsers]    Script Date: 12/07/2025 12:36:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[GetUsers]
AS
BEGIN
	SELECT u.Id, u.Name, u.BirthDate, u.Gender
	FROM Users u
END
GO
/****** Object:  StoredProcedure [dbo].[SaveUsers]    Script Date: 12/07/2025 12:36:47 p. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SaveUsers](
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
