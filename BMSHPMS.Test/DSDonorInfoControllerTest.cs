using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSManage.Controllers;
using BMSHPMS.DSManage.ViewModels.DSDonorInfoVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSDonorInfoControllerTest
    {
        private DSDonorInfoController _controller;
        private string _seed;

        public DSDonorInfoControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSDonorInfoController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSDonorInfoListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorInfoVM));

            DSDonorInfoVM vm = rv.Model as DSDonorInfoVM;
            DSDonorInfo v = new DSDonorInfo();
			
            v.LongevityName = "9db3LZP3R64mv9uFn";
            v.DeceasedName = "R";
            v.BenefactorName = "lmyycZZd362uQ";
            v.Sum = 42;
            v.SerialCode = "4no3bJjaIORDl30BKX";
            v.DSRemark = "nJPL";
            v.ReceiptInfoID = AddDSReceiptInfo();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonorInfo>().Find(v.ID);
				
                Assert.AreEqual(data.LongevityName, "9db3LZP3R64mv9uFn");
                Assert.AreEqual(data.DeceasedName, "R");
                Assert.AreEqual(data.BenefactorName, "lmyycZZd362uQ");
                Assert.AreEqual(data.Sum, 42);
                Assert.AreEqual(data.SerialCode, "4no3bJjaIORDl30BKX");
                Assert.AreEqual(data.DSRemark, "nJPL");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSDonorInfo v = new DSDonorInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.LongevityName = "9db3LZP3R64mv9uFn";
                v.DeceasedName = "R";
                v.BenefactorName = "lmyycZZd362uQ";
                v.Sum = 42;
                v.SerialCode = "4no3bJjaIORDl30BKX";
                v.DSRemark = "nJPL";
                v.ReceiptInfoID = AddDSReceiptInfo();
                context.Set<DSDonorInfo>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorInfoVM));

            DSDonorInfoVM vm = rv.Model as DSDonorInfoVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSDonorInfo();
            v.ID = vm.Entity.ID;
       		
            v.LongevityName = "TsDle2v";
            v.DeceasedName = "0SbtXIMBH";
            v.BenefactorName = "kUDm2OU0aj8HP3G";
            v.Sum = 95;
            v.SerialCode = "EOLH0d2";
            v.DSRemark = "Z7yE4";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.LongevityName", "");
            vm.FC.Add("Entity.DeceasedName", "");
            vm.FC.Add("Entity.BenefactorName", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.SerialCode", "");
            vm.FC.Add("Entity.DSRemark", "");
            vm.FC.Add("Entity.ReceiptInfoID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonorInfo>().Find(v.ID);
 				
                Assert.AreEqual(data.LongevityName, "TsDle2v");
                Assert.AreEqual(data.DeceasedName, "0SbtXIMBH");
                Assert.AreEqual(data.BenefactorName, "kUDm2OU0aj8HP3G");
                Assert.AreEqual(data.Sum, 95);
                Assert.AreEqual(data.SerialCode, "EOLH0d2");
                Assert.AreEqual(data.DSRemark, "Z7yE4");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSDonorInfo v = new DSDonorInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.LongevityName = "9db3LZP3R64mv9uFn";
                v.DeceasedName = "R";
                v.BenefactorName = "lmyycZZd362uQ";
                v.Sum = 42;
                v.SerialCode = "4no3bJjaIORDl30BKX";
                v.DSRemark = "nJPL";
                v.ReceiptInfoID = AddDSReceiptInfo();
                context.Set<DSDonorInfo>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorInfoVM));

            DSDonorInfoVM vm = rv.Model as DSDonorInfoVM;
            v = new DSDonorInfo();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonorInfo>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSDonorInfo v = new DSDonorInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.LongevityName = "9db3LZP3R64mv9uFn";
                v.DeceasedName = "R";
                v.BenefactorName = "lmyycZZd362uQ";
                v.Sum = 42;
                v.SerialCode = "4no3bJjaIORDl30BKX";
                v.DSRemark = "nJPL";
                v.ReceiptInfoID = AddDSReceiptInfo();
                context.Set<DSDonorInfo>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSDonorInfo v1 = new DSDonorInfo();
            DSDonorInfo v2 = new DSDonorInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.LongevityName = "9db3LZP3R64mv9uFn";
                v1.DeceasedName = "R";
                v1.BenefactorName = "lmyycZZd362uQ";
                v1.Sum = 42;
                v1.SerialCode = "4no3bJjaIORDl30BKX";
                v1.DSRemark = "nJPL";
                v1.ReceiptInfoID = AddDSReceiptInfo();
                v2.LongevityName = "TsDle2v";
                v2.DeceasedName = "0SbtXIMBH";
                v2.BenefactorName = "kUDm2OU0aj8HP3G";
                v2.Sum = 95;
                v2.SerialCode = "EOLH0d2";
                v2.DSRemark = "Z7yE4";
                v2.ReceiptInfoID = v1.ReceiptInfoID; 
                context.Set<DSDonorInfo>().Add(v1);
                context.Set<DSDonorInfo>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorInfoBatchVM));

            DSDonorInfoBatchVM vm = rv.Model as DSDonorInfoBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSDonorInfo>().Find(v1.ID);
                var data2 = context.Set<DSDonorInfo>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSDonorInfo v1 = new DSDonorInfo();
            DSDonorInfo v2 = new DSDonorInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.LongevityName = "9db3LZP3R64mv9uFn";
                v1.DeceasedName = "R";
                v1.BenefactorName = "lmyycZZd362uQ";
                v1.Sum = 42;
                v1.SerialCode = "4no3bJjaIORDl30BKX";
                v1.DSRemark = "nJPL";
                v1.ReceiptInfoID = AddDSReceiptInfo();
                v2.LongevityName = "TsDle2v";
                v2.DeceasedName = "0SbtXIMBH";
                v2.BenefactorName = "kUDm2OU0aj8HP3G";
                v2.Sum = 95;
                v2.SerialCode = "EOLH0d2";
                v2.DSRemark = "Z7yE4";
                v2.ReceiptInfoID = v1.ReceiptInfoID; 
                context.Set<DSDonorInfo>().Add(v1);
                context.Set<DSDonorInfo>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorInfoBatchVM));

            DSDonorInfoBatchVM vm = rv.Model as DSDonorInfoBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSDonorInfo>().Find(v1.ID);
                var data2 = context.Set<DSDonorInfo>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSDonorInfoListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSReceiptInfo()
        {
            DSReceiptInfo v = new DSReceiptInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ReceiptNumber = "ae1zQY";
                v.ReceiptOwn = "qdU7wt7jQSpzOmJE";
                v.ContactName = "nDSM2rSy7iUz";
                v.ContactPhone = "MwIBHJeqhhZl3";
                v.Sum = 31;
                v.DSProjectName = "W2IAOjmfGlBwzlE";
                v.DSRemark = "l";
                v.ReceiptDate = DateTime.Parse("2023-12-19 14:07:25");
                context.Set<DSReceiptInfo>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
