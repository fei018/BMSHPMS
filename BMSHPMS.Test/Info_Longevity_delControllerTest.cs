using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSManage.Controllers;
using BMSHPMS.DSManage.ViewModels.Info_Longevity_delVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class Info_Longevity_delControllerTest
    {
        private Info_Longevity_delController _controller;
        private string _seed;

        public Info_Longevity_delControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<Info_Longevity_delController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as Info_Longevity_delListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Longevity_delVM));

            Info_Longevity_delVM vm = rv.Model as Info_Longevity_delVM;
            Info_Longevity_del v = new Info_Longevity_del();
			
            v.Name = "d0xRNdKrjbeOhM0Uk";
            v.Sum = 83;
            v.SerialCode = "X0zkUqaxSlRdoqPQF";
            v.DSRemark = "MNmYqYROsgztr";
            v.Receipt_delID = AddInfo_Receipt_del();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Info_Longevity_del>().Find(v.ID);
				
                Assert.AreEqual(data.Name, "d0xRNdKrjbeOhM0Uk");
                Assert.AreEqual(data.Sum, 83);
                Assert.AreEqual(data.SerialCode, "X0zkUqaxSlRdoqPQF");
                Assert.AreEqual(data.DSRemark, "MNmYqYROsgztr");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Info_Longevity_del v = new Info_Longevity_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "d0xRNdKrjbeOhM0Uk";
                v.Sum = 83;
                v.SerialCode = "X0zkUqaxSlRdoqPQF";
                v.DSRemark = "MNmYqYROsgztr";
                v.Receipt_delID = AddInfo_Receipt_del();
                context.Set<Info_Longevity_del>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Longevity_delVM));

            Info_Longevity_delVM vm = rv.Model as Info_Longevity_delVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Info_Longevity_del();
            v.ID = vm.Entity.ID;
       		
            v.Name = "A7pcEvXbo7wsAd";
            v.Sum = 44;
            v.SerialCode = "a";
            v.DSRemark = "LA";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.SerialCode", "");
            vm.FC.Add("Entity.DSRemark", "");
            vm.FC.Add("Entity.Receipt_delID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Info_Longevity_del>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "A7pcEvXbo7wsAd");
                Assert.AreEqual(data.Sum, 44);
                Assert.AreEqual(data.SerialCode, "a");
                Assert.AreEqual(data.DSRemark, "LA");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Info_Longevity_del v = new Info_Longevity_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "d0xRNdKrjbeOhM0Uk";
                v.Sum = 83;
                v.SerialCode = "X0zkUqaxSlRdoqPQF";
                v.DSRemark = "MNmYqYROsgztr";
                v.Receipt_delID = AddInfo_Receipt_del();
                context.Set<Info_Longevity_del>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Longevity_delVM));

            Info_Longevity_delVM vm = rv.Model as Info_Longevity_delVM;
            v = new Info_Longevity_del();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Info_Longevity_del>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Info_Longevity_del v = new Info_Longevity_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Name = "d0xRNdKrjbeOhM0Uk";
                v.Sum = 83;
                v.SerialCode = "X0zkUqaxSlRdoqPQF";
                v.DSRemark = "MNmYqYROsgztr";
                v.Receipt_delID = AddInfo_Receipt_del();
                context.Set<Info_Longevity_del>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            Info_Longevity_del v1 = new Info_Longevity_del();
            Info_Longevity_del v2 = new Info_Longevity_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "d0xRNdKrjbeOhM0Uk";
                v1.Sum = 83;
                v1.SerialCode = "X0zkUqaxSlRdoqPQF";
                v1.DSRemark = "MNmYqYROsgztr";
                v1.Receipt_delID = AddInfo_Receipt_del();
                v2.Name = "A7pcEvXbo7wsAd";
                v2.Sum = 44;
                v2.SerialCode = "a";
                v2.DSRemark = "LA";
                v2.Receipt_delID = v1.Receipt_delID; 
                context.Set<Info_Longevity_del>().Add(v1);
                context.Set<Info_Longevity_del>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Longevity_delBatchVM));

            Info_Longevity_delBatchVM vm = rv.Model as Info_Longevity_delBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Info_Longevity_del>().Find(v1.ID);
                var data2 = context.Set<Info_Longevity_del>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            Info_Longevity_del v1 = new Info_Longevity_del();
            Info_Longevity_del v2 = new Info_Longevity_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "d0xRNdKrjbeOhM0Uk";
                v1.Sum = 83;
                v1.SerialCode = "X0zkUqaxSlRdoqPQF";
                v1.DSRemark = "MNmYqYROsgztr";
                v1.Receipt_delID = AddInfo_Receipt_del();
                v2.Name = "A7pcEvXbo7wsAd";
                v2.Sum = 44;
                v2.SerialCode = "a";
                v2.DSRemark = "LA";
                v2.Receipt_delID = v1.Receipt_delID; 
                context.Set<Info_Longevity_del>().Add(v1);
                context.Set<Info_Longevity_del>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Longevity_delBatchVM));

            Info_Longevity_delBatchVM vm = rv.Model as Info_Longevity_delBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Info_Longevity_del>().Find(v1.ID);
                var data2 = context.Set<Info_Longevity_del>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as Info_Longevity_delListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddInfo_Receipt_del()
        {
            Info_Receipt_del v = new Info_Receipt_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ReceiptNumber = "DYKoHENKG8Vzuyo";
                v.ReceiptDate = DateTime.Parse("2024-09-27 16:51:44");
                v.DharmaServiceYear = 61;
                v.DharmaServiceName = "2";
                v.ReceiptOwn = "T8yGNsfTZRF";
                v.ContactName = "phV";
                v.ContactPhone = "HwG1wDyfk";
                v.Sum = 96;
                v.DSRemark = "BgNVad4";
                context.Set<Info_Receipt_del>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
