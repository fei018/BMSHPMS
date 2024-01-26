using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSManage.Controllers;
using BMSHPMS.DSManage.ViewModels.Info_Memorial_delVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class Info_Memorial_delControllerTest
    {
        private Info_Memorial_delController _controller;
        private string _seed;

        public Info_Memorial_delControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<Info_Memorial_delController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as Info_Memorial_delListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Memorial_delVM));

            Info_Memorial_delVM vm = rv.Model as Info_Memorial_delVM;
            Info_Memorial_del v = new Info_Memorial_del();
			
            v.SerialCode = "I";
            v.BenefactorName = "noMhn9iecU6";
            v.DeceasedName_1 = "lvK";
            v.DeceasedName_2 = "S";
            v.DeceasedName_3 = "IMZ2miMOj";
            v.Sum = 11;
            v.DSRemark = "3D9y5Fre";
            v.Receipt_delID = AddInfo_Receipt_del();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Info_Memorial_del>().Find(v.ID);
				
                Assert.AreEqual(data.SerialCode, "I");
                Assert.AreEqual(data.BenefactorName, "noMhn9iecU6");
                Assert.AreEqual(data.DeceasedName_1, "lvK");
                Assert.AreEqual(data.DeceasedName_2, "S");
                Assert.AreEqual(data.DeceasedName_3, "IMZ2miMOj");
                Assert.AreEqual(data.Sum, 11);
                Assert.AreEqual(data.DSRemark, "3D9y5Fre");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            Info_Memorial_del v = new Info_Memorial_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.SerialCode = "I";
                v.BenefactorName = "noMhn9iecU6";
                v.DeceasedName_1 = "lvK";
                v.DeceasedName_2 = "S";
                v.DeceasedName_3 = "IMZ2miMOj";
                v.Sum = 11;
                v.DSRemark = "3D9y5Fre";
                v.Receipt_delID = AddInfo_Receipt_del();
                context.Set<Info_Memorial_del>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Memorial_delVM));

            Info_Memorial_delVM vm = rv.Model as Info_Memorial_delVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new Info_Memorial_del();
            v.ID = vm.Entity.ID;
       		
            v.SerialCode = "Vg3wZO63761l";
            v.BenefactorName = "heaYpb7sr79UP";
            v.DeceasedName_1 = "NzjBQIri";
            v.DeceasedName_2 = "nCV";
            v.DeceasedName_3 = "7QX7";
            v.Sum = 13;
            v.DSRemark = "aTM3hE3Xh7svVQYiZ";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.SerialCode", "");
            vm.FC.Add("Entity.BenefactorName", "");
            vm.FC.Add("Entity.DeceasedName_1", "");
            vm.FC.Add("Entity.DeceasedName_2", "");
            vm.FC.Add("Entity.DeceasedName_3", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.DSRemark", "");
            vm.FC.Add("Entity.Receipt_delID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Info_Memorial_del>().Find(v.ID);
 				
                Assert.AreEqual(data.SerialCode, "Vg3wZO63761l");
                Assert.AreEqual(data.BenefactorName, "heaYpb7sr79UP");
                Assert.AreEqual(data.DeceasedName_1, "NzjBQIri");
                Assert.AreEqual(data.DeceasedName_2, "nCV");
                Assert.AreEqual(data.DeceasedName_3, "7QX7");
                Assert.AreEqual(data.Sum, 13);
                Assert.AreEqual(data.DSRemark, "aTM3hE3Xh7svVQYiZ");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            Info_Memorial_del v = new Info_Memorial_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.SerialCode = "I";
                v.BenefactorName = "noMhn9iecU6";
                v.DeceasedName_1 = "lvK";
                v.DeceasedName_2 = "S";
                v.DeceasedName_3 = "IMZ2miMOj";
                v.Sum = 11;
                v.DSRemark = "3D9y5Fre";
                v.Receipt_delID = AddInfo_Receipt_del();
                context.Set<Info_Memorial_del>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Memorial_delVM));

            Info_Memorial_delVM vm = rv.Model as Info_Memorial_delVM;
            v = new Info_Memorial_del();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<Info_Memorial_del>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            Info_Memorial_del v = new Info_Memorial_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.SerialCode = "I";
                v.BenefactorName = "noMhn9iecU6";
                v.DeceasedName_1 = "lvK";
                v.DeceasedName_2 = "S";
                v.DeceasedName_3 = "IMZ2miMOj";
                v.Sum = 11;
                v.DSRemark = "3D9y5Fre";
                v.Receipt_delID = AddInfo_Receipt_del();
                context.Set<Info_Memorial_del>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            Info_Memorial_del v1 = new Info_Memorial_del();
            Info_Memorial_del v2 = new Info_Memorial_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.SerialCode = "I";
                v1.BenefactorName = "noMhn9iecU6";
                v1.DeceasedName_1 = "lvK";
                v1.DeceasedName_2 = "S";
                v1.DeceasedName_3 = "IMZ2miMOj";
                v1.Sum = 11;
                v1.DSRemark = "3D9y5Fre";
                v1.Receipt_delID = AddInfo_Receipt_del();
                v2.SerialCode = "Vg3wZO63761l";
                v2.BenefactorName = "heaYpb7sr79UP";
                v2.DeceasedName_1 = "NzjBQIri";
                v2.DeceasedName_2 = "nCV";
                v2.DeceasedName_3 = "7QX7";
                v2.Sum = 13;
                v2.DSRemark = "aTM3hE3Xh7svVQYiZ";
                v2.Receipt_delID = v1.Receipt_delID; 
                context.Set<Info_Memorial_del>().Add(v1);
                context.Set<Info_Memorial_del>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Memorial_delBatchVM));

            Info_Memorial_delBatchVM vm = rv.Model as Info_Memorial_delBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Info_Memorial_del>().Find(v1.ID);
                var data2 = context.Set<Info_Memorial_del>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            Info_Memorial_del v1 = new Info_Memorial_del();
            Info_Memorial_del v2 = new Info_Memorial_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.SerialCode = "I";
                v1.BenefactorName = "noMhn9iecU6";
                v1.DeceasedName_1 = "lvK";
                v1.DeceasedName_2 = "S";
                v1.DeceasedName_3 = "IMZ2miMOj";
                v1.Sum = 11;
                v1.DSRemark = "3D9y5Fre";
                v1.Receipt_delID = AddInfo_Receipt_del();
                v2.SerialCode = "Vg3wZO63761l";
                v2.BenefactorName = "heaYpb7sr79UP";
                v2.DeceasedName_1 = "NzjBQIri";
                v2.DeceasedName_2 = "nCV";
                v2.DeceasedName_3 = "7QX7";
                v2.Sum = 13;
                v2.DSRemark = "aTM3hE3Xh7svVQYiZ";
                v2.Receipt_delID = v1.Receipt_delID; 
                context.Set<Info_Memorial_del>().Add(v1);
                context.Set<Info_Memorial_del>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(Info_Memorial_delBatchVM));

            Info_Memorial_delBatchVM vm = rv.Model as Info_Memorial_delBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<Info_Memorial_del>().Find(v1.ID);
                var data2 = context.Set<Info_Memorial_del>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as Info_Memorial_delListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddInfo_Receipt_del()
        {
            Info_Receipt_del v = new Info_Receipt_del();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ReceiptNumber = "bG";
                v.ReceiptDate = DateTime.Parse("2024-06-14 16:55:47");
                v.DharmaServiceYear = 12;
                v.DharmaServiceName = "lHy6PYf";
                v.ReceiptOwn = "0HXwGDQK";
                v.ContactName = "hBlOMdCp7L7";
                v.ContactPhone = "Nkg";
                v.Sum = 55;
                v.DSRemark = "kppB7yuX6J2h1bMbje";
                context.Set<Info_Receipt_del>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
