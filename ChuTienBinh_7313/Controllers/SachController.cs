using ChuTienBinh_7313.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.Mvc;
using System.Globalization;

namespace ChuTienBinh_7313.Controllers
{
    public class SachController : Controller
    {
        // GET: Sach
        MyDataDataContext data = new MyDataDataContext();
        public ActionResult Index(string SearchString="")
        {
            //string searchStringNormalized = RemoveDiacritics(searchString);
            if (SearchString != "")
            {
                var sp = data.Saches.Where(s => s.tensach.ToUpper().Contains(SearchString.ToUpper()));
                return View(sp.ToList());
            }
            else
            {
                var all_theloai = from tt in data.Saches select tt;
                return View(all_theloai);
            }          
        }



        public ActionResult Detail(int id)
        {
            var D_theloai = data.Saches.Where(m => m.masach == id).First();
            return View(D_theloai);
        }
        public ActionResult Edit(int id)
        {
            var E_category = data.Saches.First(m => m.masach == id);
            return View(E_category);
        }
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            var theloai = data.Saches.First(m => m.masach == id);
            var E_tenloai = collection["tensach"];
            theloai.masach = id;
            if (string.IsNullOrEmpty(E_tenloai))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                theloai.tensach = E_tenloai;
                UpdateModel(theloai);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Edit(id);
        }
        public ActionResult Delete(int id)
        {
            var D_theloai = data.Saches.First(m => m.masach == id);
            return View(D_theloai);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            var D_theloai = data.Saches.Where(m => m.masach == id).First();

            data.Saches.DeleteOnSubmit(D_theloai);
            data.SubmitChanges();
            return RedirectToAction("Index");
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(FormCollection collection, Sach tl)
        {
            var ten = collection["tensach"];
            var maloai = collection["maloai"];
            if (string.IsNullOrEmpty(ten))
            {
                ViewData["Error"] = "Don't empty!";
            }
            else
            {
                tl.tensach = ten;
               
                data.Saches.InsertOnSubmit(tl);
                data.SubmitChanges();
                return RedirectToAction("Index");
            }
            return this.Create();
        }
    }
}