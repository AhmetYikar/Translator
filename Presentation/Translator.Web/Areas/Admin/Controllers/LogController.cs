using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Translator.Application.Repositories.UnitOfWork;

namespace Translator.Web.Areas.Admin.Controllers
{
    [Authorize()]
    [Area("Admin")]
    public class LogController : Controller
    {
        private readonly IUnitOfWork _uow;

        public LogController(IUnitOfWork uow)
        {
            _uow = uow;
        }

        public IActionResult Index()
        {
            var allLogs = _uow.LogRead.GetAll().OrderByDescending(a => a.Logged).ToList();
            return View(allLogs);
        }
    }
}
