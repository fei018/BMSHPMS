using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSManage.Controllers;
using BMSHPMS.DSManage.ViewModels.DServiceProjectVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DServiceProjectControllerTest
    {
        private DServiceProjectController _controller;
        private string _seed;

        public DServiceProjectControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DServiceProjectController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DServiceProjectListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DServiceProjectVM));

            DServiceProjectVM vm = rv.Model as DServiceProjectVM;
            DServiceProject v = new DServiceProject();
			
            v.ProjectName = "i8VJ8yVci";
            v.ProjectCode = "9xs3KmDLY4RH";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DServiceProject>().Find(v.ID);
				
                Assert.AreEqual(data.ProjectName, "i8VJ8yVci");
                Assert.AreEqual(data.ProjectCode, "9xs3KmDLY4RH");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DServiceProject v = new DServiceProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ProjectName = "i8VJ8yVci";
                v.ProjectCode = "9xs3KmDLY4RH";
                context.Set<DServiceProject>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DServiceProjectVM));

            DServiceProjectVM vm = rv.Model as DServiceProjectVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DServiceProject();
            v.ID = vm.Entity.ID;
       		
            v.ProjectName = "H2e5gUCuk";
            v.ProjectCode = "wKSy7cDCkbSX";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ProjectName", "");
            vm.FC.Add("Entity.ProjectCode", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DServiceProject>().Find(v.ID);
 				
                Assert.AreEqual(data.ProjectName, "H2e5gUCuk");
                Assert.AreEqual(data.ProjectCode, "wKSy7cDCkbSX");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DServiceProject v = new DServiceProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ProjectName = "i8VJ8yVci";
                v.ProjectCode = "9xs3KmDLY4RH";
                context.Set<DServiceProject>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DServiceProjectVM));

            DServiceProjectVM vm = rv.Model as DServiceProjectVM;
            v = new DServiceProject();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DServiceProject>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DServiceProject v = new DServiceProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ProjectName = "i8VJ8yVci";
                v.ProjectCode = "9xs3KmDLY4RH";
                context.Set<DServiceProject>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DServiceProject v1 = new DServiceProject();
            DServiceProject v2 = new DServiceProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ProjectName = "i8VJ8yVci";
                v1.ProjectCode = "9xs3KmDLY4RH";
                v2.ProjectName = "H2e5gUCuk";
                v2.ProjectCode = "wKSy7cDCkbSX";
                context.Set<DServiceProject>().Add(v1);
                context.Set<DServiceProject>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DServiceProjectBatchVM));

            DServiceProjectBatchVM vm = rv.Model as DServiceProjectBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DServiceProject>().Find(v1.ID);
                var data2 = context.Set<DServiceProject>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DServiceProject v1 = new DServiceProject();
            DServiceProject v2 = new DServiceProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ProjectName = "i8VJ8yVci";
                v1.ProjectCode = "9xs3KmDLY4RH";
                v2.ProjectName = "H2e5gUCuk";
                v2.ProjectCode = "wKSy7cDCkbSX";
                context.Set<DServiceProject>().Add(v1);
                context.Set<DServiceProject>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DServiceProjectBatchVM));

            DServiceProjectBatchVM vm = rv.Model as DServiceProjectBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DServiceProject>().Find(v1.ID);
                var data2 = context.Set<DServiceProject>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DServiceProjectListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
