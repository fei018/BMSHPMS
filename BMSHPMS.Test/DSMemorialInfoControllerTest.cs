using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSManage.Controllers;
using BMSHPMS.DSManage.ViewModels.DSMemorialInfoVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSMemorialInfoControllerTest
    {
        private DSMemorialInfoController _controller;
        private string _seed;

        public DSMemorialInfoControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSMemorialInfoController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSMemorialInfoListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialInfoVM));

            DSMemorialInfoVM vm = rv.Model as DSMemorialInfoVM;
            DSMemorialInfo v = new DSMemorialInfo();
			
            v.SerialCode = "Hz2iGUhzOwntKsYY";
            v.BenefactorName = "9QaiCRGvuZt";
            v.DeceasedName = "40uIKzIldiRw4DE3";
            v.Sum = 35;
            v.DSRemark = "1wKEglFqPBsH";
            v.ReceiptInfoID = AddDSReceiptInfo();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSMemorialInfo>().Find(v.ID);
				
                Assert.AreEqual(data.SerialCode, "Hz2iGUhzOwntKsYY");
                Assert.AreEqual(data.BenefactorName, "9QaiCRGvuZt");
                Assert.AreEqual(data.DeceasedName, "40uIKzIldiRw4DE3");
                Assert.AreEqual(data.Sum, 35);
                Assert.AreEqual(data.DSRemark, "1wKEglFqPBsH");
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSMemorialInfo v = new DSMemorialInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.SerialCode = "Hz2iGUhzOwntKsYY";
                v.BenefactorName = "9QaiCRGvuZt";
                v.DeceasedName = "40uIKzIldiRw4DE3";
                v.Sum = 35;
                v.DSRemark = "1wKEglFqPBsH";
                v.ReceiptInfoID = AddDSReceiptInfo();
                context.Set<DSMemorialInfo>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialInfoVM));

            DSMemorialInfoVM vm = rv.Model as DSMemorialInfoVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSMemorialInfo();
            v.ID = vm.Entity.ID;
       		
            v.SerialCode = "0PtB3fXlo";
            v.BenefactorName = "gknrLPeBTqy";
            v.DeceasedName = "OyAgCuI";
            v.Sum = 36;
            v.DSRemark = "Fq";
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.SerialCode", "");
            vm.FC.Add("Entity.BenefactorName", "");
            vm.FC.Add("Entity.DeceasedName", "");
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.DSRemark", "");
            vm.FC.Add("Entity.ReceiptInfoID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSMemorialInfo>().Find(v.ID);
 				
                Assert.AreEqual(data.SerialCode, "0PtB3fXlo");
                Assert.AreEqual(data.BenefactorName, "gknrLPeBTqy");
                Assert.AreEqual(data.DeceasedName, "OyAgCuI");
                Assert.AreEqual(data.Sum, 36);
                Assert.AreEqual(data.DSRemark, "Fq");
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSMemorialInfo v = new DSMemorialInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.SerialCode = "Hz2iGUhzOwntKsYY";
                v.BenefactorName = "9QaiCRGvuZt";
                v.DeceasedName = "40uIKzIldiRw4DE3";
                v.Sum = 35;
                v.DSRemark = "1wKEglFqPBsH";
                v.ReceiptInfoID = AddDSReceiptInfo();
                context.Set<DSMemorialInfo>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialInfoVM));

            DSMemorialInfoVM vm = rv.Model as DSMemorialInfoVM;
            v = new DSMemorialInfo();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSMemorialInfo>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSMemorialInfo v = new DSMemorialInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.SerialCode = "Hz2iGUhzOwntKsYY";
                v.BenefactorName = "9QaiCRGvuZt";
                v.DeceasedName = "40uIKzIldiRw4DE3";
                v.Sum = 35;
                v.DSRemark = "1wKEglFqPBsH";
                v.ReceiptInfoID = AddDSReceiptInfo();
                context.Set<DSMemorialInfo>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSMemorialInfo v1 = new DSMemorialInfo();
            DSMemorialInfo v2 = new DSMemorialInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.SerialCode = "Hz2iGUhzOwntKsYY";
                v1.BenefactorName = "9QaiCRGvuZt";
                v1.DeceasedName = "40uIKzIldiRw4DE3";
                v1.Sum = 35;
                v1.DSRemark = "1wKEglFqPBsH";
                v1.ReceiptInfoID = AddDSReceiptInfo();
                v2.SerialCode = "0PtB3fXlo";
                v2.BenefactorName = "gknrLPeBTqy";
                v2.DeceasedName = "OyAgCuI";
                v2.Sum = 36;
                v2.DSRemark = "Fq";
                v2.ReceiptInfoID = v1.ReceiptInfoID; 
                context.Set<DSMemorialInfo>().Add(v1);
                context.Set<DSMemorialInfo>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialInfoBatchVM));

            DSMemorialInfoBatchVM vm = rv.Model as DSMemorialInfoBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSMemorialInfo>().Find(v1.ID);
                var data2 = context.Set<DSMemorialInfo>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSMemorialInfo v1 = new DSMemorialInfo();
            DSMemorialInfo v2 = new DSMemorialInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.SerialCode = "Hz2iGUhzOwntKsYY";
                v1.BenefactorName = "9QaiCRGvuZt";
                v1.DeceasedName = "40uIKzIldiRw4DE3";
                v1.Sum = 35;
                v1.DSRemark = "1wKEglFqPBsH";
                v1.ReceiptInfoID = AddDSReceiptInfo();
                v2.SerialCode = "0PtB3fXlo";
                v2.BenefactorName = "gknrLPeBTqy";
                v2.DeceasedName = "OyAgCuI";
                v2.Sum = 36;
                v2.DSRemark = "Fq";
                v2.ReceiptInfoID = v1.ReceiptInfoID; 
                context.Set<DSMemorialInfo>().Add(v1);
                context.Set<DSMemorialInfo>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSMemorialInfoBatchVM));

            DSMemorialInfoBatchVM vm = rv.Model as DSMemorialInfoBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSMemorialInfo>().Find(v1.ID);
                var data2 = context.Set<DSMemorialInfo>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSMemorialInfoListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDSReceiptInfo()
        {
            DSReceiptInfo v = new DSReceiptInfo();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ReceiptNumber = "FJgVXPidpWkIpCWDkA";
                v.ReceiptOwn = "B6nnkW0Rtw1z";
                v.ContactName = "Jmi";
                v.ContactPhone = "zoyuy";
                v.Sum = 80;
                v.DSProjectName = "4GLDEahZ";
                v.DSRemark = "f2KihVjAFS";
                v.ReceiptDate = DateTime.Parse("2023-02-04 14:09:52");
                context.Set<DSReceiptInfo>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
