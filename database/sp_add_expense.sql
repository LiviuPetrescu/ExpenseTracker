SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[AddExpense]
    @Title NVARCHAR(100),
    @Amount DECIMAL(10,2),
    @Date DATE,
    @Category NVARCHAR(50),
    @Notes NVARCHAR(MAX)
AS
BEGIN
    INSERT INTO Expenses (Title, Amount, Date, Category, Notes)
    VALUES (@Title, @Amount, @Date, @Category, @Notes)
END;
GO
