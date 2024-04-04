using Microsoft.AspNetCore.Mvc;
using Application.DTOs.Handle;
using Microsoft.Extensions.Options;
using Application.Interfaces.Services;
using Application.Interfaces.Common;
using Application.Common.Utilities;
using Newtonsoft.Json;
using Application.DTOs;

namespace WebApiHttp.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IHandle _handle;
        private readonly IOptionsMonitor<BusinessSettings> _optionsMonitor;
        private readonly ValidationSettings? _validationSettings;

        public ProductController(IProductService productService, IHandle handle, IOptionsMonitor<BusinessSettings> optionsMonitor)
        {
            _productService = productService;
            _handle = handle;
            _optionsMonitor = optionsMonitor;
            _validationSettings = JsonConvert.DeserializeObject<ValidationSettings>(_optionsMonitor.CurrentValue.ValidationSettings);
        }
    }
}
