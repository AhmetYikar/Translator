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
using Microsoft.Extensions.Logging;
using NLog.Web;
using Translator.Application.Repositories.UnitOfWork;

namespace Translator.Web.Controllers
{
 
    public class LeetSpeakTranslationController : Controller
    {
        private readonly IUnitOfWork _uow;

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<LeetSpeakTranslationController> _logger;
        public LeetSpeakTranslationController(IHttpClientFactory httpClientFactory, ILogger<LeetSpeakTranslationController> logger, IUnitOfWork uow)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
            _uow = uow;
        }



        // This method return the main view for translation 
        public IActionResult TranslateText()
        {
            return View();
        }


        // This method gets called by ajax. It translates text entered by user and return the result as a partial view.
        [HttpPost()]        
        public async Task<IActionResult> _TranslateTextPartial(TextViewModel textModel)
        {
            var statusModel = new TranslateStatusViewModel();

            if (!ModelState.IsValid)
            {
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        statusModel.Messages.Add(error.ErrorMessage);
                    }
                }

                statusModel.Success = false;
                return PartialView(statusModel);
            }
           
            statusModel =await LeetSpeakTranslate(textModel);

            return PartialView(statusModel);
        }


        //Translate method
        [NonAction()]
        public async Task<TranslateStatusViewModel>  LeetSpeakTranslate(TextViewModel textModel)
        {
            var statusModel = new TranslateStatusViewModel();

            //If the text has been saved in our database before, retrieve it from our database without calling the API            
            var translateFromOurDataBase = await _uow.LeetSpeakTranslationRead.GetSingleAsync(a => a.Text == textModel.Text);
            if (translateFromOurDataBase != null)
            {
                //logging
                _logger.LogInformation($"The text was translated from our database without calling the API; text: {textModel.Text}");

                statusModel.Success = true;
                statusModel.Translated = translateFromOurDataBase.Translated;
                statusModel.Text = translateFromOurDataBase.Text;
                return statusModel;
            }

            //
            var client = _httpClientFactory.CreateClient("FunTranslation");


            var response = await client.GetAsync($"/translate/leetspeak.json?text={textModel.Text}");
            var requestUriToBeLogged = response.RequestMessage.RequestUri.ToString();
            _logger.LogInformation($"Request uri: {requestUriToBeLogged}");

            if (response.IsSuccessStatusCode)
            {
                try
                {
                    var result = await response.Content.ReadAsStringAsync();
                    //logg result 
                    _logger.LogInformation($"FunTranslation API response: {result}");


                    var translationResponseModel = JsonConvert.DeserializeObject<FunTranslationResponseModel>(result);

                    statusModel.Success = true;
                    statusModel.Text= translationResponseModel.Contents == null ? "" : translationResponseModel.Contents.Text;
                    statusModel.Translated = translationResponseModel.Contents == null ? "" : translationResponseModel.Contents.Translated;

                    //insert into database
                    var leetSpeakTranslation = new LeetSpeakTranslation
                    {
                        Text = textModel.Text,
                        Translated = statusModel.Translated
                    };
                    await _uow.LeetSpeakTranslationWrite.AddAsync(leetSpeakTranslation);
                    await _uow.LeetSpeakTranslationWrite.SaveChangesAsync();

                }
                catch (Exception ex)
                {
                    //logging
                    _logger.LogError(ex, "Translation process faced an error:");

                    statusModel.Success = false;
                    statusModel.Messages.Add("Something went wrong!");
                }

            }
            else
            {
                try
                {
                    var errorResult = await response.Content.ReadAsStringAsync();
                    //logg error result
                    _logger.LogError($"FunTranslation API error response: {errorResult}");

                    var errorResponseModel = JsonConvert.DeserializeObject<FunTranslationResponseErrorModel>(errorResult);
                    statusModel.Success = false;
                    statusModel.Messages.Add(errorResponseModel.Error == null ? "" : errorResponseModel.Error.Message);
                }
                catch (JsonSerializationException ex)
                {
                    //logging
                    _logger.LogError(ex, "DeSerialize errorResult exception:");

                    statusModel.Success = false;
                    statusModel.Messages.Add("Something went wrong!");
                }

            }

            return statusModel;
        }
    }
}
