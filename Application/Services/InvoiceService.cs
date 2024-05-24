using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Common.FluentValidations.Extentions;
using Application.Common.FluentValidations.Validators;
using Application.Common.Helpers.Exceptions;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using Common.Helpers.Exceptions;
using Microsoft.Extensions.Logging;

namespace Application.Services
{
    public class InvoiceService : IInvoiceService
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly ILogger<InvoiceService> _logger;
        private readonly IShoppingCartRepository _shoppingCartRepository;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="shoppingCartRepository"></param>
        /// <param name="logger"></param>
        public InvoiceService(IInvoiceRepository invoiceRepository, ILogger<InvoiceService> logger, IShoppingCartRepository shoppingCartRepository)
        {
            _invoiceRepository = invoiceRepository;
            _logger = logger;
            _shoppingCartRepository = shoppingCartRepository;
        }

        /// <summary>
        /// Private method controls the process of create a shopping cart
        /// </summary>
        /// <param name="shoppingCart"></param>
        /// <returns></returns>
        /// <exception cref="BusinessException"></exception>
        public async Task GenerateInvoice(Invoice invoice)
        {
            try
            {
                await _invoiceRepository.GenerateInvoiceAsync(invoice);
            }
            catch (BusinessException bex)
            {
                _logger.LogError(bex, "Error: {message} Error Code: {code-message} creating invoice: {invoice}"
                    , bex.Code, bex.Message, invoice);
                throw new BusinessException(bex.Message, bex.Code);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error: {message} creating invoice: {invoice} ", ex.Message, invoice);
                throw new BusinessException(nameof(GateWayBusinessException.NotControlerException),
                    nameof(GateWayBusinessException.NotControlerException));
            }
        }
    }
}
