using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSCategory.Controllers;
using BMSHPMS.DSCategory.ViewModels.DSDonorCategoryVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSDonorCategoryControllerTest
    {
        private DSDonorCategoryController _controller;
        private string _seed;

        public DSDonorCategoryControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSDonorCategoryController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSDonorCategoryListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorCategoryVM));

            DSDonorCategoryVM vm = rv.Model as DSDonorCategoryVM;
            DSDonorCategory v = new DSDonorCategory();
			
            v.Sum = 59;
            v.Code = "t3kX6RzEOOH5jyDIaU4";
            v.UsedNo = 17;
            v.DSNameCategID = AddDSNameCategory();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonorCategory>().Find(v.ID);
				
                Assert.AreEqual(data.Sum, 59);
                Assert.AreEqual(data.Code, "t3kX6RzEOOH5jyDIaU4");
                Assert.AreEqual(data.UsedNo, 17);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSDonorCategory v = new DSDonorCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Sum = 59;
                v.Code = "t3kX6RzEOOH5jyDIaU4";
                v.UsedNo = 17;
                v.DSNameCategID = AddDSNameCategory();
                context.Set<DSDonorCategory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorCategoryVM));

            DSDonorCategoryVM vm = rv.Model as DSDonorCategoryVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSDonorCategory();
            v.ID = vm.Entity.ID;
       		
            v.Sum = 17;
            v.Code = "CV9aAzC4EhZNu";
            v.UsedNo = 93;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.UsedNo", "");
            vm.FC.Add("Entity.DSNameCategID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonorCategory>().Find(v.ID);
 				
                Assert.AreEqual(data.Sum, 17);
                Assert.AreEqual(data.Code, "CV9aAzC4EhZNu");
                Assert.AreEqual(data.UsedNo, 93);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSDonorCategory v = new DSDonorCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Sum = 59;
                v.Code = "t3kX6RzEOOH5jyDIaU4";
                v.UsedNo = 17;
                v.DSNameCategID = AddDSNameCategory();
                context.Set<DSDonorCategory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorCategoryVM));

            DSDonorCategoryVM vm = rv.Model as DSDonorCategoryVM;
            v = new DSDonorCategory();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonorCategory>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSDonorCategory v = new DSDonorCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Sum = 59;
                v.Code = "t3kX6RzEOOH5jyDIaU4";
                v.UsedNo = 17;
                v.DSNameCategID = AddDSNameCategory();
                context.Set<DSDonorCategory>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSDonorCategory v1 = new DSDonorCategory();
            DSDonorCategory v2 = new DSDonorCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 59;
                v1.Code = "t3kX6RzEOOH5jyDIaU4";
                v1.UsedNo = 17;
                v1.DSNameCategID = AddDSNameCategory();
                v2.Sum = 17;
                v2.Code = "CV9aAzC4EhZNu";
                v2.UsedNo = 93;
                v2.DSNameCategID = v1.DSNameCategID; 
                context.Set<DSDonorCategory>().Add(v1);
                context.Set<DSDonorCategory>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorCategoryBatchVM));

            DSDonorCategoryBatchVM vm = rv.Model as DSDonorCategoryBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSDonorCategory>().Find(v1.ID);
                var data2 = context.Set<DSDonorCategory>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSDonorCategory v1 = new DSDonorCategory();
            DSDonorCategory v2 = new DSDonorCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 59;
                v1.Code = "t3kX6RzEOOH5jyDIaU4";
                v1.UsedNo = 17;
                v1.DSNameCategID = AddDSNameCategory();
                v2.Sum = 17;
                v2.Code = "CV9aAzC4EhZNu";
                v2.UsedNo = 93;
                v2.DSNameCategID = v1.DSNameCategID; 
                context.Set<DSDonorCategory>().Add(v1);
                context.Set<DSDonorCategory>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorCategoryBatchVM));

            DSDonorCategoryBatchVM vm = rv.Model as DSDonorCategoryBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSDonorCategory>().Find(v1.ID);
                var data2 = context.Set<DSDonorCategory>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSDonorCategoryListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSNameCategory()
        {
            DSNameCategory v = new DSNameCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Name = "2QG";
                v.Code = "2zRrr7h3c0NU";
                context.Set<DSNameCategory>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
