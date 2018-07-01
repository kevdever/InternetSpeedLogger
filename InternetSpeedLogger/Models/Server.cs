/*
 * Copyright 2018 KevDever 
 * See LICENSE.md
 */

using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetSpeedLogger.Models
{
    public class Server
    {
        public int Id { get; set; }
        public string Url { get; set; }
        [JsonProperty(propertyName: "lat")]
        public decimal Latitude { get; set; }
        [JsonProperty(propertyName: "lon")]
        public decimal Longitude { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }
        public string CC { get; set; }
        public string Sponsor { get; set; }
        
        public string Host { get; set; }
        public decimal D { get; set; }
        public decimal Latency { get; set; }
    }
}
