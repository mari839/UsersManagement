USE Users;
GO

/*
CREATE TABLE Users( 
 [Id] [int] IDENTITY(100,1) PRIMARY KEY,
 [FirstName] [varchar](100) NULL,
 [LastName] [varchar](50) NULL,
 [Email] [varchar](50) NULL,
 [Password] [varchar](50) NULL,
)
GO

CREATE TABLE Accounts( 
 [Id] [int] IDENTITY(100,1) PRIMARY KEY,
 [AccountNumber] [varchar](100) NULL,
 [Sum] int NULL,
 [UserId] int NOT NULL,
)
GO
*/

/*
ALTER TABLE Accounts WITH NOCHECK
  ADD FOREIGN KEY ([UserId]) REFERENCES Users (Id)
GO
*/


Create Procedure sp_AddUsers
@FirstName nvarchar(100),
@LastName nvarchar(50),
@Email nvarchar(50),
@Password nvarchar(50),
@AccountNumber nvarchar(100),
@Amount int
as 
Begin 
Insert into [Users].[dbo].[Users] values(@FirstName, @LastName, @Email, @Password)
Insert into [Users].[dbo].[Accounts] (UserId, AccountNumber, Amount) VALUES (SCOPE_IDENTITY(), @AccountNumber, @Amount);
End


--DROP PROCEDURE sp_AddUsers;

SELECT Id, FirstName, LastName, Email FROM [Users].[dbo].[Users];


Create Procedure sp_TransferMoneyToSomeone
@SourceAccountNumber NVARCHAR(100),
    @DestinationAccountNumber NVARCHAR(100),
    @Amount DECIMAL(18, 2)
AS
BEGIN
    UPDATE [Users].[dbo].[Accounts] 
    SET Amount = Amount - @Amount
    WHERE AccountNumber = @SourceAccountNumber
      AND Amount >= @Amount;

    UPDATE [Users].[dbo].[Accounts] 
    SET Amount = Amount + @Amount
    WHERE AccountNumber = @DestinationAccountNumber;

END;

DROP PROCEDURE sp_TransferMoneyToSomeone;




CREATE PROCEDURE sp_GetAmountByAccountNumber
    @UserInputAccountNumber NVARCHAR(100),
    @Amount DECIMAL(18, 2) OUTPUT
AS
BEGIN
    SELECT @Amount = Amount
    FROM [Users].[dbo].[Accounts]
    WHERE AccountNumber = @UserInputAccountNumber;
END;

--DROP PROCEDURE sp_GetAmountByAccountNumber;




CREATE PROCEDURE sp_WithdrawMoneyFromAccount
    @UserInputAccountNumber NVARCHAR(100),
    @WithdrawalAmount DECIMAL(18, 2)
AS
BEGIN
    DECLARE @AccountId INT;

    SELECT @AccountId = Id FROM [Users].[dbo].[Accounts] WHERE AccountNumber = @UserInputAccountNumber;

    IF @AccountId IS NOT NULL
    BEGIN
        -- Check if there is enough balance in the account
        IF (SELECT Amount FROM [Users].[dbo].[Accounts] WHERE Id = @AccountId) >= @WithdrawalAmount
        BEGIN
            -- Perform the withdrawal
            UPDATE [Users].[dbo].[Accounts] SET Amount = Amount - @WithdrawalAmount WHERE Id = @AccountId; 
        END
    END
END;

--DROP PROCEDURE sp_WithdrawMoney;




CREATE PROCEDURE sp_DepositMoney
    @UserInputAccountNumber NVARCHAR(100),
    @DepositAmount DECIMAL(18, 2)
AS
BEGIN
    DECLARE @AccountId INT;

    SELECT @AccountId = Id FROM [Users].[dbo].[Accounts] WHERE AccountNumber = @UserInputAccountNumber;

    IF @AccountId IS NOT NULL
    BEGIN
        UPDATE [Users].[dbo].[Accounts] SET Amount = Amount + @DepositAmount WHERE Id = @AccountId;
		End;
END;

--DROP PROCEDURE sp_DepositMoney;

