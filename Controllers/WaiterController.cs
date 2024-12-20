using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestoranBoshqaruvi.context;
using RestoranBoshqaruvi.Models;

namespace RestoranBoshqaruvi.Controllers
{
    // Controllers/WaiterController.cs
    public class WaiterController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WaiterController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Barcha buyurtmalarni ko‘rsatish
        public async Task<IActionResult> ViewOrders()
        {
            var orders = await _context.Orders.ToListAsync();
            return View(orders);
        }

        // Yangi buyurtma qo‘shish sahifasi
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            if (order !=null)
            {
                order.Status = OrderStatus.Pending; // Yangi buyurtma boshlang‘ich holati Pending
                order.OrderDate = DateTime.UtcNow; // Joriy sana qo‘shiladi
                _context.Orders.Add(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewOrders));
            }
            return View(order);
        }

        // Yetkazib berilgan buyurtmani o‘chirish
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(ViewOrders));
        }
    }

}
