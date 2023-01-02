using Secrets_Sharing.DAL.Interfaces;
using Secrets_Sharing.Domain.Enums;
using Secrets_Sharing.Domain.Models;
using Secrets_Sharing.Domain.Response;
using Secrets_Sharing.Profile.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secrets_Sharing.Profile.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<User>> GetProfile(string userName)
        {
            try
            {
                var users = await _userRepository.GetAll();
                var user = users.FirstOrDefault(x => x.Email == userName);

                return new BaseResponse<User>()
                {
                    Data = user,
                    StatusCode = StatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<User>()
                {
                    StatusCode = StatusCode.InternalServerError,
                    Description = $"Внутренняя ошибка: {ex.Message}"
                };
            }
        }
    }
}
