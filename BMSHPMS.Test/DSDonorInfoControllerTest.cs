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
			
            v.LongevityName = "Gj";
            v.DeceasedName = "7X";
            v.BenefactorName = "lkglqVi4J";
            v.Sum = 26;
            v.Serial = "zyAK5nKvSjDE7tcPu";
            v.DSRemark = "YwkTgKGpy0BXb01MP";
            v.ReceiptNumber = "8EO";
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonorInfo>().Find(v.ID);
				
                Assert.AreEqual(data.LongevityName, "Gj");
                Assert.AreEqual(data.DeceasedName, "7X");
                Assert.AreEqual(data.BenefactorName, "lkglqVi4J");
                Assert.AreEqual(data.Sum, 26);
                Assert.AreEqual(data.Serial, "zyAK5nKvSjDE7tcPu");
                Assert.AreEqual(data.DSRemark, "YwkTgKGpy0BXb01MP");
                Assert.AreEqual(data.ReceiptNumber, "8EO");
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
       			
                v.LongevityName = "Gj";
                v.DeceasedName = "7X";
                v.BenefactorName = "lkglqVi4J";
                v.Sum = 26;
                v.Serial = "zyAK5nKvSjDE7tcPu";
                v.DSRemark = "YwkTgKGpy0BXb01MP";
                v.ReceiptNumber = "8EO";
                context.Set<DSDonorInfo>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonorInfoVM));

            DSDonorInfoVM vm = rv.Model as DSDonorInfoVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSDonorInfo();
            v.ID = vm.Entity.ID;
       		
            v.LongevityName = "sMidJCwc";
            v.DeceasedName = "MQO4lpL";
            v.BenefactorName = "z5bqCSz";
            v.Sum = 47;
            v.Serial = "x5ZfBop3";
            v.DSRemark = "kATm0Mbo8J6RPbUPg6";
            v.ReceiptNumber = "wHrvgI5kHNk";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.LongevityName", "");
            vm.FC.Add("Entity.DeceasedName", "");
            vm.FC.Add("Entity.BenefactorName", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.Serial", "");
            vm.FC.Add("Entity.DSRemark", "");
            vm.FC.Add("Entity.ReceiptNumber", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonorInfo>().Find(v.ID);
 				
                Assert.AreEqual(data.LongevityName, "sMidJCwc");
                Assert.AreEqual(data.DeceasedName, "MQO4lpL");
                Assert.AreEqual(data.BenefactorName, "z5bqCSz");
                Assert.AreEqual(data.Sum, 47);
                Assert.AreEqual(data.Serial, "x5ZfBop3");
                Assert.AreEqual(data.DSRemark, "kATm0Mbo8J6RPbUPg6");
                Assert.AreEqual(data.ReceiptNumber, "wHrvgI5kHNk");
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
        		
                v.LongevityName = "Gj";
                v.DeceasedName = "7X";
                v.BenefactorName = "lkglqVi4J";
                v.Sum = 26;
                v.Serial = "zyAK5nKvSjDE7tcPu";
                v.DSRemark = "YwkTgKGpy0BXb01MP";
                v.ReceiptNumber = "8EO";
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
				
                v.LongevityName = "Gj";
                v.DeceasedName = "7X";
                v.BenefactorName = "lkglqVi4J";
                v.Sum = 26;
                v.Serial = "zyAK5nKvSjDE7tcPu";
                v.DSRemark = "YwkTgKGpy0BXb01MP";
                v.ReceiptNumber = "8EO";
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
				
                v1.LongevityName = "Gj";
                v1.DeceasedName = "7X";
                v1.BenefactorName = "lkglqVi4J";
                v1.Sum = 26;
                v1.Serial = "zyAK5nKvSjDE7tcPu";
                v1.DSRemark = "YwkTgKGpy0BXb01MP";
                v1.ReceiptNumber = "8EO";
                v2.LongevityName = "sMidJCwc";
                v2.DeceasedName = "MQO4lpL";
                v2.BenefactorName = "z5bqCSz";
                v2.Sum = 47;
                v2.Serial = "x5ZfBop3";
                v2.DSRemark = "kATm0Mbo8J6RPbUPg6";
                v2.ReceiptNumber = "wHrvgI5kHNk";
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
				
                v1.LongevityName = "Gj";
                v1.DeceasedName = "7X";
                v1.BenefactorName = "lkglqVi4J";
                v1.Sum = 26;
                v1.Serial = "zyAK5nKvSjDE7tcPu";
                v1.DSRemark = "YwkTgKGpy0BXb01MP";
                v1.ReceiptNumber = "8EO";
                v2.LongevityName = "sMidJCwc";
                v2.DeceasedName = "MQO4lpL";
                v2.BenefactorName = "z5bqCSz";
                v2.Sum = 47;
                v2.Serial = "x5ZfBop3";
                v2.DSRemark = "kATm0Mbo8J6RPbUPg6";
                v2.ReceiptNumber = "wHrvgI5kHNk";
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


    }
}
