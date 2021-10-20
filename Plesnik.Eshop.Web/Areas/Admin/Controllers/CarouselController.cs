using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plesnik.Eshop.Web.Models.Database;
using Plesnik.Eshop.Web.Models.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CarouselController : Controller
    {
        readonly EShopDbContext _dbContext;
        public CarouselController(EShopDbContext eShopDbContext)
        {
            _dbContext = eShopDbContext;
        }

        public IActionResult Select()
        {
            IList<CarouselItem> carouselItems = _dbContext.CarouselItems.ToList();

            return View(carouselItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CarouselItem carouselItem)
        {
            if (carouselItem != null && !string.IsNullOrEmpty(carouselItem.ImageSource) && !string.IsNullOrEmpty(carouselItem.ImageAlt))
            {
                _dbContext.CarouselItems.Add(carouselItem);

                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(CarouselController.Select));
            }
            else
            {
                return View(carouselItem);
            }
        }

        public IActionResult Edit(int id)
        {
            var foundItem = _dbContext.CarouselItems.FirstOrDefault(c => c.Id == id);
            if (foundItem != null)
            {
                return View(foundItem);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CarouselItem carouselItemEdit)
        {
            var foundItem = _dbContext.CarouselItems.FirstOrDefault(c => c.Id == carouselItemEdit.Id);
            if (foundItem != null)
            {
                foundItem.ImageSource = carouselItemEdit.ImageSource;
                foundItem.ImageAlt = carouselItemEdit.ImageAlt;

                await _dbContext.SaveChangesAsync();

                return RedirectToAction(nameof(CarouselController.Select));
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            DbSet<CarouselItem> carouselItems = _dbContext.CarouselItems;
            var carouselItem = carouselItems.FirstOrDefault(i => i.Id == id);
            if (carouselItem != null)
                carouselItems.Remove(carouselItem);

            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(CarouselController.Select));
        }
    }
}
