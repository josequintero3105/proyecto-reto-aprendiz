using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Collections;
using System.Text;
using System.Threading.Tasks;
using Application.DTOs;
using Application.DTOs.Commands;
using Application.DTOs.Entries;
using Application.DTOs.Responses;
using Application.Interfaces.Infrastructure.Mongo;
using Application.Interfaces.Services;
using AutoMapper;
using Core.Entities.MongoDB;
using MongoDB.Driver;
using Newtonsoft.Json;

namespace Infrastructure.Services.MongoDB.Adapters
{
    public class TransactionAdapter : ITransactionRepository
    {
        /// <summary>
        /// Variables
        /// </summary>
        private readonly IContext _context;
        private readonly IMapper _mapper;
        /// <summary>
        /// Constructor defines the DataBase context and the mapper between product and productCollection
        /// </summary>
        /// <param name="context"></param>
        /// <param name="mapper"></param>
        public TransactionAdapter(IContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        /// <summary>
        /// Constructor defines parameters for connection to mongodb database
        /// </summary>
        /// <param name="stringMongoConnection"></param>
        /// <param name="dataBaseName"></param>
        /// <param name="mapper"></param>
        public TransactionAdapter(string stringMongoConnection, string dataBaseName, IMapper mapper)
        {
            _context = DataBaseContext.GetMongoDatabase(stringMongoConnection, dataBaseName);
            _mapper = mapper;
        }

        public Task<TransactionOutput> CreateTransactionAsync(TransactionInput transactionInput)
        {
            throw new NotImplementedException();
        }

        public Task<TransactionOutput> GetTransactionByIdAsync(string _id)
        {
            throw new NotImplementedException();
        }
    }
}
