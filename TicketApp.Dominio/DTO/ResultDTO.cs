using Newtonsoft.Json;

namespace TicketApp.Dominio.DTO
{
    public class ResultDTO
    {
        [JsonProperty("isTrue")]
        public bool IsTrue { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("data_Object")]
        public object DataObject { get; set; }
    }
}
