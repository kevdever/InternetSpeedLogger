/*
 * Copyright 2018 KevDever 
 * See LICENSE.md
 */

using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetSpeedLogger.Models
{

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
