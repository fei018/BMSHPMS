using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSManage.Controllers;
using BMSHPMS.DSManage.ViewModels.DSLongevityInfoVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSLongevityInfoControllerTest
    {
        private DSLongevityInfoController _controller;
        private string _seed;

        public DSLongevityInfoControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSLongevityInfoController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSLongevityInfoListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityInfoVM));

            DSLongevityInfoVM vm = rv.Model as DSLongevityInfoVM;
            DSLongevityInfo v = new DSLongevityInfo();
			
            v.Name = "eNegLGNscqvf06";
            v.Sum = 14;
            v.SerialCode = "Mka8ci1Ct8GpXHTieEe";
            v.DSRemark = "qWUV";
            v.ReceiptInfoID = AddDSReceiptInfo();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLongevityInfo>().Find(v.ID);
				
                Assert.AreEqual(data.Name, "eNegLGNscqvf06");
                Assert.AreEqual(data.Sum, 14);
                Assert.AreEqual(data.SerialCode, "Mka8ci1Ct8GpXHTieEe");
                Assert.AreEqual(data.DSRemark, "qWUV");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSLongevityInfo v = new DSLongevityInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "eNegLGNscqvf06";
                v.Sum = 14;
                v.SerialCode = "Mka8ci1Ct8GpXHTieEe";
                v.DSRemark = "qWUV";
                v.ReceiptInfoID = AddDSReceiptInfo();
                context.Set<DSLongevityInfo>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityInfoVM));

            DSLongevityInfoVM vm = rv.Model as DSLongevityInfoVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSLongevityInfo();
            v.ID = vm.Entity.ID;
       		
            v.Name = "YvgUtCYLa";
            v.Sum = 89;
            v.SerialCode = "eNpLLBQ";
            v.DSRemark = "ZOqm4SXTrFx56wwUZx";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.SerialCode", "");
            vm.FC.Add("Entity.DSRemark", "");
            vm.FC.Add("Entity.ReceiptInfoID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLongevityInfo>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "YvgUtCYLa");
                Assert.AreEqual(data.Sum, 89);
                Assert.AreEqual(data.SerialCode, "eNpLLBQ");
                Assert.AreEqual(data.DSRemark, "ZOqm4SXTrFx56wwUZx");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSLongevityInfo v = new DSLongevityInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "eNegLGNscqvf06";
                v.Sum = 14;
                v.SerialCode = "Mka8ci1Ct8GpXHTieEe";
                v.DSRemark = "qWUV";
                v.ReceiptInfoID = AddDSReceiptInfo();
                context.Set<DSLongevityInfo>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityInfoVM));

            DSLongevityInfoVM vm = rv.Model as DSLongevityInfoVM;
            v = new DSLongevityInfo();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSLongevityInfo>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSLongevityInfo v = new DSLongevityInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Name = "eNegLGNscqvf06";
                v.Sum = 14;
                v.SerialCode = "Mka8ci1Ct8GpXHTieEe";
                v.DSRemark = "qWUV";
                v.ReceiptInfoID = AddDSReceiptInfo();
                context.Set<DSLongevityInfo>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSLongevityInfo v1 = new DSLongevityInfo();
            DSLongevityInfo v2 = new DSLongevityInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "eNegLGNscqvf06";
                v1.Sum = 14;
                v1.SerialCode = "Mka8ci1Ct8GpXHTieEe";
                v1.DSRemark = "qWUV";
                v1.ReceiptInfoID = AddDSReceiptInfo();
                v2.Name = "YvgUtCYLa";
                v2.Sum = 89;
                v2.SerialCode = "eNpLLBQ";
                v2.DSRemark = "ZOqm4SXTrFx56wwUZx";
                v2.ReceiptInfoID = v1.ReceiptInfoID; 
                context.Set<DSLongevityInfo>().Add(v1);
                context.Set<DSLongevityInfo>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityInfoBatchVM));

            DSLongevityInfoBatchVM vm = rv.Model as DSLongevityInfoBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSLongevityInfo>().Find(v1.ID);
                var data2 = context.Set<DSLongevityInfo>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSLongevityInfo v1 = new DSLongevityInfo();
            DSLongevityInfo v2 = new DSLongevityInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "eNegLGNscqvf06";
                v1.Sum = 14;
                v1.SerialCode = "Mka8ci1Ct8GpXHTieEe";
                v1.DSRemark = "qWUV";
                v1.ReceiptInfoID = AddDSReceiptInfo();
                v2.Name = "YvgUtCYLa";
                v2.Sum = 89;
                v2.SerialCode = "eNpLLBQ";
                v2.DSRemark = "ZOqm4SXTrFx56wwUZx";
                v2.ReceiptInfoID = v1.ReceiptInfoID; 
                context.Set<DSLongevityInfo>().Add(v1);
                context.Set<DSLongevityInfo>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSLongevityInfoBatchVM));

            DSLongevityInfoBatchVM vm = rv.Model as DSLongevityInfoBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSLongevityInfo>().Find(v1.ID);
                var data2 = context.Set<DSLongevityInfo>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSLongevityInfoListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSReceiptInfo()
        {
            DSReceiptInfo v = new DSReceiptInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ReceiptNumber = "7gBBO0OxE0Byn7E9";
                v.ReceiptOwn = "TvpiAF";
                v.ContactName = "wzgk";
                v.ContactPhone = "UJTHcmWxzUNhIX";
                v.Sum = 28;
                v.DSProjectName = "ffvEQbYjX";
                v.DSRemark = "mBoWFHo3BH";
                v.ReceiptDate = DateTime.Parse("2024-10-11 14:08:45");
                context.Set<DSReceiptInfo>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
