﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProniaTask.Extensions;
using ProniaTask.Services.Interfaces;
using ProniaTask.ViewModels.ProductVMs;

namespace ProniaTask.Areas.Manage.Controllers;

[Area("Manage")]
public class ProductController : Controller
{
    readonly IProductService _service;
    readonly ICategoryService _catservice;

    public ProductController(IProductService service, ICategoryService catService)
    {
        _service = service;
        _catservice = catService;
    }
    public async Task<IActionResult> Index()
    {
        return View(await _service.GetTable.Include(p => p.ProductCategories).
            ThenInclude(pc => pc.Category).ToListAsync());
    }
    public IActionResult Create()
    {
        ViewBag.Categories = new SelectList(_catservice.GetTable, "Id", "Name");
        return View();
    }
    [HttpPost]
    public async Task<IActionResult> Create(CreateProductVM vm)
    {
        if (vm.MainImageFile != null)
        {
            if (!vm.MainImageFile.IsTypeValid("image"))
            {
                ModelState.AddModelError("MainImageFile", "Wrong file type");
            }
            if (!vm.MainImageFile.IsSizeValid(2))
            {
                ModelState.AddModelError("MainImageFile", "file max size is 2 mb");
            }
        }
        if (vm.HoverImageFile != null)
        {
            if (!vm.HoverImageFile.IsTypeValid("image"))
            {
                ModelState.AddModelError("HoverImageFile", "Wrong file type");
            }
            if (!vm.HoverImageFile.IsSizeValid(2))
            {
                ModelState.AddModelError("HoverImageFile", "file max size is 2 mb");
            }
        }
        if (vm.ImageFiles != null)
        {
            foreach (var item in vm.ImageFiles)
            {
                if (!item.IsTypeValid("image"))
                {
                    ModelState.AddModelError("ImageFiles", "Wrong file type");
                }
                if (!item.IsSizeValid(2))
                {
                    ModelState.AddModelError("ImageFiles", "file max size is 2 mb");
                }
            }
        }
        if (!ModelState.IsValid)
        {
            ViewBag.Categories = new SelectList(_catservice.GetTable, "Id", "Name");
            return View();
        }
        await _service.Create(vm);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Delete(int? id)
    {
        await _service.Delete(id);
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> ChangeStatus(int? id)
    {
        await _service.SoftDelete(id);
        TempData["IsDeleted"] = true;
        return RedirectToAction(nameof(Index));
    }
    public async Task<IActionResult> Update(int? id)
    {
        if (id == null || id <= 0) return BadRequest();
        var entity = await _service.GetTable.Include(p => p.ProductImages).
            Include(p => p.ProductCategories).
            SingleOrDefaultAsync(p => p.Id == id);
        if (entity == null) return BadRequest();
        ViewBag.Categories = new SelectList(_catservice.GetTable, "Id", "Name");
        UpdateProductGetVM vm = new UpdateProductGetVM
        {
            Name = entity.Name,
            Description = entity.Description,
            Discount = entity.Discount,
            Price = entity.Price,
            StockCount = entity.StockCount,
            Rating = entity.Rating,
            MainImage = entity.MainImage,
            HoverImage = entity.HoverImage,
            ProductImages = entity.ProductImages,
            ProductCategoryIds = entity.ProductCategories.Select(p => p.CategoryId).ToList()
        };
        return View(vm);
    }
    [HttpPost]
    public async Task<IActionResult> Update(int? id, UpdateProductGetVM vM)
    {
        if (id == null || id <= 0) return BadRequest();
        var entity = await _service.GetById(id);
        if (entity == null) return BadRequest();
        UpdateProductVM updateVm = new UpdateProductVM
        {
            Name = vM.Name,
            Description = vM.Description,
            Discount = vM.Discount,
            Price = vM.Price,
            StockCount = vM.StockCount,
            Rating = vM.Rating,
            HoverImage = vM.HoverImageFile,
            MainImage = vM.MainImageFile,
            ProductImages = vM.ProductImagesFile,
            CategoryIds = vM.ProductCategoryIds
        };
        await _service.Update(id, updateVm);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteImage(int id)
    {
        if (id == null || id <= 0) return BadRequest();
        await _service.DeleteImage(id);
        return Ok();
    }
}
