using System.ComponentModel.DataAnnotations;

namespace KoolStuff.Models
{
    public class EmailStore
    {
        internal readonly object userName;

        [Key]
        public int Id { get; set; }
        public string? UserName { get; set; }

        public string? Email { get; set; }
    }
}
