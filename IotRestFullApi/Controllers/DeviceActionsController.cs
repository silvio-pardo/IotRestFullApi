using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IotRestFullApi.Controllers
{
    public class DeviceActionsController : Controller
    {
        // GET: DeviceActionsController
        public ActionResult Index()
        {
            return View();
        }

        // GET: DeviceActionsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeviceActionsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeviceActionsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DeviceActionsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeviceActionsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DeviceActionsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeviceActionsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
