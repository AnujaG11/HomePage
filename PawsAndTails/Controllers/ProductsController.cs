using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HomePage.Data;
using HomePage.Models;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Data.SqlClient;
using MySql.Data.MySqlClient;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Identity;

namespace HomePage.Controllers
{
    public class ProductsController : Controller
    {
        private readonly MyDbContext _context;

        public ProductsController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Products
        public async Task<IActionResult> Index()
        {

              return _context.Products != null ? 

                          View(await _context.Products.ToListAsync()) :
                          Problem("Entity set 'MyDbContext.Products'  is null.");
        }

        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProductName,Img,Category,Price,Stock,Breed,Age,Origination,Color")] Product product)
        {
            if (ModelState.IsValid)
            {
                _context.Add(product);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProductName,Img,Category,Price,Stock,Breed,Age,Origination,Color")] Product product)
        {
            if (id != product.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(product);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.Id))
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
            return View(product);
        }

        // GET: Products/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products
                .FirstOrDefaultAsync(m => m.Id == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Products == null)
            {
                return Problem("Entity set 'MyDbContext.Products'  is null.");
            }
            var product = await _context.Products.FindAsync(id);
            if (product != null)
            {
                _context.Products.Remove(product);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProductExists(int id)
        {
          return (_context.Products?.Any(e => e.Id == id)).GetValueOrDefault();
        }


        public IActionResult ProductPage(string category)
        {
            List<Product> dogsList = new List<Product>(); 
            foreach(var product in _context.Products)
            {
                if (product.Category=="Dogs")
                {
                    dogsList.Add( product);
                }
            }
            return dogsList != null ?

                        View(dogsList) :
                        Problem("Entity set 'MyDbContext.Products'  is null.");
        }

        public async Task<IActionResult> PCats(string category)
        {
            List<Product> catsList = new List<Product>();
            foreach (var product in _context.Products)
            {
                if (product.Category == "Cats")
                {
                    catsList.Add(product);
                }
            }
            return _context.Products != null ?
                        View(catsList) :
                        Problem("Entity set 'MyDbContext.Products'  is null.");
        }

        public async Task<IActionResult> PFishes(string category)
        {

            List<Product> fishesList = new List<Product>();
            foreach (var product in _context.Products)
            {
                if (product.Category == "Fishes")
                {
                    fishesList.Add(product);
                }
            }
            return _context.Products != null ?
                        View(fishesList) :
                        Problem("Entity set 'MyDbContext.Products'  is null.");
        }

        public async Task<IActionResult> PBirds(string category)
        {

            List<Product> birdsList = new List<Product>();
            foreach (var product in _context.Products)
            {
                if (product.Category == "Birds")
                {
                    birdsList.Add(product);
                }
            }
            return _context.Products != null ?
                        View(birdsList) :
                        Problem("Entity set 'MyDbContext.Products'  is null.");
        }

        public async Task<IActionResult> AddToCart(int? id)
        {
            if (id == null || _context.Products == null)
            {
                return NotFound();
            }

            var product = await _context.Products.FindAsync(id);
            if (product == null)
            {
                return NotFound();
            }
            MySqlConnection cn = new MySqlConnection();
            cn.ConnectionString = @"server=localhost; port=3306; database=p_and_t; user=kshitij; password=xls; ";
            cn.Open();
            string email = User.Identity.Name;
            await Console.Out.WriteLineAsync("Test....................");
            MySqlCommand cmd = cn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = $"Insert into cart (email_id, quantity, Id) values ({email}, 2, 1)";
            cmd.ExecuteNonQuery();
            ViewBag.Message = "Passed";
            return RedirectToAction("Index", "Carts");
        
        }
    }
}
