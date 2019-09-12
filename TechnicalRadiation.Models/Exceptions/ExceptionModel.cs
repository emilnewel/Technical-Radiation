using Newtonsoft.Json;

namespace TechnicalRadiation.Models.Exceptions
{
    public class ExceptionModel
    {
        public int statusCode { get; set; }
        public string Message { get; set; }

        public override string ToString() => JsonConvert.SerializeObject(this);
    }
}