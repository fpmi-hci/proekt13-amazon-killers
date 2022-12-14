using AmazonKillers.Catalogue.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using System.Net;

namespace AmazonKillers.Catalogue.Api.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("[controller]")]
    //[RequiredScope(RequiredScopesConfigurationKey = "AzureAd:Scopes")]
    public class CatalogueController : ControllerBase
    {
        private readonly CatalogueContext _context;

        public CatalogueController(
            CatalogueContext context)
        {
            _context = context;
        }





        //BOOKS








        [HttpGet]
        [Route("books")]
        [ProducesResponseType(typeof(IEnumerable<Book>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> BooksAsync(
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageIndex = 0,
            [FromQuery] int[] categoryIds = null,
            [FromQuery] int[] authorIds = null,
            [FromQuery] double minPrice = 0,
            [FromQuery] double maxPrice = double.MaxValue,
            [FromQuery] int minPages = 0,
            [FromQuery] int maxPages = int.MaxValue,
            [FromQuery] int[] publisherIds = null,
            [FromQuery] CoverStyle[] cs = null,
            [FromQuery] AvailabilityStatus[] avs = null,
            [FromQuery] string search = "",
            [FromQuery] string sortingBy = "name",
            [FromQuery] bool isDescending = false)
        {
            //var totalItems = await _context.Books.LongCountAsync();
            var booksOnPage = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Categories)
                .Include(b => b.Comments)
                .Where(b => b.Name.Contains(search) || b.Annotation.Contains(search))
                .Where(b => b.Pages >= minPages && b.Pages <= maxPages)
                .Where(b => b.Price >= minPrice && b.Price <= maxPrice)
                .Where(b => cs == null || cs.Length == 0 || cs.Contains(b.CoverStyle))
                .Where(b => avs == null || avs.Length == 0 || avs.Contains(b.Availability))
                .Where(b => publisherIds == null || publisherIds.Length == 0 || publisherIds.Contains(b.PublisherId))
                .Where(b => authorIds == null || authorIds.Length == 0 || b.Authors.Any(a => authorIds.Contains(a.Id)))
                .Where(b => categoryIds == null || categoryIds.Length == 0 || b.Categories.Any(c => categoryIds.Contains(c.Id)))
                //.OrderByDescending(b => categoryIds.Intersect(b.Categories.Select(a => a.Id)).Count())
                //.OrderByDescending(b => authorIds.Intersect(b.Authors.Select(a => a.Id)).Count())
                .Skip(pageSize * pageIndex)
                .Take(pageSize)
                .ToListAsync();

            switch(sortingBy)
            {
                case "name":
                    if (isDescending)
                    {
                        booksOnPage.OrderByDescending(b => b.Name);
                    }
                    else
                    {
                        booksOnPage.OrderBy(b => b.Name);
                    }
                    break;
                case "price":
                    if (isDescending)
                    {
                        booksOnPage.OrderByDescending(b => b.Price);
                    }
                    else
                    {
                        booksOnPage.OrderBy(b => b.Price);
                    }
                    break;
                case "pages":
                    if (isDescending)
                    {
                        booksOnPage.OrderByDescending(b => b.Pages);
                    }
                    else
                    {
                        booksOnPage.OrderBy(b => b.Pages);
                    }
                    break;
                case "year":
                    if (isDescending)
                    {
                        booksOnPage.OrderByDescending(b => b.PublishingYear);
                    }
                    else
                    {
                        booksOnPage.OrderBy(b => b.PublishingYear);
                    }
                    break;
                case "rating":
                    if (isDescending)
                    {
                        booksOnPage.OrderByDescending(b => b.Comments.Select(c => c.Rating).Average());
                    }
                    else
                    {
                        booksOnPage.OrderBy(b => b.Comments.Select(c => c.Rating).Average());
                    }
                    break;
            }

            return Ok(booksOnPage);
        }

        [HttpGet]
        [Route("books/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Book), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> BookByIdAsync(int id)
        {
            var book = await _context.Books
                .Include(b => b.Authors)
                .Include(b => b.Categories)
                .Include(b => b.Comments)
                .FirstOrDefaultAsync(b => b.Id == id);
            return Ok(book);
        }

        [HttpPost]
        [Route("books")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateBookAsync(
            [FromBody] Book book)
        {
            _context.Books.Add(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(BookByIdAsync), new { id = book.Id }, null);
        }

        [HttpDelete]
        [Route("books/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteBookAsync(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }
            _context.Books.Remove(book);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        [Route("books")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateBookAsync([FromBody] Book bookToUpdate)
        {
            var book = await _context.Books.SingleOrDefaultAsync(b => b.Id == bookToUpdate.Id);
            if (book == null)
            {
                return NotFound(new { Message = $"Book with id {bookToUpdate.Id} not found." });
            }
            book = bookToUpdate;
            _context.Books.Update(book);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(BookByIdAsync), new { id = bookToUpdate.Id }, null);
        }






        //AUTHORS







        [HttpGet]
        [Route("authors/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Author), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AuthorByIdAsync(int id)
        {
            var author = await _context.Authors
                .Include(a => a.Books)
                .FirstOrDefaultAsync(a => a.Id == id);
            return Ok(author);
        }

        [HttpGet]
        [Route("authors")]
        [ProducesResponseType(typeof(IEnumerable<Author>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> AuthorsAsync(int id)
        {
            var authors = await _context.Authors
                .Include(a => a.Books)
                .ToListAsync();
            return Ok(authors);
        }

        [HttpPost]
        [Route("authors")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAuthorAsync(
            [FromBody] Author author)
        {
            _context.Authors.Add(author);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AuthorByIdAsync), new { id = author.Id }, null);
        }

        [HttpDelete]
        [Route("authors/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteAuthorAsync(int id)
        {
            var author = _context.Authors.SingleOrDefault(a => a.Id == id);
            if (author == null)
            {
                return NotFound();
            }
            _context.Authors.Remove(author);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        [Route("authors")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateAuthorAsync([FromBody] Author authorToUpdate)
        {
            var author = await _context.Authors.SingleOrDefaultAsync(a => a.Id == authorToUpdate.Id);
            if (author == null)
            {
                return NotFound(new { Message = $"Author with id {authorToUpdate.Id} not found." });
            }
            author = authorToUpdate;
            _context.Authors.Update(author);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(AuthorByIdAsync), new { id = authorToUpdate.Id }, null);
        }




        //CATEGORIES




        [HttpGet]
        [Route("categories/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Category), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CategoryByIdAsync(int id)
        {
            var category = await _context.Categories
                .Include(c => c.Books)
                .FirstOrDefaultAsync(c => c.Id == id);
            return Ok(category);
        }

        [HttpPost]
        [Route("categories")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateCategoryAsync(
            [FromBody] Category category)
        {
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CategoryByIdAsync), new { id = category.Id }, null);
        }

        [HttpDelete]
        [Route("categories/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteCategoryAsync(int id)
        {
            var category = _context.Categories.SingleOrDefault(c => c.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        [Route("categories")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdateCategoryAsync([FromBody] Category categoryToUpdate)
        {
            var category = await _context.Categories.SingleOrDefaultAsync(a => a.Id == categoryToUpdate.Id);
            if (category == null)
            {
                return NotFound(new { Message = $"Category with id {categoryToUpdate.Id} not found." });
            }
            category = categoryToUpdate;
            _context.Categories.Update(category);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CategoryByIdAsync), new { id = categoryToUpdate.Id }, null);
        }

        [HttpGet]
        [Route("categories")]
        [ProducesResponseType(typeof(IEnumerable<Category>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CategoriesAsync()
        {
            var categories = await _context.Categories
                .ToListAsync();
            return Ok(categories);
        }
    }
}