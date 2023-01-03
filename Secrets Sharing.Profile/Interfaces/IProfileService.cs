using Secrets_Sharing.Domain.Models;
using Secrets_Sharing.Domain.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Secrets_Sharing.Profile.Interfaces
{
    public interface IProfileService
    {
        Task<BaseResponse<User>> GetProfile(string email);
    }
}
