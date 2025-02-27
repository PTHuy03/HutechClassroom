USE [ProjectCS]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoleClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetRoles](
	[Id] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](256) NULL,
	[NormalizedName] [nvarchar](256) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetRoles] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserClaims](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [nvarchar](450) NOT NULL,
	[ClaimType] [nvarchar](max) NULL,
	[ClaimValue] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](128) NOT NULL,
	[ProviderKey] [nvarchar](128) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserRoles](
	[UserId] [nvarchar](450) NOT NULL,
	[RoleId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUsers](
	[Id] [nvarchar](450) NOT NULL,
	[UserName] [nvarchar](256) NULL,
	[NormalizedUserName] [nvarchar](256) NULL,
	[Email] [nvarchar](256) NULL,
	[NormalizedEmail] [nvarchar](256) NULL,
	[EmailConfirmed] [bit] NOT NULL,
	[PasswordHash] [nvarchar](max) NULL,
	[SecurityStamp] [nvarchar](max) NULL,
	[ConcurrencyStamp] [nvarchar](max) NULL,
	[PhoneNumber] [nvarchar](max) NULL,
	[PhoneNumberConfirmed] [bit] NOT NULL,
	[TwoFactorEnabled] [bit] NOT NULL,
	[LockoutEnd] [datetimeoffset](7) NULL,
	[LockoutEnabled] [bit] NOT NULL,
	[AccessFailedCount] [int] NOT NULL,
	[FullName] [nvarchar](450) NOT NULL,
	[AvatarPath] [nvarchar](256) NULL,
	[IsPassword] [bit] NOT NULL,
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](128) NOT NULL,
	[Name] [nvarchar](128) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Assign]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Assign](
	[AssignId] [nvarchar](450) NOT NULL,
	[AssignName] [nvarchar](450) NOT NULL,
	[Description] [nvarchar](450) NULL,
	[Posttime] [datetime] NOT NULL,
	[AssignFile1] [nvarchar](450) NULL,
	[AssignFile2] [nvarchar](450) NULL,
	[ClassId] [nvarchar](450) NOT NULL,
	[LoaiId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Assign] PRIMARY KEY CLUSTERED 
(
	[AssignId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Class]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Class](
	[ClassId] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](150) NOT NULL,
	[Titlle] [nvarchar](50) NULL,
	[Topic] [nvarchar](50) NULL,
	[Room] [nvarchar](50) NULL,
	[Image] [nvarchar](255) NULL,
 CONSTRAINT [PK_Class] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListAssign]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListAssign](
	[UserId] [nvarchar](450) NOT NULL,
	[AssignId] [nvarchar](450) NOT NULL,
	[LoaiId] [nvarchar](450) NOT NULL,
	[Point] [decimal](18, 2) NULL,
 CONSTRAINT [PK_ListAssign] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[AssignId] ASC,
	[LoaiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListFile]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListFile](
	[UserId] [nvarchar](450) NOT NULL,
	[AssignId] [nvarchar](450) NOT NULL,
	[LoaiId] [nvarchar](450) NOT NULL,
	[FileId] [nvarchar](450) NOT NULL,
	[FilePath] [nvarchar](450) NULL,
	[SubmissTime] [datetime] NULL,
 CONSTRAINT [PK_ListFile] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[AssignId] ASC,
	[LoaiId] ASC,
	[FileId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ListStudent]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ListStudent](
	[UserId] [nvarchar](450) NOT NULL,
	[ClassId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_ListStudent] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[ClassId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Loai]    Script Date: 6/7/2024 11:51:35 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Loai](
	[LoaiId] [nvarchar](450) NOT NULL,
	[LoaiName] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_Loai] PRIMARY KEY CLUSTERED 
(
	[LoaiId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'00000000000000_CreateIdentitySchema', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240514061137_v1', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240514064732_v2', N'8.0.4')
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20240531135058_v3', N'8.0.4')
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'1', N'Adm', N'ADM', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'2', N'Teacher', N'TEACHER', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'3', N'Student', N'STUDENT', NULL)
INSERT [dbo].[AspNetRoles] ([Id], [Name], [NormalizedName], [ConcurrencyStamp]) VALUES (N'4', N'Admin', N'ADMIN', NULL)
GO
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'05b30b34-6925-4ff6-8924-7c791c1b215b', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'359df117-b9d7-4bc1-a6c9-c46177985ce6', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'5468951c-1643-487a-adf0-ed74b85825d4', N'4')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'822922f9-dce6-42ba-9a9e-dda8929bd11a', N'2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'93aa97d3-ec9f-47b0-a380-d342b00ba592', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'2')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b3bc9a42-95cc-4a02-8f2c-d4afae099038', N'3')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'dfa078f9-d5a2-4e6d-813b-f350789d539e', N'3')
GO
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName], [AvatarPath], [IsPassword]) VALUES (N'05b30b34-6925-4ff6-8924-7c791c1b215b', N'chienthanacquy2000@gmail.com', N'CHIENTHANACQUY2000@GMAIL.COM', N'chienthanacquy2000@gmail.com', N'CHIENTHANACQUY2000@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEPAQm0Q9Fbjt4xs24JWau/ktJsBmWOn7Y7zEmQvwfmNM5fQrDbbNSZe6NlBbL08qoA==', N'3UGFHV4Q7PPC56ODMPKL4LNMEUB4PGWX', N'c2ac91b2-d566-4fd6-bc1a-b07063e99f39', NULL, 0, 0, NULL, 1, 0, N'Lê Cảnh Đôn', NULL, 1)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName], [AvatarPath], [IsPassword]) VALUES (N'359df117-b9d7-4bc1-a6c9-c46177985ce6', N'nguyentinh692093@gmail.com', N'NGUYENTINH692093@GMAIL.COM', N'nguyentinh692093@gmail.com', N'NGUYENTINH692093@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEBbPXI7PNJw1pA2Zn6gn2D/ERWi+Y772LbBsyTBHjBeZ4zvPx68TVmVnhw4YK+W32Q==', N'SFDBFZDCXOSDPH3NYFG6QH5F66AMOZQA', N'7a6d87dd-6703-4dd6-9a94-dee73557bb7d', NULL, 0, 0, NULL, 1, 0, N'Nguyễn Trần Minh Tính', NULL, 1)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName], [AvatarPath], [IsPassword]) VALUES (N'5468951c-1643-487a-adf0-ed74b85825d4', N'richard.dataprotection@gmail.com', N'ADMIN1@GMAIL.COM', N'richard.dataprotection@gmail.com', N'ADMIN1@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEMUP83ORBMcBWJUZBkLFxZrKaD2dmUte+0duB+BnrUN39sWLMu1vNu9IcVufom977w==', N'S522PTCPZ6POIQOVI7MOC5TRRHMUKDSU', N'ee47eee4-9756-4656-ba1d-558409d40cd4', NULL, 0, 0, NULL, 1, 0, N'Admin', N'2e337499-fede-4f09-961a-0ac67d713ddc_1.jpg', 1)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName], [AvatarPath], [IsPassword]) VALUES (N'822922f9-dce6-42ba-9a9e-dda8929bd11a', N'khoaaccai@gmail.com', N'KHOAACCAI@GMAIL.COM', N'khoaaccai@gmail.com', N'KHOAACCAI@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEOtBcCaw2J56SG6fOunEnhTTbEVCRBrEqA1zguAJGkt9K5asqVGfZ4a3vPAx4omUDw==', N'B24TSEYQDPB756EXWMZNVONFXS2RXSXH', N'a385ef5c-5a31-4ecc-a6c9-c3c1e795d625', NULL, 0, 0, NULL, 1, 0, N'Giảng viên Hutech 2', NULL, 1)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName], [AvatarPath], [IsPassword]) VALUES (N'93aa97d3-ec9f-47b0-a380-d342b00ba592', N'mt.minhtinh2003@gmail.com', N'MT.MINHTINH2003@GMAIL.COM', N'mt.minhtinh2003@gmail.com', N'MT.MINHTINH2003@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEEsPObcNTYTXjX/uninfASF6SdXFeJ0+YkcsHvd7fEWlTUMfwusGlRIKWmv3eF/FBQ==', N'BXSRHE5FPH2WLR5DHYNH5EIVGG5ZJGOW', N'289afd4b-72b0-4e1d-a9c6-2ee169efd368', NULL, 0, 0, NULL, 1, 0, N'Bùi An Nhiên', NULL, 1)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName], [AvatarPath], [IsPassword]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'pthphicong373@gmail.com', N'PTHPHICONG373@GMAIL.COM', N'pthphicong373@gmail.com', N'PTHPHICONG373@GMAIL.COM', 0, N'AQAAAAIAAYagAAAAEJHGXfjexlOZyPwBzd9ffz/MIBb1nGL5cpexE4wo7tRLl4O2L8Hen2zJ45bEGhT1vQ==', N'GCZYBHJUDNG77N2TK5HTPNWSA7SKRCM3', N'8bab47d7-c400-4dc9-b2d4-4d0431caec60', NULL, 0, 0, NULL, 1, 0, N'Giảng viên Hutech 1', NULL, 1)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName], [AvatarPath], [IsPassword]) VALUES (N'b3bc9a42-95cc-4a02-8f2c-d4afae099038', N'pthuy200307@gmail.com', N'PTHUY200307@GMAIL.COM', N'pthuy200307@gmail.com', N'PTHUY200307@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEKce6+PD3ONVyXwqtYRwgfHzIO9eqiQs2MID01c8AcRV6iQC0BExOn/QK+HK6YFhNQ==', N'YVIUBZBIV4VLUZ2LKYDL5ORBJ73UPYL6', N'fa96e300-1c7d-45b3-b5b8-dab6479c217a', NULL, 0, 0, NULL, 1, 0, N'Phan Trọng Huy', NULL, 1)
INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount], [FullName], [AvatarPath], [IsPassword]) VALUES (N'dfa078f9-d5a2-4e6d-813b-f350789d539e', N'phamquoccuong.bmo@gmail.com', N'PHAMQUOCCUONG.BMO@GMAIL.COM', N'phamquoccuong.bmo@gmail.com', N'PHAMQUOCCUONG.BMO@GMAIL.COM', 1, N'AQAAAAIAAYagAAAAEJWCEIpY69A5QBdjbQlHsxS6hZjHc45YkxgVCA6UTz3SXP9X1DnN0K85lGgJbaK9hQ==', N'LO7EM5CS4IJD7ZL7DHWBEYBUZEXL5GKY', N'926c28ca-e041-489b-af15-ad4adfc3e166', NULL, 0, 0, NULL, 1, 0, N'Phạm Quốc Cường', NULL, 1)
GO
INSERT [dbo].[Assign] ([AssignId], [AssignName], [Description], [Posttime], [AssignFile1], [AssignFile2], [ClassId], [LoaiId]) VALUES (N'AS01', N'Làm quen Ngôn ngữ python', NULL, CAST(N'2024-05-23T15:21:21.000' AS DateTime), NULL, NULL, N'CLS002', N'2')
INSERT [dbo].[Assign] ([AssignId], [AssignName], [Description], [Posttime], [AssignFile1], [AssignFile2], [ClassId], [LoaiId]) VALUES (N'CHJTF', N'Làm quen với visual', N'Tải visual studio', CAST(N'2024-05-23T13:24:54.000' AS DateTime), NULL, NULL, N'OSDSK', N'2')
GO
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'8LE6W', N'Nhập môn hệ điều hành', N'Thực hành trên cisco', N'21DTHE2', N'E1.7.6', N'hinh-nen-don-gian-7.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS001', N'Lớp Toán Căn Bản', N'Toán Căn Bản A', N'Mathematics', N'Phòng A101', N'hinh-nen-don-gian-1.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS002', N'Lớp Lập Trình Python', N'Python Programming', N'Computer Science', N'Phòng B203', N'hinh-nen-don-gian-2.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS003', N'Lớp Hóa Học Hữu Cơ', N'Hóa Học Hữu Cơ', N'Chemistry', N'Phòng C305', N'hinh-nen-don-gian-3.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS004', N'Lớp Tiếng Anh Giao Tiếp', N'Tiếng Anh Giao Tiếp', N'English', N'Phòng D402', N'hinh-nen-don-gian-4.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS005', N'Lớp Vật Lý Cơ Bản', N'Vật Lý Cơ Bản A', N'Physics', N'Phòng E501', N'hinh-nen-don-gian-5.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS006', N'Lớp Quản Trị Kinh Doanh', N'Quản Trị Kinh Doanh', N'Business Management', N'Phòng F601', N'hinh-nen-don-gian-6.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS007', N'Lớp Lịch Sử Thế Giới', N'Lịch Sử Thế Giới', N'History', N'Phòng G701', N'hinh-nen-don-gian-7.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS008', N'Lớp Sinh Học Môi Trường', N'Sinh Học Môi Trường', N'Environmental Biology', N'Phòng H801', N'hinh-nen-don-gian-8.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS009', N'Lớp Ngôn Ngữ Học', N'Ngôn Ngữ Học', N'Linguistics', N'Phòng I901', N'hinh-nen-don-gian-9.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS010', N'Lớp Địa Lý Học', N'Địa Lý Học', N'Geography', N'Phòng J1001', N'hinh-nen-don-gian-5.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS011', N'Lớp Nghệ Thuật Ẩm Thực', N'Nghệ Thuật Ẩm Thực', N'Culinary Arts', N'Phòng K1101', N'hinh-nen-don-gian-9.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS012', N'Lớp Công Nghệ Sinh Học', N'Công Nghệ Sinh Học', N'Biotechnology', N'Phòng L1201', N'hinh-nen-don-gian-8.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS013', N'Lớp Kỹ Thuật Điện Tử', N'Kỹ Thuật Điện Tử', N'Electronics Engineering', N'Phòng M1301', N'hinh-nen-don-gian-7.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'CLS014', N'Lớp Thể Dục Thể Thao', N'Thể Dục Thể Thao', N'Physical Education', N'Phòng N1401', N'hinh-nen-don-gian-6.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'LT23J', N'Nhập môn kiến trúc máy tính', N'Cơ sở máy tính', NULL, N'E1.7.4', N'hinh-nen-don-gian-1.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'M9GAW', N'Tư tưởng Hồ Chí Minh', NULL, NULL, N'E2.3.6', N'hinh-nen-don-gian-8.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'NV9G6', N'Lap trinh di dong android', NULL, N'21DTHE2', N'E1.7.6', N'hinh-nen-don-gian-1.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'OSDSK', N'Lập trình Web', N'21DTHE1', N'Lập trình C#', N'E1.9.14', N'hinh-nen-don-gian-1.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'VAT9Q', N'Toán rời rạc', N'Đại cương CNTT', NULL, N'E2.2.6', N'hinh-nen-don-gian-5.jpg')
INSERT [dbo].[Class] ([ClassId], [Name], [Titlle], [Topic], [Room], [Image]) VALUES (N'VSC5E', N'Toán Cao Cấp', NULL, N'21DTHE2', N'E1.9.14', N'hinh-nen-don-gian-7.jpg')
GO
INSERT [dbo].[ListAssign] ([UserId], [AssignId], [LoaiId], [Point]) VALUES (N'359df117-b9d7-4bc1-a6c9-c46177985ce6', N'CHJTF', N'2', NULL)
INSERT [dbo].[ListAssign] ([UserId], [AssignId], [LoaiId], [Point]) VALUES (N'822922f9-dce6-42ba-9a9e-dda8929bd11a', N'AS01', N'2', NULL)
INSERT [dbo].[ListAssign] ([UserId], [AssignId], [LoaiId], [Point]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'AS01', N'2', NULL)
INSERT [dbo].[ListAssign] ([UserId], [AssignId], [LoaiId], [Point]) VALUES (N'b3bc9a42-95cc-4a02-8f2c-d4afae099038', N'AS01', N'2', NULL)
GO
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'05b30b34-6925-4ff6-8924-7c791c1b215b', N'CLS001')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'05b30b34-6925-4ff6-8924-7c791c1b215b', N'CLS002')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'05b30b34-6925-4ff6-8924-7c791c1b215b', N'CLS003')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'05b30b34-6925-4ff6-8924-7c791c1b215b', N'CLS004')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'05b30b34-6925-4ff6-8924-7c791c1b215b', N'CLS005')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'05b30b34-6925-4ff6-8924-7c791c1b215b', N'CLS006')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'359df117-b9d7-4bc1-a6c9-c46177985ce6', N'CLS001')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'359df117-b9d7-4bc1-a6c9-c46177985ce6', N'CLS002')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'359df117-b9d7-4bc1-a6c9-c46177985ce6', N'CLS003')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'359df117-b9d7-4bc1-a6c9-c46177985ce6', N'CLS004')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'359df117-b9d7-4bc1-a6c9-c46177985ce6', N'CLS005')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'359df117-b9d7-4bc1-a6c9-c46177985ce6', N'CLS006')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'359df117-b9d7-4bc1-a6c9-c46177985ce6', N'OSDSK')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'822922f9-dce6-42ba-9a9e-dda8929bd11a', N'CLS002')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'822922f9-dce6-42ba-9a9e-dda8929bd11a', N'LT23J')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'822922f9-dce6-42ba-9a9e-dda8929bd11a', N'M9GAW')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'822922f9-dce6-42ba-9a9e-dda8929bd11a', N'OSDSK')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'822922f9-dce6-42ba-9a9e-dda8929bd11a', N'VAT9Q')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'93aa97d3-ec9f-47b0-a380-d342b00ba592', N'CLS001')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'93aa97d3-ec9f-47b0-a380-d342b00ba592', N'CLS002')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'93aa97d3-ec9f-47b0-a380-d342b00ba592', N'CLS003')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'93aa97d3-ec9f-47b0-a380-d342b00ba592', N'CLS004')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'93aa97d3-ec9f-47b0-a380-d342b00ba592', N'CLS005')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'93aa97d3-ec9f-47b0-a380-d342b00ba592', N'CLS006')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'8LE6W')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'CLS001')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'CLS002')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'CLS003')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'CLS004')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'CLS005')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'CLS006')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'CLS007')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'CLS008')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'CLS009')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'CLS010')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'CLS011')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'NV9G6')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b0e607ad-b6f9-4023-8cc9-ea59a145b0bf', N'VSC5E')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b3bc9a42-95cc-4a02-8f2c-d4afae099038', N'CLS001')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b3bc9a42-95cc-4a02-8f2c-d4afae099038', N'CLS002')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b3bc9a42-95cc-4a02-8f2c-d4afae099038', N'CLS003')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b3bc9a42-95cc-4a02-8f2c-d4afae099038', N'CLS004')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'b3bc9a42-95cc-4a02-8f2c-d4afae099038', N'NV9G6')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'dfa078f9-d5a2-4e6d-813b-f350789d539e', N'CLS001')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'dfa078f9-d5a2-4e6d-813b-f350789d539e', N'CLS002')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'dfa078f9-d5a2-4e6d-813b-f350789d539e', N'CLS003')
INSERT [dbo].[ListStudent] ([UserId], [ClassId]) VALUES (N'dfa078f9-d5a2-4e6d-813b-f350789d539e', N'CLS004')
GO
INSERT [dbo].[Loai] ([LoaiId], [LoaiName]) VALUES (N'1', N'Thông báo')
INSERT [dbo].[Loai] ([LoaiId], [LoaiName]) VALUES (N'2', N'Bài tập')
INSERT [dbo].[Loai] ([LoaiId], [LoaiName]) VALUES (N'3', N'Câu hỏi')
GO
ALTER TABLE [dbo].[AspNetUsers] ADD  DEFAULT (CONVERT([bit],(0))) FOR [IsPassword]
GO
ALTER TABLE [dbo].[AspNetRoleClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetRoleClaims] CHECK CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserClaims]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserClaims] CHECK CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserLogins]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserLogins] CHECK CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY([RoleId])
REFERENCES [dbo].[AspNetRoles] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId]
GO
ALTER TABLE [dbo].[AspNetUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserRoles] CHECK CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[AspNetUserTokens]  WITH CHECK ADD  CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[AspNetUserTokens] CHECK CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[Assign]  WITH CHECK ADD  CONSTRAINT [FK_Assign_Class_ClassId] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([ClassId])
GO
ALTER TABLE [dbo].[Assign] CHECK CONSTRAINT [FK_Assign_Class_ClassId]
GO
ALTER TABLE [dbo].[Assign]  WITH CHECK ADD  CONSTRAINT [FK_Assign_Loai_LoaiId] FOREIGN KEY([LoaiId])
REFERENCES [dbo].[Loai] ([LoaiId])
GO
ALTER TABLE [dbo].[Assign] CHECK CONSTRAINT [FK_Assign_Loai_LoaiId]
GO
ALTER TABLE [dbo].[ListAssign]  WITH CHECK ADD  CONSTRAINT [FK_ListAssign_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ListAssign] CHECK CONSTRAINT [FK_ListAssign_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[ListAssign]  WITH CHECK ADD  CONSTRAINT [FK_ListAssign_Assign_AssignId] FOREIGN KEY([AssignId])
REFERENCES [dbo].[Assign] ([AssignId])
GO
ALTER TABLE [dbo].[ListAssign] CHECK CONSTRAINT [FK_ListAssign_Assign_AssignId]
GO
ALTER TABLE [dbo].[ListFile]  WITH CHECK ADD  CONSTRAINT [FK_ListFile_ListAssign_UserId_AssignId_LoaiId] FOREIGN KEY([UserId], [AssignId], [LoaiId])
REFERENCES [dbo].[ListAssign] ([UserId], [AssignId], [LoaiId])
GO
ALTER TABLE [dbo].[ListFile] CHECK CONSTRAINT [FK_ListFile_ListAssign_UserId_AssignId_LoaiId]
GO
ALTER TABLE [dbo].[ListStudent]  WITH CHECK ADD  CONSTRAINT [FK_ListStudent_AspNetUsers_UserId] FOREIGN KEY([UserId])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO
ALTER TABLE [dbo].[ListStudent] CHECK CONSTRAINT [FK_ListStudent_AspNetUsers_UserId]
GO
ALTER TABLE [dbo].[ListStudent]  WITH CHECK ADD  CONSTRAINT [FK_ListStudent_Class_ClassId] FOREIGN KEY([ClassId])
REFERENCES [dbo].[Class] ([ClassId])
GO
ALTER TABLE [dbo].[ListStudent] CHECK CONSTRAINT [FK_ListStudent_Class_ClassId]
GO
