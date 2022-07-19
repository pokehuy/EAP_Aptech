using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using EAP_C2007_NGUYENQUANGHUY.Models;
using PagedList;

namespace EAP_C2007_NGUYENQUANGHUY
{
    [Authorize(Users ="admin@mvc.com")]
    public class ProductsController : Controller
    {
        private DataContext db = new DataContext();

        // GET: Products
        [AllowAnonymous]
        public ActionResult Index(string sortOrder, string currentFilter, string searchName, int? page, string pageSize)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.CategorySort = string.IsNullOrEmpty(sortOrder) ? "category_desc" : "";
            ViewBag.NameSort = sortOrder == "Name" ? "name_desc" : "Name";
            ViewBag.ReleaseSort = sortOrder == "Release Date" ? "release_desc" : "Release Date";
            ViewBag.QuantitySort = sortOrder == "Quantity" ? "quantity_desc" : "Quantity";
            ViewBag.PriceSort = sortOrder == "Price" ? "price_desc" : "Price";

            if (searchName != null)
            {
                page = 1;
            }
            else
            {
                searchName = currentFilter;
            }

            ViewBag.CurrentFilter = searchName;
            var products = db.Products.Include(a => a.Category);

            if (!string.IsNullOrEmpty(searchName))
            {
                products = products.Where(a => a.Name.ToLower().Contains(searchName.ToLower().Trim()));
            }
            switch (sortOrder)
            {
                case "category_desc":
                    products = products.OrderByDescending(s => s.Category.CategoryName);
                    break;
                case "Name":
                    products = products.OrderBy(s => s.Name);
                    break;
                case "name_desc":
                    products = products.OrderByDescending(s => s.Name);
                    break;
                case "Release Date":
                    products = products.OrderBy(s => s.ReleaseDate);
                    break;
                case "release_desc":
                    products = products.OrderByDescending(s => s.ReleaseDate);
                    break;
                case "Quantity":
                    products = products.OrderBy(s => s.Quantity);
                    break;
                case "quantity_desc":
                    products = products.OrderByDescending(s => s.Quantity);
                    break;
                case "Price":
                    products = products.OrderBy(s => s.Price);
                    break;
                case "price_desc":
                    products = products.OrderByDescending(s => s.Price);
                    break;
                default:
                    products = products.OrderBy(s => s.Category.CategoryName);
                    break;
            }
            //List<SelectListItem> items = new List<SelectListItem>();

            //items.Add(new SelectListItem { Text = "5", Value = "5", Selected = true });

            //items.Add(new SelectListItem { Text = "10", Value = "10" });

            //items.Add(new SelectListItem { Text = "15", Value = "15" });

            //items.Add(new SelectListItem { Text = "20", Value = "20" });

            //ViewBag.PageSize = items;

            int pageNumber = (page ?? 1);
            int pgSize = 5;
            switch (pageSize)
            {
                case "10": pgSize = 10;
                    break;
                case "15": pgSize = 15;
                    break;
                case "20": pgSize = 20;
                    break;
                default: pgSize = 5;
                    break;
            }
            return View(products.ToPagedList(pageNumber, pgSize));
        }

        // GET: Products/Details/5
        [AllowAnonymous]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,Name,ReleaseDate,Quantity,Price,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,Name,ReleaseDate,Quantity,Price,CategoryId")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryId = new SelectList(db.Categories, "CategoryId", "CategoryName", product.CategoryId);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
