using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Translator.Application.Repositories;
using Translator.Domain.Entities;
using Translator.Web.Models;

namespace Translator.Web.Controllers
{
    [Authorize()]
    public class LeetSpeakTranslationController : Controller
    {
        private readonly ILeetSpeakTranslationWriteRepository _leetSpeakTranslationWriteRepository;
        private readonly ILeetSpeakTranslationReadRepository _leetSpeakTranslationReadRepository;

        private readonly IHttpClientFactory _httpClientFactory;
        public LeetSpeakTranslationController(ILeetSpeakTranslationWriteRepository leetSpeakTranslationWriteRepository, IHttpClientFactory httpClientFactory, ILeetSpeakTranslationReadRepository leetSpeakTranslationReadRepository)
        {
            _leetSpeakTranslationWriteRepository = leetSpeakTranslationWriteRepository;
            _httpClientFactory = httpClientFactory;
            _leetSpeakTranslationReadRepository = leetSpeakTranslationReadRepository;
        }



        // This method return the main view for translation 
        public IActionResult TranslateText()
        {
            return View();
        }


        // This method gets called by ajax. It translates text entered by user and return the result as a partial view.
        [HttpPost()]
        public async Task<IActionResult> _TranslateTextPartial(string text)
        {
            var model = new TranslateStatusViewModel();

            if (string.IsNullOrEmpty(text))
            {
                model.Success = false;
                model.Message = "Please enter a text!";
                return PartialView(model);
            }

            //If the text has been saved in our database before, retrieve it from our database without call the API
            var translateFromOurDataBase =await _leetSpeakTranslationReadRepository.GetSingleAsync(a=>a.Text.Trim()==text.Trim());
            if (translateFromOurDataBase!=null)
            {
                //logging
                model.Success = true;
                model.Translated = translateFromOurDataBase.Translated;
                return PartialView(model);
            }

            //
            var client = _httpClientFactory.CreateClient("FunTranslation");

            var response = await client.GetAsync($"/translate/leetspeak.json?text={text}");
            var requestUriToBeLogged = response.RequestMessage.RequestUri.ToString();

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var result = await response.Content.ReadAsStringAsync();
                    //logg result 
                    ///

                    var translationResponseModel = JsonConvert.DeserializeObject<FunTranslationResponseModel>(result);
                   
                    model.Success = true;
                    model.Translated = translationResponseModel.Contents == null ? "" : translationResponseModel.Contents.Translated;

                    //insert into database
                    var leetSpeakTranslation = new LeetSpeakTranslation
                    {
                        Text = text,
                        Translated = model.Translated
                    };
                    await _leetSpeakTranslationWriteRepository.AddAsync(leetSpeakTranslation);
                    await _leetSpeakTranslationWriteRepository.SaveChangesAsync();
                  
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
                    //logg error result

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
