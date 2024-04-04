using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs.PlantillaEntitys
{
    public  class Entity
    {
        /// <summary>
        /// Id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// descrip
        /// </summary>
        public string descrip { get; set; }

        /// <summary>
        /// Mock bool sucess of request
        /// </summary>
        public bool IsSucess { get; set; }


        public int SimulateResult { get; set; }
    }
}
