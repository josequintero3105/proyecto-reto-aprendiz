using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.Commands
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CommandResponse<T>
    {
        /// <summary>
        /// Gets or sets the item.
        /// </summary>
        /// <value>
        /// The item.
        /// </value>
        public T? Item { get; set; }

        /// <summary>
        /// Gets or sets the items.
        /// </summary>
        /// <value>
        /// The items.
        /// </value>
        public List<T>? Items { get; set; }

        /// <summary>
        /// Gets or sets the item in output.
        /// </summary>
        /// <value>
        /// The item in output.
        /// </value>
        public Hashtable? ItemInOutput { get; set; }
    }
}
