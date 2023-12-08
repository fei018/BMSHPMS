using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSManage.Controllers;
using BMSHPMS.DSManage.ViewModels.DSReceiptInfoVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSReceiptInfoControllerTest
    {
        private DSReceiptInfoController _controller;
        private string _seed;

        public DSReceiptInfoControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSReceiptInfoController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSReceiptInfoListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSReceiptInfoVM));

            DSReceiptInfoVM vm = rv.Model as DSReceiptInfoVM;
            DSReceiptInfo v = new DSReceiptInfo();
			
            v.ReceiptNumber = "q7RhxVQvP";
            v.ReceiptOwn = "NIghtzxBoOq";
            v.ContactName = "QnipoMrYTxWt";
            v.ContactPhone = "v";
            v.Sum = 62;
            v.DSRemark = "p";
            v.ReceiptDate = DateTime.Parse("2022-07-27 13:34:04");
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSReceiptInfo>().Find(v.ID);
				
                Assert.AreEqual(data.ReceiptNumber, "q7RhxVQvP");
                Assert.AreEqual(data.ReceiptOwn, "NIghtzxBoOq");
                Assert.AreEqual(data.ContactName, "QnipoMrYTxWt");
                Assert.AreEqual(data.ContactPhone, "v");
                Assert.AreEqual(data.Sum, 62);
                Assert.AreEqual(data.DSRemark, "p");
                Assert.AreEqual(data.ReceiptDate, DateTime.Parse("2022-07-27 13:34:04"));
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSReceiptInfo v = new DSReceiptInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ReceiptNumber = "q7RhxVQvP";
                v.ReceiptOwn = "NIghtzxBoOq";
                v.ContactName = "QnipoMrYTxWt";
                v.ContactPhone = "v";
                v.Sum = 62;
                v.DSRemark = "p";
                v.ReceiptDate = DateTime.Parse("2022-07-27 13:34:04");
                context.Set<DSReceiptInfo>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSReceiptInfoVM));

            DSReceiptInfoVM vm = rv.Model as DSReceiptInfoVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSReceiptInfo();
            v.ID = vm.Entity.ID;
       		
            v.ReceiptNumber = "oy";
            v.ReceiptOwn = "wQB8KrxAOjPW";
            v.ContactName = "xX1QA6";
            v.ContactPhone = "2HNEPjXqHoFBQJ7S";
            v.Sum = 65;
            v.DSRemark = "xnDoLJdIpjw0nkM";
            v.ReceiptDate = DateTime.Parse("2024-09-24 13:34:04");
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
                var data = context.Set<DSReceiptInfo>().Find(v.ID);
 				
                Assert.AreEqual(data.ReceiptNumber, "oy");
                Assert.AreEqual(data.ReceiptOwn, "wQB8KrxAOjPW");
                Assert.AreEqual(data.ContactName, "xX1QA6");
                Assert.AreEqual(data.ContactPhone, "2HNEPjXqHoFBQJ7S");
                Assert.AreEqual(data.Sum, 65);
                Assert.AreEqual(data.DSRemark, "xnDoLJdIpjw0nkM");
                Assert.AreEqual(data.ReceiptDate, DateTime.Parse("2024-09-24 13:34:04"));
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSReceiptInfo v = new DSReceiptInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ReceiptNumber = "q7RhxVQvP";
                v.ReceiptOwn = "NIghtzxBoOq";
                v.ContactName = "QnipoMrYTxWt";
                v.ContactPhone = "v";
                v.Sum = 62;
                v.DSRemark = "p";
                v.ReceiptDate = DateTime.Parse("2022-07-27 13:34:04");
                context.Set<DSReceiptInfo>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSReceiptInfoVM));

            DSReceiptInfoVM vm = rv.Model as DSReceiptInfoVM;
            v = new DSReceiptInfo();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSReceiptInfo>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSReceiptInfo v = new DSReceiptInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ReceiptNumber = "q7RhxVQvP";
                v.ReceiptOwn = "NIghtzxBoOq";
                v.ContactName = "QnipoMrYTxWt";
                v.ContactPhone = "v";
                v.Sum = 62;
                v.DSRemark = "p";
                v.ReceiptDate = DateTime.Parse("2022-07-27 13:34:04");
                context.Set<DSReceiptInfo>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSReceiptInfo v1 = new DSReceiptInfo();
            DSReceiptInfo v2 = new DSReceiptInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ReceiptNumber = "q7RhxVQvP";
                v1.ReceiptOwn = "NIghtzxBoOq";
                v1.ContactName = "QnipoMrYTxWt";
                v1.ContactPhone = "v";
                v1.Sum = 62;
                v1.DSRemark = "p";
                v1.ReceiptDate = DateTime.Parse("2022-07-27 13:34:04");
                v2.ReceiptNumber = "oy";
                v2.ReceiptOwn = "wQB8KrxAOjPW";
                v2.ContactName = "xX1QA6";
                v2.ContactPhone = "2HNEPjXqHoFBQJ7S";
                v2.Sum = 65;
                v2.DSRemark = "xnDoLJdIpjw0nkM";
                v2.ReceiptDate = DateTime.Parse("2024-09-24 13:34:04");
                context.Set<DSReceiptInfo>().Add(v1);
                context.Set<DSReceiptInfo>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSReceiptInfoBatchVM));

            DSReceiptInfoBatchVM vm = rv.Model as DSReceiptInfoBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSReceiptInfo>().Find(v1.ID);
                var data2 = context.Set<DSReceiptInfo>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSReceiptInfo v1 = new DSReceiptInfo();
            DSReceiptInfo v2 = new DSReceiptInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ReceiptNumber = "q7RhxVQvP";
                v1.ReceiptOwn = "NIghtzxBoOq";
                v1.ContactName = "QnipoMrYTxWt";
                v1.ContactPhone = "v";
                v1.Sum = 62;
                v1.DSRemark = "p";
                v1.ReceiptDate = DateTime.Parse("2022-07-27 13:34:04");
                v2.ReceiptNumber = "oy";
                v2.ReceiptOwn = "wQB8KrxAOjPW";
                v2.ContactName = "xX1QA6";
                v2.ContactPhone = "2HNEPjXqHoFBQJ7S";
                v2.Sum = 65;
                v2.DSRemark = "xnDoLJdIpjw0nkM";
                v2.ReceiptDate = DateTime.Parse("2024-09-24 13:34:04");
                context.Set<DSReceiptInfo>().Add(v1);
                context.Set<DSReceiptInfo>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSReceiptInfoBatchVM));

            DSReceiptInfoBatchVM vm = rv.Model as DSReceiptInfoBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSReceiptInfo>().Find(v1.ID);
                var data2 = context.Set<DSReceiptInfo>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSReceiptInfoListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
