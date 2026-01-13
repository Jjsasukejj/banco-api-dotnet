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

CREATE TABLE [Clientes] (
    [Id] int NOT NULL IDENTITY,
    [ClienteId] nvarchar(20) NOT NULL,
    [Contrasena] nvarchar(max) NOT NULL,
    [Estado] bit NOT NULL,
    [Nombre] nvarchar(100) NOT NULL,
    [Genero] nvarchar(max) NOT NULL,
    [Edad] int NOT NULL,
    [Identificacion] nvarchar(max) NOT NULL,
    [Direccion] nvarchar(max) NOT NULL,
    [Telefono] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Clientes] PRIMARY KEY ([Id]),
    CONSTRAINT [AK_Clientes_ClienteId] UNIQUE ([ClienteId])
);
GO

CREATE TABLE [Cuentas] (
    [Id] int NOT NULL IDENTITY,
    [NumeroCuenta] nvarchar(20) NOT NULL,
    [TipoCuenta] int NOT NULL,
    [Saldo] decimal(18,2) NOT NULL,
    [Estado] bit NOT NULL,
    [ClienteId] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Cuentas] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Cuentas_Clientes_ClienteId] FOREIGN KEY ([ClienteId]) REFERENCES [Clientes] ([ClienteId]) ON DELETE CASCADE
);
GO

CREATE TABLE [Movimientos] (
    [Id] int NOT NULL IDENTITY,
    [Fecha] datetime2 NOT NULL,
    [TipoMovimiento] int NOT NULL,
    [Valor] decimal(18,2) NOT NULL,
    [Saldo] decimal(18,2) NOT NULL,
    [CuentaId] int NOT NULL,
    CONSTRAINT [PK_Movimientos] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Movimientos_Cuentas_CuentaId] FOREIGN KEY ([CuentaId]) REFERENCES [Cuentas] ([Id]) ON DELETE CASCADE
);
GO

CREATE UNIQUE INDEX [IX_Clientes_ClienteId] ON [Clientes] ([ClienteId]);
GO

CREATE INDEX [IX_Cuentas_ClienteId] ON [Cuentas] ([ClienteId]);
GO

CREATE UNIQUE INDEX [IX_Cuentas_NumeroCuenta] ON [Cuentas] ([NumeroCuenta]);
GO

CREATE INDEX [IX_Movimientos_CuentaId] ON [Movimientos] ([CuentaId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20260113031617_InitialCreate', N'8.0.0');
GO

COMMIT;
GO

