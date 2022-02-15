using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PAsia_Dashboard.Areas.FSM.Models.BEL.BEO;
using PAsia_Dashboard.Areas.FSM.Models.DAL;
using PAsia_Dashboard.Areas.FSM.Models.DAL.DAO;
using PAsia_Dashboard.Universal.Gateway;

namespace PAsia_Dashboard.Areas.FSM.Controllers
{
    [LogInChecker]
    public class StructureRefreshController : Controller
    {

        StructureRefreshDAO StructureRefreshDAO = new StructureRefreshDAO();
        // GET: FSM/StructureRefresh
        public ActionResult frmStructureRefresh()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetStructureRefreshValue(string fromDate, string toDate)
        {
            var listData = StructureRefreshDAO.GetStructureRefreshValue(fromDate, toDate);
            var data = Json(listData, JsonRequestBehavior.AllowGet);
            data.MaxJsonLength = int.MaxValue;
            return data;

        }

    }
}