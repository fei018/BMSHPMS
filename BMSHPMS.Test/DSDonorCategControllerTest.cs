using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DharmaService.Controllers;
using BMSHPMS.DharmaService.ViewModels.DSDonorCategVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSDonorCategControllerTest
    {
        private DSDonorCategController _controller;
        private string _seed;

        public DSDonorCategControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSDonorCategController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSDonorCategListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorCategVM));

            DSDonorCategVM vm = rv.Model as DSDonorCategVM;
            DSDonorCateg v = new DSDonorCateg();
			
            v.Sum = 26;
            v.Code = "lT6Q9";
            v.UsedNo = 0;
            v.DSCategID = AddDSCategory();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonorCateg>().Find(v.ID);
				
                Assert.AreEqual(data.Sum, 26);
                Assert.AreEqual(data.Code, "lT6Q9");
                Assert.AreEqual(data.UsedNo, 0);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSDonorCateg v = new DSDonorCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Sum = 26;
                v.Code = "lT6Q9";
                v.UsedNo = 0;
                v.DSCategID = AddDSCategory();
                context.Set<DSDonorCateg>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorCategVM));

            DSDonorCategVM vm = rv.Model as DSDonorCategVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSDonorCateg();
            v.ID = vm.Entity.ID;
       		
            v.Sum = 56;
            v.Code = "ROvN4ER";
            v.UsedNo = 29;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.UsedNo", "");
            vm.FC.Add("Entity.DSCategID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonorCateg>().Find(v.ID);
 				
                Assert.AreEqual(data.Sum, 56);
                Assert.AreEqual(data.Code, "ROvN4ER");
                Assert.AreEqual(data.UsedNo, 29);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSDonorCateg v = new DSDonorCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Sum = 26;
                v.Code = "lT6Q9";
                v.UsedNo = 0;
                v.DSCategID = AddDSCategory();
                context.Set<DSDonorCateg>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorCategVM));

            DSDonorCategVM vm = rv.Model as DSDonorCategVM;
            v = new DSDonorCateg();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonorCateg>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSDonorCateg v = new DSDonorCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Sum = 26;
                v.Code = "lT6Q9";
                v.UsedNo = 0;
                v.DSCategID = AddDSCategory();
                context.Set<DSDonorCateg>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSDonorCateg v1 = new DSDonorCateg();
            DSDonorCateg v2 = new DSDonorCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 26;
                v1.Code = "lT6Q9";
                v1.UsedNo = 0;
                v1.DSCategID = AddDSCategory();
                v2.Sum = 56;
                v2.Code = "ROvN4ER";
                v2.UsedNo = 29;
                v2.DSCategID = v1.DSCategID; 
                context.Set<DSDonorCateg>().Add(v1);
                context.Set<DSDonorCateg>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorCategBatchVM));

            DSDonorCategBatchVM vm = rv.Model as DSDonorCategBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSDonorCateg>().Find(v1.ID);
                var data2 = context.Set<DSDonorCateg>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSDonorCateg v1 = new DSDonorCateg();
            DSDonorCateg v2 = new DSDonorCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 26;
                v1.Code = "lT6Q9";
                v1.UsedNo = 0;
                v1.DSCategID = AddDSCategory();
                v2.Sum = 56;
                v2.Code = "ROvN4ER";
                v2.UsedNo = 29;
                v2.DSCategID = v1.DSCategID; 
                context.Set<DSDonorCateg>().Add(v1);
                context.Set<DSDonorCateg>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorCategBatchVM));

            DSDonorCategBatchVM vm = rv.Model as DSDonorCategBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSDonorCateg>().Find(v1.ID);
                var data2 = context.Set<DSDonorCateg>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSDonorCategListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSCategory()
        {
            DSCategory v = new DSCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.DSName = "xflEvnU";
                v.Code = "fRdTjbOwPlH";
                context.Set<DSCategory>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
