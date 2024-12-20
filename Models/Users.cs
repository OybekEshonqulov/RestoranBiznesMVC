namespace RestoranBoshqaruvi.Models
{
    public class Users
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public Role Role { get; set; }  // User role: Admin, Waiter, Chef
    }

    
}
