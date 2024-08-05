using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ApiEntities.Response
{
    public class ResultResponse
    {
        public string? function { get; set; }
        public int errorCode { get; set; }
        public string? message { get; set; }
        public string? country { get; set; }
        public Data? data { get; set; }
    }
}
