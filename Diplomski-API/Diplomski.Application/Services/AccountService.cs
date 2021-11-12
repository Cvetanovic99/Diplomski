using AutoMapper;
using Diplomski.Application.Dtos;
using Diplomski.Application.Interfaces;
using Diplomski.Application.Interfaces.ThirdPartyContracts;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Diplomski.Application.Services
{
    class AccountService : IAccountService
    {
        private readonly IAuthService _authService;
        private readonly ITokenService _tokenService;
        private readonly IUserService _userService;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public AccountService(IAuthService authService, ITokenService tokenService, IUserService userService, IUserRepository userRepository, IMapper mapper)
        {
            this._authService = authService;
            this._tokenService = tokenService;
            this._userService = userService;
            this._userRepository = userRepository;
            this._mapper = mapper;
        }
        public async Task RegisterAsync(RegisterDto registerDto)
        {
           var identityId = await this._authService.CreateIdentityAsync(registerDto);
           //TODO: Da se doda da upise i u bazu sa domenskim entitetima, prosledi se i identityId da bi entitet iz baze znao koji su njegovi podaci u za autentifikaciju koji se nalaze u bazi sa podacima za autentifikaciju (za Identity).
           await _userService.CreateUserAsync(new CreateUserDto { FullName = registerDto.Email, IdentityId = identityId });
           Console.WriteLine(identityId);
        }

        public async Task<TokenDto> LoginAsync(LoginDto loginDto)
        {
            return await _authService.LoginAsync(loginDto);
        }

        public async Task<TokenDto> RevokeTokenAsync(RevokeTokenDto revokeTokenDto)
        {
            return await _authService.RevokeAsync(revokeTokenDto);
        }

        public async Task<UserDto> GetAuthenticatedUserAsync(string token)
        {

            var (_, identityId) = _tokenService.GetUserClaimsFromToken(token);

            //TODO: Mora da se pozove i iz baze koja sadrzi entitet i da se taj user mapira na UserWithEmail, zove se samo pomocu identityId-a jer entitet u bazi ima identityId koji mu pokazuje koji su njegovi podaci iz baze za autentifikaciju(Za Identity).
            var user = await _userRepository.GetUserByIdentityId(identityId);
            var userDto = _mapper.Map<UserDto>(user);
            return userDto;
        }
    }
}
