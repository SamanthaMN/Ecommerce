using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce_Web.Controllers
{
    public class ClienteController : Controller
    {
        // GET: ClienteCadastrarController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ClienteCadastrarController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ClienteCadastrarController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClienteCadastrarController/Create
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

        // GET: ClienteCadastrarController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ClienteCadastrarController/Edit/5
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

        // GET: ClienteCadastrarController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClienteCadastrarController/Delete/5
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
