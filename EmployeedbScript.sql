USE [Employee]
GO
/****** Object:  Table [dbo].[Emp]    Script Date: 08-10-2023 15:25:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Emp](
	[EmpId] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [varchar](50) NULL,
	[LastName] [varchar](50) NULL,
	[PhoneNumber] [varchar](50) NULL,
	[Email] [varchar](50) NULL,
	[Salary] [float] NULL,
	[CreatedOn] [datetime] NULL,
	[UpdatedOn] [datetime] NULL,
 CONSTRAINT [PK_Emp] PRIMARY KEY CLUSTERED 
(
	[EmpId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Emp] ON 

INSERT [dbo].[Emp] ([EmpId], [FirstName], [LastName], [PhoneNumber], [Email], [Salary], [CreatedOn], [UpdatedOn]) VALUES (1, N'Will', N'Marker', N'1234567890', N'will@gmail.com', 15000, CAST(N'2023-10-07T19:30:43.067' AS DateTime), CAST(N'2023-10-07T20:02:52.303' AS DateTime))
INSERT [dbo].[Emp] ([EmpId], [FirstName], [LastName], [PhoneNumber], [Email], [Salary], [CreatedOn], [UpdatedOn]) VALUES (2, N'joe', N'doe', N'0987654321', N'joe@gmail.com', 20000, CAST(N'2023-10-08T10:32:21.257' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Emp] OFF
GO
