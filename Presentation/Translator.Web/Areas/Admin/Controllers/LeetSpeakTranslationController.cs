using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Translator.Application.Repositories;
using Translator.Application.Repositories.UnitOfWork;

namespace Translator.Web.Areas.Admin.Controllers
{

    //[Authorize(Roles = "Admin")]
    [Authorize()]
    [Area("Admin")]
    public class LeetSpeakTranslationController : Controller
    {
        private readonly IUnitOfWork _uow;

        public LeetSpeakTranslationController(IUnitOfWork uow)
        {
            _uow = uow;
        }


        /// <summary>
        /// Index method fetches previously translated texts from Database
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var translations = _uow.LeetSpeakTranslationRead.GetWhere(a => a.DeletedAt == null).OrderByDescending(a=>a.CreatedAt).ToList();
            return View(translations);
        }
    }

   
}
