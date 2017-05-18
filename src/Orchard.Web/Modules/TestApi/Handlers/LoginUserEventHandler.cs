﻿using System;
using System.Web;
using Orchard.Mvc;
using Orchard.Security;
using Orchard.Users.Events;
using TestApi.Services;

namespace TestApi.Handlers
{
    public class LoginUserEventHandler : IUserEventHandler {
        private readonly IAuthService _authService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginUserEventHandler(IAuthService authService, IHttpContextAccessor httpContextAccessor) {
            _authService = authService;
            _httpContextAccessor = httpContextAccessor;
        }

        public void Creating(UserContext context) {
            throw new NotImplementedException();
        }

        public void Created(UserContext context) {
            throw new NotImplementedException();
        }

        public void LoggingIn(string userNameOrEmail, string password) {
            throw new NotImplementedException();
        }

        public void LoggedIn(IUser user) {

            var httpContext = _httpContextAccessor.Current();
            var owinContext = httpContext.GetOwinContext();
            var token = _authService.GenerateLocalAccessTokenResponse(user.UserName, owinContext);
            httpContext.Response.Cookies.Add(new HttpCookie("tokenInfo", token.ToString()));
        }

        public void LogInFailed(string userNameOrEmail, string password) {
            throw new NotImplementedException();
        }

        public void LoggedOut(IUser user) {
            throw new NotImplementedException();
        }

        public void AccessDenied(IUser user) {
            throw new NotImplementedException();
        }

        public void ChangedPassword(IUser user) {
            throw new NotImplementedException();
        }

        public void SentChallengeEmail(IUser user) {
            throw new NotImplementedException();
        }

        public void ConfirmedEmail(IUser user) {
            throw new NotImplementedException();
        }

        public void Approved(IUser user) {
            throw new NotImplementedException();
        }
    }
}