USE foodie;


DROP TABLE IF EXISTS [dbo].[AspNetUserLogins];
DROP TABLE IF EXISTS [dbo].[AspNetUserClaims];
DROP TABLE IF EXISTS [dbo].[AspNetUserRoles];
DROP TABLE IF EXISTS [dbo].[AspNetUsers];
DROP TABLE IF EXISTS [dbo].[AspNetRoles];
DROP TABLE IF EXISTS [dbo].[AspNetProduct];
DROP TABLE IF EXISTS [dbo].[AspNetCategory];
DROP TABLE IF EXISTS [dbo].[AspNetCart];
DROP TABLE IF EXISTS [dbo].[AspNetOrder];
DROP TABLE IF EXISTS [dbo].[AspNetOrderDetail];
DROP TABLE IF EXISTS [dbo].[AspNetPaymentType];
DROP TABLE IF EXISTS [dbo].[AspNetPaymentMethod];

CREATE TABLE [dbo].[AspNetUsers] (
    [Id]                   NVARCHAR (128) NOT NULL,
	[FirstName]            NVARCHAR (256) NULL,
    [LastName]             NVARCHAR (256) NULL,
	[UserName]             NVARCHAR (256) NOT NULL,
    [Email]                NVARCHAR (256) NULL,
    [EmailConfirmed]       BIT            NOT NULL,
    [PasswordHash]         NVARCHAR (MAX) NULL,
    [SecurityStamp]        NVARCHAR (MAX) NULL,
    [PhoneNumber]          NVARCHAR (MAX) NULL,
    [PhoneNumberConfirmed] BIT            NOT NULL,
    [TwoFactorEnabled]     BIT            NOT NULL,
    [LockoutEndDateUtc]    DATETIME       NULL,
    [LockoutEnabled]       BIT            NOT NULL,
    [AccessFailedCount]    INT            NOT NULL,
   
    CONSTRAINT [PK_dbo.AspNetUsers] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_UserName_dbo.AspNetUsers] UNIQUE(UserName)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [UserNameIndex]
    ON [dbo].[AspNetUsers]([UserName] ASC);

CREATE TABLE [dbo].[AspNetRoles] (
    [Id]   NVARCHAR (128) NOT NULL,
    [Name] NVARCHAR (256) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetRoles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [RoleNameIndex] 
	ON [dbo].[AspNetRoles]([Name] ASC);

CREATE TABLE [dbo].[AspNetUserRoles] (
    [UserId] NVARCHAR (128) NOT NULL,
    [RoleId] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserRoles] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [dbo].[AspNetRoles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetUserRoles_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserRoles]([UserId] ASC);

GO
CREATE NONCLUSTERED INDEX [IX_RoleId]
    ON [dbo].[AspNetUserRoles]([RoleId] ASC);

CREATE TABLE [dbo].[AspNetUserLogins] (
    [LoginProvider] NVARCHAR (128) NOT NULL,
    [ProviderKey]   NVARCHAR (128) NOT NULL,
    [UserId]        NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetUserLogins] PRIMARY KEY CLUSTERED ([LoginProvider] ASC, [ProviderKey] ASC, [UserId] ASC),
    CONSTRAINT [FK_dbo.AspNetUserLogins_dbo.AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserLogins]([UserId] ASC);

CREATE TABLE [dbo].[AspNetUserClaims] (
    [Id]         INT IDENTITY (1, 1) NOT NULL,
    [UserId]     NVARCHAR (128) NOT NULL,
    [ClaimType]  NVARCHAR (MAX) NULL,
    [ClaimValue] NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_dbo.AspNetUserClaims] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetUserClaims_dbo.AspNetUsers_UserId] FOREIGN KEY (UserId) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE
);

GO
CREATE NONCLUSTERED INDEX [IX_UserId]
    ON [dbo].[AspNetUserClaims]([UserId] ASC);


CREATE TABLE [dbo].[AspNetCategory] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetCategory] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Name_dbo.AspNetCategory] UNIQUE (Name)
);


CREATE TABLE [dbo].[AspNetProduct] (
    [Id]       INT IDENTITY (1, 1) NOT NULL,
    [Name]     NVARCHAR (128) NOT NULL,
	[Price]    DECIMAL(12,4) NOT NULL,
	[Quantity] INT NOT NULL DEFAULT 0,
	[Rating]   INT NOT NULL DEFAULT 0,
	[ImageUrl] NVARCHAR(256) NOT NULL,
	[CategoryId] INT NOT NULL,
    CONSTRAINT [PK_dbo.AspNetProduct] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [UK_Name_dbo.AspNetProduct] UNIQUE (Name),
	CONSTRAINT [FK_dbo.AspNetProduct_dbo.AspNetCategory] FOREIGN KEY (CategoryId) REFERENCES [dbo].[AspNetCategory] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [CK_Rating_dbo.AspNetProduct] CHECK (Rating >= 0 AND Rating <=5 )
);

CREATE TABLE [dbo].[AspNetCart] (
    [Id]        INT IDENTITY (1, 1) NOT NULL,
    [UserId]    NVARCHAR (128) NOT NULL,
    [ProductId] INT NOT NULL,
	[Quantity]  INT NOT NULL,
    CONSTRAINT [PK_dbo.AspNetCart] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetCart_dbo.AspNetUsers] FOREIGN KEY (UserId) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetCart_dbo.AspNetProduct] FOREIGN KEY (ProductId) REFERENCES [dbo].[AspNetProduct] ([Id]) ON DELETE CASCADE
);

CREATE TABLE [dbo].[AspNetPaymentMethod] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
	[CardNumber]     NVARCHAR (256) NOT NULL,
	[ExpiryDate]  DATE NOT NULL,
    [Country]     NVARCHAR (256) NOT NULL,
    [CvvNumber]   INT  NOT NULL,
    [PaymentMethodTypeId] INT NOT NULL,
    [UserId]         NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetPaymentMethod] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetPaymentMethod_dbo.AspNetPaymentMethodType] FOREIGN KEY (PaymentTypeId) REFERENCES [dbo].[AspNetPaymentMethodType] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_dbo.AspNetPaymentMethod_dbo.AspNetUsers] FOREIGN KEY (UserId) REFERENCES [dbo].[AspNetUsers] ([Id]) ON DELETE CASCADE,
);

CREATE TABLE [dbo].[AspNetOrder] (
    [Id]          INT IDENTITY (1, 1) NOT NULL,
    [Address]     NVARCHAR (256) NOT NULL,
    [Total]       DECIMAL(12,4) NOT NULL,
    [SellerId]    NVARCHAR (128)  NULL,
    [CustomerId]  NVARCHAR (128) NOT NULL,
    [PaymentMethodId]   INT NOT NULL,
    [OrderDateTimeUTC]   DATETIME NOT NULL,
    CONSTRAINT [PK_dbo.AspNetOrder] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetOrder_dbo.AspNetUsersSeller] FOREIGN KEY (SellerId) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.AspNetOrder_dbo.AspNetUsersCustomer] FOREIGN KEY (CustomerId) REFERENCES [dbo].[AspNetUsers] ([Id]),
    CONSTRAINT [FK_dbo.AspNetOrder_dbo.AspNetPaymentMethod] FOREIGN KEY (PaymentMethodId) REFERENCES [dbo].[AspNetPaymentMethod] ([Id]) 
);

CREATE TABLE [dbo].[AspNetOrderDetail] (
    [Id]            INT IDENTITY (1, 1) NOT NULL,
	[OrderId]       INT NOT NULL,
    [ProductId]     INT NOT NULL,
    [Quantity]      INT NOT NULL,
    [UnitPrice]     DECIMAL(12,4) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetOrderDetail] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.AspNetOrderDetail_dbo.AspNetOrder] FOREIGN KEY (OrderId) REFERENCES [dbo].[AspNetOrder] ([Id]) ON DELETE CASCADE,
	CONSTRAINT [FK_dbo.AspNetOrderDetail_dbo.AspNetProduct] FOREIGN KEY (ProductId) REFERENCES [dbo].[AspNetProduct] ([Id]) ON DELETE CASCADE,
); 

CREATE TABLE [dbo].[AspNetPaymentMethodType] (
    [Id]  INT IDENTITY (1, 1) NOT NULL,
    [Name] NVARCHAR (128) NOT NULL,
    CONSTRAINT [PK_dbo.AspNetPaymentMethodType] PRIMARY KEY CLUSTERED ([Id] ASC),
);



