using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace SplitAdminEcomerce.Uniones
{
    [JsonObject]
    public class ResponseWS
    {
        [JsonProperty("CreatePedidoB2CResult")]
        public LogResult LogResult { get; set; }
    }
    [JsonObject]
    public class LogResult
    {
        [JsonProperty("ErrorCode")]
        public int ErrorCode { get; set; }

        [JsonProperty("ErrorDescription")]
        public string ErrorDescription { get; set; }

        [JsonProperty("ErrorType")]
        public string ErrorType { get; set; }

        [JsonProperty("Diccionary")]
        public Diccionary[] Diccionary { set; get; }
    }
    [JsonObject]
    public class Diccionary
    {
        [JsonProperty("Key")]
        public string Key { get; set; }
        [JsonProperty("Value")]
        public string Value { get; set; }
    }
}
