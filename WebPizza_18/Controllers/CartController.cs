using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebPizza_18.Models;
using System.Transactions;
using WebPizza_18.Models;

namespace WebPizza_18.Controllers
{
    public class CartController : Controller
    {
        WebPizza18Entities db = new WebPizza18Entities();

        private List<CartModel> GetListCarts()
        {
            List<CartModel> carts = Session["CartModel"] as List<CartModel>; //ds ca sp trong gio hang
            if (carts == null) //chua co sp
            {
                carts = new List<CartModel>();
                Session["CartModel"] = carts;
            }
            return carts;
        }
        public ActionResult ListCarts()
        {
            ViewData["ProductName"] = "Tên sản phẩm";
            ViewData["Quantity"] = "Số lượng";
            ViewData["UnitPrice"] = "Giá";
            ViewData["Total"] = "Tổng cộng";
            //lay ds sp trong gio hang
            List<CartModel> carts = GetListCarts();

            ViewBag.CountProduct = carts.Sum(a => a.Quantity);
            ViewBag.Total = carts.Sum(a => a.Total);

            return View(carts);
        }
        public ActionResult AddCart(int id)
        {
            //Lay ds gio hang da co
            List<CartModel> carts = GetListCarts();
            //tao moi 1 sp trong gio hang : CartModel
            CartModel c = carts.Find(s => s.ProductID == id);
            if (c == null)
            {
                c = new CartModel(id);
                carts.Add(c);//add sp do vao ds
            }
            else
            {
                c.Quantity++;
            }
            return RedirectToAction("ListCarts");
        }
        public ActionResult DeleteProduct(int id)
        {
            //Lay ds gio hang da co
            List<CartModel> carts = GetListCarts();
            //tao moi 1 sp trong gio hang : CartModel
            CartModel c = carts.Find(s => s.ProductID == id);
            if (c.Quantity == 1)
            {
                carts.Remove(c);//xoa sp do khoi ds
                Session["CartModel"] = carts;
            }
            else
            {
                c.Quantity--;
            }
            return RedirectToAction("ListCarts");
        }

        public ActionResult CreateOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateOrder(Customer customer, FormCollection collection)
        {
            //Lay sp trả ve
            Customer c = customer;
            try
            {

                //Them sp vao bang product
                db.Customers.Add(c);
                //cap nhat xuong database
                db.SaveChanges();
                //hien thi lai dssp
            }
            catch
            {
                return View();
            }
            using (TransactionScope tranScope = new TransactionScope())
            {
                try
                {
                    //1. Tạo mới 1 đơn hàng
                    //1.1 Tạo mới 1 đối tượng order
                    Order order = new Order();
                    List<CartModel> cartModels = GetListCarts();
                    //1.2 Thiết lập thuộc tính cho order
                    Random rand = new Random();
                    int employee = rand.Next(0, 9);
                    order.EmployeeID = employee;
                    order.CustomerID = c.CustomerID;
                    order.Create_at = DateTime.Now;
                    //1.3 Add order vào bảng orders
                    db.Orders.Add(order);
                    //1.4 Cap nhật db
                    db.SaveChanges();
                    //2. Duyệt từng sp trong giỏ hàng, thêm sp đó vào bảng orderDetail
                    //2.1 Duyệt từng sản phẩm tỏng giỏ hàng
                    foreach (var item in cartModels)
                    {
                        //2.2.1 Tạo mới 1 đối tượng orderdetail
                        Order_Detail orderDetail = new Order_Detail();
                        //2.2.2 Thiết lập thuộc tính cho orderdetail
                        orderDetail.OrderID = order.OrderID;
                        orderDetail.ProductID = item.ProductID;
                        orderDetail.UnitPrice = (int)decimal.Parse(item.UnitPrice.ToString());
                        orderDetail.Quantity = (byte)short.Parse(item.Quantity.ToString());
                        //orderDetail.Discount = 0;
                        //2.2.3 Add order vào bảng ordersdetail
                        db.Order_Details.Add(orderDetail);
                    }

                    //2.3 Câp nhat db
                    db.SaveChanges();
                    Session["CartModel"] = null;
                    tranScope.Complete();
                }
                catch (Exception)
                {

                    tranScope.Dispose();
                }
            }
            return RedirectToAction("OrderSS");

        }


        public ActionResult OrderSS()
        {
            return View();
        }


        //public ActionResult ListOrder()
        //{

        //    ViewData["Create_at"] = "Thời gian tạo";
        //    ViewData["OrderID"] = "Mã đơn hàng";
        //    ViewData["CustomerID"] = "Mã Khách hàng";
        //    ViewData["EmployeeID"] = "Mã Nhân viên";
        //    ViewData["ShipAddress"] = "Địa chỉ";
        //    var ds = db.Orders.OrderByDescending(a => a.Create_at).ToList();
        //    return View(ds);
        //}

        //public ActionResult DeleteOrder()
        //{
        //    return View();
        //}


        //public ActionResult DetailOrder(int id)
        //{
        //    ViewData["OrderID"] = "Mã đơn hàng";
        //    ViewData["ProductID"] = "Mã sản phẩm";
        //    ViewData["Quantity"] = "Số lượng";
        //    ViewData["UnitPrice"] = "Giá";
        //    ViewData["Total"] = "Tổng cộng";
        //    var detail = from l in db.Order_Details // lấy toàn bộ liên kết
        //                 select l;
        //    detail = detail.Where(s => s.OrderID == id); //lọc theo chuỗi tìm kiếm
        //    return View(detail);
        //}





    }
}
