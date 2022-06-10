using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Translator.Application.Repositories;

namespace Translator.Web.Areas.Admin.Controllers
{

    //[Authorize()]
    //[Authorize(Roles = "Admin")]
    [Area("Admin")]
    public class LeetSpeakTranslationController : Controller
    {
        private readonly ILeetSpeakTranslationReadRepository _leetSpeakTranslationReadRepository;

        public LeetSpeakTranslationController(ILeetSpeakTranslationReadRepository leetSpeakTranslationReadRepository)
        {
            _leetSpeakTranslationReadRepository = leetSpeakTranslationReadRepository;
        }


        /// <summary>
        /// Index method fetches previously translated texts from Database
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var translations = _leetSpeakTranslationReadRepository.GetWhere(a => a.DeletedAt == null).ToList();
            return View(translations);
        }
    }
}
