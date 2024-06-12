﻿using CookingBook.Web.Data;
using CookingBook.Web.Models.Domena;
using CookingBook.Web.Models.ViewModels;
using CookingBook.Web.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CookingBook.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminTagiController : Controller
    {
        private readonly ITagRepository tagRepository;

        public AdminTagiController(ITagRepository tagRepository)
        {
            this.tagRepository = tagRepository;
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Add")]
        public async Task<IActionResult> Add(AddTagRequest addTagRequest)
        {
            // Mapping AddTagRequest to Tag domain model
            var tag = new Tag
            {
                Nazwa = addTagRequest.Nazwa,
                WyświetlanaNazwa = addTagRequest.WyświetlanaNazwa
            };

            await tagRepository.AddAsync(tag);

            return RedirectToAction("List");
        }

        [HttpGet]
        [ActionName("List")]
        public async Task<IActionResult> List()
        {
            var tagi = await tagRepository.GetAllAsync();



			return View(tagi);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id) 
        {
            var tag = await tagRepository.GetAsync(id);

            if(tag != null)
            {
                var editTagRequest = new EditTagRequest
                {
                    Id = tag.Id,
                    Nazwa = tag.Nazwa,
                    WyświetlanaNazwa = tag.WyświetlanaNazwa
                };

                return View(editTagRequest);
            }

            return View(null);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditTagRequest editTagRequest)
        {
            var tag = new Tag
            {
                Id = editTagRequest.Id,
                Nazwa = editTagRequest.Nazwa,
                WyświetlanaNazwa = editTagRequest.WyświetlanaNazwa
            };

            var updatedTag = await tagRepository.UpdateAsync(tag);

           
            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpPost]
        public async Task<IActionResult> Delete(EditTagRequest editTagRequest)
        {
            var deletedTag = await tagRepository.DeleteAsync(editTagRequest.Id);

            if(deletedTag != null)
            {
                return RedirectToAction("List");
            }

            return RedirectToAction("Edit", new { id = editTagRequest.Id });
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
