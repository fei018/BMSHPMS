BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Info_Memorial]') AND [c].[name] = N'DeceasedName');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Info_Memorial] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Info_Memorial] DROP COLUMN [DeceasedName];
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Info_Donor]') AND [c].[name] = N'DeceasedName');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Info_Donor] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Info_Donor] DROP COLUMN [DeceasedName];
GO

ALTER TABLE [Info_Memorial] ADD [DeceasedName_1] nvarchar(max) NULL;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'附薦宗親名及稱呼_1';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'DeceasedName_1';
GO

ALTER TABLE [Info_Memorial] ADD [DeceasedName_2] nvarchar(max) NULL;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'附薦宗親名及稱呼_2';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'DeceasedName_2';
GO

ALTER TABLE [Info_Memorial] ADD [DeceasedName_3] nvarchar(max) NULL;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'附薦宗親名及稱呼_3';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'DeceasedName_3';
GO

ALTER TABLE [Info_Donor] ADD [DeceasedName_1] nvarchar(max) NULL;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'附薦宗親名及稱呼_1';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'DeceasedName_1';
GO

ALTER TABLE [Info_Donor] ADD [DeceasedName_2] nvarchar(max) NULL;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'附薦宗親名及稱呼_2';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'DeceasedName_2';
GO

ALTER TABLE [Info_Donor] ADD [DeceasedName_3] nvarchar(max) NULL;
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'附薦宗親名及稱呼_3';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'DeceasedName_3';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240116090047_UpdateModel', N'6.0.25');
GO

COMMIT;
GO

