using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IotRestFullApi.Controllers
{
    public class StatisticsController : Controller
    {
        // GET: DeviceStatistics
        public ActionResult Index()
        {
            return View();
        }

        // GET: DeviceStatistics/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DeviceStatistics/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: DeviceStatistics/Create
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

        // GET: DeviceStatistics/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DeviceStatistics/Edit/5
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

        // GET: DeviceStatistics/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DeviceStatistics/Delete/5
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
