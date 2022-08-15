using AutoMapper;
using BackgroundJobs.Abstract;
using Business.Abstract;
using Business.Configuration.Validator.FluentValidation;
using Business.Contants.Message;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Dto.Concrete;
using Entity.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class PersonService : IPersonService
    {
        private readonly IPersonDal _personDal;
        private readonly IMapper _mapper;
        private readonly IJobs _jobs;

        public PersonService(IPersonDal personDal, IMapper mapper, IJobs jobs)
        {
            _personDal = personDal;
            _mapper = mapper;
            _jobs = jobs;
        }

        public IDataResult<String> Add(CreatePersonDto createPersonDto)
        {
            var validator = new CreatePersonValidator();
            var valid = validator.Validate(createPersonDto);

            if (!valid.IsValid)
            {
                var messageText = string.Join(',', valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorDataResult<String>(messageText);
            }

            var result = _personDal.Get(p => (p.FirstName == createPersonDto.FirstName && p.LastName == createPersonDto.LastName) || (p.TCNo == createPersonDto.TCNo) || (p.Email == createPersonDto.Email));
            if(result is not null)
            {
                return new ErrorDataResult<String>(ResultMessage.UserAlreadyExits);
            }
     
            var person = _mapper.Map<Person>(createPersonDto);
             byte[] passwordHash, passwordSalt;

            var password = RandomPasswordHelper.GeneratePassword(6,true);

            HashingHelper.CreateHash(password, out passwordSalt, out passwordHash);

            person.Password = new PersonPassword() {
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _personDal.Add(person);

            _jobs.FireAndForget(person.Email,"New Password",ResultMessage.PasswordMailContent + " "+  password);

            return new SuccessDataResult<String>(password,ResultMessage.UserAdded);
        }

        public IResult Delete(int id)
        {
            var person = _personDal.Get(p => p.Id == id);
            if(person is null)
            {
                return new ErrorResult(ResultMessage.UserNotFound);
            }

            _personDal.Delete(person);
            return new SuccessResult(ResultMessage.UserDeleted);
        }

        public IDataResult<Person> Get(int id)
        {
            return new SuccessDataResult<Person>(_personDal.GetPersonWithPassword(p => p.Id == id));
        }

        public IDataResult<List<Person>> GetAll()
        {
            return new SuccessDataResult<List<Person>>();
        }

        public IDataResult<List<PersonDetailDto>> GetAllDetail()
        {
            return new SuccessDataResult<List<PersonDetailDto>>(_personDal.GetAllPersonDetail());
        }

        public IDataResult<Person> GetByEmail(string email)
        {
            var result = _personDal.GetPersonWithPassword(p => p.Email == email);
            if(result is null)
            {
                return new ErrorDataResult<Person>(ResultMessage.UserNotFound);
            }
            return new SuccessDataResult<Person>(result);
        }

        public IDataResult<PersonDetailDto> GetDetail(int id)
        {
            var result = _personDal.GetPersonDetail(id);
            if(result is null)
            {
                return new ErrorDataResult<PersonDetailDto>(ResultMessage.UserNotFound);
            }
            return new SuccessDataResult<PersonDetailDto>(result);
        }

        public IResult Update(UpdatePersonDto updatePersonDto)
        {
            var validator = new UpdatePersonValidator();
            var valid = validator.Validate(updatePersonDto);
            if (!valid.IsValid)
            {
                var messageText = string.Join(',', valid.Errors.Select(x => x.ErrorMessage));
                return new ErrorResult(messageText);
            }

            var result = _personDal.Get(p => p.Id == updatePersonDto.Id);
            if(result is null)
            {
                return new ErrorResult(ResultMessage.UserNotFound);
            }

            var person = _mapper.Map<Person>(updatePersonDto);
            _personDal.Update(person);
            return new SuccessResult();
        }
    }
}
