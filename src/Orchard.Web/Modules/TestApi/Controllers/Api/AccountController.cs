using System.Web;
using System.Web.Http;
using Orchard.Localization;
using Orchard.Security;
using Orchard.Users.Services;
using TestApi.ViewModels;

namespace TestApi.Controllers.Api
{
    [Authorize]
    public class AccountController : ApiController
    {
        private readonly IMembershipService _membershipService;
        private readonly IUserService _userService;

        public AccountController(
            IMembershipService membershipService,
            IUserService userService)
        {
            _membershipService = membershipService;
            _userService = userService;
            T = NullLocalizer.Instance;
        }

        public Localizer T { get; set; }

        [AllowAnonymous]
        [HttpPost]
        public IHttpActionResult Register(RegisterBindingModel model) {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (!string.IsNullOrEmpty(model.Email))
            {
                if (!_userService.VerifyUserUnicity(model.Email, model.Email))
                {
                    AddModelError("NotUniqueUserName", T("User with that email already exists."));
                    return BadRequest(ModelState);
                }
            }

            var user = _membershipService.CreateUser(new CreateUserParams(model.Email, model.Password, model.Email, null, null, true));
            if (user != null)
                return Ok();

            return InternalServerError();
        }

        public void AddModelError(string key, LocalizedString errorMessage)
        {
            ModelState.AddModelError(key, errorMessage.ToString());
        }
    }
}