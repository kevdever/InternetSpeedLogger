using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetSpeedLogger
{
    public class Result
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ResultId { get; set; }
        public double Download { get; set; }
        public double Upload { get; set; }
        public double Ping { get; set; }
        public DateTime Timestamp { get; set; }

        [JsonProperty(propertyName:"bytes_sent")]
        public int BytesSent { get; set; }
        [JsonProperty(propertyName: "bytes_received")]
        public int BytesReceived { get; set; }
        public string Share { get; set; }

        public virtual Server Server { get; set; }
        public virtual Client Client { get; set; }
    }

    public class Server
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ServerId { get; set; }
        public string Url { get; set; }
        [JsonProperty(propertyName: "lat")]
        public decimal Latitude { get; set; }
        [JsonProperty(propertyName: "lon")]
        public decimal Longitude { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string CC { get; set; }
        public string Sponsor { get; set; }
        public int Id { get; set; }
        public string Host { get; set; }
        public decimal D { get; set; }
        public decimal Latency { get; set; }
    }

    public class Client
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }
        public string Ip { get; set; }
        [JsonProperty(propertyName: "lat")]
        public decimal Latitude { get; set; }
        [JsonProperty(propertyName: "lon")]
        public decimal Longitude { get; set; }
        public string Isp { get; set; }
        [JsonProperty(propertyName: "isprating")]
        public decimal IspRating { get; set; }
        public decimal Rating { get; set; }
        [JsonProperty(propertyName: "ispdlavg")]
        public decimal IspDlAvg { get; set; }
        [JsonProperty(propertyName: "ispulavg")]
        public decimal IspUlAvg { get; set; }
        [JsonProperty(propertyName: "loggedin")]
        public int LoggedIn { get; set; }
        public string Country { get; set; }
    }
}
