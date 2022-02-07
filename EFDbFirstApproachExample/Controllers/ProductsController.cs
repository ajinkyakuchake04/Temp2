using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using EFDbFirstApproachExample.Models;//Include Manually

namespace EFDbFirstApproachExample.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products/Index
        public ActionResult Index(string search="")
        {
           
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            List <Product> products = db.Products.ToList();

            ViewBag.search = search;//Added so that we can keep the value in theserach box as it is

            //To display only products with category ID as 1
            List<Product> productsName = db.Products.Where(temp=>temp.ProductName.Contains(search)).ToList();
            List<Product> productsAvail = db.Products.Where(temp => temp.AvailabilityStatus.Contains(search)).ToList();

            if (productsName.Count >= 1)
            {
                return View(productsName);
            }
            else if (productsAvail.Count >= 1)
            {
                return View(productsAvail);
            }
            else
            {
                string message = "No Records Found";
                MessageBox.Show(message);
                return View(products);
            }

        }

        public ActionResult Details(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product p = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(p);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            db.Products.Add(p);
            db.SaveChanges();

            return RedirectToAction("Index");
        }

        public ActionResult Edit(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existingProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            return View(existingProduct);
        }

        [HttpPost]
        public ActionResult Edit(Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existingProduct = db.Products.Where(temp=>temp.ProductID==p.ProductID).FirstOrDefault();
            existingProduct.ProductName = p.ProductName;
            existingProduct.Price = p.Price;
            existingProduct.DateOfPurchase = p.DateOfPurchase;
            existingProduct.AvailabilityStatus = p.AvailabilityStatus;
            existingProduct.CategoryID = p.CategoryID;
            existingProduct.BrandID = p.BrandID;
            existingProduct.Active = p.Active;

            db.SaveChanges();
            return RedirectToAction("Index","Products");
        }

        public ActionResult Delete(long id)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existingProduct = db.Products.Where(temp => temp.ProductID==id).FirstOrDefault();
            return View(existingProduct);
        }

        [HttpPost]
        public ActionResult Delete(long id,Product p)
        {
            EFDBFirstDatabaseEntities db = new EFDBFirstDatabaseEntities();
            Product existingProduct = db.Products.Where(temp => temp.ProductID == id).FirstOrDefault();
            db.Products.Remove(existingProduct);
            db.SaveChanges();
            return RedirectToAction("Index","Products");
        }
    }
}