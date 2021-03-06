
CREATE DATABASE [SoCarroVeiculos]
GO

USE [SoCarroVeiculos]
GO


/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 10/02/2022 05:22:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/****** Object:  Table [dbo].[AspNetRoleClaims]    Script Date: 10/02/2022 05:22:41 ******/
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
/****** Object:  Table [dbo].[AspNetRoles]    Script Date: 10/02/2022 05:22:41 ******/
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
/****** Object:  Table [dbo].[AspNetUserClaims]    Script Date: 10/02/2022 05:22:41 ******/
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
/****** Object:  Table [dbo].[AspNetUserLogins]    Script Date: 10/02/2022 05:22:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserLogins](
	[LoginProvider] [nvarchar](450) NOT NULL,
	[ProviderKey] [nvarchar](450) NOT NULL,
	[ProviderDisplayName] [nvarchar](max) NULL,
	[UserId] [nvarchar](450) NOT NULL,
 CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY CLUSTERED 
(
	[LoginProvider] ASC,
	[ProviderKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserRoles]    Script Date: 10/02/2022 05:22:41 ******/
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
/****** Object:  Table [dbo].[AspNetUsers]    Script Date: 10/02/2022 05:22:41 ******/
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
 CONSTRAINT [PK_AspNetUsers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AspNetUserTokens]    Script Date: 10/02/2022 05:22:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AspNetUserTokens](
	[UserId] [nvarchar](450) NOT NULL,
	[LoginProvider] [nvarchar](450) NOT NULL,
	[Name] [nvarchar](450) NOT NULL,
	[Value] [nvarchar](max) NULL,
 CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC,
	[LoginProvider] ASC,
	[Name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

INSERT [dbo].[AspNetUsers] ([Id], [UserName], [NormalizedUserName], [Email], [NormalizedEmail], [EmailConfirmed], [PasswordHash], [SecurityStamp], [ConcurrencyStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEnd], [LockoutEnabled], [AccessFailedCount]) VALUES (N'8e570a7f-2ef1-4a78-a45b-4724e1a35ba3', N'teste@teste.com', N'TESTE@TESTE.COM', N'teste@teste.com', N'TESTE@TESTE.COM', 1, N'AQAAAAEAACcQAAAAEN7B5FPIixLO8lAiA9mvSYEH8T6AwLdCpokYH+FFgjcpX2R1izQeguahXKP4HcUC5Q==', N'FEZPD2III3O6O5ZTFU6TW7C62OMU6RGX', N'e9757915-8b89-4f0d-a54b-ae1a3b1bee67', NULL, 0, 0, NULL, 1, 0)
GO
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] ON 
GO
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (1, N'8e570a7f-2ef1-4a78-a45b-4724e1a35ba3', N'Marca', N'CRUD')
GO
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (2, N'8e570a7f-2ef1-4a78-a45b-4724e1a35ba3', N'Veiculo', N'CRUD')
GO
INSERT [dbo].[AspNetUserClaims] ([Id], [UserId], [ClaimType], [ClaimValue]) VALUES (3, N'8e570a7f-2ef1-4a78-a45b-4724e1a35ba3', N'Proprietario', N'CRUD')
GO
SET IDENTITY_INSERT [dbo].[AspNetUserClaims] OFF
GO

 
--DROP TABLE Modelo;
--DROP TABLE Veiculo;
--DROP TABLE Endereco;
--DROP TABLE Proprietario;
--DROP TABLE Marca;
--DELETE FROM  __EFMigrationsHistory WHERE MigrationId = '20220212150125_TabelasIniciais';


IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Marca] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(50) NOT NULL,
    [Status] int NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    CONSTRAINT [PK_Marca] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Proprietario] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(50) NOT NULL,
    [Documento] varchar(14) NOT NULL,
    [Status] int NOT NULL,
    [TipoProprietario] int NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [Email] varchar(250) NOT NULL,
    CONSTRAINT [PK_Proprietario] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Endereco] (
    [Id] uniqueidentifier NOT NULL,
    [Logradouro] varchar(200) NOT NULL,
    [Numero] varchar(20) NOT NULL,
    [Complemento] varchar(250) NOT NULL,
    [Bairro] varchar(100) NOT NULL,
    [Cep] varchar(20) NOT NULL,
    [Cidade] varchar(100) NOT NULL,
    [Estado] varchar(50) NOT NULL,
    [ProprietarioId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Endereco] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Endereco_Proprietario_ProprietarioId] FOREIGN KEY ([ProprietarioId]) REFERENCES [Proprietario] ([Id])
);
GO

CREATE TABLE [Veiculo] (
    [Id] uniqueidentifier NOT NULL,
    [Nome] varchar(50) NOT NULL,
    [Renavam] varchar(11) NOT NULL,
    [Quilometragem] int NOT NULL,
    [Valor] decimal(18,2) NOT NULL,
    [Status] int NOT NULL,
    [DataCadastro] datetime2 NOT NULL,
    [MarcaId] uniqueidentifier NOT NULL,
    [ProprietarioId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Veiculo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Veiculo_Marca_MarcaId] FOREIGN KEY ([MarcaId]) REFERENCES [Marca] ([Id]),
    CONSTRAINT [FK_Veiculo_Proprietario_ProprietarioId] FOREIGN KEY ([ProprietarioId]) REFERENCES [Proprietario] ([Id])
);
GO

CREATE TABLE [Modelo] (
    [Id] uniqueidentifier NOT NULL,
    [Descricao] varchar(50) NOT NULL,
    [AnoFabricacao] int NOT NULL,
    [AnoModelo] int NOT NULL,
    [VeiculoId] uniqueidentifier NOT NULL,
    CONSTRAINT [PK_Modelo] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Modelo_Veiculo_VeiculoId] FOREIGN KEY ([VeiculoId]) REFERENCES [Veiculo] ([Id])
);
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataCadastro', N'Nome', N'Status') AND [object_id] = OBJECT_ID(N'[Marca]'))
    SET IDENTITY_INSERT [Marca] ON;
INSERT INTO [Marca] ([Id], [DataCadastro], [Nome], [Status])
VALUES ('1962c218-a0dc-4746-b406-0bd79aa6e5a8', '2022-02-12T11:01:25.5972529-04:00', 'Ford', 0),
('879d7c81-9841-4469-b71d-ecd09ae07fdf', '2022-02-12T11:01:25.5972531-04:00', 'Honda', 0),
('b358b8dd-be36-4afe-8c6b-4ce7438a434a', '2022-02-12T11:01:25.5972509-04:00', 'Volkswagen', 0),
('c30375f4-d336-4a5f-af87-f8bd87eabc6e', '2022-02-12T11:01:25.5972532-04:00', 'Hyundai', 0),
('ef2ae5a0-9b10-4372-a214-79ded3e0f5a3', '2022-02-12T11:01:25.5972528-04:00', 'Toyota', 0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataCadastro', N'Nome', N'Status') AND [object_id] = OBJECT_ID(N'[Marca]'))
    SET IDENTITY_INSERT [Marca] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataCadastro', N'Documento', N'Email', N'Nome', N'Status', N'TipoProprietario') AND [object_id] = OBJECT_ID(N'[Proprietario]'))
    SET IDENTITY_INSERT [Proprietario] ON;
INSERT INTO [Proprietario] ([Id], [DataCadastro], [Documento], [Email], [Nome], [Status], [TipoProprietario])
VALUES ('47c477a8-3063-4df1-be95-1d8919850f40', '0001-01-01T00:00:00.0000000', '80292613067', 'jose@socarro.com.br', 'Zé do Carro', 0, 1),
('5f535587-ea67-4395-aca1-47261934ac84', '0001-01-01T00:00:00.0000000', '66689736040', 'tonho@socarro.com.br', 'Tonho do Carro', 0, 1),
('8ccca536-c35e-4eb2-9bdb-7b23f764ad92', '0001-01-01T00:00:00.0000000', '65695214033', 'maria.jose@socarro.com.br', 'Maria José', 0, 1),
('f499f8c0-15d8-4a79-b66a-35728712dea8', '0001-01-01T00:00:00.0000000', '66689736040', 'jose.maria@socarro.com.br', 'José Maria', 0, 1);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataCadastro', N'Documento', N'Email', N'Nome', N'Status', N'TipoProprietario') AND [object_id] = OBJECT_ID(N'[Proprietario]'))
    SET IDENTITY_INSERT [Proprietario] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Bairro', N'Cep', N'Cidade', N'Complemento', N'Estado', N'Logradouro', N'Numero', N'ProprietarioId') AND [object_id] = OBJECT_ID(N'[Endereco]'))
    SET IDENTITY_INSERT [Endereco] ON;
INSERT INTO [Endereco] ([Id], [Bairro], [Cep], [Cidade], [Complemento], [Estado], [Logradouro], [Numero], [ProprietarioId])
VALUES ('5c824376-c134-43cf-ac42-5ba1e3a1d9e1', 'Centro', '78098000', 'Cuiabá', 'Quadra 15', 'MT', 'Rua 10', '01', '47c477a8-3063-4df1-be95-1d8919850f40'),
('6d335490-c849-4792-919e-adb38dd964f2', 'Jardim Imperial', '78098000', 'Cuiabá', 'Quadra 70', 'MT', 'Rua 42', '395', '8ccca536-c35e-4eb2-9bdb-7b23f764ad92'),
('9c3fee74-05ae-42ec-b841-88a31c243401', 'Porto', '78098000', 'Cuiabá', 'Quadra 36', 'MT', 'Rua Brazil', '10', '5f535587-ea67-4395-aca1-47261934ac84'),
('b397ea69-31cf-48fb-9425-8222a95e1cc4', 'Jardim Universitário', '78098000', 'Cuiabá', 'Quadra 98', 'MT', 'Rua Orquideas', '01', 'f499f8c0-15d8-4a79-b66a-35728712dea8');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Bairro', N'Cep', N'Cidade', N'Complemento', N'Estado', N'Logradouro', N'Numero', N'ProprietarioId') AND [object_id] = OBJECT_ID(N'[Endereco]'))
    SET IDENTITY_INSERT [Endereco] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataCadastro', N'MarcaId', N'Nome', N'ProprietarioId', N'Quilometragem', N'Renavam', N'Status', N'Valor') AND [object_id] = OBJECT_ID(N'[Veiculo]'))
    SET IDENTITY_INSERT [Veiculo] ON;
INSERT INTO [Veiculo] ([Id], [DataCadastro], [MarcaId], [Nome], [ProprietarioId], [Quilometragem], [Renavam], [Status], [Valor])
VALUES ('1c667238-fe0e-4c7e-9519-94438d04ce8d', '0001-01-01T00:00:00.0000000', 'b358b8dd-be36-4afe-8c6b-4ce7438a434a', 'Voyage', '47c477a8-3063-4df1-be95-1d8919850f40', 50000, '74493153016', 0, 32000.0),
('228f3149-61d1-4e7e-96bf-29d27da650df', '0001-01-01T00:00:00.0000000', '879d7c81-9841-4469-b71d-ecd09ae07fdf', 'Sandero', '5f535587-ea67-4395-aca1-47261934ac84', 65000, '75106570228', 0, 55000.0),
('48ecbd3c-660b-42fb-90f4-38cc33e596ea', '0001-01-01T00:00:00.0000000', 'b358b8dd-be36-4afe-8c6b-4ce7438a434a', 'Gol', '5f535587-ea67-4395-aca1-47261934ac84', 15000, '76473056607', 0, 45000.0),
('50e2c51f-9684-42b2-8bb1-972ee8d22038', '0001-01-01T00:00:00.0000000', 'ef2ae5a0-9b10-4372-a214-79ded3e0f5a3', 'Corolla', '8ccca536-c35e-4eb2-9bdb-7b23f764ad92', 0, '91838212968', 0, 105000.0),
('8599fc54-30b3-4b7d-846b-00da02d5e5e1', '0001-01-01T00:00:00.0000000', '1962c218-a0dc-4746-b406-0bd79aa6e5a8', 'Palio', 'f499f8c0-15d8-4a79-b66a-35728712dea8', 118000, '83663783642', 0, 25000.0);
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'DataCadastro', N'MarcaId', N'Nome', N'ProprietarioId', N'Quilometragem', N'Renavam', N'Status', N'Valor') AND [object_id] = OBJECT_ID(N'[Veiculo]'))
    SET IDENTITY_INSERT [Veiculo] OFF;
GO

IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AnoFabricacao', N'AnoModelo', N'Descricao', N'VeiculoId') AND [object_id] = OBJECT_ID(N'[Modelo]'))
    SET IDENTITY_INSERT [Modelo] ON;
INSERT INTO [Modelo] ([Id], [AnoFabricacao], [AnoModelo], [Descricao], [VeiculoId])
VALUES ('489b69d4-5025-4526-a42c-68de204a1b08', 2020, 2021, 'Trend', '1c667238-fe0e-4c7e-9519-94438d04ce8d'),
('5f3c18db-d77f-424c-ba90-58e2de3ab2d9', 2022, 2022, 'Esportivo', '228f3149-61d1-4e7e-96bf-29d27da650df'),
('8d3bc0ee-f683-4f99-af82-c51f7c5d7aef', 2021, 2022, 'Verão', '48ecbd3c-660b-42fb-90f4-38cc33e596ea'),
('91c37cdb-fb51-4766-a23e-d204af8a9516', 2021, 2021, 'Confort line', '50e2c51f-9684-42b2-8bb1-972ee8d22038'),
('e77d018a-ab5e-404a-930a-1e9ee4f3792f', 2021, 2022, 'Inferno', '8599fc54-30b3-4b7d-846b-00da02d5e5e1');
IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'AnoFabricacao', N'AnoModelo', N'Descricao', N'VeiculoId') AND [object_id] = OBJECT_ID(N'[Modelo]'))
    SET IDENTITY_INSERT [Modelo] OFF;
GO

CREATE INDEX [IX_Endereco_ProprietarioId] ON [Endereco] ([ProprietarioId]);
GO

CREATE INDEX [IX_Modelo_VeiculoId] ON [Modelo] ([VeiculoId]);
GO

CREATE INDEX [IX_Veiculo_MarcaId] ON [Veiculo] ([MarcaId]);
GO

CREATE INDEX [IX_Veiculo_ProprietarioId] ON [Veiculo] ([ProprietarioId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220212150125_TabelasIniciais', N'6.0.1');
GO

COMMIT;
GO

