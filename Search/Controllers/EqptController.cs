using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessLayer;
using Microsoft.AspNetCore.Mvc;

namespace Search.Controllers
{
    public class EqptController : Controller
    {
        EqptBusiness businessLayer = new EqptBusiness();
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult LoadData()
        {
            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = HttpContext.Request.Form["start"].FirstOrDefault();
            var length = HttpContext.Request.Form["length"].FirstOrDefault();
            string search = HttpContext.Request.Form["search[value]"].FirstOrDefault();
            
            var sortColumn = HttpContext.Request.Form["columns[" + HttpContext.Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = HttpContext.Request.Form["order[0][dir]"].FirstOrDefault();

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;
            var eqptDetails = businessLayer.Eqpt_details(search, sortColumn, sortColumnDir);
            recordsTotal = eqptDetails.Count();
            var data = eqptDetails.ToList();
            if (pageSize != -1)
            {
                data = eqptDetails.Skip(skip).Take(pageSize).ToList();
            }
            var json = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
            return Ok(json);

        }

    }
}