using Autofac;
using DataImporter.WebApp.Models.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Controllers
{
    [Authorize(Policy = "ViewPermission")]
    public class DashboardController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly UserModel _userModel;
        public DashboardController(ILifetimeScope scope, UserModel userModel)
        {
            _scope = scope;
            _userModel = userModel;
        }
        public async Task<IActionResult> Index()
        {
            var userId = await Task.Run(() => _userModel.GetUserId(HttpContext));
            var model = new DashboardModel();
            model.Resolve(_scope);
            await Task.Run(() => model.Get(userId));
            return View(model);
        }
    }
}
