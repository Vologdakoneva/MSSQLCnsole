CREATE TABLE [dbo].[Employees](
	[EmployeeID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nchar](50) NULL,
	[LastName] [nchar](50) NULL,
	[Email] [nchar](100) NULL,
	[DateOfBirth] [date] NULL,
	[Salary] [decimal](18, 0) NULL
) ON [PRIMARY]



