using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyNews.Core.Data;
using MyNews.Core.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace MyNews.Admin.Controllers
{
    public class NewsController : Controller
    {
        private readonly AplicationDbContext _context;
        private readonly IHostingEnvironment hostingEnvironment;

        public NewsController(AplicationDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            this.hostingEnvironment = hostingEnvironment;
        }

        // GET: News
        public async Task<IActionResult> Index()
        {
            var aplicationDbContext = _context.News.Include(n => n.Category);
            return View(await aplicationDbContext.ToListAsync());
        }

        // GET: News/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // GET: News/Create
        public IActionResult Create()
        {

            News n = new News();
            n.CreateDate = DateTime.Now;
            n.CreatedBy = User.Identity.Name;
            n.UpdateDate = DateTime.Now;
            n.UpdatedBy = User.Identity.Name;


            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name");
            return View(n);
        }

        // POST: News/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Photo,PublishDate,IsPublished,CreateDate,CreatedBy,UpdateDate,UpdatedBy,CategoryId")] News news,IFormFile upload)
        {
            //dosya uzantısı ıcın gecerliik denetimi
            if(upload !=null && !IsExtensionValid(upload))
            {
                ModelState.AddModelError("Photo", "Dosya uzantısı jpg, jpeg ,gif , png olmalıdır.");            
            }
            else if (upload  ==null )
            {
                ModelState.AddModelError("Photo", "Resim yüklemeniz gerekmektedir");

            }

            if (ModelState.IsValid)
            {
                news.CreateDate = DateTime.Now;
                news.CreatedBy = User.Identity.Name;
                news.UpdateDate = DateTime.Now;
                news.UpdatedBy = User.Identity.Name;
                news.PublishDate = DateTime.Now;

                //dosya yuklemesi
                if (upload != null && upload.Length > 0 && IsExtensionValid(upload))
                {
                    news.Photo = await UploadFileAsync(upload);
                }

                    _context.Add(news);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", news.CategoryId);
            return View(news);
        }

        // GET: News/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News.SingleOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", news.CategoryId);
            return View(news);
        }

        // POST: News/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Photo,PublishDate,IsPublished,CreateDate,CreatedBy,UpdateDate,UpdatedBy,CategoryId")] News news,IFormFile upload)
        {
            if (id != news.Id)
            {
                return NotFound();
            }

            //dosya uzantısı ıcın gecerliik denetimi
            if (upload != null && !IsExtensionValid(upload))
            {
                ModelState.AddModelError("Photo", "Dosya uzantısı jpg, jpeg ,gif , png olmalıdır.");
            }
            else if (upload == null && news.Photo==null)//eger resim yüklenmisse bir daha sectirmiyor
            {
                ModelState.AddModelError("Photo", "Resim yüklemeniz gerekmektedir");

            }




            if (ModelState.IsValid)
            {
                try
                {

                    news.UpdateDate = DateTime.Now;
                news.UpdatedBy = User.Identity.Name;

                    //dosya yuklemesi
                    if (upload != null && upload.Length > 0 && IsExtensionValid(upload))
                    {
                        news.Photo = await UploadFileAsync(upload);
                    }


                    _context.Update(news);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NewsExists(news.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(_context.Categories, "Id", "Name", news.CategoryId);
            return View(news);
        }

        // GET: News/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var news = await _context.News
                .Include(n => n.Category)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (news == null)
            {
                return NotFound();
            }

            return View(news);
        }

        // POST: News/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var news = await _context.News.SingleOrDefaultAsync(m => m.Id == id);
            _context.News.Remove(news);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NewsExists(int id)
        {
            return _context.News.Any(e => e.Id == id);
        }

        //upload edilecek dosyanın geçerli mi onu kontrol eder.
        private bool IsExtensionValid(IFormFile upload)
        {
            if (upload !=null)
            {
                var allowedExtension = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
                var extension = Path.GetExtension(upload.FileName).ToLowerInvariant();
                return allowedExtension.Contains(extension);
            }
            return false;
        }


        private async Task<string> UploadFileAsync(IFormFile upload)
        {
            //dosya upload
            if (upload != null && upload.Length > 0 && IsExtensionValid(upload))
            {
                var fileName = upload.FileName;
                var extension = Path.GetExtension(fileName);
                var uploadLocation = Path.Combine(hostingEnvironment.WebRootPath, "uploads");



                //yoksa olustur kodu
                if (!Directory.Exists(uploadLocation))
                {
                    Directory.CreateDirectory(uploadLocation);
                }

                uploadLocation += "/" + fileName;

                using (var stream = new FileStream(uploadLocation, FileMode.Create))
                {
                    await upload.CopyToAsync(stream);
                }
                return  fileName;
            }
            return null;
        }
    }
}
