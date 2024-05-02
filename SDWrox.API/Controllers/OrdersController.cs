using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SDWrox.API.Core;
using SDWrox.DataModel.Models;
using SDWrox.Entity;

namespace SDWrox.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrdersController : ControllerBase
    {
        private readonly SdwroxModelContext _context;

        public OrdersController(SdwroxModelContext context)
        {
            _context = context;
        }

        // GET: api/Orders
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CustomOrder>>> GetTbOrders()
        {
            var result = (from o in _context.TbOrders
                          join od in _context.TbOrderDetails on o.Id equals od.Id
                          select new CustomOrder
                          {
                              Id = o.Id,
                              Count = od.Count,
                              Date = o.Date,
                              DeliveryDate = o.DeliveryDate,
                              Description = o.Description,
                              Discount = od.Discount,
                              Number = o.Number,
                              Owner = o.Owner,
                              Price = od.Price,
                              ProductId = od.ProductId,
                              ProductName = od.Product.Title,
                              ShopId = o.ShopId,
                              ShopName = o.Shop.Name,
                              Title = o.Title,
                          }).ToListAsync();

            return Ok(await result);
        }

        // GET: api/Orders/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CustomOrder>> GetOrder(int id)
        {
            var result = (from o in _context.TbOrders
                          join od in _context.TbOrderDetails on o.Id equals od.Id
                          where o.Id == id
                          select new CustomOrder
                          {
                              Id = o.Id,
                              Count = od.Count,
                              Date = o.Date,
                              DeliveryDate = o.DeliveryDate,
                              Description = o.Description,
                              Discount = od.Discount,
                              Number = o.Number,
                              Owner = o.Owner,
                              Price = od.Price,
                              ProductId = od.ProductId,
                              ProductName = od.Product.Title,
                              ShopId = o.ShopId,
                              ShopName = o.Shop.Name,
                              Title = o.Title,
                          }).FirstOrDefaultAsync();

            var fetchContent = await result;

            if (fetchContent == null)
            {
                return NotFound();
            }

            return Ok(fetchContent);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutOrder(CustomOrder order)
        {
            if (!OrderExists(order.Id))
            {
                return BadRequest();
            }

            var uOrder = new TbOrder { 
                Number = order.Number,
                Description = order.Description,
                Date = order.Date,
                DeliveryDate= order.DeliveryDate,
                Owner = order.Owner,
                Title = order.Title,
                ShopId = order.ShopId
            };
            var uOrderDetail = new TbOrderDetail { 
                Count = order.Count,
                Discount= order.Discount,
                OrderId = order.Id,
                Price = order.Price,
                ProductId = order.ProductId
            };

            var updatableOrder = DetectOrderChange(uOrder);

            if (updatableOrder != null)
                _context.Entry(updatableOrder).State = EntityState.Modified;

            var updatableDetail = DetectOrderDetailChange(uOrderDetail);

            if (updatableDetail != null)
                _context.Entry(updatableDetail).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!OrderExists(order.Id))
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

        private TbOrder? DetectOrderChange(TbOrder newOrder)
        {
            var oldOrder = _context.TbOrders.Find(newOrder.Id);
            var modified = false;

            if (oldOrder != null)
            {
                PropertyInfo[] properties = typeof(TbOrder).GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    object existingValue = property.GetValue(oldOrder);
                    object updatedValue = property.GetValue(newOrder);

                    if (!existingValue.Equals(updatedValue))
                    {
                        property.SetValue(oldOrder, updatedValue, null);
                        modified = true;

                    }
                }
            }

            if (modified)
                return oldOrder;
            else
                return null;
        }

        private TbOrderDetail? DetectOrderDetailChange(TbOrderDetail newOrderDetail)
        {
            var oldOrderDetail = _context.TbOrderDetails.FirstOrDefault(m => m.OrderId == newOrderDetail.OrderId);
            var modified = false;

            if (oldOrderDetail != null)
            {
                PropertyInfo[] properties = typeof(TbOrderDetail).GetProperties();

                foreach (PropertyInfo property in properties)
                {
                    object existingValue = property.GetValue(oldOrderDetail);
                    object updatedValue = property.GetValue(newOrderDetail);

                    if (property.Name != "Id" && existingValue != null && updatedValue != null && !existingValue.Equals(updatedValue))
                    {
                        property.SetValue(oldOrderDetail, updatedValue, null);
                        modified = true;

                    }
                }
            }

            if (modified)
                return oldOrderDetail;
            else
                return null;
        }

        // POST: api/Orders
        [HttpPost]
        public async Task<ActionResult<TbOrder>> PostOrder(CustomOrder order)
        {
            if (!ShopExists(order.ShopId))
            {
                return BadRequest();
            }

            var nOrder = new TbOrder
            {
                Number = order.Number,
                DeliveryDate = order.DeliveryDate,
                Date = order.Date,
                Description = order.Description,
                Owner = order.Owner,
                Title = order.Title,
                ShopId = order.ShopId
            };
            _context.TbOrders.Add(nOrder);
            _context.SaveChanges();

            if (!productExists(order.ProductId))
            {
                return BadRequest();
            }

            if (nOrder.Id != 0)
            {
                _context.TbOrderDetails.Add(new TbOrderDetail
                {
                    OrderId = nOrder.Id,
                    Count = order.Count,
                    Discount = order.Discount,
                    Price = order.Price,
                    ProductId = order.ProductId,
                });

                await _context.SaveChangesAsync();
            }

            return CreatedAtAction("GetOrder", new { id = order.Id }, order);
        }

        // DELETE: api/Orders/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var tbOrder = await _context.TbOrders.FindAsync(id);
            if (tbOrder == null)
            {
                return NotFound();
            }
            var removableItems = _context.TbOrderDetails.Where(n=>n.OrderId == id).ToList();
            if (removableItems.Any())
                _context.TbOrderDetails.RemoveRange(removableItems);

            _context.TbOrders.Remove(tbOrder);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool OrderExists(int id)
        {
            return _context.TbOrders.Any(e => e.Id == id);
        }

        private bool ShopExists(int shopId)
        {
            return _context.TbShops.Any(e => e.Id == shopId);
        }

        private bool productExists(int productId)
        {
            return _context.TbProducts.Any(e => e.Id == productId);
        }
    }
}
