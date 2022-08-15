using Core.Utilities.Results;
using Core.Utilities.Security.JWT;
using Dto.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<PersonDetailDto> Login(PersonForLoginDto personForLoginDto);
        IDataResult<AccessToken> CreateAccessToken(PersonDetailDto personDetailDto);
    }
}
