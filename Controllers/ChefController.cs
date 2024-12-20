using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestoranBoshqaruvi.context;
using RestoranBoshqaruvi.Models;
using System.Linq;
using System.Threading.Tasks;

namespace RestoranBoshqaruvi.Controllers
{
    // Controllers/ChefController.cs
    public class ChefController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ChefController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Oshpazga buyurtmalarni ko'rsatish (Pending holatidagi buyurtmalar)
        public async Task<IActionResult> ViewOrders()
        {
            // Faqat oshpaz uchun qiziqarli buyurtmalarni ko'rish
            var orders = await _context.Orders
                .OrderByDescending(o => o.OrderDate) // Buyurtmalarni oxirgi kiritilgan tartibda
                .ToListAsync();
            return View(orders);
        }



        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status; // Statusni yangilash
                _context.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(ViewOrders)); // Sahifani qayta yuklash
            }
            return NotFound(); // Buyurtma topilmagan holatda
        }



    }
}
