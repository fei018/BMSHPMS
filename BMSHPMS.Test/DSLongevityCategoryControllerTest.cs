using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSCategory.Controllers;
using BMSHPMS.DSCategory.ViewModels.DSLongevityCategoryVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSLongevityCategoryControllerTest
    {
        private DSLongevityCategoryController _controller;
        private string _seed;

        public DSLongevityCategoryControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSLongevityCategoryController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSLongevityCategoryListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityCategoryVM));

            DSLongevityCategoryVM vm = rv.Model as DSLongevityCategoryVM;
            DSLongevityCategory v = new DSLongevityCategory();
			
            v.Sum = 46;
            v.Code = "ElIyTh3";
            v.UsedNo = 98;
            v.DSNameCategID = AddDSNameCategory();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLongevityCategory>().Find(v.ID);
				
                Assert.AreEqual(data.Sum, 46);
                Assert.AreEqual(data.Code, "ElIyTh3");
                Assert.AreEqual(data.UsedNo, 98);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSLongevityCategory v = new DSLongevityCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Sum = 46;
                v.Code = "ElIyTh3";
                v.UsedNo = 98;
                v.DSNameCategID = AddDSNameCategory();
                context.Set<DSLongevityCategory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityCategoryVM));

            DSLongevityCategoryVM vm = rv.Model as DSLongevityCategoryVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSLongevityCategory();
            v.ID = vm.Entity.ID;
       		
            v.Sum = 50;
            v.Code = "jcpDXk2";
            v.UsedNo = 3;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.UsedNo", "");
            vm.FC.Add("Entity.DSNameCategID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLongevityCategory>().Find(v.ID);
 				
                Assert.AreEqual(data.Sum, 50);
                Assert.AreEqual(data.Code, "jcpDXk2");
                Assert.AreEqual(data.UsedNo, 3);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSLongevityCategory v = new DSLongevityCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Sum = 46;
                v.Code = "ElIyTh3";
                v.UsedNo = 98;
                v.DSNameCategID = AddDSNameCategory();
                context.Set<DSLongevityCategory>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityCategoryVM));

            DSLongevityCategoryVM vm = rv.Model as DSLongevityCategoryVM;
            v = new DSLongevityCategory();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLongevityCategory>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSLongevityCategory v = new DSLongevityCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Sum = 46;
                v.Code = "ElIyTh3";
                v.UsedNo = 98;
                v.DSNameCategID = AddDSNameCategory();
                context.Set<DSLongevityCategory>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSLongevityCategory v1 = new DSLongevityCategory();
            DSLongevityCategory v2 = new DSLongevityCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 46;
                v1.Code = "ElIyTh3";
                v1.UsedNo = 98;
                v1.DSNameCategID = AddDSNameCategory();
                v2.Sum = 50;
                v2.Code = "jcpDXk2";
                v2.UsedNo = 3;
                v2.DSNameCategID = v1.DSNameCategID; 
                context.Set<DSLongevityCategory>().Add(v1);
                context.Set<DSLongevityCategory>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityCategoryBatchVM));

            DSLongevityCategoryBatchVM vm = rv.Model as DSLongevityCategoryBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSLongevityCategory>().Find(v1.ID);
                var data2 = context.Set<DSLongevityCategory>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSLongevityCategory v1 = new DSLongevityCategory();
            DSLongevityCategory v2 = new DSLongevityCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 46;
                v1.Code = "ElIyTh3";
                v1.UsedNo = 98;
                v1.DSNameCategID = AddDSNameCategory();
                v2.Sum = 50;
                v2.Code = "jcpDXk2";
                v2.UsedNo = 3;
                v2.DSNameCategID = v1.DSNameCategID; 
                context.Set<DSLongevityCategory>().Add(v1);
                context.Set<DSLongevityCategory>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityCategoryBatchVM));

            DSLongevityCategoryBatchVM vm = rv.Model as DSLongevityCategoryBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSLongevityCategory>().Find(v1.ID);
                var data2 = context.Set<DSLongevityCategory>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSLongevityCategoryListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSNameCategory()
        {
            DSNameCategory v = new DSNameCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.Name = "hFyVOGSL1t1rlmu5i9";
                v.Code = "w3OC2u2yf3";
                context.Set<DSNameCategory>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
