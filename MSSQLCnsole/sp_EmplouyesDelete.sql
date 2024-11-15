create PROCEDURE [dbo].[sp_EmplouyesDelete]
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


  delete from Employees where EmployeeID = @EmployeeID;  

END
