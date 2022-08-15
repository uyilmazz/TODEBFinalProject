using Business.Abstract;
using Business.Contants.Message;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Dto.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly IPersonService _personService;
        private readonly ITokenHelper _tokenHelper;
        public AuthService(IPersonService personService, ITokenHelper tokenHelper)
        {
            _personService = personService;
            _tokenHelper = tokenHelper;
        }

        public IDataResult<AccessToken> CreateAccessToken(PersonDetailDto personDetailDto)
        {
            var token = _tokenHelper.CreateToken(personDetailDto);
            return new SuccessDataResult<AccessToken>(token, ResultMessage.AccessTokenCreated);
        }

        public IDataResult<PersonDetailDto> Login(PersonForLoginDto personForLoginDto)
        {
            var checkEmail = _personService.GetByEmail(personForLoginDto.Email);
            if (!checkEmail.Success)
            {
                return new ErrorDataResult<PersonDetailDto>(ResultMessage.UserNotFound);
            }

            var passwordVerify = HashingHelper.VerifyPasswordHash(personForLoginDto.Password, checkEmail.Data.Password.PasswordSalt, checkEmail.Data.Password.PasswordHash);
            if (!passwordVerify)
            {
                return new ErrorDataResult<PersonDetailDto>(ResultMessage.LoginError);
            }

            var personDetail = _personService.GetDetail(checkEmail.Data.Id);
            return new SuccessDataResult<PersonDetailDto>(personDetail.Data, ResultMessage.SuccessLogin);
        }
    }
}
