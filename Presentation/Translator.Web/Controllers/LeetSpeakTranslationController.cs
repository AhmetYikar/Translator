using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Translator.Application.Repositories;
using Translator.Web.Models;

namespace Translator.Web.Controllers
{
    public class LeetSpeakTranslationController : Controller
    {
        private readonly ILeetSpeakTranslationReadRepository _leetSpeakTranslationReadRepository;
        private readonly ILeetSpeakTranslationWriteRepository _leetSpeakTranslationWriteRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        public LeetSpeakTranslationController(ILeetSpeakTranslationReadRepository leetSpeakTranslationReadRepository, ILeetSpeakTranslationWriteRepository leetSpeakTranslationWriteRepository, IHttpClientFactory httpClientFactory)
        {
            _leetSpeakTranslationReadRepository = leetSpeakTranslationReadRepository;
            _leetSpeakTranslationWriteRepository = leetSpeakTranslationWriteRepository;
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Index method fetches previously translated texts from Database
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var translations = _leetSpeakTranslationReadRepository.GetWhere(a=>a.DeletedAt==null);
            return View();
        }

        // This method return the main view for translation 
        public IActionResult  TranslateText()
        {
        
            return View();
        }


        // This method gets called by ajax. It translates text entered by user and return the result as a partial view.
        [HttpPost()]
        public async Task<IActionResult>  _TranslateTextPartial(string text)
        {
            var model = new TranslateStatusViewModel();
           
            var client = _httpClientFactory.CreateClient("FunTranslation");

            var response = await client.GetAsync($"/translate/leetspeak.json?text={text}");
            var requestUriToBeLogged = response.RequestMessage.RequestUri.ToString();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var result = await response.Content.ReadAsStringAsync();
                    //logg result here
                   
                    ///
                    var translationResponseModel = JsonConvert.DeserializeObject<FunTranslationResponseModel>(result);
                    model.Success = true;
                    model.Translated = translationResponseModel.Contents == null ? "" : translationResponseModel.Contents.Translated;
                }
                catch (Exception ex)
                {
                    //logging
                    //
                    model.Success = false;
                    model.Message = "Something went wrong!";
                }

            }
            else
            {
                try
                {
                    var errorResult = await response.Content.ReadAsStringAsync();
                    //logg error result here

                    ///
                    var errorResponseModel = JsonConvert.DeserializeObject<FunTranslationResponseErrorModel>(errorResult);
                    model.Success = false;
                    model.Message = errorResponseModel.Error == null ? "" : errorResponseModel.Error.Message;
                }
                catch (Exception ex)
                {
                    //logging

                    model.Success = false;
                    model.Message = "Something went wrong!";
                }

            }


            return PartialView(model);
        }
    }
}
