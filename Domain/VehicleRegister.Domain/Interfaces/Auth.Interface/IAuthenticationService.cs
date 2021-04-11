using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleRegister.Domain.DTO.UserDTO.Request;
using VehicleRegister.Domain.DTO.UserDTO.Response;

namespace VehicleRegister.Domain.Interfaces.Auth.Interface
{
    public interface IAuthenticationService
    {
        Task<bool> IsValidUserNameAndPassword(string username, string password);
        Task<List<string>> GetUsersRole(string username);
        Task<bool> RegisterUser(RegisterUserRequest request);
        Task<GetUserInformationDto> GetUserInformation(string userName);
    }
}
