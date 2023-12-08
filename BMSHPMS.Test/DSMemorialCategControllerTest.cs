using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DharmaService.Controllers;
using BMSHPMS.DharmaService.ViewModels.DSMemorialCategVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSMemorialCategControllerTest
    {
        private DSMemorialCategController _controller;
        private string _seed;

        public DSMemorialCategControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSMemorialCategController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSMemorialCategListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialCategVM));

            DSMemorialCategVM vm = rv.Model as DSMemorialCategVM;
            DSMemorialCateg v = new DSMemorialCateg();
			
            v.Sum = 14;
            v.Code = "r";
            v.UsedNo = 87;
            v.DSCategID = AddDSCategory();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSMemorialCateg>().Find(v.ID);
				
                Assert.AreEqual(data.Sum, 14);
                Assert.AreEqual(data.Code, "r");
                Assert.AreEqual(data.UsedNo, 87);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSMemorialCateg v = new DSMemorialCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Sum = 14;
                v.Code = "r";
                v.UsedNo = 87;
                v.DSCategID = AddDSCategory();
                context.Set<DSMemorialCateg>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialCategVM));

            DSMemorialCategVM vm = rv.Model as DSMemorialCategVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSMemorialCateg();
            v.ID = vm.Entity.ID;
       		
            v.Sum = 62;
            v.Code = "b2oo0maZeGes12opmp";
            v.UsedNo = 16;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.Code", "");
            vm.FC.Add("Entity.UsedNo", "");
            vm.FC.Add("Entity.DSCategID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSMemorialCateg>().Find(v.ID);
 				
                Assert.AreEqual(data.Sum, 62);
                Assert.AreEqual(data.Code, "b2oo0maZeGes12opmp");
                Assert.AreEqual(data.UsedNo, 16);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSMemorialCateg v = new DSMemorialCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Sum = 14;
                v.Code = "r";
                v.UsedNo = 87;
                v.DSCategID = AddDSCategory();
                context.Set<DSMemorialCateg>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialCategVM));

            DSMemorialCategVM vm = rv.Model as DSMemorialCategVM;
            v = new DSMemorialCateg();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSMemorialCateg>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSMemorialCateg v = new DSMemorialCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Sum = 14;
                v.Code = "r";
                v.UsedNo = 87;
                v.DSCategID = AddDSCategory();
                context.Set<DSMemorialCateg>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSMemorialCateg v1 = new DSMemorialCateg();
            DSMemorialCateg v2 = new DSMemorialCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 14;
                v1.Code = "r";
                v1.UsedNo = 87;
                v1.DSCategID = AddDSCategory();
                v2.Sum = 62;
                v2.Code = "b2oo0maZeGes12opmp";
                v2.UsedNo = 16;
                v2.DSCategID = v1.DSCategID; 
                context.Set<DSMemorialCateg>().Add(v1);
                context.Set<DSMemorialCateg>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialCategBatchVM));

            DSMemorialCategBatchVM vm = rv.Model as DSMemorialCategBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSMemorialCateg>().Find(v1.ID);
                var data2 = context.Set<DSMemorialCateg>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSMemorialCateg v1 = new DSMemorialCateg();
            DSMemorialCateg v2 = new DSMemorialCateg();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 14;
                v1.Code = "r";
                v1.UsedNo = 87;
                v1.DSCategID = AddDSCategory();
                v2.Sum = 62;
                v2.Code = "b2oo0maZeGes12opmp";
                v2.UsedNo = 16;
                v2.DSCategID = v1.DSCategID; 
                context.Set<DSMemorialCateg>().Add(v1);
                context.Set<DSMemorialCateg>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialCategBatchVM));

            DSMemorialCategBatchVM vm = rv.Model as DSMemorialCategBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSMemorialCateg>().Find(v1.ID);
                var data2 = context.Set<DSMemorialCateg>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSMemorialCategListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSCategory()
        {
            DSCategory v = new DSCategory();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.DSName = "dR";
                v.Code = "JILxo0C3Is";
                context.Set<DSCategory>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
