using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Application.DTOs.Common
{
    public  class GenericOutput<T, Y>
    {
        /// <summary>
        /// Get or sets the data
        /// </summary>
        [JsonProperty("data")]
        public T? data {  get; set; }

        /// <summary>
        /// Get or sets the error
        /// </summary>
        [JsonProperty("error")]
        public Y? error { get; set; }
    }
}
