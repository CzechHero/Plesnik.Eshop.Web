using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Select()
        {
            IList<CarouselItem> carouselItems = DatabaseFake.CarouselItems;

            return View(carouselItems);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarouselItem carouselItem)
        {
            if (carouselItem != null && !string.IsNullOrEmpty(carouselItem.ImageSource) && !string.IsNullOrEmpty(carouselItem.ImageAlt))
            {
                if (DatabaseFake.CarouselItems != null && DatabaseFake.CarouselItems.Count > 0)
                {
                    carouselItem.Id = DatabaseFake.CarouselItems.Last().Id + 1;
                }

                DatabaseFake.CarouselItems.Add(carouselItem);
                return RedirectToAction(nameof(CarouselController.Select));
            }
            else
            {
                return View(carouselItem);
            }
        }

        public IActionResult Edit(int id)
        {
            var foundItem = DatabaseFake.CarouselItems.FirstOrDefault(c => c.Id == id);
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
        public IActionResult Edit(CarouselItem carouselItemEdit)
        {
            var foundItem = DatabaseFake.CarouselItems.FirstOrDefault(c => c.Id == carouselItemEdit.Id);
            if (foundItem != null)
            {
                foundItem.ImageSource = carouselItemEdit.ImageSource;
                foundItem.ImageAlt = carouselItemEdit.ImageAlt;
                return RedirectToAction(nameof(CarouselController.Select));
            }
            else
            {
                return NotFound();
            }
        }

        public IActionResult Delete(int id)
        {
            IList<CarouselItem> carouselItems = DatabaseFake.CarouselItems;
            var carouselItem = carouselItems.FirstOrDefault(i => i.Id == id);
            if (carouselItem != null)
                carouselItems.Remove(carouselItem);

            return RedirectToAction(nameof(CarouselController.Select));
        }
    }
}
