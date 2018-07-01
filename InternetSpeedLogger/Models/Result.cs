/*
 * Copyright 2018 KevDever 
 * See LICENSE.md
 */

using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace InternetSpeedLogger.Models
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

        [ForeignKey("Server")]
        public int ServerId { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }
    }
}
