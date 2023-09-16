using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPizza_18.Models;

namespace WebPizza_18.Controllers
{
    public class ProductController : Controller
    {
        WebPizza18Entities db = new WebPizza18Entities();

        // GET: Home
        
        public ActionResult DetailsGroup(int id)
        {
            //ViewData["ProductN"] = (from p in db.Groups where p.GroupID == id select p.GroupName).FirstOrDefault();
            ViewData["ProductName"] = "Tên sản phẩm";
            ViewData["Ingredients"] = "Thành phần";
            ViewData["UnitPrice"] = "Giá";
            ViewData["Size"] = "Kích cỡ";
            List<Product> products = (from p in db.Products join g in db.Groups on p.GroupID equals g.GroupID where g.GroupID == id select p).ToList();
            return View(products);
        }
        public ActionResult DetailsProduct(int id)
        {
            ViewData["ProductName"] = "Tên sản phẩm";
            ViewData["Ingredients"] = "Thành phần";
            ViewData["UnitPrice"] = "Giá";
            ViewData["Size"] = "Kích cỡ";
            Product products = db.Products.FirstOrDefault(a => a.ProductID == id);
            return View(products);
        }

        public ActionResult Search(string searchString)
        {
            //ViewData["NCC"] = new SelectList(db.Categories, "CategoryID", "CategoryName");
            ViewData["ProductName"] = "Tên sản phẩm";
            ViewData["Ingredients"] = "Thành phần";
            ViewData["UnitPrice"] = "Giá";
            ViewData["Size"] = "Kích cỡ";
            var links = from l in db.Products // lấy toàn bộ liên kết
                        select l;
            if (!String.IsNullOrEmpty(searchString)) // kiểm tra chuỗi tìm kiếm có rỗng/null hay không
            {
                links = links.Where(s => s.ProductName.Contains(searchString)); //lọc theo chuỗi tìm kiếm
            }
            return View(links);
        }









        // GET: Product
        public ActionResult Index()
        {
            return View();
        }

        // GET: Product/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Product/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Product/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
