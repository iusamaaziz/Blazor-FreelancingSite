using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using JobPortalBlazor.Shared;
using Microsoft.AspNetCore.Components.Forms;

namespace JobPortalBlazor.Server.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CategoriesController : ControllerBase
	{
		private readonly JobPortalDBContext _context;

		public CategoriesController(JobPortalDBContext context)
		{
			_context = context;
		}

		// GET: api/Categories
		[HttpGet]
		public async Task<ActionResult<IEnumerable<Category>>> GetCatogories()
		{
			var categories = await _context.Catogories.ToListAsync();
			return categories;
		}
		// GET: api/Categories/5
		[HttpGet("{id}")]
		public async Task<ActionResult<Category>> GetCategory(int id)
		{
			var category = await _context.Catogories.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}
			await _context.Entry(category).Collection(g => g.Gigs).Query().Include(e=>e.Freelancer).LoadAsync();
			await _context.Entry(category).Collection(g => g.CustomOrderRequests).Query().Include(e=>e.Client).LoadAsync();

			return category;
		}

		// PUT: api/Categories/5
		[HttpPut("{id}")]
		public async Task<IActionResult> PutCategory(int id, Category category)
		{
			if (id != category.Id)
			{
				return BadRequest();
			}

			_context.Entry(category).State = EntityState.Modified;

			try
			{
				await _context.SaveChangesAsync();
			}
			catch (DbUpdateConcurrencyException)
			{
				if (!CategoryExists(id))
				{
					return NotFound();
				}
				else
				{
					throw;
				}
			}

			return NoContent();
		}

		// POST: api/Categories
		[HttpPost]
		public async Task<ActionResult<Category>> PostCategory(Category category)
		{
			_context.Catogories.Add(category);
			await _context.SaveChangesAsync();

			return CreatedAtAction("GetCategory", new { id = category.Id }, category);
		}

		// DELETE: api/Categories/5
		[HttpDelete("{id}")]
		public async Task<IActionResult> DeleteCategory(int id)
		{
			var category = await _context.Catogories.FindAsync(id);
			if (category == null)
			{
				return NotFound();
			}

			_context.Catogories.Remove(category);
			await _context.SaveChangesAsync();

			return NoContent();
		}

		private bool CategoryExists(int id)
		{
			return _context.Catogories.Any(e => e.Id == id);
		}
	}
}
