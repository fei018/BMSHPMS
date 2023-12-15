using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSInfoManage.Controllers;
using BMSHPMS.DSInfoManage.ViewModels.DSDonationOptionVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSDonationOptionControllerTest
    {
        private DSDonationOptionController _controller;
        private string _seed;

        public DSDonationOptionControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSDonationOptionController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSDonationOptionListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonationOptionVM));

            DSDonationOptionVM vm = rv.Model as DSDonationOptionVM;
            DSDonationOption v = new DSDonationOption();
			
            v.Sum = 37;
            v.SumCode = "fo3WCRE";
            v.UsedNumber = 22;
            v.DonationProjectID = AddDSDonationProject();
            v.DSProjectID = AddDSProject();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonationOption>().Find(v.ID);
				
                Assert.AreEqual(data.Sum, 37);
                Assert.AreEqual(data.SumCode, "fo3WCRE");
                Assert.AreEqual(data.UsedNumber, 22);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSDonationOption v = new DSDonationOption();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Sum = 37;
                v.SumCode = "fo3WCRE";
                v.UsedNumber = 22;
                v.DonationProjectID = AddDSDonationProject();
                v.DSProjectID = AddDSProject();
                context.Set<DSDonationOption>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonationOptionVM));

            DSDonationOptionVM vm = rv.Model as DSDonationOptionVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSDonationOption();
            v.ID = vm.Entity.ID;
       		
            v.Sum = 83;
            v.SumCode = "xGsSwMa9e";
            v.UsedNumber = 7;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.SumCode", "");
            vm.FC.Add("Entity.UsedNumber", "");
            vm.FC.Add("Entity.DonationProjectID", "");
            vm.FC.Add("Entity.DSProjectID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonationOption>().Find(v.ID);
 				
                Assert.AreEqual(data.Sum, 83);
                Assert.AreEqual(data.SumCode, "xGsSwMa9e");
                Assert.AreEqual(data.UsedNumber, 7);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSDonationOption v = new DSDonationOption();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Sum = 37;
                v.SumCode = "fo3WCRE";
                v.UsedNumber = 22;
                v.DonationProjectID = AddDSDonationProject();
                v.DSProjectID = AddDSProject();
                context.Set<DSDonationOption>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonationOptionVM));

            DSDonationOptionVM vm = rv.Model as DSDonationOptionVM;
            v = new DSDonationOption();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonationOption>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSDonationOption v = new DSDonationOption();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Sum = 37;
                v.SumCode = "fo3WCRE";
                v.UsedNumber = 22;
                v.DonationProjectID = AddDSDonationProject();
                v.DSProjectID = AddDSProject();
                context.Set<DSDonationOption>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSDonationOption v1 = new DSDonationOption();
            DSDonationOption v2 = new DSDonationOption();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 37;
                v1.SumCode = "fo3WCRE";
                v1.UsedNumber = 22;
                v1.DonationProjectID = AddDSDonationProject();
                v1.DSProjectID = AddDSProject();
                v2.Sum = 83;
                v2.SumCode = "xGsSwMa9e";
                v2.UsedNumber = 7;
                v2.DonationProjectID = v1.DonationProjectID; 
                v2.DSProjectID = v1.DSProjectID; 
                context.Set<DSDonationOption>().Add(v1);
                context.Set<DSDonationOption>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonationOptionBatchVM));

            DSDonationOptionBatchVM vm = rv.Model as DSDonationOptionBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSDonationOption>().Find(v1.ID);
                var data2 = context.Set<DSDonationOption>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSDonationOption v1 = new DSDonationOption();
            DSDonationOption v2 = new DSDonationOption();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 37;
                v1.SumCode = "fo3WCRE";
                v1.UsedNumber = 22;
                v1.DonationProjectID = AddDSDonationProject();
                v1.DSProjectID = AddDSProject();
                v2.Sum = 83;
                v2.SumCode = "xGsSwMa9e";
                v2.UsedNumber = 7;
                v2.DonationProjectID = v1.DonationProjectID; 
                v2.DSProjectID = v1.DSProjectID; 
                context.Set<DSDonationOption>().Add(v1);
                context.Set<DSDonationOption>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonationOptionBatchVM));

            DSDonationOptionBatchVM vm = rv.Model as DSDonationOptionBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSDonationOption>().Find(v1.ID);
                var data2 = context.Set<DSDonationOption>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSDonationOptionListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSDonationProject()
        {
            DSDonationProject v = new DSDonationProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ProjectName = "HqH45fPPvdKJ";
                context.Set<DSDonationProject>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }

        private Guid AddDSProject()
        {
            DSProject v = new DSProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ProjectName = "Y";
                v.ProjectCode = "MqTbSS";
                context.Set<DSProject>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
