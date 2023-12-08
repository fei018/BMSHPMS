//using BMSHPMS.Areas.Reception.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using WalkingTec.Mvvm.Core;
//using WalkingTec.Mvvm.Mvc;

//namespace BMSHPMS.Areas.Reception.Controllers
//{
//    [NoLog]
//    [Area("Reception")]
//    [ActionDescription("法會功德登記")]
//    public class DharmaServiceController : BaseController
//    {
//        [ActionDescription("登記")]
//        public IActionResult Index()
//        {
//            return PartialView();
//        }

//        [HttpPost]
//        public IActionResult Register(DSRegisterVM registerVM)
//        {
//            try
//            {
//                return PartialView(registerVM);
//            }
//            catch (System.Exception ex)
//            {
//                return View("Exception", ex);
//            }
//        }
//    }
//}
