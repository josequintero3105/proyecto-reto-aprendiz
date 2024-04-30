using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using AutoMapper;
using Core.Entities.MongoDB;

namespace Infrastructure.Services.MongoDB.Adapters
{
    public class InvoiceAdapter : IInvoiceRepository
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IContext _context;
        private readonly IMapper _mapper;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public InvoiceAdapter(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Constructor for mongo collection
        /// </summary>
        /// <param name="stringMongoConnection"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="mapper"></param>
        public InvoiceAdapter(string stringMongoConnection, string dataBaseName, IMapper mapper)
        {
            _context = DataBaseContext.GetMongoDatabase(stringMongoConnection, dataBaseName);
            _mapper = mapper;
        }

        /// <summary>
        /// Create a new invoice
        /// </summary>
        /// <param name="InvoiceToCreate"></param>
        /// <returns></returns>
        public async Task<Invoice> CreateInvoiceAsync(Invoice InvoiceToCreate)
        {
            InvoiceCollection invoiceCollectionToCreate = _mapper.Map<InvoiceCollection>(InvoiceToCreate);
            await _context.InvoiceCollection.InsertOneAsync(invoiceCollectionToCreate);
            return _mapper.Map<Invoice>(InvoiceToCreate);
        }
    }
}
