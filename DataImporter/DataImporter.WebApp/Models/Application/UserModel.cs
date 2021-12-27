using Autofac;
using DataImporter.Users.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Models.Application
{
    public class UserModel
    {
        private UserManager<ApplicationUser> _userManager;
        private ILifetimeScope _scope;
        public UserModel()
        {
        }

        public void Resolve(ILifetimeScope scope)
        {
            _scope = scope;
            _userManager = _scope.Resolve<UserManager<ApplicationUser>>();
        }
        public UserModel(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }
        public Guid GetUserId(HttpContext context)
        {
            var userId = _userManager.GetUserId(context.User);
            return new Guid(userId);
        }
    }
}
