using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DharmaService.Controllers;
using BMSHPMS.DharmaService.ViewModels.DSLongevityCategVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSLongevityCategControllerTest
    {
        private DSLongevityCategController _controller;
        private string _seed;

        public DSLongevityCategControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSLongevityCategController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSLongevityCategListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityCategVM));

            DSLongevityCategVM vm = rv.Model as DSLongevityCategVM;
            DSLongevityCateg v = new DSLongevityCateg();
			
            v.Sum = 3;
            v.Code = "pHnez7yVtHOI";
            v.UsedNo = 20;
            v.DSCategID = AddDSCategory();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLongevityCateg>().Find(v.ID);
				
                Assert.AreEqual(data.Sum, 3);
                Assert.AreEqual(data.Code, "pHnez7yVtHOI");
                Assert.AreEqual(data.UsedNo, 20);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSLongevityCateg v = new DSLongevityCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Sum = 3;
                v.Code = "pHnez7yVtHOI";
                v.UsedNo = 20;
                v.DSCategID = AddDSCategory();
                context.Set<DSLongevityCateg>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityCategVM));

            DSLongevityCategVM vm = rv.Model as DSLongevityCategVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSLongevityCateg();
            v.ID = vm.Entity.ID;
       		
            v.Sum = 22;
            v.Code = "FyRrjWpjvbAn";
            v.UsedNo = 5;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.UsedNo", "");
            vm.FC.Add("Entity.DSCategID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLongevityCateg>().Find(v.ID);
 				
                Assert.AreEqual(data.Sum, 22);
                Assert.AreEqual(data.Code, "FyRrjWpjvbAn");
                Assert.AreEqual(data.UsedNo, 5);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSLongevityCateg v = new DSLongevityCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Sum = 3;
                v.Code = "pHnez7yVtHOI";
                v.UsedNo = 20;
                v.DSCategID = AddDSCategory();
                context.Set<DSLongevityCateg>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityCategVM));

            DSLongevityCategVM vm = rv.Model as DSLongevityCategVM;
            v = new DSLongevityCateg();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLongevityCateg>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSLongevityCateg v = new DSLongevityCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Sum = 3;
                v.Code = "pHnez7yVtHOI";
                v.UsedNo = 20;
                v.DSCategID = AddDSCategory();
                context.Set<DSLongevityCateg>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSLongevityCateg v1 = new DSLongevityCateg();
            DSLongevityCateg v2 = new DSLongevityCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 3;
                v1.Code = "pHnez7yVtHOI";
                v1.UsedNo = 20;
                v1.DSCategID = AddDSCategory();
                v2.Sum = 22;
                v2.Code = "FyRrjWpjvbAn";
                v2.UsedNo = 5;
                v2.DSCategID = v1.DSCategID; 
                context.Set<DSLongevityCateg>().Add(v1);
                context.Set<DSLongevityCateg>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityCategBatchVM));

            DSLongevityCategBatchVM vm = rv.Model as DSLongevityCategBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSLongevityCateg>().Find(v1.ID);
                var data2 = context.Set<DSLongevityCateg>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSLongevityCateg v1 = new DSLongevityCateg();
            DSLongevityCateg v2 = new DSLongevityCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 3;
                v1.Code = "pHnez7yVtHOI";
                v1.UsedNo = 20;
                v1.DSCategID = AddDSCategory();
                v2.Sum = 22;
                v2.Code = "FyRrjWpjvbAn";
                v2.UsedNo = 5;
                v2.DSCategID = v1.DSCategID; 
                context.Set<DSLongevityCateg>().Add(v1);
                context.Set<DSLongevityCateg>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityCategBatchVM));

            DSLongevityCategBatchVM vm = rv.Model as DSLongevityCategBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSLongevityCateg>().Find(v1.ID);
                var data2 = context.Set<DSLongevityCateg>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSLongevityCategListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSCategory()
        {
            DSCategory v = new DSCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.DSName = "sHTylM7jsCP2K";
                v.Code = "6DSMU81OMR";
                context.Set<DSCategory>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
