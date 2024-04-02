BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Info_Memorial_del]') AND [c].[name] = N'IsDataValid');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Info_Memorial_del] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Info_Memorial_del] DROP COLUMN [IsDataValid];
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Opt_DonationProject', 'COLUMN', N'UsedNumber';
SET @description = N'編號已計數';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Opt_DonationProject', 'COLUMN', N'UsedNumber';
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Info_Receipt]') AND [c].[name] = N'DharmaServiceName');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Info_Receipt] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Info_Receipt] ALTER COLUMN [DharmaServiceName] nvarchar(max) NULL;
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'DeceasedName_3';
SET @description = N'附薦名稱_3';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'DeceasedName_3';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'DeceasedName_2';
SET @description = N'附薦名稱_2';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'DeceasedName_2';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'DeceasedName_1';
SET @description = N'附薦名稱_1';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial_del', 'COLUMN', N'DeceasedName_1';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'DeceasedName_3';
SET @description = N'附薦名稱_3';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'DeceasedName_3';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'DeceasedName_2';
SET @description = N'附薦名稱_2';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'DeceasedName_2';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'DeceasedName_1';
SET @description = N'附薦名稱_1';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'DeceasedName_1';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'DeceasedName_3';
SET @description = N'附薦名稱_3';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'DeceasedName_3';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'DeceasedName_2';
SET @description = N'附薦名稱_2';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'DeceasedName_2';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'DeceasedName_1';
SET @description = N'附薦名稱_1';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor_del', 'COLUMN', N'DeceasedName_1';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'DeceasedName_3';
SET @description = N'附薦名稱_3';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'DeceasedName_3';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'DeceasedName_2';
SET @description = N'附薦名稱_2';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'DeceasedName_2';
GO

DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
EXEC sp_dropextendedproperty 'MS_Description', 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'DeceasedName_1';
SET @description = N'附薦名稱_1';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'DeceasedName_1';
GO

CREATE TABLE [DSRegRollback] (
    [ID] uniqueidentifier NOT NULL,
    [DonationProjectID] uniqueidentifier NULL,
    [PreUsedNumber] int NULL,
    [LastReceiptNumber] nvarchar(max) NULL,
    CONSTRAINT [PK_DSRegRollback] PRIMARY KEY ([ID])
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'功德ID';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'DSRegRollback', 'COLUMN', N'DonationProjectID';
SET @description = N'功德已使用數';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'DSRegRollback', 'COLUMN', N'PreUsedNumber';
SET @description = N'收據號碼';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'DSRegRollback', 'COLUMN', N'LastReceiptNumber';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240325051337_UpdateRollbackInfo', N'6.0.25');
GO

COMMIT;
GO

