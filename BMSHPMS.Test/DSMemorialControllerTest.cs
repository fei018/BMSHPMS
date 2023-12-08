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
    public class DSMemorialControllerTest
    {
        private DSMemorialController _controller;
        private string _seed;

        public DSMemorialControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSMemorialController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSMemorialListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialVM));

            DSMemorialVM vm = rv.Model as DSMemorialVM;
            DSMemorial v = new DSMemorial();
			
            v.Serial = "1Ns6Xc0lez";
            v.BenefactorName = "RZqEoohAa2QBw";
            v.DeceasedName = "lRy56pSo";
            v.Sum = 82;
            v.PRemark = "X76zzHmGoGxxNOQLBF";
            v.ReceiptID = AddDSReceipt();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSMemorial>().Find(v.ID);
				
                Assert.AreEqual(data.Serial, "1Ns6Xc0lez");
                Assert.AreEqual(data.BenefactorName, "RZqEoohAa2QBw");
                Assert.AreEqual(data.DeceasedName, "lRy56pSo");
                Assert.AreEqual(data.Sum, 82);
                Assert.AreEqual(data.PRemark, "X76zzHmGoGxxNOQLBF");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSMemorial v = new DSMemorial();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Serial = "1Ns6Xc0lez";
                v.BenefactorName = "RZqEoohAa2QBw";
                v.DeceasedName = "lRy56pSo";
                v.Sum = 82;
                v.PRemark = "X76zzHmGoGxxNOQLBF";
                v.ReceiptID = AddDSReceipt();
                context.Set<DSMemorial>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialVM));

            DSMemorialVM vm = rv.Model as DSMemorialVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSMemorial();
            v.ID = vm.Entity.ID;
       		
            v.Serial = "ifUWfZOPUMhtEpV";
            v.BenefactorName = "E2";
            v.DeceasedName = "6YvS4nJPGU";
            v.Sum = 69;
            v.PRemark = "Lo2craklgYMk2";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Serial", "");
            vm.FC.Add("Entity.BenefactorName", "");
            vm.FC.Add("Entity.DeceasedName", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.PRemark", "");
            vm.FC.Add("Entity.ReceiptID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSMemorial>().Find(v.ID);
 				
                Assert.AreEqual(data.Serial, "ifUWfZOPUMhtEpV");
                Assert.AreEqual(data.BenefactorName, "E2");
                Assert.AreEqual(data.DeceasedName, "6YvS4nJPGU");
                Assert.AreEqual(data.Sum, 69);
                Assert.AreEqual(data.PRemark, "Lo2craklgYMk2");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSMemorial v = new DSMemorial();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Serial = "1Ns6Xc0lez";
                v.BenefactorName = "RZqEoohAa2QBw";
                v.DeceasedName = "lRy56pSo";
                v.Sum = 82;
                v.PRemark = "X76zzHmGoGxxNOQLBF";
                v.ReceiptID = AddDSReceipt();
                context.Set<DSMemorial>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialVM));

            DSMemorialVM vm = rv.Model as DSMemorialVM;
            v = new DSMemorial();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSMemorial>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSMemorial v = new DSMemorial();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Serial = "1Ns6Xc0lez";
                v.BenefactorName = "RZqEoohAa2QBw";
                v.DeceasedName = "lRy56pSo";
                v.Sum = 82;
                v.PRemark = "X76zzHmGoGxxNOQLBF";
                v.ReceiptID = AddDSReceipt();
                context.Set<DSMemorial>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSMemorial v1 = new DSMemorial();
            DSMemorial v2 = new DSMemorial();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Serial = "1Ns6Xc0lez";
                v1.BenefactorName = "RZqEoohAa2QBw";
                v1.DeceasedName = "lRy56pSo";
                v1.Sum = 82;
                v1.PRemark = "X76zzHmGoGxxNOQLBF";
                v1.ReceiptID = AddDSReceipt();
                v2.Serial = "ifUWfZOPUMhtEpV";
                v2.BenefactorName = "E2";
                v2.DeceasedName = "6YvS4nJPGU";
                v2.Sum = 69;
                v2.PRemark = "Lo2craklgYMk2";
                v2.ReceiptID = v1.ReceiptID; 
                context.Set<DSMemorial>().Add(v1);
                context.Set<DSMemorial>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialBatchVM));

            DSMemorialBatchVM vm = rv.Model as DSMemorialBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSMemorial>().Find(v1.ID);
                var data2 = context.Set<DSMemorial>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSMemorial v1 = new DSMemorial();
            DSMemorial v2 = new DSMemorial();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Serial = "1Ns6Xc0lez";
                v1.BenefactorName = "RZqEoohAa2QBw";
                v1.DeceasedName = "lRy56pSo";
                v1.Sum = 82;
                v1.PRemark = "X76zzHmGoGxxNOQLBF";
                v1.ReceiptID = AddDSReceipt();
                v2.Serial = "ifUWfZOPUMhtEpV";
                v2.BenefactorName = "E2";
                v2.DeceasedName = "6YvS4nJPGU";
                v2.Sum = 69;
                v2.PRemark = "Lo2craklgYMk2";
                v2.ReceiptID = v1.ReceiptID; 
                context.Set<DSMemorial>().Add(v1);
                context.Set<DSMemorial>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialBatchVM));

            DSMemorialBatchVM vm = rv.Model as DSMemorialBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSMemorial>().Find(v1.ID);
                var data2 = context.Set<DSMemorial>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSMemorialListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSReceipt()
        {
            DSReceipt v = new DSReceipt();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ReceiptNumber = "ySC23kE91b2wfMCOw";
                v.ReceiptOwn = "5AQ50aiLkl";
                v.ContactName = "gGBgnA2QZ1yL";
                v.ContactPhone = "Gk8pDQER3dlXCHGck";
                v.Sum = 44;
                v.PRemark = "KmtkP";
                v.ReceiptDate = DateTime.Parse("2023-09-14 10:35:34");
                context.Set<DSReceipt>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
