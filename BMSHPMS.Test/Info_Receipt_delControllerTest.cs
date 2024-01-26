using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSManage.Controllers;
using BMSHPMS.DSManage.ViewModels.Info_Receipt_delVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class Info_Receipt_delControllerTest
    {
        private Info_Receipt_delController _controller;
        private string _seed;

        public Info_Receipt_delControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<Info_Receipt_delController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as Info_Receipt_delListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Receipt_delVM));

            Info_Receipt_delVM vm = rv.Model as Info_Receipt_delVM;
            Info_Receipt_del v = new Info_Receipt_del();
			
            v.ReceiptNumber = "IgWDPgxRKH9AaW";
            v.ReceiptDate = DateTime.Parse("2023-10-27 16:49:46");
            v.DharmaServiceYear = 7;
            v.DharmaServiceName = "mscDKpOi";
            v.ReceiptOwn = "F";
            v.ContactName = "QB4j0NgbKGX44ynJxkK";
            v.ContactPhone = "kxyIo34I";
            v.Sum = 24;
            v.DSRemark = "rRVpyvFK4GmV1mEoMQ";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Info_Receipt_del>().Find(v.ID);
				
                Assert.AreEqual(data.ReceiptNumber, "IgWDPgxRKH9AaW");
                Assert.AreEqual(data.ReceiptDate, DateTime.Parse("2023-10-27 16:49:46"));
                Assert.AreEqual(data.DharmaServiceYear, 7);
                Assert.AreEqual(data.DharmaServiceName, "mscDKpOi");
                Assert.AreEqual(data.ReceiptOwn, "F");
                Assert.AreEqual(data.ContactName, "QB4j0NgbKGX44ynJxkK");
                Assert.AreEqual(data.ContactPhone, "kxyIo34I");
                Assert.AreEqual(data.Sum, 24);
                Assert.AreEqual(data.DSRemark, "rRVpyvFK4GmV1mEoMQ");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Info_Receipt_del v = new Info_Receipt_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.ReceiptNumber = "IgWDPgxRKH9AaW";
                v.ReceiptDate = DateTime.Parse("2023-10-27 16:49:46");
                v.DharmaServiceYear = 7;
                v.DharmaServiceName = "mscDKpOi";
                v.ReceiptOwn = "F";
                v.ContactName = "QB4j0NgbKGX44ynJxkK";
                v.ContactPhone = "kxyIo34I";
                v.Sum = 24;
                v.DSRemark = "rRVpyvFK4GmV1mEoMQ";
                context.Set<Info_Receipt_del>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Receipt_delVM));

            Info_Receipt_delVM vm = rv.Model as Info_Receipt_delVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Info_Receipt_del();
            v.ID = vm.Entity.ID;
       		
            v.ReceiptNumber = "VZraMtbKut46R5o";
            v.ReceiptDate = DateTime.Parse("2023-09-21 16:49:46");
            v.DharmaServiceYear = 86;
            v.DharmaServiceName = "V3MeRyfEM0sufV";
            v.ReceiptOwn = "ROVOXOUMHE3xaT5U1";
            v.ContactName = "suuVK";
            v.ContactPhone = "vIWKLeKk5xM2";
            v.Sum = 32;
            v.DSRemark = "KZXKlSZs";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.ReceiptNumber", "");
            vm.FC.Add("Entity.ReceiptDate", "");
            vm.FC.Add("Entity.DharmaServiceYear", "");
            vm.FC.Add("Entity.DharmaServiceName", "");
            vm.FC.Add("Entity.ReceiptOwn", "");
            vm.FC.Add("Entity.ContactName", "");
            vm.FC.Add("Entity.ContactPhone", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.DSRemark", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Info_Receipt_del>().Find(v.ID);
 				
                Assert.AreEqual(data.ReceiptNumber, "VZraMtbKut46R5o");
                Assert.AreEqual(data.ReceiptDate, DateTime.Parse("2023-09-21 16:49:46"));
                Assert.AreEqual(data.DharmaServiceYear, 86);
                Assert.AreEqual(data.DharmaServiceName, "V3MeRyfEM0sufV");
                Assert.AreEqual(data.ReceiptOwn, "ROVOXOUMHE3xaT5U1");
                Assert.AreEqual(data.ContactName, "suuVK");
                Assert.AreEqual(data.ContactPhone, "vIWKLeKk5xM2");
                Assert.AreEqual(data.Sum, 32);
                Assert.AreEqual(data.DSRemark, "KZXKlSZs");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Info_Receipt_del v = new Info_Receipt_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.ReceiptNumber = "IgWDPgxRKH9AaW";
                v.ReceiptDate = DateTime.Parse("2023-10-27 16:49:46");
                v.DharmaServiceYear = 7;
                v.DharmaServiceName = "mscDKpOi";
                v.ReceiptOwn = "F";
                v.ContactName = "QB4j0NgbKGX44ynJxkK";
                v.ContactPhone = "kxyIo34I";
                v.Sum = 24;
                v.DSRemark = "rRVpyvFK4GmV1mEoMQ";
                context.Set<Info_Receipt_del>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Receipt_delVM));

            Info_Receipt_delVM vm = rv.Model as Info_Receipt_delVM;
            v = new Info_Receipt_del();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Info_Receipt_del>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Info_Receipt_del v = new Info_Receipt_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.ReceiptNumber = "IgWDPgxRKH9AaW";
                v.ReceiptDate = DateTime.Parse("2023-10-27 16:49:46");
                v.DharmaServiceYear = 7;
                v.DharmaServiceName = "mscDKpOi";
                v.ReceiptOwn = "F";
                v.ContactName = "QB4j0NgbKGX44ynJxkK";
                v.ContactPhone = "kxyIo34I";
                v.Sum = 24;
                v.DSRemark = "rRVpyvFK4GmV1mEoMQ";
                context.Set<Info_Receipt_del>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            Info_Receipt_del v1 = new Info_Receipt_del();
            Info_Receipt_del v2 = new Info_Receipt_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ReceiptNumber = "IgWDPgxRKH9AaW";
                v1.ReceiptDate = DateTime.Parse("2023-10-27 16:49:46");
                v1.DharmaServiceYear = 7;
                v1.DharmaServiceName = "mscDKpOi";
                v1.ReceiptOwn = "F";
                v1.ContactName = "QB4j0NgbKGX44ynJxkK";
                v1.ContactPhone = "kxyIo34I";
                v1.Sum = 24;
                v1.DSRemark = "rRVpyvFK4GmV1mEoMQ";
                v2.ReceiptNumber = "VZraMtbKut46R5o";
                v2.ReceiptDate = DateTime.Parse("2023-09-21 16:49:46");
                v2.DharmaServiceYear = 86;
                v2.DharmaServiceName = "V3MeRyfEM0sufV";
                v2.ReceiptOwn = "ROVOXOUMHE3xaT5U1";
                v2.ContactName = "suuVK";
                v2.ContactPhone = "vIWKLeKk5xM2";
                v2.Sum = 32;
                v2.DSRemark = "KZXKlSZs";
                context.Set<Info_Receipt_del>().Add(v1);
                context.Set<Info_Receipt_del>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Receipt_delBatchVM));

            Info_Receipt_delBatchVM vm = rv.Model as Info_Receipt_delBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Info_Receipt_del>().Find(v1.ID);
                var data2 = context.Set<Info_Receipt_del>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            Info_Receipt_del v1 = new Info_Receipt_del();
            Info_Receipt_del v2 = new Info_Receipt_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.ReceiptNumber = "IgWDPgxRKH9AaW";
                v1.ReceiptDate = DateTime.Parse("2023-10-27 16:49:46");
                v1.DharmaServiceYear = 7;
                v1.DharmaServiceName = "mscDKpOi";
                v1.ReceiptOwn = "F";
                v1.ContactName = "QB4j0NgbKGX44ynJxkK";
                v1.ContactPhone = "kxyIo34I";
                v1.Sum = 24;
                v1.DSRemark = "rRVpyvFK4GmV1mEoMQ";
                v2.ReceiptNumber = "VZraMtbKut46R5o";
                v2.ReceiptDate = DateTime.Parse("2023-09-21 16:49:46");
                v2.DharmaServiceYear = 86;
                v2.DharmaServiceName = "V3MeRyfEM0sufV";
                v2.ReceiptOwn = "ROVOXOUMHE3xaT5U1";
                v2.ContactName = "suuVK";
                v2.ContactPhone = "vIWKLeKk5xM2";
                v2.Sum = 32;
                v2.DSRemark = "KZXKlSZs";
                context.Set<Info_Receipt_del>().Add(v1);
                context.Set<Info_Receipt_del>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Receipt_delBatchVM));

            Info_Receipt_delBatchVM vm = rv.Model as Info_Receipt_delBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Info_Receipt_del>().Find(v1.ID);
                var data2 = context.Set<Info_Receipt_del>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as Info_Receipt_delListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }


    }
}
