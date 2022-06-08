using System.Text.Json.Serialization;

namespace Translator.Web.Models
{
    public class FunTranslationResponseErrorModel
    {
        [JsonPropertyName("error")]
        public Error Error { get; set; }
    }

    public class Error
    {
        [JsonPropertyName("code")]
        public int Code { get; set; }

        [JsonPropertyName("message")]
        public string Message { get; set; }
    }
}
