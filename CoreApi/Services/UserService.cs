using EmaAPI.Helpers;
using EmaAPI.Models;
using EmaAPI.Models.Request.User;
using EmaAPI.Models.Response.User;
using EmaAPI.Models.Token;
using EmaAPI.Repositories;
using EmaAPI.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EmaAPI.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly ITokenService _tokenService;

        public UserService(IRepository<User> userRepository, ITokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public User Register(UserRequestModel request)
        {
            var newUser = new User();
            GenericMappingHelper.Map(request, newUser);
            newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

            _userRepository.Add(newUser);
            return newUser;
        }

        public async Task<UserLoginResponse> LoginUserAsync(UserLoginRequestModel request)
        {
            UserLoginResponse response = new();
            var user = _userRepository.List().FirstOrDefault(x => x.UserName == request.UserName);

            if (user == null)
            {
                throw new Exception("Kullanıcı bulunamadı. Lütfen geçerli bir kullanıcı adı girin.");
            }

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);

            if (!isPasswordValid)
            {
                throw new Exception("Hatalı Şifre");
            }
            else
            {
                var generatedTokenInformation = await _tokenService.GenerateToken(new GenerateTokenRequest { Username = request.UserName, RecordId = user.RecordId });

                response.AuthenticateResult = true;
                response.AuthToken = generatedTokenInformation.Token;
                response.AccessTokenExpireDate = generatedTokenInformation.TokenExpireDate;
            }

            return response;
        }

        public User Update(UserRequestModel request)
        {
            var user = _userRepository.List().FirstOrDefault(x => x.RecordId == request.RecordId);
            if (user != null)
            {
                GenericMappingHelper.Map(request, user);
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                _userRepository.Update(user);
            }

            return user;
        }

        public User Delete(User user)
        {
            if (user != null)
            {
                user.Deleted = true;
                _userRepository.Update(user);
            }

            return user;
        }

        public List<User> SearchUsers(string searchTerm)
        {
            return _userRepository.List()
                .Where(u =>
                    EF.Functions.Like(u.UserName, $"%{searchTerm}%") ||
                    EF.Functions.Like(u.Email, $"%{searchTerm}%") ||
                    EF.Functions.Like(u.Name, $"%{searchTerm}%") ||
                    EF.Functions.Like(u.Surname, $"%{searchTerm}%"))
                .ToList();
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.List().Where(x => x.Deleted == false).ToList();
        }

        public User GetUserById(int recordId)
        {
            return _userRepository.GetById(recordId);
        }
    }
}
