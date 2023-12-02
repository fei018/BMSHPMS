using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.Controllers;
using BMSHPMS.Areas.BackStage.ViewModels.T_LongevityPlaqueVMs;
using BMSHPMS.Models.Plaque;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class T_LongevityPlaqueControllerTest
    {
        private T_LongevityPlaqueController _controller;
        private string _seed;

        public T_LongevityPlaqueControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<T_LongevityPlaqueController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as T_LongevityPlaqueListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(T_LongevityPlaqueVM));

            T_LongevityPlaqueVM vm = rv.Model as T_LongevityPlaqueVM;
            T_LongevityPlaque v = new T_LongevityPlaque();
			
            v.Name = "VOH9uhd";
            v.Sum = 93;
            v.Serial = "LkVMVY0SjA8x";
            v.Remark = "IvX58rO";
            v.ReceiptID = AddT_Receipt();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<T_LongevityPlaque>().Find(v.ID);
				
                Assert.AreEqual(data.Name, "VOH9uhd");
                Assert.AreEqual(data.Sum, 93);
                Assert.AreEqual(data.Serial, "LkVMVY0SjA8x");
                Assert.AreEqual(data.Remark, "IvX58rO");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            T_LongevityPlaque v = new T_LongevityPlaque();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Name = "VOH9uhd";
                v.Sum = 93;
                v.Serial = "LkVMVY0SjA8x";
                v.Remark = "IvX58rO";
                v.ReceiptID = AddT_Receipt();
                context.Set<T_LongevityPlaque>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(T_LongevityPlaqueVM));

            T_LongevityPlaqueVM vm = rv.Model as T_LongevityPlaqueVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new T_LongevityPlaque();
            v.ID = vm.Entity.ID;
       		
            v.Name = "Npt878KA8O";
            v.Sum = 51;
            v.Serial = "l5d";
            v.Remark = "L";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Name", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.Serial", "");
            vm.FC.Add("Entity.Remark", "");
            vm.FC.Add("Entity.ReceiptID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<T_LongevityPlaque>().Find(v.ID);
 				
                Assert.AreEqual(data.Name, "Npt878KA8O");
                Assert.AreEqual(data.Sum, 51);
                Assert.AreEqual(data.Serial, "l5d");
                Assert.AreEqual(data.Remark, "L");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            T_LongevityPlaque v = new T_LongevityPlaque();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Name = "VOH9uhd";
                v.Sum = 93;
                v.Serial = "LkVMVY0SjA8x";
                v.Remark = "IvX58rO";
                v.ReceiptID = AddT_Receipt();
                context.Set<T_LongevityPlaque>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(T_LongevityPlaqueVM));

            T_LongevityPlaqueVM vm = rv.Model as T_LongevityPlaqueVM;
            v = new T_LongevityPlaque();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<T_LongevityPlaque>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            T_LongevityPlaque v = new T_LongevityPlaque();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Name = "VOH9uhd";
                v.Sum = 93;
                v.Serial = "LkVMVY0SjA8x";
                v.Remark = "IvX58rO";
                v.ReceiptID = AddT_Receipt();
                context.Set<T_LongevityPlaque>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            T_LongevityPlaque v1 = new T_LongevityPlaque();
            T_LongevityPlaque v2 = new T_LongevityPlaque();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "VOH9uhd";
                v1.Sum = 93;
                v1.Serial = "LkVMVY0SjA8x";
                v1.Remark = "IvX58rO";
                v1.ReceiptID = AddT_Receipt();
                v2.Name = "Npt878KA8O";
                v2.Sum = 51;
                v2.Serial = "l5d";
                v2.Remark = "L";
                v2.ReceiptID = v1.ReceiptID; 
                context.Set<T_LongevityPlaque>().Add(v1);
                context.Set<T_LongevityPlaque>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(T_LongevityPlaqueBatchVM));

            T_LongevityPlaqueBatchVM vm = rv.Model as T_LongevityPlaqueBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.LinkedVM.Name = "HeOXuddzOESZIX";
            vm.LinkedVM.Sum = 78;
            vm.LinkedVM.Serial = "B7RBkGLWtaZJE";
            vm.LinkedVM.Remark = "PbyIfo";
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("LinkedVM.Name", "");
            vm.FC.Add("LinkedVM.Sum", "");
            vm.FC.Add("LinkedVM.Serial", "");
            vm.FC.Add("LinkedVM.Remark", "");
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<T_LongevityPlaque>().Find(v1.ID);
                var data2 = context.Set<T_LongevityPlaque>().Find(v2.ID);
 				
                Assert.AreEqual(data1.Name, "HeOXuddzOESZIX");
                Assert.AreEqual(data2.Name, "HeOXuddzOESZIX");
                Assert.AreEqual(data1.Sum, 78);
                Assert.AreEqual(data2.Sum, 78);
                Assert.AreEqual(data1.Serial, "B7RBkGLWtaZJE");
                Assert.AreEqual(data2.Serial, "B7RBkGLWtaZJE");
                Assert.AreEqual(data1.Remark, "PbyIfo");
                Assert.AreEqual(data2.Remark, "PbyIfo");
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            T_LongevityPlaque v1 = new T_LongevityPlaque();
            T_LongevityPlaque v2 = new T_LongevityPlaque();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Name = "VOH9uhd";
                v1.Sum = 93;
                v1.Serial = "LkVMVY0SjA8x";
                v1.Remark = "IvX58rO";
                v1.ReceiptID = AddT_Receipt();
                v2.Name = "Npt878KA8O";
                v2.Sum = 51;
                v2.Serial = "l5d";
                v2.Remark = "L";
                v2.ReceiptID = v1.ReceiptID; 
                context.Set<T_LongevityPlaque>().Add(v1);
                context.Set<T_LongevityPlaque>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(T_LongevityPlaqueBatchVM));

            T_LongevityPlaqueBatchVM vm = rv.Model as T_LongevityPlaqueBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<T_LongevityPlaque>().Find(v1.ID);
                var data2 = context.Set<T_LongevityPlaque>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as T_LongevityPlaqueListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddT_Receipt()
        {
            T_Receipt v = new T_Receipt();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ReceiptNumber = "K1TYbzWsZYqf";
                v.ContactName = "yqrWEQzS1E";
                v.ContactPhone = "tbv3EDYRRVuKjMD03N";
                v.Sum = 99;
                context.Set<T_Receipt>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
