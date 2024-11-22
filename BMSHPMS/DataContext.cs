using BMSHPMS.Models.CommonDService;
using BMSHPMS.Models.DharmaService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System.Linq;
using System.Threading.Tasks;
using WalkingTec.Mvvm.Core;

namespace BMSHPMS
{
    public class DataContext : FrameworkContext
    {
        #region DbSet<Models>
        public DbSet<FrameworkUser> FrameworkUsers { get; set; }

        public DbSet<Opt_DharmaService> Opt_DharmaServices { get; set; }

        public DbSet<Opt_DonationProject> Opt_DonationProjects { get; set; }

        public DbSet<Info_Receipt> Info_Receipts { get; set; }

        public DbSet<Info_Receipt_del> Info_Receipts_del { get; set; }

        public DbSet<Info_Donor> Info_Donors { get; set; }

        public DbSet<Info_Donor_del> Info_Donors_del { get; set; }

        public DbSet<Info_Longevity> Info_Longevitys { get; set; }

        public DbSet<Info_Longevity_del> Info_Longevitys_del { get; set; }

        public DbSet<Info_Memorial> Info_Memorials { get; set; }

        public DbSet<Info_Memorial_del> Info_Memorials_del { get; set; }

        public DbSet<Reg_RollbackInfo> Reg_RollbackInfos { get; set; }

        public DbSet<Info_AutoComplete> Info_AutoCompletes { get; set; }

        //public DbSet<CommonReceipt> CommonReceipts { get; set; }

        //public DbSet<AnnualDabeiInfo> AnnualDabeiInfos { get; set; }

        //public DbSet<AnnualLightInfo> AnnualLightInfos { get; set; }
        #endregion


        public DataContext(CS cs)
             : base(cs)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype)
            : base(cs, dbtype)
        {
        }

        public DataContext(string cs, DBTypeEnum dbtype, string version = null)
            : base(cs, dbtype, version)
        {
        }


        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        #region protected override void OnModelCreating(ModelBuilder modelBuilder)
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //modelBuilder.Entity<DSReceiptInfo>().HasMany(x => x.DSDonorInfos)
            //                                    .WithOne(x => x.ReceiptInfo).HasForeignKey(x => x.ReceiptInfoID)
            //                                    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<DSReceiptInfo>().HasMany(x => x.DSLongevityInfos)
            //                                    .WithOne(x => x.ReceiptInfo).HasForeignKey(x => x.ReceiptInfoID)
            //                                    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<DSReceiptInfo>().HasMany(x => x.DSMemorialInfos)
            //                                    .WithOne(x => x.ReceiptInfo).HasForeignKey(x => x.ReceiptInfoID)
            //                                    .OnDelete(DeleteBehavior.Restrict);

            //modelBuilder.Entity<CommonReceipt>()
            //            .Property(e => e.DonationCategory)
            //            .HasConversion(v => string.Join(',', v), v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
        }
        #endregion


        #region 數據庫初始化
        public override async Task<bool> DataInit(object allModules, bool IsSpa)
        {
            var state = await base.DataInit(allModules, IsSpa);
            bool emptydb = false;
            try
            {
                emptydb = Set<FrameworkUser>().Count() == 0 && Set<FrameworkUserRole>().Count() == 0;
            }
            catch { }
            if (state == true || emptydb == true)
            {
                //when state is true, means it's the first time EF create database, do data init here
                //当state是true的时候，表示这是第一次创建数据库，可以在这里进行数据初始化
                var user = new FrameworkUser
                {
                    ITCode = "admin",
                    Password = Utils.GetMD5String("000000"),
                    IsValid = true,
                    Name = "Admin"
                };

                var userrole = new FrameworkUserRole
                {
                    UserCode = user.ITCode,
                    RoleCode = "001"
                };

                Set<FrameworkUser>().Add(user);
                Set<FrameworkUserRole>().Add(userrole);

                await SaveChangesAsync();
            }
            return state;
        }


        #endregion

    }


    /// <summary>
    /// DesignTimeFactory for EF Migration, use your full connection string,
    /// EF will find this class and use the connection defined here to run Add-Migration and Update-Database
    /// </summary>
    public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            string conn = "Data Source=localhost\\SQLEXPRESS;Database=BMSHPMS_db;Integrated Security=True;";
            //string conn = "Server=192.168.0.201\\sqlexpress;Database=BMSHPMS_db;User Id=bmshpms;Password=bmsh@1234";

            return new DataContext(conn, DBTypeEnum.SqlServer);
        }
    }

}
