BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Info_Receipt]') AND [c].[name] = N'IsDataValid');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Info_Receipt] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Info_Receipt] DROP COLUMN [IsDataValid];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Info_Memorial]') AND [c].[name] = N'IsDataValid');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Info_Memorial] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Info_Memorial] DROP COLUMN [IsDataValid];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Info_Longevity]') AND [c].[name] = N'IsDataValid');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Info_Longevity] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Info_Longevity] DROP COLUMN [IsDataValid];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Info_Donor]') AND [c].[name] = N'IsDataValid');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Info_Donor] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Info_Donor] DROP COLUMN [IsDataValid];
GO

CREATE TABLE [Info_Receipt_del] (
    [ID] uniqueidentifier NOT NULL,
    [ReceiptNumber] nvarchar(450) NOT NULL,
    [ReceiptDate] datetime2 NULL,
    [DharmaServiceYear] int NULL,
    [DharmaServiceName] nvarchar(max) NOT NULL,
    [ReceiptOwn] nvarchar(max) NULL,
    [ContactName] nvarchar(max) NULL,
    [ContactPhone] nvarchar(max) NULL,
    [Sum] int NULL,
    [DSRemark] nvarchar(max) NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_Info_Receipt_del] PRIMARY KEY ([ID])
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'收據號碼';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt_del', 'COLUMN', N'ReceiptNumber';
SET @description = N'收據日期';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt_del', 'COLUMN', N'ReceiptDate';
SET @description = N'法會年份';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt_del', 'COLUMN', N'DharmaServiceYear';
SET @description = N'法會名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt_del', 'COLUMN', N'DharmaServiceName';
SET @description = N'收據人姓名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt_del', 'COLUMN', N'ReceiptOwn';
SET @description = N'聯絡人姓名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt_del', 'COLUMN', N'ContactName';
SET @description = N'聯絡人電話';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt_del', 'COLUMN', N'ContactPhone';
SET @description = N'金額';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt_del', 'COLUMN', N'Sum';
SET @description = N'備註';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt_del', 'COLUMN', N'DSRemark';
GO

CREATE TABLE [Info_Donor_del] (
    [ID] uniqueidentifier NOT NULL,
    [LongevityName] nvarchar(max) NULL,
    [DeceasedName_1] nvarchar(max) NULL,
    [DeceasedName_2] nvarchar(max) NULL,
    [DeceasedName_3] nvarchar(max) NULL,
    [BenefactorName] nvarchar(max) NULL,
    [Sum] int NULL,
    [SerialCode] nvarchar(max) NULL,
    [DSRemark] nvarchar(max) NULL,
    [Receipt_delID] uniqueidentifier NOT NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_Info_Donor_del] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Info_Donor_del_Info_Receipt_del_Receipt_delID] FOREIGN KEY ([Receipt_delID]) REFERENCES [Info_Receipt_del] ([ID]) ON DELETE CASCADE
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'延生位姓名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'LongevityName';
SET @description = N'附薦宗親名及稱呼_1';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'DeceasedName_1';
SET @description = N'附薦宗親名及稱呼_2';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'DeceasedName_2';
SET @description = N'附薦宗親名及稱呼_3';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'DeceasedName_3';
SET @description = N'陽居姓名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'BenefactorName';
SET @description = N'金額';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'Sum';
SET @description = N'功德主編號';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'SerialCode';
SET @description = N'備註';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'DSRemark';
SET @description = N'收據ID';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'Receipt_delID';
GO

CREATE TABLE [Info_Longevity_del] (
    [ID] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Sum] int NULL,
    [SerialCode] nvarchar(max) NULL,
    [DSRemark] nvarchar(max) NULL,
    [Receipt_delID] uniqueidentifier NOT NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_Info_Longevity_del] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Info_Longevity_del_Info_Receipt_del_Receipt_delID] FOREIGN KEY ([Receipt_delID]) REFERENCES [Info_Receipt_del] ([ID]) ON DELETE CASCADE
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'姓名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Longevity_del', 'COLUMN', N'Name';
SET @description = N'金額';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Longevity_del', 'COLUMN', N'Sum';
SET @description = N'延生編號';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Longevity_del', 'COLUMN', N'SerialCode';
SET @description = N'備註';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Longevity_del', 'COLUMN', N'DSRemark';
SET @description = N'收據ID';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Longevity_del', 'COLUMN', N'Receipt_delID';
GO

CREATE TABLE [Info_Memorial_del] (
    [ID] uniqueidentifier NOT NULL,
    [SerialCode] nvarchar(max) NULL,
    [BenefactorName] nvarchar(max) NULL,
    [DeceasedName_1] nvarchar(max) NULL,
    [DeceasedName_2] nvarchar(max) NULL,
    [DeceasedName_3] nvarchar(max) NULL,
    [Sum] int NULL,
    [DSRemark] nvarchar(max) NULL,
    [IsDataValid] bit NOT NULL,
    [Receipt_delID] uniqueidentifier NOT NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_Info_Memorial_del] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Info_Memorial_del_Info_Receipt_del_Receipt_delID] FOREIGN KEY ([Receipt_delID]) REFERENCES [Info_Receipt_del] ([ID]) ON DELETE CASCADE
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'附薦編號';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'SerialCode';
SET @description = N'陽居姓名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'BenefactorName';
SET @description = N'附薦宗親名及稱呼_1';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'DeceasedName_1';
SET @description = N'附薦宗親名及稱呼_2';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'DeceasedName_2';
SET @description = N'附薦宗親名及稱呼_3';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'DeceasedName_3';
SET @description = N'金額';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'Sum';
SET @description = N'備註';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'DSRemark';
SET @description = N'數據有效';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'IsDataValid';
SET @description = N'收據ID';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'Receipt_delID';
GO

CREATE INDEX [IX_Info_Donor_del_Receipt_delID] ON [Info_Donor_del] ([Receipt_delID]);
GO

CREATE INDEX [IX_Info_Longevity_del_Receipt_delID] ON [Info_Longevity_del] ([Receipt_delID]);
GO

CREATE INDEX [IX_Info_Memorial_del_Receipt_delID] ON [Info_Memorial_del] ([Receipt_delID]);
GO

CREATE INDEX [IX_Info_Receipt_del_ReceiptNumber] ON [Info_Receipt_del] ([ReceiptNumber]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240118083552_UpdateModel2', N'6.0.25');
GO

COMMIT;
GO

