using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.Interfaces.Infrastructure.Mongo;
using AutoMapper;
using Core.Entities.MongoDB;
using MongoDB.Bson;
using MongoDB.Driver;

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

        /// <summary>
        /// Update the invoice
        /// </summary>
        /// <param name="InvoiceToUpdate"></param>
        /// <returns></returns>
        public async Task<bool> UpdateInvoiceAsync(Invoice InvoiceToUpdate)
        {
            InvoiceCollection invoiceCollectionToUpdate = _mapper.Map<InvoiceCollection>(InvoiceToUpdate);
            var IdFinded = Builders<InvoiceCollection>.Filter.Eq("_id", ObjectId.Parse(invoiceCollectionToUpdate._id));
            var result = _context.InvoiceCollection.Find(IdFinded).FirstOrDefault();
            if (result != null)
            {
                var resultUpdate = await _context.InvoiceCollection.ReplaceOneAsync(IdFinded, invoiceCollectionToUpdate);
                return resultUpdate.ModifiedCount == 1;
            }
            else
            {
                return false;
            }
        }
    }
}
