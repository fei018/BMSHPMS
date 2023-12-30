using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BMSHPMS.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Elsa");

            migrationBuilder.CreateTable(
                name: "ActionLogs",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ModuleName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ActionName = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                    ITCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ActionUrl = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    ActionTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: false),
                    Remark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IP = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LogType = table.Column<int>(type: "int", nullable: false),
                    TenantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActionLogs", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Bookmarks",
                schema: "Elsa",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ActivityId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    WorkflowInstanceId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    CorrelationId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookmarks", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DataPrivileges",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    GroupCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TableName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RelateId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DataPrivileges", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FileAttachments",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FileName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FileExt = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Length = table.Column<long>(type: "bigint", nullable: false),
                    UploadTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    SaveMode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FileData = table.Column<byte[]>(type: "varbinary(max)", nullable: true),
                    ExtraInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HandlerInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FileAttachments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkGroups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GroupName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GroupRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Manager = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkGroups", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FrameworkGroups_FrameworkGroups_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FrameworkGroups",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FrameworkMenus",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PageName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ActionName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ModuleName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FolderOnly = table.Column<bool>(type: "bit", nullable: false),
                    IsInherit = table.Column<bool>(type: "bit", nullable: false),
                    ClassName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MethodName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Domain = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ShowOnMenu = table.Column<bool>(type: "bit", nullable: false),
                    IsPublic = table.Column<bool>(type: "bit", nullable: false),
                    DisplayOrder = table.Column<int>(type: "int", nullable: false),
                    IsInside = table.Column<bool>(type: "bit", nullable: false),
                    TenantAllowed = table.Column<bool>(type: "bit", nullable: true),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Icon = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ParentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkMenus", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FrameworkMenus_FrameworkMenus_ParentId",
                        column: x => x.ParentId,
                        principalTable: "FrameworkMenus",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "FrameworkRoles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleRemark = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TenantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkTenants",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TDb = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TDbType = table.Column<int>(type: "int", nullable: true),
                    DbContext = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TDomain = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    TenantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EnableSub = table.Column<bool>(type: "bit", nullable: false),
                    Enabled = table.Column<bool>(type: "bit", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkTenants", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkUserGroups",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    GroupCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkUserGroups", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkUserRoles",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    RoleCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkUserRoles", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkWorkflows",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WorkflowName = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ModelType = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ModelID = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Submitter = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    WorkflowId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    ActivityId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    TenantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkWorkflows", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "FunctionPrivileges",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MenuItemId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Allowed = table.Column<bool>(type: "bit", nullable: false),
                    TenantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FunctionPrivileges", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Info_Receipt",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ReceiptNumber = table.Column<string>(type: "nvarchar(450)", nullable: false, comment: "收據號碼"),
                    ReceiptDate = table.Column<DateTime>(type: "datetime2", nullable: true, comment: "收據日期"),
                    DharmaServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "法會名"),
                    ReceiptOwn = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "收據人姓名"),
                    ContactName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "聯絡人姓名"),
                    ContactPhone = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "聯絡人電話"),
                    Sum = table.Column<int>(type: "int", nullable: true, comment: "金額"),
                    DSRemark = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "備註"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_Receipt", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Opt_DharmaService",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ServiceName = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "法會名"),
                    SerialCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "編號代碼"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opt_DharmaService", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Triggers",
                schema: "Elsa",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Hash = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModelType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ActivityId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    WorkflowDefinitionId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Triggers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowDefinitions",
                schema: "Elsa",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    DefinitionId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    DisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    IsSingleton = table.Column<bool>(type: "bit", nullable: false),
                    PersistenceBehavior = table.Column<int>(type: "int", nullable: false),
                    DeleteCompletedInstances = table.Column<bool>(type: "bit", nullable: false),
                    IsPublished = table.Column<bool>(type: "bit", nullable: false),
                    IsLatest = table.Column<bool>(type: "bit", nullable: false),
                    Tag = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowExecutionLogRecords",
                schema: "Elsa",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    WorkflowInstanceId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ActivityType = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ActivityId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    Timestamp = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    EventName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Source = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowExecutionLogRecords", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WorkflowInstances",
                schema: "Elsa",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    DefinitionId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Version = table.Column<int>(type: "int", nullable: false),
                    WorkflowStatus = table.Column<int>(type: "int", nullable: false),
                    CorrelationId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false),
                    ContextType = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    ContextId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    Name = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastExecutedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FinishedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    CancelledAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    FaultedAt = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastExecutedActivityId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DefinitionVersionId = table.Column<string>(type: "nvarchar(450)", maxLength: 450, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkflowInstances", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FrameworkUsers",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: true),
                    CellPhone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    HomePhone = table.Column<string>(type: "nvarchar(30)", maxLength: 30, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ZipCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    ITCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(32)", maxLength: 32, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    IsValid = table.Column<bool>(type: "bit", nullable: false),
                    PhotoId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    TenantCode = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FrameworkUsers", x => x.ID);
                    table.ForeignKey(
                        name: "FK_FrameworkUsers_FileAttachments_PhotoId",
                        column: x => x.PhotoId,
                        principalTable: "FileAttachments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Info_Donor",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LongevityName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "延生位姓名"),
                    DeceasedName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "附薦宗親名及稱呼"),
                    BenefactorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "陽居姓名"),
                    Sum = table.Column<int>(type: "int", nullable: true, comment: "金額"),
                    SerialCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "功德主編號"),
                    DSRemark = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "備註"),
                    ReceiptID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "收據ID"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_Donor", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Info_Donor_Info_Receipt_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Info_Receipt",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Info_Longevity",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "姓名"),
                    Sum = table.Column<int>(type: "int", nullable: true, comment: "金額"),
                    SerialCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "延生編號"),
                    DSRemark = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "備註"),
                    ReceiptID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "收據ID"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_Longevity", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Info_Longevity_Info_Receipt_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Info_Receipt",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Info_Memorial",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SerialCode = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "附薦編號"),
                    BenefactorName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "陽居姓名"),
                    DeceasedName = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "附薦宗親名及稱呼"),
                    Sum = table.Column<int>(type: "int", nullable: true, comment: "金額"),
                    DSRemark = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "備註"),
                    ReceiptID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "收據ID"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    IsValid = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Info_Memorial", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Info_Memorial_Info_Receipt_ReceiptID",
                        column: x => x.ReceiptID,
                        principalTable: "Info_Receipt",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Opt_DonationProject",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Sum = table.Column<int>(type: "int", nullable: false, comment: "金額"),
                    SerialCode = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "編號代碼"),
                    DonationCategory = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "功德類別"),
                    UsedNumber = table.Column<int>(type: "int", nullable: false, comment: "已使用數"),
                    DharmaServiceID = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "法會項目ID"),
                    CreateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    CreateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    UpdateBy = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opt_DonationProject", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Opt_DonationProject_Opt_DharmaService_DharmaServiceID",
                        column: x => x.DharmaServiceID,
                        principalTable: "Opt_DharmaService",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkGroups_ParentId",
                table: "FrameworkGroups",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkMenus_ParentId",
                table: "FrameworkMenus",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_FrameworkUsers_PhotoId",
                table: "FrameworkUsers",
                column: "PhotoId");

            migrationBuilder.CreateIndex(
                name: "IX_Info_Donor_ReceiptID",
                table: "Info_Donor",
                column: "ReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_Info_Longevity_ReceiptID",
                table: "Info_Longevity",
                column: "ReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_Info_Memorial_ReceiptID",
                table: "Info_Memorial",
                column: "ReceiptID");

            migrationBuilder.CreateIndex(
                name: "IX_Info_Receipt_ReceiptNumber",
                table: "Info_Receipt",
                column: "ReceiptNumber");

            migrationBuilder.CreateIndex(
                name: "IX_Opt_DonationProject_DharmaServiceID",
                table: "Opt_DonationProject",
                column: "DharmaServiceID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActionLogs");

            migrationBuilder.DropTable(
                name: "Bookmarks",
                schema: "Elsa");

            migrationBuilder.DropTable(
                name: "DataPrivileges");

            migrationBuilder.DropTable(
                name: "FrameworkGroups");

            migrationBuilder.DropTable(
                name: "FrameworkMenus");

            migrationBuilder.DropTable(
                name: "FrameworkRoles");

            migrationBuilder.DropTable(
                name: "FrameworkTenants");

            migrationBuilder.DropTable(
                name: "FrameworkUserGroups");

            migrationBuilder.DropTable(
                name: "FrameworkUserRoles");

            migrationBuilder.DropTable(
                name: "FrameworkUsers");

            migrationBuilder.DropTable(
                name: "FrameworkWorkflows");

            migrationBuilder.DropTable(
                name: "FunctionPrivileges");

            migrationBuilder.DropTable(
                name: "Info_Donor");

            migrationBuilder.DropTable(
                name: "Info_Longevity");

            migrationBuilder.DropTable(
                name: "Info_Memorial");

            migrationBuilder.DropTable(
                name: "Opt_DonationProject");

            migrationBuilder.DropTable(
                name: "Triggers",
                schema: "Elsa");

            migrationBuilder.DropTable(
                name: "WorkflowDefinitions",
                schema: "Elsa");

            migrationBuilder.DropTable(
                name: "WorkflowExecutionLogRecords",
                schema: "Elsa");

            migrationBuilder.DropTable(
                name: "WorkflowInstances",
                schema: "Elsa");

            migrationBuilder.DropTable(
                name: "FileAttachments");

            migrationBuilder.DropTable(
                name: "Info_Receipt");

            migrationBuilder.DropTable(
                name: "Opt_DharmaService");
        }
    }
}
