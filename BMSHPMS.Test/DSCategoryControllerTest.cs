using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DharmaService.Controllers;
using BMSHPMS.DharmaService.ViewModels.DSCategoryVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSCategoryControllerTest
    {
        private DSCategoryController _controller;
        private string _seed;

        public DSCategoryControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSCategoryController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSCategoryListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSCategoryVM));

            DSCategoryVM vm = rv.Model as DSCategoryVM;
            DSCategory v = new DSCategory();
			
            v.DSName = "mCjEHHBCUFK0hxG";
            v.Code = "cfalAb8UbfH2Kfu";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSCategory>().Find(v.ID);
				
                Assert.AreEqual(data.DSName, "mCjEHHBCUFK0hxG");
                Assert.AreEqual(data.Code, "cfalAb8UbfH2Kfu");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSCategory v = new DSCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.DSName = "mCjEHHBCUFK0hxG";
                v.Code = "cfalAb8UbfH2Kfu";
                context.Set<DSCategory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSCategoryVM));

            DSCategoryVM vm = rv.Model as DSCategoryVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSCategory();
            v.ID = vm.Entity.ID;
       		
            v.DSName = "BaMfXe4mVefFvfFBM6";
            v.Code = "wAQGw7WJqoVk9xIfU1";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.DSName", "");
            vm.FC.Add("Entity.Code", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSCategory>().Find(v.ID);
 				
                Assert.AreEqual(data.DSName, "BaMfXe4mVefFvfFBM6");
                Assert.AreEqual(data.Code, "wAQGw7WJqoVk9xIfU1");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSCategory v = new DSCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.DSName = "mCjEHHBCUFK0hxG";
                v.Code = "cfalAb8UbfH2Kfu";
                context.Set<DSCategory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSCategoryVM));

            DSCategoryVM vm = rv.Model as DSCategoryVM;
            v = new DSCategory();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSCategory>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSCategory v = new DSCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.DSName = "mCjEHHBCUFK0hxG";
                v.Code = "cfalAb8UbfH2Kfu";
                context.Set<DSCategory>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSCategory v1 = new DSCategory();
            DSCategory v2 = new DSCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DSName = "mCjEHHBCUFK0hxG";
                v1.Code = "cfalAb8UbfH2Kfu";
                v2.DSName = "BaMfXe4mVefFvfFBM6";
                v2.Code = "wAQGw7WJqoVk9xIfU1";
                context.Set<DSCategory>().Add(v1);
                context.Set<DSCategory>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSCategoryBatchVM));

            DSCategoryBatchVM vm = rv.Model as DSCategoryBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSCategory>().Find(v1.ID);
                var data2 = context.Set<DSCategory>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSCategory v1 = new DSCategory();
            DSCategory v2 = new DSCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.DSName = "mCjEHHBCUFK0hxG";
                v1.Code = "cfalAb8UbfH2Kfu";
                v2.DSName = "BaMfXe4mVefFvfFBM6";
                v2.Code = "wAQGw7WJqoVk9xIfU1";
                context.Set<DSCategory>().Add(v1);
                context.Set<DSCategory>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSCategoryBatchVM));

            DSCategoryBatchVM vm = rv.Model as DSCategoryBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSCategory>().Find(v1.ID);
                var data2 = context.Set<DSCategory>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSCategoryListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
