SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[DeleteExpense]
    @Id INT
AS
BEGIN
    DELETE FROM Expenses
    WHERE Id = @Id
END
GO
