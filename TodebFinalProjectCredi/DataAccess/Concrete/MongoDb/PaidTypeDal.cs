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
    public class PaidTypeDal : DocumentRepository<PaidType>, IPaidTypeDal
    {
        private const string CollectionName = "PaidTypes";
        public PaidTypeDal(MongoClient client, IConfiguration configuration) : base(client, configuration, CollectionName)
        {
        }
    }
}
