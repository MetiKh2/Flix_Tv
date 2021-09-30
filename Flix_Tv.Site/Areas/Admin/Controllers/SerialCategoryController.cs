using Flix_Tv.Application.DTOs.SerialCategory.Admin.Edit;
using Flix_Tv.Application.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Flix_Tv.Site.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SerialCategoryController : Controller
    {
        private readonly ISerialService _serialService;
        public SerialCategoryController(ISerialService serialService)
        {
            _serialService = serialService;
        }
        [Route("Admin/SerialCategories")]
        public async Task< IActionResult >Index(int pageId=1,string filter="")
        {
            ViewBag.filter = filter;
            ViewBag.pageId = pageId;
            return View(await _serialService.GetSerialCategoriesInAdmin(pageId,filter));
        }

        [Route("Admin/CreateSerialCategory")]
        [HttpPost]
        public async Task<IActionResult> CreateSerialCategory(string categoryName, IFormFile categoryImage)
        {
            try
            {
                if (string.IsNullOrEmpty(categoryName) || categoryName.Length > 50)
                {
                    return Json(false);
                }
                var category = await _serialService.CreateSerialCategory(categoryName, categoryImage);

                return Json(category);
            }
            catch
            {
                return Json(false);
                throw;
            }
        }
        [HttpPost]
        [Route("Admin/DeleteSerialCategory")]
        public async Task<IActionResult> DeleteSerialCategory(long Id)
        {
            return Json(await _serialService.DeleteSerialCategory(Id));
        }

        [Route("Admin/EditSerialCategory/{id}")]
        public async Task<IActionResult> EditSerialCategory(long id)
        {
            var category = await _serialService.GetSerialCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }
            return View(new SerialCategoryForEditDto
            {
                Id = category.Id,
                ImageName = category.ImageSrc,
                Title = category.Title
            });
        }
        [HttpPost]
        [Route("Admin/EditSerialCategory")]
        public async Task<IActionResult> EditSerialCategory(SerialCategoryForEditDto dto, IFormFile image)
        {
            if (!ModelState.IsValid)
            {
                return View(dto);
            }
            await _serialService.EditSerialCategory(dto, image);
            return Redirect("/Admin/SerialCategories");
        }
    }
}
