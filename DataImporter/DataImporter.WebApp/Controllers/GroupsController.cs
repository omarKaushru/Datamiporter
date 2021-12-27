using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.WebApp.Models.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Controllers
{
    [Authorize(Policy = "ViewPermission")]
    public class GroupsController : Controller
    {
        private readonly ILifetimeScope _scope;
        private readonly UserModel _userModel;
        private readonly ILogger<GroupsController> _logger;
        public GroupsController(ILifetimeScope scope, UserModel userModel, ILogger<GroupsController> logger)
        {
            _scope = scope;
            _userModel = userModel;
            _logger = logger;
        }
        public async Task<IActionResult> Create()
        {
            var model = await Task.Run(() => _scope.Resolve<CreateGroupModel>());
            model.ApplicationUserId = await Task.Run(() => _userModel.GetUserId(HttpContext));
            return await Task.Run(() => View(model));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Create(CreateGroupModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    model.CreateGroup();
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogInformation("", ex);
                }
            }
            return  View(model);
        }
        public async Task<IActionResult> Edit(Guid id)
        {
            var model = new EditGroupModel();
            await Task.Run(() => model.Resolve(_scope));
            await Task.Run(() => model.LoadModelData(id));
            return await Task.Run(() => View(model));
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGroupModel model)
        {
            var userId = _userModel.GetUserId(HttpContext);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    model.Update(userId);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogError(ex.Message, "Upadating Group Failed");
                }
            }
            return await Task.Run(() => RedirectToAction(nameof(All)));
        }
        public async Task<IActionResult> All()
        {
            return await Task.Run(() => View());
        }
        public JsonResult GetGroupData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var userId = _userModel.GetUserId(HttpContext);
            var model = new GroupListModel();
            model.Resolve(_scope);
            var data = model.GetAll(userId, dataTablesModel);
            return Json(data);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Delete(Guid id)
        {
            var model =new DeleteGroupModel();
            model.Resolve(_scope);
            try
            {
                model.Delete(id);
            }
            catch(Exception ex)
            {
                _logger.LogInformation("", ex.Message);
            }
            return RedirectToAction(nameof(All));
        }
    }
}
