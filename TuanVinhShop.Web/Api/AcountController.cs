using Microsoft.AspNet.Identity.Owin;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TuanVinhShop.Web.App_Start;
using TuanVinhShop.Web.Models;

namespace TuanVinhShop.Web.Api
{
    [RoutePrefix("Api/Account")]
    public class AcountController : ApiController
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        public AcountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.Current.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        [Route("Login")]
        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> Login(HttpRequestMessage request,AcountLogin acount)
        {
            if (!ModelState.IsValid)
            {
                return request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            else
            {
                var result = await SignInManager.PasswordSignInAsync(acount.UserName, acount.Password, acount.RememberMe, shouldLockout: false);
                return request.CreateResponse(HttpStatusCode.OK, result);
            }     
        }
    }
}
