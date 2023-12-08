using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.BackStage.Controllers;
using BMSHPMS.BackStage.ViewModels.DharmaService;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSLongevityControllerTest
    {
        private DSLongevityController _controller;
        private string _seed;

        public DSLongevityControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSLongevityController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSLongevityListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityVM));

            DSLongevityVM vm = rv.Model as DSLongevityVM;
            DSLongevity v = new DSLongevity();
			
            v.Name = "LP56rVWjZroQGwmU";
            v.Sum = 71;
            v.Serial = "qksxZIWJiUuIXm";
            v.PRemark = "3rAtYjkbNjvJr9rJYE";
            v.ReceiptID = AddDSReceipt();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLongevity>().Find(v.ID);
				
                Assert.AreEqual(data.Name, "LP56rVWjZroQGwmU");
                Assert.AreEqual(data.Sum, 71);
                Assert.AreEqual(data.Serial, "qksxZIWJiUuIXm");
                Assert.AreEqual(data.PRemark, "3rAtYjkbNjvJr9rJYE");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSLongevity v = new DSLongevity();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "LP56rVWjZroQGwmU";
                v.Sum = 71;
                v.Serial = "qksxZIWJiUuIXm";
                v.PRemark = "3rAtYjkbNjvJr9rJYE";
                v.ReceiptID = AddDSReceipt();
                context.Set<DSLongevity>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityVM));

            DSLongevityVM vm = rv.Model as DSLongevityVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSLongevity();
            v.ID = vm.Entity.ID;
       		
            v.Name = "I1Byb6aAM7AHbt";
            v.Sum = 72;
            v.Serial = "vWLAJyhKF7szebQu";
            v.PRemark = "PEMvUUZOSvmfGJP6ulh";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.Serial", "");
            vm.FC.Add("Entity.PRemark", "");
            vm.FC.Add("Entity.ReceiptID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLongevity>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "I1Byb6aAM7AHbt");
                Assert.AreEqual(data.Sum, 72);
                Assert.AreEqual(data.Serial, "vWLAJyhKF7szebQu");
                Assert.AreEqual(data.PRemark, "PEMvUUZOSvmfGJP6ulh");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSLongevity v = new DSLongevity();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "LP56rVWjZroQGwmU";
                v.Sum = 71;
                v.Serial = "qksxZIWJiUuIXm";
                v.PRemark = "3rAtYjkbNjvJr9rJYE";
                v.ReceiptID = AddDSReceipt();
                context.Set<DSLongevity>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityVM));

            DSLongevityVM vm = rv.Model as DSLongevityVM;
            v = new DSLongevity();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLongevity>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSLongevity v = new DSLongevity();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Name = "LP56rVWjZroQGwmU";
                v.Sum = 71;
                v.Serial = "qksxZIWJiUuIXm";
                v.PRemark = "3rAtYjkbNjvJr9rJYE";
                v.ReceiptID = AddDSReceipt();
                context.Set<DSLongevity>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSLongevity v1 = new DSLongevity();
            DSLongevity v2 = new DSLongevity();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "LP56rVWjZroQGwmU";
                v1.Sum = 71;
                v1.Serial = "qksxZIWJiUuIXm";
                v1.PRemark = "3rAtYjkbNjvJr9rJYE";
                v1.ReceiptID = AddDSReceipt();
                v2.Name = "I1Byb6aAM7AHbt";
                v2.Sum = 72;
                v2.Serial = "vWLAJyhKF7szebQu";
                v2.PRemark = "PEMvUUZOSvmfGJP6ulh";
                v2.ReceiptID = v1.ReceiptID; 
                context.Set<DSLongevity>().Add(v1);
                context.Set<DSLongevity>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityBatchVM));

            DSLongevityBatchVM vm = rv.Model as DSLongevityBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSLongevity>().Find(v1.ID);
                var data2 = context.Set<DSLongevity>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSLongevity v1 = new DSLongevity();
            DSLongevity v2 = new DSLongevity();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "LP56rVWjZroQGwmU";
                v1.Sum = 71;
                v1.Serial = "qksxZIWJiUuIXm";
                v1.PRemark = "3rAtYjkbNjvJr9rJYE";
                v1.ReceiptID = AddDSReceipt();
                v2.Name = "I1Byb6aAM7AHbt";
                v2.Sum = 72;
                v2.Serial = "vWLAJyhKF7szebQu";
                v2.PRemark = "PEMvUUZOSvmfGJP6ulh";
                v2.ReceiptID = v1.ReceiptID; 
                context.Set<DSLongevity>().Add(v1);
                context.Set<DSLongevity>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityBatchVM));

            DSLongevityBatchVM vm = rv.Model as DSLongevityBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSLongevity>().Find(v1.ID);
                var data2 = context.Set<DSLongevity>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSLongevityListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSReceipt()
        {
            DSReceipt v = new DSReceipt();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ReceiptNumber = "62YzUDs7F02";
                v.ReceiptOwn = "SYMWwaiKkKXcbGzqP2";
                v.ContactName = "5Nq3jq4ztwSKWuu";
                v.ContactPhone = "nHUiZfnTtXCudnQ";
                v.Sum = 75;
                v.PRemark = "sohTpPYbhzd9l1nW1";
                v.ReceiptDate = DateTime.Parse("2023-06-01 10:36:43");
                context.Set<DSReceipt>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
