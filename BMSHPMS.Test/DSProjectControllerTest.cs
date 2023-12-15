using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSInfoManage.Controllers;
using BMSHPMS.DSInfoManage.ViewModels.DSProjectVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSProjectControllerTest
    {
        private DSProjectController _controller;
        private string _seed;

        public DSProjectControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSProjectController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSProjectListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSProjectVM));

            DSProjectVM vm = rv.Model as DSProjectVM;
            DSProject v = new DSProject();
			
            v.ProjectName = "4FL";
            v.ProjectCode = "LX9uAsvBamx9vpCmEO";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSProject>().Find(v.ID);
				
                Assert.AreEqual(data.ProjectName, "4FL");
                Assert.AreEqual(data.ProjectCode, "LX9uAsvBamx9vpCmEO");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSProject v = new DSProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ProjectName = "4FL";
                v.ProjectCode = "LX9uAsvBamx9vpCmEO";
                context.Set<DSProject>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSProjectVM));

            DSProjectVM vm = rv.Model as DSProjectVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSProject();
            v.ID = vm.Entity.ID;
       		
            v.ProjectName = "9qT1KCV4N";
            v.ProjectCode = "M8az9fenCD";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ProjectName", "");
            vm.FC.Add("Entity.ProjectCode", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSProject>().Find(v.ID);
 				
                Assert.AreEqual(data.ProjectName, "9qT1KCV4N");
                Assert.AreEqual(data.ProjectCode, "M8az9fenCD");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSProject v = new DSProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ProjectName = "4FL";
                v.ProjectCode = "LX9uAsvBamx9vpCmEO";
                context.Set<DSProject>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSProjectVM));

            DSProjectVM vm = rv.Model as DSProjectVM;
            v = new DSProject();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSProject>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSProject v = new DSProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ProjectName = "4FL";
                v.ProjectCode = "LX9uAsvBamx9vpCmEO";
                context.Set<DSProject>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSProject v1 = new DSProject();
            DSProject v2 = new DSProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ProjectName = "4FL";
                v1.ProjectCode = "LX9uAsvBamx9vpCmEO";
                v2.ProjectName = "9qT1KCV4N";
                v2.ProjectCode = "M8az9fenCD";
                context.Set<DSProject>().Add(v1);
                context.Set<DSProject>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSProjectBatchVM));

            DSProjectBatchVM vm = rv.Model as DSProjectBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSProject>().Find(v1.ID);
                var data2 = context.Set<DSProject>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSProject v1 = new DSProject();
            DSProject v2 = new DSProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ProjectName = "4FL";
                v1.ProjectCode = "LX9uAsvBamx9vpCmEO";
                v2.ProjectName = "9qT1KCV4N";
                v2.ProjectCode = "M8az9fenCD";
                context.Set<DSProject>().Add(v1);
                context.Set<DSProject>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSProjectBatchVM));

            DSProjectBatchVM vm = rv.Model as DSProjectBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSProject>().Find(v1.ID);
                var data2 = context.Set<DSProject>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSProjectListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
