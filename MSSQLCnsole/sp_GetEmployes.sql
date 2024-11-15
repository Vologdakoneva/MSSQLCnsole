CREATE PROCEDURE [dbo].[sp_GetEmployes]
	-- Add the parameters for the stored procedure here
	@id int 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT em.EmployeeID, em.FirstName, em.LastName, em.Email, em.DateOfBirth, em.salary
	 from Employees em
    where em.EmployeeID = @id or @id=0
END
