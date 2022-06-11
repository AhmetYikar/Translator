using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Translator.Application.Repositories;
using Translator.Application.Repositories.UnitOfWork;
using Translator.Domain.Entities;
using Translator.Persistence.Context;
using Translator.Persistence.Repositories;
using Translator.Persistence.Repositories.UnitOfWork;
using Translator.Web.Controllers;
using Translator.Web.Models;

namespace Translator.UnitTest
{
    [TestClass]
    public class TranslationTests
    {

        /*There's a lot of different alphabets for leetspeak, and no official one. 
        Even the API we use, "https://funtranslations.com/api", gives us different translations for the same text every time.
        That's why you can't decode and test whether the translation is true or not. All we can do is check that the text we 
        sent is the same as the text that came with the translation.
         */
        [TestMethod]
        public async Task TestLeetSpeakTranslation_Works()
        {
            //Arrange     
            var options = new DbContextOptionsBuilder<TranslationDbContext>()
              .UseInMemoryDatabase(databaseName: "TranslationDataBase").Options;
            using (var context = new TranslationDbContext(options))
            {
                context.LeetSpeakTranslations.Add(new LeetSpeakTranslation
                {
                    Id = 1,
                    Text = "leetspeak",
                    Translated = "l33tsp34k",
                    CreatedAt = DateTime.Now
                });

                context.LeetSpeakTranslations.Add(new LeetSpeakTranslation
                {
                    Id = 2,
                    Text = "test",
                    Translated = "T3sT",
                    CreatedAt = DateTime.Now
                });
                context.SaveChanges();
            }

            using (var context = new TranslationDbContext(options))
            {
                var mockUow = new UnitOfWork(context);

                var mockLogger = new Mock<ILogger<LeetSpeakTranslationController>>();

                var client = new HttpClient();
                client.BaseAddress = new Uri("https://api.funtranslations.com");
                var mockHttpClientFactory = new Mock<IHttpClientFactory>();
                mockHttpClientFactory.Setup(a => a.CreateClient("FunTranslation")).Returns(client);

                var textModel = new TextViewModel
                {
                    Text = "leetspeak"
                };

                var leetSpeakTranslationController = new LeetSpeakTranslationController(mockHttpClientFactory.Object, mockLogger.Object, mockUow);

                //Act
                var result = await leetSpeakTranslationController.LeetSpeakTranslate(textModel);

                //Assert
                if (result.Success == true)
                {
                    Assert.AreEqual(textModel.Text, result.Text);
                }
                else
                {
                    Assert.IsNull(result.Text);
                }
            }              
           
        }

        [TestMethod]
        public async Task Test_TranslateTextPartial_ReturnsAPartialViewResult()
        {
            //Arrange     
            var options = new DbContextOptionsBuilder<TranslationDbContext>()
              .UseInMemoryDatabase(databaseName: "TranslationDataBase").Options;
            using (var context = new TranslationDbContext(options))
            {
              
                context.LeetSpeakTranslations.Add(new LeetSpeakTranslation
                {
                    Id = 3,
                    Text = "leetspeak",
                    Translated = "l33tsp34k",
                    CreatedAt = DateTime.Now
                });

                context.LeetSpeakTranslations.Add(new LeetSpeakTranslation
                {
                    Id = 4,
                    Text = "test",
                    Translated = "T3sT",
                    CreatedAt = DateTime.Now
                });
                context.SaveChanges();
            }

            using (var context = new TranslationDbContext(options))
            {
                var mockUow = new UnitOfWork(context);

                var mockLogger = new Mock<ILogger<LeetSpeakTranslationController>>();
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://api.funtranslations.com");
                var mockHttpClientFactory = new Mock<IHttpClientFactory>();
                mockHttpClientFactory.Setup(a => a.CreateClient("FunTranslation")).Returns(client);


                var textModel = new TextViewModel
                {
                    Text = "leetspeak"
                };

                var leetSpeakTranslationController = new LeetSpeakTranslationController(mockHttpClientFactory.Object, mockLogger.Object, mockUow);

                //Act
                var result = await leetSpeakTranslationController._TranslateTextPartial(textModel);
                 Assert.IsInstanceOfType(result, new PartialViewResult().GetType());
               
            }
        }


        [TestMethod]
        public async Task Test_TranslateTextPartial_ReturnsTranslateStatusFalse_WhenModelStateIsInvalid()
        {
            var options = new DbContextOptionsBuilder<TranslationDbContext>()
             .UseInMemoryDatabase(databaseName: "TranslationDataBase").Options;
            using (var context = new TranslationDbContext(options))
            {

                context.LeetSpeakTranslations.Add(new LeetSpeakTranslation
                {
                    Id = 5,
                    Text = "leetspeak",
                    Translated = "l33tsp34k",
                    CreatedAt = DateTime.Now
                });

                context.LeetSpeakTranslations.Add(new LeetSpeakTranslation
                {
                    Id = 6,
                    Text = "test",
                    Translated = "T3sT",
                    CreatedAt = DateTime.Now
                });
                context.SaveChanges();
            }

            using (var context = new TranslationDbContext(options))
            {
                var mockUow = new UnitOfWork(context);

                var mockLogger = new Mock<ILogger<LeetSpeakTranslationController>>();
                var client = new HttpClient();
                client.BaseAddress = new Uri("https://api.funtranslations.com");
                var mockHttpClientFactory = new Mock<IHttpClientFactory>();
                mockHttpClientFactory.Setup(a => a.CreateClient("FunTranslation")).Returns(client);


                var textModel = new TextViewModel
                {
                    //test model validation 
                    Text = "<script>test</script>"
                };

                var leetSpeakTranslationController = new LeetSpeakTranslationController(mockHttpClientFactory.Object, mockLogger.Object, mockUow);

                //Act
                PartialViewResult result =(PartialViewResult) await leetSpeakTranslationController._TranslateTextPartial(textModel);
                  

                Assert.AreEqual(false, (result.ViewData.Model as TranslateStatusViewModel).Success);

            }
        }
    }
}
