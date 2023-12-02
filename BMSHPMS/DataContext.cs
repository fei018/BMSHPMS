using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using WalkingTec.Mvvm.Core;
using BMSHPMS.Models.DharmaService;
using System.Collections.Generic;
using System;
using WalkingTec.Mvvm.Core.Extensions;

namespace BMSHPMS
{
    public class DataContext : FrameworkContext
    {
        #region DbSet<Models>
        public DbSet<FrameworkUser> FrameworkUsers { get; set; }

        /// <summary>
        /// 護法功德主表
        /// </summary>
        public DbSet<T_LeadDonorPlaque> T_LeadDonorPlaques { get; set; }

        /// <summary>
        /// 護法功德主編號表
        /// </summary>
        public DbSet<T_LeadDonorSerial> T_LeadDonorSerials { get; set; }

        /// <summary>
        /// 附薦表
        /// </summary>
        public DbSet<T_MemorialPlaque> T_MemorialPlaques { get; set; }

        /// <summary>
        /// 附薦編號表
        /// </summary>
        public DbSet<T_MemorialSerial> T_MemorialSerials { get; set; }

        /// <summary>
        /// 延生表
        /// </summary>

        public DbSet<T_LongevityPlaque> T_LongevityPlaques { get; set; }

        /// <summary>
        /// 延生編號表
        /// </summary>
        public DbSet<T_LongevitySerial> T_LongevitySerials { get; set; }

        /// <summary>
        /// 收據表
        /// </summary>
        public DbSet<T_Receipt> T_Receipt { get; set; }

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

                DataInit2();

                await SaveChangesAsync();
            }
            return state;
        }

        private void DataInit2()
        {
            for (int i = 1; i < 50000; i++)
            {
                string lead = "LD" + i.ToString().PadLeft(5,'0');
                string longevity = "LG" + i.ToString().PadLeft(5, '0');
                string memorial = "ME" + i.ToString().PadLeft(5, '0');

                T_LeadDonorSerials.Add(new T_LeadDonorSerial { Serial = lead });
                T_LongevitySerials.Add(new T_LongevitySerial { Serial = longevity });
                T_MemorialSerials.Add(new T_MemorialSerial { Serial = memorial });
            }
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
