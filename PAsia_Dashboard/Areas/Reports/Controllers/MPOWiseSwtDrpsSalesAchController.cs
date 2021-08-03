using PAsia_Dashboard.Areas.Reports.Models.DAO;
using PAsia_Dashboard.Universal.Gateway;
using System.Web.Mvc;

namespace PAsia_Dashboard.Areas.Reports.Controllers
{
    [LogInChecker]
    public class MPOWiseSwtDrpsSalesAchController : Controller
    {
        MPOWiseSwtDrpsSalesAchDAO mpoWiseSwtDrpsSalesAchDao = new MPOWiseSwtDrpsSalesAchDAO();
        public ActionResult frmMPOWiseSwtDrpsSalesAch()
        {
            return View();
        }
        [HttpPost]
        public ActionResult GetMPOWiseSwtDrpsSalesValueAch(string fromDate, string toDate)
        {
            var data = mpoWiseSwtDrpsSalesAchDao.GetMPOWiseSwtDrpsSalesValueAch(fromDate, toDate);
            if (data.Count > 0)
            {
                return Json(new { Data = data, Status = "Ok" });
            }

            else
            {
                return Json(new { Data = data, Status = "Not Ok" });
            }

        }
    }
}