CREATE PROCEDURE [dbo].[sp_EmplouyesUpdate]
	-- Add the parameters for the stored procedure here

	@EmployeeID [int],
	@FirstName [nchar](50) NULL,
	@LastName [nchar](50) NULL,
	@Email [nchar](100) NULL,
	@DateOfBirth [date] NULL,
	@Salary [decimal](18, 0) NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


  update Employees set 
   FirstName=@FirstName,LastName=@LastName, Email=@Email, DateOfBirth=@DateOfBirth, Salary=@Salary	
  where EmployeeID =  @EmployeeID; 
         
END
