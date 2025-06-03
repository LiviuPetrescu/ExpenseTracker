-- Creates the ExpenseTracker database
-- Replace DB name if needed
IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'ExpenseTracker')
BEGIN
    CREATE DATABASE ExpenseTracker;
END
GO

USE ExpenseTracker;
GO
