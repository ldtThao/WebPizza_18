using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPizza_18.Models;

namespace WebPizza_18.Areas.Admin.Controllers
{
    public class AdminHomeController : Controller
    {
        WebPizza18Entities db = new WebPizza18Entities();
        // GET: Product
        public ActionResult ListProduct()
        {
            ViewData["ProductName"] = "Tên sản phẩm";
            ViewData["CategoryName"] = "Loại";
            ViewData["Ingredients"] = "Thành phần";
            ViewData["UnitPrice"] = "Giá";
            ViewData["Size"] = "Kích cỡ";
            ViewData["GroupName"] = "Nhóm";
            List<Product> listProduct = db.Products.ToList();
            return View(listProduct);
        }
        public ActionResult LOrder()
        {
            ViewData["CustomerID"] = "Ma Khach Hang";
            ViewData["EmployeeId"] = "Ma nhan vien";
            ViewData["Create_at"] = "Thoi gian tao";
            ViewData["ShipAddress"] = "Dia chi giao hang";
            List<Order> listOrder = db.Orders.ToList();
            return View(listOrder);
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Product product, FormCollection collection)
        {
            try
            {

                // TODO: Add insert logic here
                Product p = product;
                db.Products.Add(p);
                db.SaveChanges();
                return RedirectToAction("ListProduct");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Edit(int id)
        {
            ViewData["ProductName"] = "Tên sản phẩm";
            ViewData["CategoryName"] = "Loại";
            ViewData["Ingredients"] = "Thành phần";
            ViewData["UnitPrice"] = "Giá";
            ViewData["Size"] = "Kích cỡ";
            ViewData["GroupName"] = "Nhóm";
            Product p = db.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }

        // POST: Product/Edit/5
        [HttpPost]
        public ActionResult Edit(Product product, int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here
                Product p = db.Products.First(s => s.ProductID == id);
                p.ProductName = product.ProductName;
                p.CategoryID = product.CategoryID;
                p.Ingredients = product.Ingredients;
                p.UnitPrice = product.UnitPrice;
                p.Size = product.Size;
                p.GroupID = product.GroupID;
                db.SaveChanges();
                return RedirectToAction("ListProduct");
            }
            catch
            {
                return View();
            }
        }
        public ActionResult Delete(int id)
        {
            ViewData["ProductName"] = "Tên sản phẩm";
            ViewData["CategoryName"] = "Loại";
            ViewData["Ingredients"] = "Thành phần";
            ViewData["UnitPrice"] = "Giá";
            ViewData["Size"] = "Kích cỡ";
            ViewData["GroupName"] = "Nhóm";
            Product p = db.Products.FirstOrDefault(s => s.ProductID == id);
            return View(p);
        }

        // POST: Product/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                Product p = db.Products.First(s => s.ProductID == id);
                db.Products.Remove(p);
                db.SaveChanges();
                return RedirectToAction("ListProduct");
            }
            catch
            {
                return View();
            }
        }
    }
}