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

IF SCHEMA_ID(N'Elsa') IS NULL EXEC(N'CREATE SCHEMA [Elsa];');
GO

CREATE TABLE [ActionLogs] (
    [ID] uniqueidentifier NOT NULL,
    [ModuleName] nvarchar(255) NULL,
    [ActionName] nvarchar(255) NULL,
    [ITCode] nvarchar(50) NULL,
    [ActionUrl] nvarchar(250) NULL,
    [ActionTime] datetime2 NOT NULL,
    [Duration] float NOT NULL,
    [Remark] nvarchar(max) NULL,
    [IP] nvarchar(50) NULL,
    [LogType] int NOT NULL,
    [TenantCode] nvarchar(50) NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_ActionLogs] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Elsa].[Bookmarks] (
    [Id] nvarchar(450) NOT NULL,
    [TenantId] nvarchar(450) NULL,
    [Hash] nvarchar(450) NOT NULL,
    [Model] nvarchar(max) NOT NULL,
    [ModelType] nvarchar(max) NOT NULL,
    [ActivityType] nvarchar(450) NOT NULL,
    [ActivityId] nvarchar(450) NOT NULL,
    [WorkflowInstanceId] nvarchar(450) NOT NULL,
    [CorrelationId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Bookmarks] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [DataPrivileges] (
    [ID] uniqueidentifier NOT NULL,
    [UserCode] nvarchar(max) NULL,
    [GroupCode] nvarchar(max) NULL,
    [TableName] nvarchar(50) NOT NULL,
    [RelateId] nvarchar(max) NULL,
    [Domain] nvarchar(max) NULL,
    [TenantCode] nvarchar(50) NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_DataPrivileges] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [FileAttachments] (
    [ID] uniqueidentifier NOT NULL,
    [FileName] nvarchar(max) NOT NULL,
    [FileExt] nvarchar(10) NOT NULL,
    [Path] nvarchar(max) NULL,
    [Length] bigint NOT NULL,
    [UploadTime] datetime2 NOT NULL,
    [SaveMode] nvarchar(max) NULL,
    [FileData] varbinary(max) NULL,
    [ExtraInfo] nvarchar(max) NULL,
    [HandlerInfo] nvarchar(max) NULL,
    [TenantCode] nvarchar(50) NULL,
    CONSTRAINT [PK_FileAttachments] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [FrameworkGroups] (
    [ID] uniqueidentifier NOT NULL,
    [GroupCode] nvarchar(50) NOT NULL,
    [GroupName] nvarchar(50) NOT NULL,
    [GroupRemark] nvarchar(max) NULL,
    [Manager] nvarchar(max) NULL,
    [TenantCode] nvarchar(50) NULL,
    [ParentId] uniqueidentifier NULL,
    CONSTRAINT [PK_FrameworkGroups] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_FrameworkGroups_FrameworkGroups_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [FrameworkGroups] ([ID])
);
GO

CREATE TABLE [FrameworkMenus] (
    [ID] uniqueidentifier NOT NULL,
    [PageName] nvarchar(100) NOT NULL,
    [ActionName] nvarchar(max) NULL,
    [ModuleName] nvarchar(max) NULL,
    [FolderOnly] bit NOT NULL,
    [IsInherit] bit NOT NULL,
    [ClassName] nvarchar(max) NULL,
    [MethodName] nvarchar(max) NULL,
    [Domain] nvarchar(max) NULL,
    [ShowOnMenu] bit NOT NULL,
    [IsPublic] bit NOT NULL,
    [DisplayOrder] int NOT NULL,
    [IsInside] bit NOT NULL,
    [TenantAllowed] bit NULL,
    [Url] nvarchar(max) NULL,
    [Icon] nvarchar(50) NULL,
    [ParentId] uniqueidentifier NULL,
    CONSTRAINT [PK_FrameworkMenus] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_FrameworkMenus_FrameworkMenus_ParentId] FOREIGN KEY ([ParentId]) REFERENCES [FrameworkMenus] ([ID])
);
GO

CREATE TABLE [FrameworkRoles] (
    [ID] uniqueidentifier NOT NULL,
    [RoleCode] nvarchar(50) NOT NULL,
    [RoleName] nvarchar(50) NOT NULL,
    [RoleRemark] nvarchar(max) NULL,
    [TenantCode] nvarchar(50) NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_FrameworkRoles] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [FrameworkTenants] (
    [ID] uniqueidentifier NOT NULL,
    [TCode] nvarchar(50) NOT NULL,
    [TName] nvarchar(50) NOT NULL,
    [TDb] nvarchar(max) NULL,
    [TDbType] int NULL,
    [DbContext] nvarchar(max) NULL,
    [TDomain] nvarchar(50) NULL,
    [TenantCode] nvarchar(50) NULL,
    [EnableSub] bit NOT NULL,
    [Enabled] bit NOT NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_FrameworkTenants] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [FrameworkUserGroups] (
    [ID] uniqueidentifier NOT NULL,
    [UserCode] nvarchar(50) NOT NULL,
    [GroupCode] nvarchar(50) NOT NULL,
    [TenantCode] nvarchar(50) NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_FrameworkUserGroups] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [FrameworkUserRoles] (
    [ID] uniqueidentifier NOT NULL,
    [UserCode] nvarchar(50) NOT NULL,
    [RoleCode] nvarchar(50) NOT NULL,
    [TenantCode] nvarchar(50) NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_FrameworkUserRoles] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [FrameworkWorkflows] (
    [ID] uniqueidentifier NOT NULL,
    [UserCode] nvarchar(50) NOT NULL,
    [Tag] nvarchar(50) NULL,
    [WorkflowName] nvarchar(450) NULL,
    [ModelType] nvarchar(450) NULL,
    [ModelID] nvarchar(100) NULL,
    [Submitter] nvarchar(50) NULL,
    [WorkflowId] nvarchar(50) NOT NULL,
    [ActivityId] nvarchar(50) NOT NULL,
    [TenantCode] nvarchar(50) NULL,
    [StartTime] datetime2 NULL,
    CONSTRAINT [PK_FrameworkWorkflows] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [FunctionPrivileges] (
    [ID] uniqueidentifier NOT NULL,
    [RoleCode] nvarchar(max) NULL,
    [MenuItemId] uniqueidentifier NOT NULL,
    [Allowed] bit NOT NULL,
    [TenantCode] nvarchar(50) NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_FunctionPrivileges] PRIMARY KEY ([ID])
);
GO

CREATE TABLE [Info_Receipt] (
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
    [IsDataValid] bit NOT NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_Info_Receipt] PRIMARY KEY ([ID])
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'收據號碼';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt', 'COLUMN', N'ReceiptNumber';
SET @description = N'收據日期';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt', 'COLUMN', N'ReceiptDate';
SET @description = N'法會年份';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt', 'COLUMN', N'DharmaServiceYear';
SET @description = N'法會名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt', 'COLUMN', N'DharmaServiceName';
SET @description = N'收據人姓名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt', 'COLUMN', N'ReceiptOwn';
SET @description = N'聯絡人姓名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt', 'COLUMN', N'ContactName';
SET @description = N'聯絡人電話';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt', 'COLUMN', N'ContactPhone';
SET @description = N'金額';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt', 'COLUMN', N'Sum';
SET @description = N'備註';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt', 'COLUMN', N'DSRemark';
SET @description = N'數據有效';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Receipt', 'COLUMN', N'IsDataValid';
GO

CREATE TABLE [Opt_DharmaService] (
    [ID] uniqueidentifier NOT NULL,
    [ServiceName] nvarchar(max) NOT NULL,
    [SerialCode] nvarchar(max) NOT NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_Opt_DharmaService] PRIMARY KEY ([ID])
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'法會名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Opt_DharmaService', 'COLUMN', N'ServiceName';
SET @description = N'編號代碼';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Opt_DharmaService', 'COLUMN', N'SerialCode';
GO

CREATE TABLE [Elsa].[Triggers] (
    [Id] nvarchar(450) NOT NULL,
    [TenantId] nvarchar(450) NULL,
    [Hash] nvarchar(450) NOT NULL,
    [Model] nvarchar(max) NOT NULL,
    [ModelType] nvarchar(max) NOT NULL,
    [ActivityType] nvarchar(450) NOT NULL,
    [ActivityId] nvarchar(450) NOT NULL,
    [WorkflowDefinitionId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_Triggers] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Elsa].[WorkflowDefinitions] (
    [Id] nvarchar(450) NOT NULL,
    [DefinitionId] nvarchar(450) NOT NULL,
    [TenantId] nvarchar(450) NULL,
    [Name] nvarchar(450) NULL,
    [DisplayName] nvarchar(max) NULL,
    [Description] nvarchar(max) NULL,
    [Version] int NOT NULL,
    [IsSingleton] bit NOT NULL,
    [PersistenceBehavior] int NOT NULL,
    [DeleteCompletedInstances] bit NOT NULL,
    [IsPublished] bit NOT NULL,
    [IsLatest] bit NOT NULL,
    [Tag] nvarchar(450) NULL,
    [Data] nvarchar(max) NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    CONSTRAINT [PK_WorkflowDefinitions] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Elsa].[WorkflowExecutionLogRecords] (
    [Id] nvarchar(450) NOT NULL,
    [TenantId] nvarchar(450) NULL,
    [WorkflowInstanceId] nvarchar(450) NOT NULL,
    [ActivityType] nvarchar(450) NOT NULL,
    [ActivityId] nvarchar(450) NOT NULL,
    [Timestamp] datetimeoffset NOT NULL,
    [EventName] nvarchar(max) NULL,
    [Message] nvarchar(max) NULL,
    [Source] nvarchar(max) NULL,
    [Data] nvarchar(max) NULL,
    CONSTRAINT [PK_WorkflowExecutionLogRecords] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [Elsa].[WorkflowInstances] (
    [Id] nvarchar(450) NOT NULL,
    [DefinitionId] nvarchar(450) NOT NULL,
    [TenantId] nvarchar(450) NULL,
    [Version] int NOT NULL,
    [WorkflowStatus] int NOT NULL,
    [CorrelationId] nvarchar(450) NOT NULL,
    [ContextType] nvarchar(450) NULL,
    [ContextId] nvarchar(450) NULL,
    [Name] nvarchar(450) NULL,
    [CreatedAt] datetimeoffset NOT NULL,
    [LastExecutedAt] datetimeoffset NULL,
    [FinishedAt] datetimeoffset NULL,
    [CancelledAt] datetimeoffset NULL,
    [FaultedAt] datetimeoffset NULL,
    [Data] nvarchar(max) NULL,
    [LastExecutedActivityId] nvarchar(max) NULL,
    [DefinitionVersionId] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_WorkflowInstances] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [FrameworkUsers] (
    [ID] uniqueidentifier NOT NULL,
    [Email] nvarchar(50) NULL,
    [Gender] int NULL,
    [CellPhone] nvarchar(max) NULL,
    [HomePhone] nvarchar(30) NULL,
    [Address] nvarchar(200) NULL,
    [ZipCode] nvarchar(max) NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    [ITCode] nvarchar(50) NOT NULL,
    [Password] nvarchar(32) NOT NULL,
    [Name] nvarchar(50) NOT NULL,
    [IsValid] bit NOT NULL,
    [PhotoId] uniqueidentifier NULL,
    [TenantCode] nvarchar(50) NULL,
    CONSTRAINT [PK_FrameworkUsers] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_FrameworkUsers_FileAttachments_PhotoId] FOREIGN KEY ([PhotoId]) REFERENCES [FileAttachments] ([ID]) ON DELETE NO ACTION
);
GO

CREATE TABLE [Info_Donor] (
    [ID] uniqueidentifier NOT NULL,
    [LongevityName] nvarchar(max) NULL,
    [DeceasedName] nvarchar(max) NULL,
    [BenefactorName] nvarchar(max) NULL,
    [Sum] int NULL,
    [SerialCode] nvarchar(max) NULL,
    [DSRemark] nvarchar(max) NULL,
    [IsDataValid] bit NOT NULL,
    [ReceiptID] uniqueidentifier NOT NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_Info_Donor] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Info_Donor_Info_Receipt_ReceiptID] FOREIGN KEY ([ReceiptID]) REFERENCES [Info_Receipt] ([ID]) ON DELETE CASCADE
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'延生位姓名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'LongevityName';
SET @description = N'附薦宗親名及稱呼';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'DeceasedName';
SET @description = N'陽居姓名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'BenefactorName';
SET @description = N'金額';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'Sum';
SET @description = N'功德主編號';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'SerialCode';
SET @description = N'備註';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'DSRemark';
SET @description = N'數據有效';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'IsDataValid';
SET @description = N'收據ID';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Donor', 'COLUMN', N'ReceiptID';
GO

CREATE TABLE [Info_Longevity] (
    [ID] uniqueidentifier NOT NULL,
    [Name] nvarchar(max) NULL,
    [Sum] int NULL,
    [SerialCode] nvarchar(max) NULL,
    [DSRemark] nvarchar(max) NULL,
    [IsDataValid] bit NOT NULL,
    [ReceiptID] uniqueidentifier NOT NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_Info_Longevity] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Info_Longevity_Info_Receipt_ReceiptID] FOREIGN KEY ([ReceiptID]) REFERENCES [Info_Receipt] ([ID]) ON DELETE CASCADE
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'姓名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Longevity', 'COLUMN', N'Name';
SET @description = N'金額';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Longevity', 'COLUMN', N'Sum';
SET @description = N'延生編號';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Longevity', 'COLUMN', N'SerialCode';
SET @description = N'備註';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Longevity', 'COLUMN', N'DSRemark';
SET @description = N'數據有效';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Longevity', 'COLUMN', N'IsDataValid';
SET @description = N'收據ID';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Longevity', 'COLUMN', N'ReceiptID';
GO

CREATE TABLE [Info_Memorial] (
    [ID] uniqueidentifier NOT NULL,
    [SerialCode] nvarchar(max) NULL,
    [BenefactorName] nvarchar(max) NULL,
    [DeceasedName] nvarchar(max) NULL,
    [Sum] int NULL,
    [DSRemark] nvarchar(max) NULL,
    [IsDataValid] bit NOT NULL,
    [ReceiptID] uniqueidentifier NOT NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_Info_Memorial] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Info_Memorial_Info_Receipt_ReceiptID] FOREIGN KEY ([ReceiptID]) REFERENCES [Info_Receipt] ([ID]) ON DELETE CASCADE
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'附薦編號';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'SerialCode';
SET @description = N'陽居姓名';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'BenefactorName';
SET @description = N'附薦宗親名及稱呼';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'DeceasedName';
SET @description = N'金額';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'Sum';
SET @description = N'備註';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'DSRemark';
SET @description = N'數據有效';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'IsDataValid';
SET @description = N'收據ID';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Info_Memorial', 'COLUMN', N'ReceiptID';
GO

CREATE TABLE [Opt_DonationProject] (
    [ID] uniqueidentifier NOT NULL,
    [Sum] int NOT NULL,
    [SerialCode] nvarchar(max) NOT NULL,
    [DonationCategory] nvarchar(max) NOT NULL,
    [UsedNumber] int NOT NULL,
    [DharmaServiceID] uniqueidentifier NOT NULL,
    [CreateTime] datetime2 NULL,
    [CreateBy] nvarchar(50) NULL,
    [UpdateTime] datetime2 NULL,
    [UpdateBy] nvarchar(50) NULL,
    CONSTRAINT [PK_Opt_DonationProject] PRIMARY KEY ([ID]),
    CONSTRAINT [FK_Opt_DonationProject_Opt_DharmaService_DharmaServiceID] FOREIGN KEY ([DharmaServiceID]) REFERENCES [Opt_DharmaService] ([ID]) ON DELETE CASCADE
);
DECLARE @defaultSchema AS sysname;
SET @defaultSchema = SCHEMA_NAME();
DECLARE @description AS sql_variant;
SET @description = N'金額';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Opt_DonationProject', 'COLUMN', N'Sum';
SET @description = N'編號代碼';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Opt_DonationProject', 'COLUMN', N'SerialCode';
SET @description = N'功德類別';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Opt_DonationProject', 'COLUMN', N'DonationCategory';
SET @description = N'已使用數';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Opt_DonationProject', 'COLUMN', N'UsedNumber';
SET @description = N'法會項目ID';
EXEC sp_addextendedproperty 'MS_Description', @description, 'SCHEMA', @defaultSchema, 'TABLE', N'Opt_DonationProject', 'COLUMN', N'DharmaServiceID';
GO

CREATE INDEX [IX_FrameworkGroups_ParentId] ON [FrameworkGroups] ([ParentId]);
GO

CREATE INDEX [IX_FrameworkMenus_ParentId] ON [FrameworkMenus] ([ParentId]);
GO

CREATE INDEX [IX_FrameworkUsers_PhotoId] ON [FrameworkUsers] ([PhotoId]);
GO

CREATE INDEX [IX_Info_Donor_ReceiptID] ON [Info_Donor] ([ReceiptID]);
GO

CREATE INDEX [IX_Info_Longevity_ReceiptID] ON [Info_Longevity] ([ReceiptID]);
GO

CREATE INDEX [IX_Info_Memorial_ReceiptID] ON [Info_Memorial] ([ReceiptID]);
GO

CREATE INDEX [IX_Info_Receipt_ReceiptNumber] ON [Info_Receipt] ([ReceiptNumber]);
GO

CREATE INDEX [IX_Opt_DonationProject_DharmaServiceID] ON [Opt_DonationProject] ([DharmaServiceID]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240104081159_InitialDb', N'6.0.25');
GO

COMMIT;
GO

