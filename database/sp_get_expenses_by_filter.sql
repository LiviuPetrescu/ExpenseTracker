SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetExpensesByDateFilter]
    @Category NVARCHAR(50) = NULL,
    @StartDate DATE = NULL,
    @EndDate DATE = NULL,
    @PageNumber INT = 1,
    @PageSize INT = 15
AS
BEGIN
    SET NOCOUNT ON;

    -- If no filters are applied, limit to first 15 results only (disable pagination)
    IF @Category IS NULL AND @StartDate IS NULL AND @EndDate IS NULL
    BEGIN
        -- Paginated limited result
        SELECT 
            Id,
            Title,
            Amount,
            [Date],
            Category,
            Notes
        FROM Expenses
        ORDER BY [Date] DESC
        OFFSET (@PageNumber - 1) * @PageSize ROWS
        FETCH NEXT @PageSize ROWS ONLY;

        -- Total sum for this limited data
        SELECT SUM(Amount) AS TotalAmount
        FROM (
            SELECT Amount
            FROM Expenses
            ORDER BY [Date] DESC
            OFFSET (@PageNumber - 1) * @PageSize ROWS
            FETCH NEXT @PageSize ROWS ONLY
        ) AS LimitedData;

        -- Fake total count = page size to disable extra pages
        SELECT @PageSize AS TotalCount;
        RETURN;
    END

    -- If category is specified but no dates, default to last 1 year
    IF @Category IS NOT NULL
    BEGIN
        IF @StartDate IS NULL
            SET @StartDate = DATEADD(YEAR, -1, GETDATE());
        IF @EndDate IS NULL
            SET @EndDate = GETDATE();
    END

    DECLARE @Offset INT = (@PageNumber - 1) * @PageSize;

    -- Return paginated and filtered expenses
    SELECT 
        Id,
        Title,
        Amount,
        [Date],
        Category,
        Notes
    FROM Expenses
    WHERE
        (@Category IS NULL OR Category = @Category)
        AND (
            @StartDate IS NULL OR @EndDate IS NULL
            OR ([Date] BETWEEN @StartDate AND @EndDate)
        )
    ORDER BY [Date] DESC
    OFFSET @Offset ROWS
    FETCH NEXT @PageSize ROWS ONLY;

    -- Return total sum of filtered expenses
    SELECT SUM(Amount) AS TotalAmount
    FROM Expenses
    WHERE
        (@Category IS NULL OR Category = @Category)
        AND (
            @StartDate IS NULL OR @EndDate IS NULL
            OR ([Date] BETWEEN @StartDate AND @EndDate)
        );

    -- Return total count of filtered expenses
    SELECT COUNT(*) AS TotalCount
    FROM Expenses
    WHERE
        (@Category IS NULL OR Category = @Category)
        AND (
            @StartDate IS NULL OR @EndDate IS NULL
            OR ([Date] BETWEEN @StartDate AND @EndDate)
        );
END
GO
