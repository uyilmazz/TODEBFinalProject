using Core.DataAccess.MongoDb;
using DataAccess.Absract;
using Microsoft.Extensions.Configuration;
using Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.MongoDb
{
    public class CreditCardDal : DocumentRepository<CreditCard>, ICreditCardDal
    {
        private const string CollectionName = "CreditCards";
        public CreditCardDal(MongoClient client, IConfiguration configuration) : base(client, configuration, CollectionName)
        {
        }
    }
}
