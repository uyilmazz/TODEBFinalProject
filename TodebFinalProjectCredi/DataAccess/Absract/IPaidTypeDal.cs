using Core.DataAccess.MongoDb;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Absract
{
    public interface IPaidTypeDal : IDocumentRepository<PaidType>
    {
    }
}
