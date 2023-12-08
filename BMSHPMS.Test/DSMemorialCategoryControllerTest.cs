using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSCategory.Controllers;
using BMSHPMS.DSCategory.ViewModels.DSMemorialCategoryVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSMemorialCategoryControllerTest
    {
        private DSMemorialCategoryController _controller;
        private string _seed;

        public DSMemorialCategoryControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSMemorialCategoryController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSMemorialCategoryListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialCategoryVM));

            DSMemorialCategoryVM vm = rv.Model as DSMemorialCategoryVM;
            DSMemorialCategory v = new DSMemorialCategory();
			
            v.Sum = 81;
            v.Code = "S";
            v.UsedNo = 98;
            v.DSNameCategID = AddDSNameCategory();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSMemorialCategory>().Find(v.ID);
				
                Assert.AreEqual(data.Sum, 81);
                Assert.AreEqual(data.Code, "S");
                Assert.AreEqual(data.UsedNo, 98);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSMemorialCategory v = new DSMemorialCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Sum = 81;
                v.Code = "S";
                v.UsedNo = 98;
                v.DSNameCategID = AddDSNameCategory();
                context.Set<DSMemorialCategory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialCategoryVM));

            DSMemorialCategoryVM vm = rv.Model as DSMemorialCategoryVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSMemorialCategory();
            v.ID = vm.Entity.ID;
       		
            v.Sum = 65;
            v.Code = "2Jj0ibNf";
            v.UsedNo = 17;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.UsedNo", "");
            vm.FC.Add("Entity.DSNameCategID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSMemorialCategory>().Find(v.ID);
 				
                Assert.AreEqual(data.Sum, 65);
                Assert.AreEqual(data.Code, "2Jj0ibNf");
                Assert.AreEqual(data.UsedNo, 17);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSMemorialCategory v = new DSMemorialCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Sum = 81;
                v.Code = "S";
                v.UsedNo = 98;
                v.DSNameCategID = AddDSNameCategory();
                context.Set<DSMemorialCategory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialCategoryVM));

            DSMemorialCategoryVM vm = rv.Model as DSMemorialCategoryVM;
            v = new DSMemorialCategory();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSMemorialCategory>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSMemorialCategory v = new DSMemorialCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Sum = 81;
                v.Code = "S";
                v.UsedNo = 98;
                v.DSNameCategID = AddDSNameCategory();
                context.Set<DSMemorialCategory>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSMemorialCategory v1 = new DSMemorialCategory();
            DSMemorialCategory v2 = new DSMemorialCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 81;
                v1.Code = "S";
                v1.UsedNo = 98;
                v1.DSNameCategID = AddDSNameCategory();
                v2.Sum = 65;
                v2.Code = "2Jj0ibNf";
                v2.UsedNo = 17;
                v2.DSNameCategID = v1.DSNameCategID; 
                context.Set<DSMemorialCategory>().Add(v1);
                context.Set<DSMemorialCategory>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialCategoryBatchVM));

            DSMemorialCategoryBatchVM vm = rv.Model as DSMemorialCategoryBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSMemorialCategory>().Find(v1.ID);
                var data2 = context.Set<DSMemorialCategory>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSMemorialCategory v1 = new DSMemorialCategory();
            DSMemorialCategory v2 = new DSMemorialCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 81;
                v1.Code = "S";
                v1.UsedNo = 98;
                v1.DSNameCategID = AddDSNameCategory();
                v2.Sum = 65;
                v2.Code = "2Jj0ibNf";
                v2.UsedNo = 17;
                v2.DSNameCategID = v1.DSNameCategID; 
                context.Set<DSMemorialCategory>().Add(v1);
                context.Set<DSMemorialCategory>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialCategoryBatchVM));

            DSMemorialCategoryBatchVM vm = rv.Model as DSMemorialCategoryBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSMemorialCategory>().Find(v1.ID);
                var data2 = context.Set<DSMemorialCategory>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSMemorialCategoryListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSNameCategory()
        {
            DSNameCategory v = new DSNameCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Name = "VBEum0rphWaoa1A59Qs";
                v.Code = "7ZrOGCZMzG0kJB9";
                context.Set<DSNameCategory>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
