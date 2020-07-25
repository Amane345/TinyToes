using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TinyToes.Models;
using TinyToes.ViewModels;

namespace TinyToes.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private readonly AtDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;
        public AdminController(AtDbContext context, IWebHostEnvironment hostEnvironment)
        {
            this._hostEnvironment = hostEnvironment;
            _context = context;
        }

        // GET: Clothes1
        public async Task<IActionResult> Index()
        {
            var atDbContext = _context.Cloth.Include(c => c.Category);
            return View(await atDbContext.ToListAsync());
        }

        // GET: Clothes1/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothes = await _context.Cloth
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.ClothesId == id);
            if (clothes == null)
            {
                return NotFound();
            }

            return View(clothes);
        }

        // GET: Clothes1/Create
        public IActionResult Create()
        {
            ViewBag.Categorys = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View();
        }

        // POST: Clothes1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ClothesId,Price,ImageUrl,DataFiles,IsOnSale,IsInStock,CategoryId")]Clothes model)
        {
            if (ModelState.IsValid)
            {
               
               
                //Save image to wwwroot/image
                string wwwRootPath = _hostEnvironment.WebRootPath;
                string fileName = Path.GetFileNameWithoutExtension(model.DataFiles.FileName);
                string extension = Path.GetExtension(model.DataFiles.FileName);
                model.ImageUrl=fileName = fileName + DateTime.Now.ToString("yymmssfff") + extension;
                string path = Path.Combine(wwwRootPath + "/Images/", fileName);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await model.DataFiles.CopyToAsync(fileStream);
                }
                
                //Insert record
                _context.Add(model);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }   
        
            return View(model);
        }
       
    

    // GET: Clothes1/Edit/5
    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothes = await _context.Cloth.FindAsync(id);
            if (clothes == null)
            {
                return NotFound();
            }
            ViewBag.Categorys = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(clothes);
        }

        // POST: Clothes1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ClothesId,Price,ImageUrl,DataFiles,IsOnSale,IsInStock,CategoryId")] Clothes clothes)
        {
            if (id != clothes.ClothesId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(clothes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClothesExists(clothes.ClothesId))
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
            ViewBag.Categorys = new SelectList(_context.Categories, "CategoryId", "CategoryName");
            return View(clothes);
        }

        // GET: Clothes1/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var clothes = await _context.Cloth
                .Include(c => c.Category)
                .FirstOrDefaultAsync(m => m.ClothesId == id);
            if (clothes == null)
            {
                return NotFound();
            }

            return View(clothes);
        }

        // POST: Clothes1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var clothes = await _context.Cloth.FindAsync(id);

            //delete image from wwwroot/image
            var imagePath = Path.Combine(_hostEnvironment.WebRootPath, "Images", clothes.ImageUrl);
            if (System.IO.File.Exists(imagePath))
                System.IO.File.Delete(imagePath);
            //delete the record
            _context.Cloth.Remove(clothes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClothesExists(int id)
        {
            return _context.Cloth.Any(e => e.ClothesId == id);
        }
    }
}
