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


        public DbSet<DSReceiptInfo> DSReceiptInfos { get; set; }

        public DbSet<DServiceProject> DServiceProjects { get; set; }

        public DbSet<DSDonationProject> DSDonationProjects { get; set; }


        public DbSet<DSDonorInfo> DSDonorInfos { get; set; }

        public DbSet<DSLongevityInfo> DSLongevityInfos { get; set; }

        public DbSet<DSMemorialInfo> DSMemorialInfos { get; set; }
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
                    Password = Utils.GetMD5String("bmsh2808"),
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
            string conn = "Data Source=localhost\\SQLEXPRESS;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False";

            return new DataContext(conn, DBTypeEnum.SqlServer);
        }
    }

}
