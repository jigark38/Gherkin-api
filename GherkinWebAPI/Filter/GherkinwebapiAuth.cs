using GherkinWebAPI.Core.Login;
using GherkinWebAPI.Utilities;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace GherkinWebAPI.Filter
{
    public class GherkinwebapiAuth : AuthorizationFilterAttribute
    {
        private ILoginService _service;

        //public GherkinwebapiAuth(ILoginService service)
        //{
        //    _service = service;
        //}
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //var requestScope = actionContext.Request.GetDependencyScope();
            //_service = requestScope.GetService(typeof(ILoginService)) as ILoginService;

            string userName = "";
            string loginUrl = "/api/V1/Login/UserLogin";
            if (actionContext.Request.RequestUri.AbsolutePath.ToLower() == loginUrl.ToLower())
            {
                return;
            }
            string pingUrl = "/api/ping";
            if (actionContext.Request.RequestUri.AbsolutePath.ToLower() == pingUrl.ToLower())
            {
                return;
            }
            if (actionContext.Request.Headers.Authorization != null)
            {
                var authToken = actionContext.Request.Headers.Authorization.Scheme;
                // decoding authToken we get decode value in 'Username:token' format  
                //var decodeauthToken = System.Text.Encoding.UTF8.GetString(Convert.FromBase64String(authToken));
                // spliting decodeauthToken using ':'   

                // at 0th postion of array we get username and at 1st we get token  
                userName = TokenManager.ValidateToken(authToken);
                if (!string.IsNullOrEmpty(Validate(authToken)))
                {
                    string Roles = "Admin";//_service.GetRoleByUserName(userName);
                    string[] roles = Roles.Split(',');
                    IPrincipal principal = new GenericPrincipal(new GenericIdentity(userName), roles);
                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }
                }
                else
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }
            }
            else
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
        //public string GetRolesByUserName(string username)
        //{
        //    string roles = string.Empty;
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["DevSqlconnection"].ConnectionString))
        //        {
        //            string cmdText = "select roles from login.Roles where username= '" + username + "'";
        //            using (SqlCommand cmd = new SqlCommand(cmdText, con))
        //            {
        //                cmd.CommandType = CommandType.Text;
        //                con.Open();
        //                roles = cmd.ExecuteScalar().ToString();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //    return roles;
        //}
        public string Validate(string token, string username = "GherkinUI")
        {
            //to do
            int UserId = 12345;
            //new UserRepository().GetUser(username);  db call
            if (UserId == 0) return "";
            //return new ResponseDto { Status = "Invalid", Message = "Invalid User." };
            string tokenUsername = TokenManager.ValidateToken(token);
            //if (username.Equals(tokenUsername))
            //{
            //    //return new ResponseDto { Status = "Success", Message = "OK", };
            //    return true;
            //}
            return tokenUsername;
            // return new ResponseDto { Status = "Invalid", Message = "Invalid Token." };
        }
    }
}