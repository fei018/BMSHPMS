using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSManage.Controllers;
using BMSHPMS.DSManage.ViewModels.Info_Donor_delVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class Info_Donor_delControllerTest
    {
        private Info_Donor_delController _controller;
        private string _seed;

        public Info_Donor_delControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<Info_Donor_delController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as Info_Donor_delListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Donor_delVM));

            Info_Donor_delVM vm = rv.Model as Info_Donor_delVM;
            Info_Donor_del v = new Info_Donor_del();
			
            v.LongevityName = "mp6mZS";
            v.DeceasedName_1 = "URip0B3V";
            v.DeceasedName_2 = "yZ5AobN";
            v.DeceasedName_3 = "s9dys59BFWU8Cl";
            v.BenefactorName = "1qhpafih6N0LzkuxW";
            v.Sum = 0;
            v.SerialCode = "ISv1PFAAoYQy1Zw";
            v.DSRemark = "lG8vG6ogQdsBGC";
            v.Receipt_delID = AddInfo_Receipt_del();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Info_Donor_del>().Find(v.ID);
				
                Assert.AreEqual(data.LongevityName, "mp6mZS");
                Assert.AreEqual(data.DeceasedName_1, "URip0B3V");
                Assert.AreEqual(data.DeceasedName_2, "yZ5AobN");
                Assert.AreEqual(data.DeceasedName_3, "s9dys59BFWU8Cl");
                Assert.AreEqual(data.BenefactorName, "1qhpafih6N0LzkuxW");
                Assert.AreEqual(data.Sum, 0);
                Assert.AreEqual(data.SerialCode, "ISv1PFAAoYQy1Zw");
                Assert.AreEqual(data.DSRemark, "lG8vG6ogQdsBGC");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Info_Donor_del v = new Info_Donor_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.LongevityName = "mp6mZS";
                v.DeceasedName_1 = "URip0B3V";
                v.DeceasedName_2 = "yZ5AobN";
                v.DeceasedName_3 = "s9dys59BFWU8Cl";
                v.BenefactorName = "1qhpafih6N0LzkuxW";
                v.Sum = 0;
                v.SerialCode = "ISv1PFAAoYQy1Zw";
                v.DSRemark = "lG8vG6ogQdsBGC";
                v.Receipt_delID = AddInfo_Receipt_del();
                context.Set<Info_Donor_del>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Donor_delVM));

            Info_Donor_delVM vm = rv.Model as Info_Donor_delVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Info_Donor_del();
            v.ID = vm.Entity.ID;
       		
            v.LongevityName = "1p1Q3HnN";
            v.DeceasedName_1 = "5Il";
            v.DeceasedName_2 = "0lIa1Kp6VuseDS";
            v.DeceasedName_3 = "TCkbtc";
            v.BenefactorName = "6";
            v.Sum = 57;
            v.SerialCode = "7enoFFw0gXoBmN";
            v.DSRemark = "q3etyRRJYWbi1";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.LongevityName", "");
            vm.FC.Add("Entity.DeceasedName_1", "");
            vm.FC.Add("Entity.DeceasedName_2", "");
            vm.FC.Add("Entity.DeceasedName_3", "");
            vm.FC.Add("Entity.BenefactorName", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.SerialCode", "");
            vm.FC.Add("Entity.DSRemark", "");
            vm.FC.Add("Entity.Receipt_delID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Info_Donor_del>().Find(v.ID);
 				
                Assert.AreEqual(data.LongevityName, "1p1Q3HnN");
                Assert.AreEqual(data.DeceasedName_1, "5Il");
                Assert.AreEqual(data.DeceasedName_2, "0lIa1Kp6VuseDS");
                Assert.AreEqual(data.DeceasedName_3, "TCkbtc");
                Assert.AreEqual(data.BenefactorName, "6");
                Assert.AreEqual(data.Sum, 57);
                Assert.AreEqual(data.SerialCode, "7enoFFw0gXoBmN");
                Assert.AreEqual(data.DSRemark, "q3etyRRJYWbi1");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Info_Donor_del v = new Info_Donor_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.LongevityName = "mp6mZS";
                v.DeceasedName_1 = "URip0B3V";
                v.DeceasedName_2 = "yZ5AobN";
                v.DeceasedName_3 = "s9dys59BFWU8Cl";
                v.BenefactorName = "1qhpafih6N0LzkuxW";
                v.Sum = 0;
                v.SerialCode = "ISv1PFAAoYQy1Zw";
                v.DSRemark = "lG8vG6ogQdsBGC";
                v.Receipt_delID = AddInfo_Receipt_del();
                context.Set<Info_Donor_del>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Donor_delVM));

            Info_Donor_delVM vm = rv.Model as Info_Donor_delVM;
            v = new Info_Donor_del();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Info_Donor_del>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Info_Donor_del v = new Info_Donor_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.LongevityName = "mp6mZS";
                v.DeceasedName_1 = "URip0B3V";
                v.DeceasedName_2 = "yZ5AobN";
                v.DeceasedName_3 = "s9dys59BFWU8Cl";
                v.BenefactorName = "1qhpafih6N0LzkuxW";
                v.Sum = 0;
                v.SerialCode = "ISv1PFAAoYQy1Zw";
                v.DSRemark = "lG8vG6ogQdsBGC";
                v.Receipt_delID = AddInfo_Receipt_del();
                context.Set<Info_Donor_del>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            Info_Donor_del v1 = new Info_Donor_del();
            Info_Donor_del v2 = new Info_Donor_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.LongevityName = "mp6mZS";
                v1.DeceasedName_1 = "URip0B3V";
                v1.DeceasedName_2 = "yZ5AobN";
                v1.DeceasedName_3 = "s9dys59BFWU8Cl";
                v1.BenefactorName = "1qhpafih6N0LzkuxW";
                v1.Sum = 0;
                v1.SerialCode = "ISv1PFAAoYQy1Zw";
                v1.DSRemark = "lG8vG6ogQdsBGC";
                v1.Receipt_delID = AddInfo_Receipt_del();
                v2.LongevityName = "1p1Q3HnN";
                v2.DeceasedName_1 = "5Il";
                v2.DeceasedName_2 = "0lIa1Kp6VuseDS";
                v2.DeceasedName_3 = "TCkbtc";
                v2.BenefactorName = "6";
                v2.Sum = 57;
                v2.SerialCode = "7enoFFw0gXoBmN";
                v2.DSRemark = "q3etyRRJYWbi1";
                v2.Receipt_delID = v1.Receipt_delID; 
                context.Set<Info_Donor_del>().Add(v1);
                context.Set<Info_Donor_del>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Donor_delBatchVM));

            Info_Donor_delBatchVM vm = rv.Model as Info_Donor_delBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Info_Donor_del>().Find(v1.ID);
                var data2 = context.Set<Info_Donor_del>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            Info_Donor_del v1 = new Info_Donor_del();
            Info_Donor_del v2 = new Info_Donor_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.LongevityName = "mp6mZS";
                v1.DeceasedName_1 = "URip0B3V";
                v1.DeceasedName_2 = "yZ5AobN";
                v1.DeceasedName_3 = "s9dys59BFWU8Cl";
                v1.BenefactorName = "1qhpafih6N0LzkuxW";
                v1.Sum = 0;
                v1.SerialCode = "ISv1PFAAoYQy1Zw";
                v1.DSRemark = "lG8vG6ogQdsBGC";
                v1.Receipt_delID = AddInfo_Receipt_del();
                v2.LongevityName = "1p1Q3HnN";
                v2.DeceasedName_1 = "5Il";
                v2.DeceasedName_2 = "0lIa1Kp6VuseDS";
                v2.DeceasedName_3 = "TCkbtc";
                v2.BenefactorName = "6";
                v2.Sum = 57;
                v2.SerialCode = "7enoFFw0gXoBmN";
                v2.DSRemark = "q3etyRRJYWbi1";
                v2.Receipt_delID = v1.Receipt_delID; 
                context.Set<Info_Donor_del>().Add(v1);
                context.Set<Info_Donor_del>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Donor_delBatchVM));

            Info_Donor_delBatchVM vm = rv.Model as Info_Donor_delBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Info_Donor_del>().Find(v1.ID);
                var data2 = context.Set<Info_Donor_del>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as Info_Donor_delListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddInfo_Receipt_del()
        {
            Info_Receipt_del v = new Info_Receipt_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ReceiptNumber = "a5MrTWjsYdoO";
                v.ReceiptDate = DateTime.Parse("2022-10-22 16:51:10");
                v.DharmaServiceYear = 57;
                v.DharmaServiceName = "wfQ9NNU4Jqp71ThCZ";
                v.ReceiptOwn = "X9NmwnCd0LByoL";
                v.ContactName = "z3kJEP";
                v.ContactPhone = "OV3QGxfWO7GDfbH";
                v.Sum = 95;
                v.DSRemark = "MVAzf1DYOW9";
                context.Set<Info_Receipt_del>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
