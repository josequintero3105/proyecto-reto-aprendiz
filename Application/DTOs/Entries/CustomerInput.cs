using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Entries
{
    public class CustomerInput
    {
        /// <summary>
        ///     DocumentType
        /// </summary>
        public string? DocumentType { get; set; }
        /// <summary>
        ///     Document
        /// </summary>
        public string? Document { get; set; }
        /// <summary>
        ///     Name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        ///     Email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        ///     Phone
        /// </summary>
        public string? Phone { get; set; }
    }
}
