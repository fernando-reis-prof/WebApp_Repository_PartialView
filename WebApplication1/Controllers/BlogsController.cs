#nullable disable
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.Repositories;

namespace WebApplication1.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IRepository _repository;

        public BlogsController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Blogs
        public async Task<IActionResult> Index()
        {
            return View(await _repository.GetAll());
        }

        // GET: Blogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();
            var blog = await _repository.GetById((int)id);
            if (blog == null) return NotFound();
            
            return View(blog);
        }

        // GET: Blogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Blogs/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Blog blog)
        {
            if (ModelState.IsValid)
            {
                if (_repository.EntityExists(blog.Id)) return BadRequest("Id do Blog já existe.");
                _repository.Create(blog);
                await _repository.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(blog);
        }

        // GET: Blogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null) return NotFound();
            var blog = await _repository.GetById((int)id);
            if (blog == null) return NotFound();

            return View(blog);
        }

        // POST: Blogs/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Blog blog)
        {
            if (id != blog.Id) return NotFound();

            if (ModelState.IsValid)
            {
                if (!_repository.EntityExists(blog.Id)) return NotFound();
                _repository.Update(blog);
                await _repository.SaveChangesAsync();
            }
            return View(blog);
        }

        // GET: Blogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
            var blog = await _repository.GetById((int)id);
            if (blog == null) return NotFound();
 
            return View(blog);
        }

        // POST: Blogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var blog = await _repository.GetById(id);
            if (blog == null) return NotFound();

            _repository.Delete(blog);
            await _repository.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}

