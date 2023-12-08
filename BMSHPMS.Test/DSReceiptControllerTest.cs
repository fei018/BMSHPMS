using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSManage.Controllers;
using BMSHPMS.DSManage.ViewModels.DSReceiptVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSReceiptControllerTest
    {
        private DSReceiptController _controller;
        private string _seed;

        public DSReceiptControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSReceiptController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSReceiptListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSReceiptVM));

            DSReceiptVM vm = rv.Model as DSReceiptVM;
            DSReceipt v = new DSReceipt();
			
            v.ReceiptNumber = "CKzQGWiUUQ1hN";
            v.ReceiptOwn = "Ka0z";
            v.ContactName = "IF6ft";
            v.ContactPhone = "jhSfMIRszv0";
            v.Sum = 46;
            v.DSRemark = "SF5RqIVgI08qo72OKx";
            v.ReceiptDate = DateTime.Parse("2025-04-16 11:37:06");
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSReceipt>().Find(v.ID);
				
                Assert.AreEqual(data.ReceiptNumber, "CKzQGWiUUQ1hN");
                Assert.AreEqual(data.ReceiptOwn, "Ka0z");
                Assert.AreEqual(data.ContactName, "IF6ft");
                Assert.AreEqual(data.ContactPhone, "jhSfMIRszv0");
                Assert.AreEqual(data.Sum, 46);
                Assert.AreEqual(data.DSRemark, "SF5RqIVgI08qo72OKx");
                Assert.AreEqual(data.ReceiptDate, DateTime.Parse("2025-04-16 11:37:06"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSReceipt v = new DSReceipt();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ReceiptNumber = "CKzQGWiUUQ1hN";
                v.ReceiptOwn = "Ka0z";
                v.ContactName = "IF6ft";
                v.ContactPhone = "jhSfMIRszv0";
                v.Sum = 46;
                v.DSRemark = "SF5RqIVgI08qo72OKx";
                v.ReceiptDate = DateTime.Parse("2025-04-16 11:37:06");
                context.Set<DSReceipt>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSReceiptVM));

            DSReceiptVM vm = rv.Model as DSReceiptVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSReceipt();
            v.ID = vm.Entity.ID;
       		
            v.ReceiptNumber = "TBIa4X";
            v.ReceiptOwn = "M";
            v.ContactName = "61QIhINAWGcJ7s";
            v.ContactPhone = "aJ0";
            v.Sum = 46;
            v.DSRemark = "sBvKxvOYxQA";
            v.ReceiptDate = DateTime.Parse("2025-03-07 11:37:06");
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ReceiptNumber", "");
            vm.FC.Add("Entity.ReceiptOwn", "");
            vm.FC.Add("Entity.ContactName", "");
            vm.FC.Add("Entity.ContactPhone", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.DSRemark", "");
            vm.FC.Add("Entity.ReceiptDate", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSReceipt>().Find(v.ID);
 				
                Assert.AreEqual(data.ReceiptNumber, "TBIa4X");
                Assert.AreEqual(data.ReceiptOwn, "M");
                Assert.AreEqual(data.ContactName, "61QIhINAWGcJ7s");
                Assert.AreEqual(data.ContactPhone, "aJ0");
                Assert.AreEqual(data.Sum, 46);
                Assert.AreEqual(data.DSRemark, "sBvKxvOYxQA");
                Assert.AreEqual(data.ReceiptDate, DateTime.Parse("2025-03-07 11:37:06"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSReceipt v = new DSReceipt();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ReceiptNumber = "CKzQGWiUUQ1hN";
                v.ReceiptOwn = "Ka0z";
                v.ContactName = "IF6ft";
                v.ContactPhone = "jhSfMIRszv0";
                v.Sum = 46;
                v.DSRemark = "SF5RqIVgI08qo72OKx";
                v.ReceiptDate = DateTime.Parse("2025-04-16 11:37:06");
                context.Set<DSReceipt>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSReceiptVM));

            DSReceiptVM vm = rv.Model as DSReceiptVM;
            v = new DSReceipt();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSReceipt>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSReceipt v = new DSReceipt();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ReceiptNumber = "CKzQGWiUUQ1hN";
                v.ReceiptOwn = "Ka0z";
                v.ContactName = "IF6ft";
                v.ContactPhone = "jhSfMIRszv0";
                v.Sum = 46;
                v.DSRemark = "SF5RqIVgI08qo72OKx";
                v.ReceiptDate = DateTime.Parse("2025-04-16 11:37:06");
                context.Set<DSReceipt>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSReceipt v1 = new DSReceipt();
            DSReceipt v2 = new DSReceipt();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ReceiptNumber = "CKzQGWiUUQ1hN";
                v1.ReceiptOwn = "Ka0z";
                v1.ContactName = "IF6ft";
                v1.ContactPhone = "jhSfMIRszv0";
                v1.Sum = 46;
                v1.DSRemark = "SF5RqIVgI08qo72OKx";
                v1.ReceiptDate = DateTime.Parse("2025-04-16 11:37:06");
                v2.ReceiptNumber = "TBIa4X";
                v2.ReceiptOwn = "M";
                v2.ContactName = "61QIhINAWGcJ7s";
                v2.ContactPhone = "aJ0";
                v2.Sum = 46;
                v2.DSRemark = "sBvKxvOYxQA";
                v2.ReceiptDate = DateTime.Parse("2025-03-07 11:37:06");
                context.Set<DSReceipt>().Add(v1);
                context.Set<DSReceipt>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSReceiptBatchVM));

            DSReceiptBatchVM vm = rv.Model as DSReceiptBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSReceipt>().Find(v1.ID);
                var data2 = context.Set<DSReceipt>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSReceipt v1 = new DSReceipt();
            DSReceipt v2 = new DSReceipt();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ReceiptNumber = "CKzQGWiUUQ1hN";
                v1.ReceiptOwn = "Ka0z";
                v1.ContactName = "IF6ft";
                v1.ContactPhone = "jhSfMIRszv0";
                v1.Sum = 46;
                v1.DSRemark = "SF5RqIVgI08qo72OKx";
                v1.ReceiptDate = DateTime.Parse("2025-04-16 11:37:06");
                v2.ReceiptNumber = "TBIa4X";
                v2.ReceiptOwn = "M";
                v2.ContactName = "61QIhINAWGcJ7s";
                v2.ContactPhone = "aJ0";
                v2.Sum = 46;
                v2.DSRemark = "sBvKxvOYxQA";
                v2.ReceiptDate = DateTime.Parse("2025-03-07 11:37:06");
                context.Set<DSReceipt>().Add(v1);
                context.Set<DSReceipt>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSReceiptBatchVM));

            DSReceiptBatchVM vm = rv.Model as DSReceiptBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSReceipt>().Find(v1.ID);
                var data2 = context.Set<DSReceipt>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSReceiptListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
