using Application.DTOs;
using Application.DTOs.Entries;
using Application.Interfaces.Common;
using Application.Interfaces.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[Controller]/[action]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IInvoiceService _invoiceService;
        private readonly IHandle _handle;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="invoiceService"></param>
        /// <param name="handle"></param>
        public InvoiceController(IInvoiceService invoiceService, IHandle handle)
        {
            _invoiceService = invoiceService;
            _handle = handle;
        }

        /// <summary>
        /// Method Post Generate the invoice
        /// </summary>
        /// <param name="body"></param>
        /// <returns></returns>
        [HttpPost()]
        [ProducesResponseType(200)]
        public async Task<IActionResult> Generate([FromBody] InvoiceInput body)
        {
            Invoice invoice = await _invoiceService.GenerateInvoice(body);
            return Ok(invoice);
        }
    }
}
