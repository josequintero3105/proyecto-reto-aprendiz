using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.ApiEntities.Input
{
    public class Client
    {
        /// <summary>
        ///     DocumentType
        /// </summary>
        public string? DocType { get; set; }
        /// <summary>
        ///     Document
        /// </summary>
        public string? Document { get; set; }
        /// <summary>
        ///     Name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        ///     LastName
        /// </summary>
        public string? LastName { get; set; }
        /// <summary>
        ///     Email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        ///     IndCountry
        /// </summary>
        public string? IndCountry { get; set; }
        /// <summary>
        ///     Phone
        /// </summary>
        public string? Phone { get; set; }
        /// <summary>
        ///     Country
        /// </summary>
        public string? Country { get; set; }
        /// <summary>
        ///     City
        /// </summary>
        public string? City { get; set; }
        /// <summary>
        ///     Address
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        ///     IpAdress
        /// </summary>
        public string? IpAddress { get; set; }
    }
}
