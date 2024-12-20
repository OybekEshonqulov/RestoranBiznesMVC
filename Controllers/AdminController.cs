using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestoranBoshqaruvi.context;
using RestoranBoshqaruvi.Models;

namespace RestoranBoshqaruvi.Controllers
{
    // Controllers/AdminController.cs
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Barcha buyurtmalarni ko'rsatish
        public async Task<IActionResult> Orders()
        {
            var orders = await _context.Orders.ToListAsync();
            return View(orders);
        }

        // Buyurtma holatini yangilash
        [HttpPost]
        public async Task<IActionResult> UpdateOrderStatus(int orderId, OrderStatus status)
        {
            var order = await _context.Orders.FindAsync(orderId);
            if (order != null)
            {
                order.Status = status;
                _context.Update(order);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Orders));
            }
            return NotFound();
        }
        public async Task<IActionResult> ManageUsers()
        {
            var users = await _context.Users.ToListAsync(); // Barcha foydalanuvchilarni olish
            return View(users);
        }

        public async Task<IActionResult> CreateUser(Users newUser)
        {
            if (newUser.Name!=null)
            {
                Random random = new Random();
                int randomId = random.Next(1000, 9999);
                newUser.UserId=randomId;
                newUser.Address = "navoiy";
                _context.Users.Add(newUser);
                await _context.SaveChangesAsync();

                // Boshqaruv paneliga qaytish
                return RedirectToAction(nameof(ManageUsers));
            }

            // Agar ma'lumotlar noto'g'ri bo'lsa, formani qaytarib berish
            return View(newUser);
        }
        // Foydalanuvchi ma'lumotlarini tahrirlash
        // AdminController.cs
        // GET: Foydalanuvchini tahrirlash uchun
        public async Task<IActionResult> UpdateUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Foydalanuvchi yangilash
        [HttpPost]
        [ValidateAntiForgeryToken] // CSRF himoya
        public async Task<IActionResult> UpdateUser(Users updatedUser)
        {
            if (updatedUser.Name!=null)
            {
                var user = await _context.Users.FindAsync(updatedUser.UserId);
                if (user == null)
                {
                    return NotFound();
                }

                // Foydalanuvchi ma'lumotlarini yangilash
                user.Name = updatedUser.Name;
                user.Email = updatedUser.Email;
                user.Role = updatedUser.Role;

                _context.Update(user);
                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(ManageUsers)); // Foydalanuvchilarni ko'rsatish sahifasiga qaytish
            }
            return View(updatedUser); // Agar forma xato bo'lsa, qaytadan ko'rsatish
        }


        // Foydalanuvchini o'chirish
        [HttpPost]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(ManageUsers));
        }
    }

}
