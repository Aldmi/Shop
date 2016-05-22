using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Domain;
using Domain.Entities;
using Domain.Interfaces;
using WebUI.Migrations;
using WebUI.Models;
using WebUI.Security;

namespace WebUI.Controllers
{
    [Admin]
    public class ProductsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductsController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: Products
        public async Task<ActionResult> Index()
        {
            return View(await _unitOfWork.Products.Get().Where(p => true).ToListAsync());
        }

        // GET: Products/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = await _unitOfWork.Products.GetAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,UnitPrice")] Product product, HttpPostedFileBase[] fileUpload)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    //Добавить файл на диск
                    var path = AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    var file = fileUpload.FirstOrDefault();
                    var filename = Path.GetFileName(file?.FileName);
                    var filePath = filename != null ? Path.Combine(path, filename) : null;
                    if (filePath != null)
                    {
                        file.SaveAs(filePath);
                        product.PictureRef = "/UploadedFiles/" + filename;
                    }


                    //Добавить в БД
                    _unitOfWork.Products.Insert(product);
                    await _unitOfWork.SaveAsync();

                }
                catch (IOException)
                {
                    return View(product);
                }
                catch (DataException)
                {
                    //Удаление файла с диска
                    if (!string.IsNullOrEmpty(product.PictureRef))
                    {
                        var filePath = AppDomain.CurrentDomain.BaseDirectory + product.PictureRef;      
                        await DeleteFileAsync(filePath);
                    }
                }

                return RedirectToAction("Index");
            }

            return View(product);
        }



        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var product = await _unitOfWork.Products.GetAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,UnitPrice")] Product product, HttpPostedFileBase[] fileUpload)
        {
            if (ModelState.IsValid)
            {
                Product oldProduct = null;//_unitOfWork.Products.Get(product.Id);

                try
                {
                    //Добавить файл на диск
                    var path = AppDomain.CurrentDomain.BaseDirectory + "UploadedFiles/";
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    var file = fileUpload.FirstOrDefault();
                    var filename = Path.GetFileName(file?.FileName);
                    var filePath = filename != null ? Path.Combine(path, filename) : null;
                    if (filePath != null)
                    {
                        file.SaveAs(filePath);
                        product.PictureRef = "/UploadedFiles/" + filename;
                    }

                    //Обновить в БД
                    _unitOfWork.Products.Update(product);
                    await _unitOfWork.SaveAsync();

                }
                catch (IOException)
                {
                    return View();
                }
                catch (DataException)
                {
                    //Удаление файла с диска
                    if (!string.IsNullOrEmpty(product.PictureRef))
                    {
                        var filePath = AppDomain.CurrentDomain.BaseDirectory + product.PictureRef;
                        await DeleteFileAsync(filePath);
                    }
                }
        
                //Удаление старого  файла с диска
                if (!string.IsNullOrEmpty(oldProduct?.PictureRef))
                {
                    var filePath = AppDomain.CurrentDomain.BaseDirectory + oldProduct.PictureRef;
                    await DeleteFileAsync(filePath);
                }

                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = await _unitOfWork.Products.GetAsync(id.Value);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Product product = await _unitOfWork.Products.GetAsync(id);
            try
            {
                //Удалние из БД
                _unitOfWork.Products.Remove(product);
                await _unitOfWork.SaveAsync();

                //Удаление файла с диска
                if (!string.IsNullOrEmpty(product.PictureRef))
                {
                    var filePath = AppDomain.CurrentDomain.BaseDirectory + product.PictureRef;// "D:\\Git\\Shop\\src\\WebUI\\UploadedFiles/saw.png"        
                    await DeleteFileAsync(filePath);
                }
            }
            catch (DataException)
            {
                return View();
            }
            catch (IOException)
            {
                //Добавить в БД
                _unitOfWork.Products.Insert(product);
                await _unitOfWork.SaveAsync();
            }

            return RedirectToAction("Index");
        }




        #region Methods

        public async Task DeleteFileAsync(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                await Task.Factory.StartNew(() => { System.IO.File.Delete(filePath); });
            }
        }

        #endregion




        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _unitOfWork.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
