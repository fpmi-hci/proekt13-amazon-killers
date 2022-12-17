using AmazonKillers.Cart.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using System.Net;

namespace AmazonKillers.Cart.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CartController : ControllerBase
    {
        private readonly CartContext _context;

        public CartController(
            CartContext context)
        {
            _context = context;
        }


        //CART



        [HttpGet]
        [Route("cart/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(CartItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> CartItemByIdAsync(int id)
        {
            var item = await _context.CartItems
                .FirstOrDefaultAsync(c => c.Id == id);
            return Ok(item);
        }

        [HttpPost]
        [Route("cart")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddCartItemAsync(
           [FromBody] CartItem item)
        {
            var _item = _context.CartItems.SingleOrDefault(c => c.UserId == item.UserId && c.BookId == item.BookId);
            if (_item == null)
            {
                _item = item;
                _context.CartItems.Add(_item);
            }
            else
            {
                _item.Amount += item.Amount;
            }
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(CartItemByIdAsync), new { id = _item.Id }, null);
        }

        [HttpDelete]
        [Route("cart")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> RemoveCartItemAsync([FromQuery] string userId, [FromQuery] int bookId)
        {
            var item = _context.CartItems.SingleOrDefault(f => f.UserId == userId && f.BookId == bookId);
            if (item == null)
            {
                return NotFound();
            }
            item.Amount--;
            if (item.Amount <= 0)
            {
                _context.CartItems.Remove(item);
            }
            await _context.SaveChangesAsync();
            return NoContent();
        }



        [HttpGet]
        [Route("cart/user/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<CartItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserCartItemsAsync(string id)
        {
            var item = await _context.CartItems
                .Where(c => c.UserId == id)
                .ToListAsync();
            return Ok(item);
        }







        //FAVOURITES



        [HttpGet]
        [Route("favourites/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(FavouriteItem), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> FavouriteItemByIdAsync(int id)
        {
            var item = await _context.FavouriteItems
                .FirstOrDefaultAsync(f => f.Id == id);
            return Ok(item);
        }

        //[HttpGet]
        //[Route("favourites")]
        //[ProducesResponseType((int)HttpStatusCode.NotFound)]
        //[ProducesResponseType((int)HttpStatusCode.BadRequest)]
        //[ProducesResponseType(typeof(FavouriteItem), (int)HttpStatusCode.OK)]
        //public async Task<IActionResult> FavouriteItemByIdAsync([FromQuery] int bookId, [FromQuery] string userId)
        //{
        //    var item = await _context.FavouriteItems
        //        .FirstOrDefaultAsync(f => f.BookId == bookId && f.UserId == userId);
        //    return Ok(item);
        //}

        [HttpPost]
        [Route("favourites")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> AddFavouriteItemAsync(
            [FromBody] FavouriteItem item)
        {
            _context.FavouriteItems.Add(item);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(FavouriteItemByIdAsync), new { id = item.Id }, null);
        }

        [HttpDelete]
        [Route("favourites")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> RemoveFavouriteItemAsync([FromQuery] string userId, [FromQuery] int bookId)
        {
            var item = _context.FavouriteItems.SingleOrDefault(f => f.UserId == userId && f.BookId == bookId);
            if (item == null)
            {
                return NotFound();
            }
            _context.FavouriteItems.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }


        [HttpGet]
        [Route("favourites/user/{id}")]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<FavouriteItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetUserFavouriteItemsAsync(string id)
        {
            var item = await _context.FavouriteItems
                .Where(c => c.UserId == id)
                .ToListAsync();
            return Ok(item);
        }

    }
}