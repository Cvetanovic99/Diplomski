using AutoMapper;
using Diplomski.Application.Dtos;
using Diplomski.Application.Exceptions;
using Diplomski.Application.Interfaces;
using Diplomski.Application.Interfaces.ThirdPartyContracts;
using Diplomski.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _extendedUserRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._extendedUserRepository = userRepository;
        }

        public async Task CreateUserAsync(CreateUserDto createUserDto)
        {
            //var user = await _useRepository.GetSingleBySpecAsync(createUserDto.IdentityId);

            //if (user is not null)
            //{
            //    throw new ApiException("UserAlreadyExist", 400);
            //}

            var user = await _extendedUserRepository.GetUserByIdentityId(createUserDto.IdentityId);

            if (user is not null)
            {
                throw new ApiException("UserAlreadyExist", 400);
            }

            var userEntity = _mapper.Map<User>(createUserDto);
            await _extendedUserRepository.AddAsync(userEntity);
        }
    }
}
