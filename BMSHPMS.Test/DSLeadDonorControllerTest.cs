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
    public class DSLeadDonorControllerTest
    {
        private DSLeadDonorController _controller;
        private string _seed;

        public DSLeadDonorControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSLeadDonorController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSLeadDonorListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSLeadDonorVM));

            DSLeadDonorVM vm = rv.Model as DSLeadDonorVM;
            DSLeadDonor v = new DSLeadDonor();
			
            v.LongevityName = "xeh0dbfHKKM";
            v.DeceasedName = "hWc92v";
            v.BenefactorName = "6";
            v.Sum = 78;
            v.Serial = "dK2sEemY1ir4Rvr";
            v.PRemark = "INZWQ93uuk3U8X2H4hR";
            v.ReceiptID = AddDSReceipt();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLeadDonor>().Find(v.ID);
				
                Assert.AreEqual(data.LongevityName, "xeh0dbfHKKM");
                Assert.AreEqual(data.DeceasedName, "hWc92v");
                Assert.AreEqual(data.BenefactorName, "6");
                Assert.AreEqual(data.Sum, 78);
                Assert.AreEqual(data.Serial, "dK2sEemY1ir4Rvr");
                Assert.AreEqual(data.PRemark, "INZWQ93uuk3U8X2H4hR");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSLeadDonor v = new DSLeadDonor();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.LongevityName = "xeh0dbfHKKM";
                v.DeceasedName = "hWc92v";
                v.BenefactorName = "6";
                v.Sum = 78;
                v.Serial = "dK2sEemY1ir4Rvr";
                v.PRemark = "INZWQ93uuk3U8X2H4hR";
                v.ReceiptID = AddDSReceipt();
                context.Set<DSLeadDonor>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSLeadDonorVM));

            DSLeadDonorVM vm = rv.Model as DSLeadDonorVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSLeadDonor();
            v.ID = vm.Entity.ID;
       		
            v.LongevityName = "dJioDWrkLhVggKcZty";
            v.DeceasedName = "kvbGufaBvEuKl0";
            v.BenefactorName = "VRJTxxVD5jJY8HKO";
            v.Sum = 67;
            v.Serial = "xvT";
            v.PRemark = "ioePopyefpr1J4XQ";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.LongevityName", "");
            vm.FC.Add("Entity.DeceasedName", "");
            vm.FC.Add("Entity.BenefactorName", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.Serial", "");
            vm.FC.Add("Entity.PRemark", "");
            vm.FC.Add("Entity.ReceiptID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLeadDonor>().Find(v.ID);
 				
                Assert.AreEqual(data.LongevityName, "dJioDWrkLhVggKcZty");
                Assert.AreEqual(data.DeceasedName, "kvbGufaBvEuKl0");
                Assert.AreEqual(data.BenefactorName, "VRJTxxVD5jJY8HKO");
                Assert.AreEqual(data.Sum, 67);
                Assert.AreEqual(data.Serial, "xvT");
                Assert.AreEqual(data.PRemark, "ioePopyefpr1J4XQ");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSLeadDonor v = new DSLeadDonor();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.LongevityName = "xeh0dbfHKKM";
                v.DeceasedName = "hWc92v";
                v.BenefactorName = "6";
                v.Sum = 78;
                v.Serial = "dK2sEemY1ir4Rvr";
                v.PRemark = "INZWQ93uuk3U8X2H4hR";
                v.ReceiptID = AddDSReceipt();
                context.Set<DSLeadDonor>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSLeadDonorVM));

            DSLeadDonorVM vm = rv.Model as DSLeadDonorVM;
            v = new DSLeadDonor();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLeadDonor>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSLeadDonor v = new DSLeadDonor();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.LongevityName = "xeh0dbfHKKM";
                v.DeceasedName = "hWc92v";
                v.BenefactorName = "6";
                v.Sum = 78;
                v.Serial = "dK2sEemY1ir4Rvr";
                v.PRemark = "INZWQ93uuk3U8X2H4hR";
                v.ReceiptID = AddDSReceipt();
                context.Set<DSLeadDonor>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSLeadDonor v1 = new DSLeadDonor();
            DSLeadDonor v2 = new DSLeadDonor();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.LongevityName = "xeh0dbfHKKM";
                v1.DeceasedName = "hWc92v";
                v1.BenefactorName = "6";
                v1.Sum = 78;
                v1.Serial = "dK2sEemY1ir4Rvr";
                v1.PRemark = "INZWQ93uuk3U8X2H4hR";
                v1.ReceiptID = AddDSReceipt();
                v2.LongevityName = "dJioDWrkLhVggKcZty";
                v2.DeceasedName = "kvbGufaBvEuKl0";
                v2.BenefactorName = "VRJTxxVD5jJY8HKO";
                v2.Sum = 67;
                v2.Serial = "xvT";
                v2.PRemark = "ioePopyefpr1J4XQ";
                v2.ReceiptID = v1.ReceiptID; 
                context.Set<DSLeadDonor>().Add(v1);
                context.Set<DSLeadDonor>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSLeadDonorBatchVM));

            DSLeadDonorBatchVM vm = rv.Model as DSLeadDonorBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSLeadDonor>().Find(v1.ID);
                var data2 = context.Set<DSLeadDonor>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSLeadDonor v1 = new DSLeadDonor();
            DSLeadDonor v2 = new DSLeadDonor();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.LongevityName = "xeh0dbfHKKM";
                v1.DeceasedName = "hWc92v";
                v1.BenefactorName = "6";
                v1.Sum = 78;
                v1.Serial = "dK2sEemY1ir4Rvr";
                v1.PRemark = "INZWQ93uuk3U8X2H4hR";
                v1.ReceiptID = AddDSReceipt();
                v2.LongevityName = "dJioDWrkLhVggKcZty";
                v2.DeceasedName = "kvbGufaBvEuKl0";
                v2.BenefactorName = "VRJTxxVD5jJY8HKO";
                v2.Sum = 67;
                v2.Serial = "xvT";
                v2.PRemark = "ioePopyefpr1J4XQ";
                v2.ReceiptID = v1.ReceiptID; 
                context.Set<DSLeadDonor>().Add(v1);
                context.Set<DSLeadDonor>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSLeadDonorBatchVM));

            DSLeadDonorBatchVM vm = rv.Model as DSLeadDonorBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSLeadDonor>().Find(v1.ID);
                var data2 = context.Set<DSLeadDonor>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSLeadDonorListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSReceipt()
        {
            DSReceipt v = new DSReceipt();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ReceiptNumber = "cQ";
                v.ReceiptOwn = "f";
                v.ContactName = "k0DftP";
                v.ContactPhone = "paICJAIj04wqXda09";
                v.Sum = 98;
                v.PRemark = "DznxjyqMS6Jdhsaxa";
                v.ReceiptDate = DateTime.Parse("2023-10-03 10:32:54");
                context.Set<DSReceipt>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
