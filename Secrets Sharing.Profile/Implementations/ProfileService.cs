using Secrets_Sharing.DAL.Interfaces;
using Secrets_Sharing.Domain.Models;
using Secrets_Sharing.Domain.Response;
using Secrets_Sharing.Profile.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Secrets_Sharing.Profile.Implementations
{
    public class ProfileService : IProfileService
    {
        private readonly IUserRepository _userRepository;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userRepository"></param>
        public ProfileService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<BaseResponse<User>> GetProfile(string email)
        {
            var users = await _userRepository.GetAll();
            var user = users.FirstOrDefault(u => u.Email == email);

            if (user == null)
            {
                return new BaseResponse<User>()
                {
                    Description = "User not found"
                };
            }

            return new BaseResponse<User>()
            {
                Data = user,
                StatusCode = Domain.Enums.StatusCode.OK
            };
        }
    }
}
