using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Utilities;

    public  class BusinessSettings
    {
        /// <summary>
        /// Gets or sets the default country.
        /// </summary>
        /// <value>
        /// The default country.
        /// </value>
        public string? DefaultCountry { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the GRPC port.
        /// </summary>
        /// <value>
        /// The GRPC port.
        /// </value>
        public string GRPCPort { get; set; } = string.Empty;
        /// <summary>
        /// Gets or sets the service exceptions.
        /// </summary>
        /// <value>
        /// The service exceptions.
        /// </value>
        public IEnumerable<ServiceException>? ServiceExceptions { get; set; }

        #region Settings Driver    
        /// <summary>
        /// <summary>
        ///     GrpcPort
        /// </summary>
        public int GrpcPort { get; set; } = 50051;

        /// <summary>
        /// HttpPort
        /// </summary>
        public int HttpPort { get; set; } = 80;

        /// <summary>
        /// Domain name
        /// </summary>
        public string DomainName { get; set; }

        /// <summary>
        /// Events Topic Exceptions
        /// </summary>
        public string EventsTopicExceptions { get; set; }

        /// <summary>
        /// Conection to service bus
        /// </summary>
        public string ServiceBusConnectionSecret { get; set; }

        /// <summary>
        /// Queue to create transaction
        /// </summary>

        public string CreateProductQueueName { get; set; }

        /// <summary>
        /// Queue to create transaction
        /// </summary>

        public string GetProductQueueName { get; set; }
        /// <summary>
        /// topic for the response of creating the transaction
        /// </summary>
        public string ResposeTopicProductCreate { get; set; }
        /// <summary>
        /// topic for the response of creating the transaction
        /// </summary>
        public string ResposeTopicTransactionGet { get; set; }

        /// <summary>
        /// Response Transaction Driver template commandName
        /// </summary>
        public string ResponseProductDrivertemplateCommandName { get; set; }

        /// <summary>
        ///     Name of the collection to save transactions
        /// </summary>
        public string ProductsCollectionName { get; set; }
        /// <summary>
        /// ValidationSettings
        /// </summary>
        public string ValidationSettings { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string TemplateEventsTopicExceptions { get; set; }
        #endregion
}