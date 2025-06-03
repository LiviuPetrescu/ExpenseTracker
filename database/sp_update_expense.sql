SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[UpdateExpense]
    @Id INT,
    @Title NVARCHAR(100),
    @Amount DECIMAL(10,2),
    @Date DATE,
    @Category NVARCHAR(50),
    @Notes NVARCHAR(MAX)
AS
BEGIN
    UPDATE Expenses
    SET 
        Title = @Title,
        Amount = @Amount,
        Date = @Date,
        Category = @Category,
        Notes = @Notes
    WHERE Id = @Id
END
GO
