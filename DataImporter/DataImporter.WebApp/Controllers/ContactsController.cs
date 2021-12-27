using Autofac;
using DataImporter.Common.Utilities;
using DataImporter.WebApp.Models.Application;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace DataImporter.WebApp.Controllers
{
    [Authorize(Policy = "ViewPermission")]
    public class ContactsController : Controller
    {
        private readonly ILogger<ContactsController> _logger;
        private readonly ILifetimeScope _scope;
        private readonly UserModel _userModel;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ContactsController(ILifetimeScope scope, UserModel userModel, ILogger<ContactsController> logger, IWebHostEnvironment webHostEnvironment)
        {
            _scope = scope;
            _userModel = userModel;
            _logger = logger;
            _webHostEnvironment = webHostEnvironment;
        }
        public async Task<IActionResult> Index()
        {
            var userId = await Task.Run(() => _userModel.GetUserId(HttpContext));
            var model = new GroupListModel();
            await Task.Run(() => model.Resolve(_scope));
            var groupList = await Task.Run(()=>model.GetAllThatHasData(userId));
            ViewBag.GroupList = groupList;
            return await Task.Run(() => View());
        }
        [HttpPost, ValidateAntiForgeryToken]
        public async Task<IActionResult> All(ContactstListModel model)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await Task.Run(() => model.Resolve(_scope));
                    await Task.Run(() => model.Get());
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", ex.Message);
                    _logger.LogInformation("", ex);
                    return RedirectToAction(nameof(Index));
                }
            }
            return await Task.Run(() => View(model));
        }
        public IActionResult Upload()
        {

            var userId = _userModel.GetUserId(HttpContext);
            var model = new GroupListModel();
            model.Resolve(_scope);
            var groupList = model.GetAllWithoutExcelRecordRecord(userId);
            ViewBag.GroupList = groupList;
            return View();
        }

        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Upload(UploadFileModel model)
        {
            var userId =  _userModel.GetUserId(HttpContext);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    model.Read(userId, _webHostEnvironment.WebRootPath);
                }
                catch(Exception ex)
                {
                    _logger.LogInformation("", ex);
                }
            }
            return RedirectToAction(nameof(UploadConfirmation));
        }
        public IActionResult UploadConfirmation()
        {
            var model = new UploadConfirmationModel();
            model.Resolve(_scope);
            var userId = _userModel.GetUserId(HttpContext);
            model.Read(userId, _webHostEnvironment.WebRootPath);
            return View(model);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult UploadConfirmation(UploadConfirmationModel model)
        {
            var userId =  _userModel.GetUserId(HttpContext);
            if (ModelState.IsValid)
            {
                try
                {
                    model.Resolve(_scope);
                    model.Save(userId, _webHostEnvironment.WebRootPath);
                }
                catch(Exception ex)
                {
                    _logger.LogInformation("", ex);
                }
            }

            return RedirectToAction(nameof(Import));
        }
        public IActionResult Cancel()
        {
            var userId = _userModel.GetUserId(HttpContext);
            var model =  new CancelFileUploadModel();
            model.Resolve(_scope);
            model.Cancel(userId, _webHostEnvironment.WebRootPath);
            return RedirectToAction(nameof(Import));
        }
        public IActionResult Import()
        {
            return View();
        }
        public JsonResult GetImportData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var userId = _userModel.GetUserId(HttpContext);
            var model = new ImportHistoryModel();
            model.Resolve(_scope);
            var data = model.GetAll(userId, dataTablesModel);
            return Json(data);
        }
        public IActionResult Export()
        {
            return View();
        }
        public JsonResult GetExportData()
        {
            var dataTablesModel = new DataTablesAjaxRequestModel(Request);
            var userId = _userModel.GetUserId(HttpContext);
            var model = new ExportHistoryModel();
            model.Resolve(_scope);
            var data = model.GetAll(userId, dataTablesModel);
            return Json(data);
        }
        [HttpPost, ValidateAntiForgeryToken]
        public IActionResult Export(Guid id, string email)
        {
            var userId = _userModel.GetUserId(HttpContext);
            var model = new ExportGroupModel();
            try
            {
                model.Resolve(_scope);
                model.Export(id, userId, email, _webHostEnvironment.WebRootPath);
            }
            catch(Exception ex)
            {
                _logger.LogInformation("", ex);
            }
            return RedirectToAction(nameof(Export));
        }
    }
}
