CREATE PROCEDURE [dbo].[SP_STORE_USER_DATA]
(
@Id VARCHAR(MAX),
@Name VARCHAR(100),
@Gender INT,
@Age INT,
@Country VARCHAR(100),
@City VARCHAR(100),
@Result   VARCHAR(100)    OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON
	
    DECLARE @ID_FOUND VARCHAR(MAX)
	DECLARE @ID_IDENTIFIER UNIQUEIDENTIFIER

    SET @ID_FOUND = (
        SELECT Id
          FROM [dbo].[TBL_USERS] 
         WHERE Name = @Name
           AND Gender = @Gender
           AND Age = @Age
           AND Country = @Country
           AND City = @City
    )

    IF @ID_FOUND IS NULL
	BEGIN
		BEGIN TRY
		
			SET @ID_IDENTIFIER = (SELECT CAST(@Id AS UNIQUEIDENTIFIER))

			INSERT INTO [dbo].[TBL_USERS] (Id, Name, Gender, Age, Country, City)
			VALUES (@ID_IDENTIFIER, @Name, @Gender, @Age, @Country, @city)

			SET @Result = 'The data was Stored with success!'

			SELECT @Result AS Result

			RETURN

		END TRY
		BEGIN CATCH

			SET @Result = 'Error to Store Data, details: ' + CONVERT(VARCHAR, ERROR_NUMBER(), 1) + ': '+ ERROR_MESSAGE()

			SELECT @Result AS Result

			RETURN

		END CATCH
    END
    ELSE
    BEGIN
		BEGIN TRY

			UPDATE [dbo].[TBL_USERS]
			   SET 
				   Name = @Name, 
				   Gender = @Gender, 
				   Age = @Age, 
			       Country = @Country, 
				   City = @City
			 WHERE Id = @ID_FOUND
        
			SET @Result = 'The data was Updated with success!'

			SELECT @Result AS Result
			
			RETURN

		END TRY
		BEGIN CATCH

			SET @Result = 'Error to Update Data, details: ' + CONVERT(VARCHAR, ERROR_NUMBER(), 1) + ': '+ ERROR_MESSAGE()

			SELECT @Result AS Result
			
			RETURN

		END CATCH
    END    
END