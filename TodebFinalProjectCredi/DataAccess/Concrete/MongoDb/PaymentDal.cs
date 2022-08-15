using Core.DataAccess.MongoDb;
using DataAccess.Absract;
using DTO.Concrete;
using Microsoft.Extensions.Configuration;
using Model;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.MongoDb
{
    public class PaymentDal : DocumentRepository<Payment>, IPaymentDal
    {
        private const string CollectionName = "Payments"; 
        public PaymentDal(MongoClient client, IConfiguration configuration) : base(client, configuration, CollectionName)
        {
        }

        public List<PaymentDetailDto> GetAllPaymentDetail(Expression<Func<Payment, bool>> expression = null)
        {
            throw new NotImplementedException();
        }
    }
}
