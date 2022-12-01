using AmazonKillers.News.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web.Resource;
using System.Net;

namespace AmazonKillers.News.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly NewsContext _context;

        public NewsController(
            NewsContext context)
        {
            _context = context;
        }

        //PUBLISHERS




        [HttpGet]
        [Route("publishers/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Publisher), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> PublisherByIdAsync(int id)
        {
            var publisher = await _context.Publishers
                .FirstOrDefaultAsync(p => p.Id == id);
            return Ok(publisher);
        }

        [HttpPost]
        [Route("publishers")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreatePublisherAsync(
            [FromBody] Publisher publisher)
        {
            _context.Publishers.Add(publisher);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PublisherByIdAsync), new { id = publisher.Id }, null);
        }

        [HttpDelete]
        [Route("publishers/{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeletePublisherAsync(int id)
        {
            var publisher = _context.Publishers.SingleOrDefault(p => p.Id == id);
            if (publisher == null)
            {
                return NotFound();
            }
            _context.Publishers.Remove(publisher);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        [Route("publishers")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<ActionResult> UpdatePublisherAsync([FromBody] Publisher publisherToUpdate)
        {
            var publisher = await _context.Publishers.SingleOrDefaultAsync(p => p.Id == publisherToUpdate.Id);
            if (publisher == null)
            {
                return NotFound(new { Message = $"Publisher with id {publisherToUpdate.Id} not found." });
            }
            publisher = publisherToUpdate;
            _context.Publishers.Update(publisher);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(PublisherByIdAsync), new { id = publisherToUpdate.Id }, null);
        }

        [HttpGet]
        [Route("publishers")]
        [ProducesResponseType(typeof(IEnumerable<Publisher>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PublishersAsync()
        {
            var publishers = await _context.Publishers
                .ToListAsync();
            return Ok(publishers);
        }



        //SUBSCRIPTIONS




        [HttpGet]
        [Route("subscriptions/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(Subscription), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> SubscriptionByIdAsync(int id)
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.Id == id);
            return Ok(subscription);
        }

        [HttpPost]
        [Route("subscriptions")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateSubscriptionAsync(
            [FromBody] Subscription subscription)
        {
            _context.Subscriptions.Add(subscription);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(SubscriptionByIdAsync), new { id = subscription.Id }, null);
        }

        [HttpDelete]
        [Route("subscriptions/{id:int}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult> DeleteSubscriptionAsync(int id)
        {
            var subscription = _context.Subscriptions.SingleOrDefault(s => s.Id == id);
            if (subscription == null)
            {
                return NotFound();
            }
            _context.Subscriptions.Remove(subscription);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpGet]
        [Route("subscriptions")]
        [ProducesResponseType(typeof(IEnumerable<Subscription>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UserSubscriptionsAsync([FromQuery] string userId)
        {
            var subscriptions = await _context.Subscriptions.Where(s => s.SubscriberId == userId)
                .ToListAsync();
            return Ok(subscriptions);
        }


        [HttpGet]
        [Route("subscriptions")]
        [ProducesResponseType(typeof(IEnumerable<Subscription>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> PublisherSubscribersAsync([FromQuery] int publisherId)
        {
            var subscriptions = await _context.Subscriptions.Where(s => s.PublisherId == publisherId)
                .ToListAsync();
            return Ok(subscriptions);
        }
    }
}