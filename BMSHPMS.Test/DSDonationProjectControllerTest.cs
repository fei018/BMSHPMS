using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WalkingTec.Mvvm.Core;
using BMSHPMS.DSManage.Controllers;
using BMSHPMS.DSManage.ViewModels.DSDonationProjectVMs;
using BMSHPMS.Models.DharmaService;
using BMSHPMS;


namespace BMSHPMS.Test
{
    [TestClass]
    public class DSDonationProjectControllerTest
    {
        private DSDonationProjectController _controller;
        private string _seed;

        public DSDonationProjectControllerTest()
        {
            _seed = Guid.NewGuid().ToString();
            _controller = MockController.CreateController<DSDonationProjectController>(new DataContext(_seed, DBTypeEnum.Memory), "user");
        }

        [TestMethod]
        public void SearchTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            string rv2 = _controller.Search((rv.Model as DSDonationProjectListVM).Searcher);
            Assert.IsTrue(rv2.Contains("\"Code\":200"));
        }

        [TestMethod]
        public void CreateTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Create();
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonationProjectVM));

            DSDonationProjectVM vm = rv.Model as DSDonationProjectVM;
            DSDonationProject v = new DSDonationProject();
			
            v.Sum = 86;
            v.SumCode = "qnRuSzvyS";
            v.DonationCategory = "swsQK6StlpKMDJMzf";
            v.UsedNumber = 26;
            v.DServiceProjID = AddDServiceProject();
            vm.Entity = v;
            _controller.Create(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonationProject>().Find(v.ID);
				
                Assert.AreEqual(data.Sum, 86);
                Assert.AreEqual(data.SumCode, "qnRuSzvyS");
                Assert.AreEqual(data.DonationCategory, "swsQK6StlpKMDJMzf");
                Assert.AreEqual(data.UsedNumber, 26);
                Assert.AreEqual(data.CreateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.CreateTime.Value).Seconds < 10);
            }

        }

        [TestMethod]
        public void EditTest()
        {
            DSDonationProject v = new DSDonationProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
       			
                v.Sum = 86;
                v.SumCode = "qnRuSzvyS";
                v.DonationCategory = "swsQK6StlpKMDJMzf";
                v.UsedNumber = 26;
                v.DServiceProjID = AddDServiceProject();
                context.Set<DSDonationProject>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Edit(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonationProjectVM));

            DSDonationProjectVM vm = rv.Model as DSDonationProjectVM;
            vm.Wtm.DC = new DataContext(_seed, DBTypeEnum.Memory);
            v = new DSDonationProject();
            v.ID = vm.Entity.ID;
       		
            v.Sum = 69;
            v.SumCode = "77EZSM6Az";
            v.DonationCategory = "I3BOekZ";
            v.UsedNumber = 80;
            vm.Entity = v;
            vm.FC = new Dictionary<string, object>();
			
            vm.FC.Add("Entity.Sum", "");
            vm.FC.Add("Entity.SumCode", "");
            vm.FC.Add("Entity.DonationCategory", "");
            vm.FC.Add("Entity.UsedNumber", "");
            vm.FC.Add("Entity.DServiceProjID", "");
            _controller.Edit(vm);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonationProject>().Find(v.ID);
 				
                Assert.AreEqual(data.Sum, 69);
                Assert.AreEqual(data.SumCode, "77EZSM6Az");
                Assert.AreEqual(data.DonationCategory, "I3BOekZ");
                Assert.AreEqual(data.UsedNumber, 80);
                Assert.AreEqual(data.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data.UpdateTime.Value).Seconds < 10);
            }

        }


        [TestMethod]
        public void DeleteTest()
        {
            DSDonationProject v = new DSDonationProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
        		
                v.Sum = 86;
                v.SumCode = "qnRuSzvyS";
                v.DonationCategory = "swsQK6StlpKMDJMzf";
                v.UsedNumber = 26;
                v.DServiceProjID = AddDServiceProject();
                context.Set<DSDonationProject>().Add(v);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.Delete(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonationProjectVM));

            DSDonationProjectVM vm = rv.Model as DSDonationProjectVM;
            v = new DSDonationProject();
            v.ID = vm.Entity.ID;
            vm.Entity = v;
            _controller.Delete(v.ID.ToString(),null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data = context.Set<DSDonationProject>().Find(v.ID);
                Assert.AreEqual(data, null);
          }

        }


        [TestMethod]
        public void DetailsTest()
        {
            DSDonationProject v = new DSDonationProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v.Sum = 86;
                v.SumCode = "qnRuSzvyS";
                v.DonationCategory = "swsQK6StlpKMDJMzf";
                v.UsedNumber = 26;
                v.DServiceProjID = AddDServiceProject();
                context.Set<DSDonationProject>().Add(v);
                context.SaveChanges();
            }
            PartialViewResult rv = (PartialViewResult)_controller.Details(v.ID.ToString());
            Assert.IsInstanceOfType(rv.Model, typeof(IBaseCRUDVM<TopBasePoco>));
            Assert.AreEqual(v.ID, (rv.Model as IBaseCRUDVM<TopBasePoco>).Entity.GetID());
        }

        [TestMethod]
        public void BatchEditTest()
        {
            DSDonationProject v1 = new DSDonationProject();
            DSDonationProject v2 = new DSDonationProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 86;
                v1.SumCode = "qnRuSzvyS";
                v1.DonationCategory = "swsQK6StlpKMDJMzf";
                v1.UsedNumber = 26;
                v1.DServiceProjID = AddDServiceProject();
                v2.Sum = 69;
                v2.SumCode = "77EZSM6Az";
                v2.DonationCategory = "I3BOekZ";
                v2.UsedNumber = 80;
                v2.DServiceProjID = v1.DServiceProjID; 
                context.Set<DSDonationProject>().Add(v1);
                context.Set<DSDonationProject>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonationProjectBatchVM));

            DSDonationProjectBatchVM vm = rv.Model as DSDonationProjectBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            
            vm.FC = new Dictionary<string, object>();
			
            _controller.DoBatchEdit(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSDonationProject>().Find(v1.ID);
                var data2 = context.Set<DSDonationProject>().Find(v2.ID);
 				
                Assert.AreEqual(data1.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data1.UpdateTime.Value).Seconds < 10);
                Assert.AreEqual(data2.UpdateBy, "user");
                Assert.IsTrue(DateTime.Now.Subtract(data2.UpdateTime.Value).Seconds < 10);
            }
        }


        [TestMethod]
        public void BatchDeleteTest()
        {
            DSDonationProject v1 = new DSDonationProject();
            DSDonationProject v2 = new DSDonationProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
				
                v1.Sum = 86;
                v1.SumCode = "qnRuSzvyS";
                v1.DonationCategory = "swsQK6StlpKMDJMzf";
                v1.UsedNumber = 26;
                v1.DServiceProjID = AddDServiceProject();
                v2.Sum = 69;
                v2.SumCode = "77EZSM6Az";
                v2.DonationCategory = "I3BOekZ";
                v2.UsedNumber = 80;
                v2.DServiceProjID = v1.DServiceProjID; 
                context.Set<DSDonationProject>().Add(v1);
                context.Set<DSDonationProject>().Add(v2);
                context.SaveChanges();
            }

            PartialViewResult rv = (PartialViewResult)_controller.BatchDelete(new string[] { v1.ID.ToString(), v2.ID.ToString() });
            Assert.IsInstanceOfType(rv.Model, typeof(DSDonationProjectBatchVM));

            DSDonationProjectBatchVM vm = rv.Model as DSDonationProjectBatchVM;
            vm.Ids = new string[] { v1.ID.ToString(), v2.ID.ToString() };
            _controller.DoBatchDelete(vm, null);

            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                var data1 = context.Set<DSDonationProject>().Find(v1.ID);
                var data2 = context.Set<DSDonationProject>().Find(v2.ID);
                Assert.AreEqual(data1, null);
            Assert.AreEqual(data2, null);
            }
        }

        [TestMethod]
        public void ExportTest()
        {
            PartialViewResult rv = (PartialViewResult)_controller.Index();
            Assert.IsInstanceOfType(rv.Model, typeof(IBasePagedListVM<TopBasePoco, BaseSearcher>));
            IActionResult rv2 = _controller.ExportExcel(rv.Model as DSDonationProjectListVM);
            Assert.IsTrue((rv2 as FileContentResult).FileContents.Length > 0);
        }

        private Guid AddDServiceProject()
        {
            DServiceProject v = new DServiceProject();
            using (var context = new DataContext(_seed, DBTypeEnum.Memory))
            {
                try{

                v.ProjectName = "SQrOp";
                v.ProjectCode = "pufZ5Quy19y";
                context.Set<DServiceProject>().Add(v);
                context.SaveChanges();
                }
                catch{}
            }
            return v.ID;
        }


    }
}
