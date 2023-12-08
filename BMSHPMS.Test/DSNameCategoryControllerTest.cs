using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSCategory.Controllers;
using BMSHPMS.DSCategory.ViewModels.DSNameCategoryVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSNameCategoryControllerTest
    {
        private DSNameCategoryController _controller;
        private string _seed;

        public DSNameCategoryControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSNameCategoryController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSNameCategoryListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSNameCategoryVM));

            DSNameCategoryVM vm = rv.Model as DSNameCategoryVM;
            DSNameCategory v = new DSNameCategory();
			
            v.Name = "9T6uEt5geIF";
            v.Code = "XSB5Btmv6kf7EXJ";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSNameCategory>().Find(v.ID);
				
                Assert.AreEqual(data.Name, "9T6uEt5geIF");
                Assert.AreEqual(data.Code, "XSB5Btmv6kf7EXJ");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSNameCategory v = new DSNameCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "9T6uEt5geIF";
                v.Code = "XSB5Btmv6kf7EXJ";
                context.Set<DSNameCategory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSNameCategoryVM));

            DSNameCategoryVM vm = rv.Model as DSNameCategoryVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSNameCategory();
            v.ID = vm.Entity.ID;
       		
            v.Name = "MY9EfS7AQFd";
            v.Code = "zUZf3ATyqNL";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Code", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSNameCategory>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "MY9EfS7AQFd");
                Assert.AreEqual(data.Code, "zUZf3ATyqNL");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSNameCategory v = new DSNameCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "9T6uEt5geIF";
                v.Code = "XSB5Btmv6kf7EXJ";
                context.Set<DSNameCategory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSNameCategoryVM));

            DSNameCategoryVM vm = rv.Model as DSNameCategoryVM;
            v = new DSNameCategory();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSNameCategory>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSNameCategory v = new DSNameCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Name = "9T6uEt5geIF";
                v.Code = "XSB5Btmv6kf7EXJ";
                context.Set<DSNameCategory>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSNameCategory v1 = new DSNameCategory();
            DSNameCategory v2 = new DSNameCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "9T6uEt5geIF";
                v1.Code = "XSB5Btmv6kf7EXJ";
                v2.Name = "MY9EfS7AQFd";
                v2.Code = "zUZf3ATyqNL";
                context.Set<DSNameCategory>().Add(v1);
                context.Set<DSNameCategory>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSNameCategoryBatchVM));

            DSNameCategoryBatchVM vm = rv.Model as DSNameCategoryBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSNameCategory>().Find(v1.ID);
                var data2 = context.Set<DSNameCategory>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSNameCategory v1 = new DSNameCategory();
            DSNameCategory v2 = new DSNameCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "9T6uEt5geIF";
                v1.Code = "XSB5Btmv6kf7EXJ";
                v2.Name = "MY9EfS7AQFd";
                v2.Code = "zUZf3ATyqNL";
                context.Set<DSNameCategory>().Add(v1);
                context.Set<DSNameCategory>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSNameCategoryBatchVM));

            DSNameCategoryBatchVM vm = rv.Model as DSNameCategoryBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSNameCategory>().Find(v1.ID);
                var data2 = context.Set<DSNameCategory>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSNameCategoryListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
