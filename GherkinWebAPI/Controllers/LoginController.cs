using GherkinWebAPI.Models.Login;
using GherkinWebAPI.Utilities;
using System.Collections.Generic;
using System.Web.Http;
using System;
using GherkinWebAPI.Core.Login;
using System.Threading.Tasks;

namespace GherkinWebAPI.Controllers
{
    [RoutePrefix("api/V1/Login")]
    public class LoginController : ApiController
    {
        private ILoginService _loginService;
        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }
        [Route("UserLogin")]
        [HttpPost]
        public async Task<LoginResponseDto> UserLogin(LoginDto loginDto)
        {
            try
            {
                var userDetails = await _loginService.GetUserByUserName(loginDto.UserName.Trim());
                var hash = new CredentialHash(userDetails.User_Password);
                if (userDetails != null && hash.Verify(loginDto.Password))
                {
                    var userRoles = _loginService.GetRoleByUserName(loginDto.UserName.Trim());

                    if (loginDto.isMobileApp)
                    {
                        return new LoginResponseDto
                        {
                            Status = "Success",
                            //ExpiryMinutes = 30,
                            UserId = userDetails.UP_Serial_No,
                            EmployeeId = userDetails.Employee_ID,
                            //IsAdmin = userDetails.Roles.Equals("Admin",StringComparison.OrdinalIgnoreCase),
                            IsAdmin = userRoles.ToLower() == "admin",
                            Message = TokenManager.GenerateToken(loginDto.UserName, loginDto.isMobileApp),
                            Permissions = null,
                            OfficeLocationCode = userDetails.Org_Office_No,
                            OrgCode = userDetails.Org_Code
                        };
                    }
                    else
                    {
                        return new LoginResponseDto
                        {
                            Status = "Success",
                            ExpiryMinutes = 30,
                            UserId = userDetails.UP_Serial_No,
                            EmployeeId = userDetails.Employee_ID,
                            //IsAdmin = userDetails.Roles.Equals("Admin",StringComparison.OrdinalIgnoreCase),
                            IsAdmin = userRoles.ToLower() == "admin",
                            Message = TokenManager.GenerateToken(loginDto.UserName, loginDto.isMobileApp),
                            Permissions = null,
                            OfficeLocationCode = userDetails.Org_Office_No,
                            OrgCode = userDetails.Org_Code
                        };
                    }

                }
                return new LoginResponseDto { Status = "Invalid", Message = $"Invalid User" };
            }
            catch (Exception ex)
            {
                return new LoginResponseDto { Status = "Invalid", Message = $"Invalid User" };
            }
        }
    }
}
