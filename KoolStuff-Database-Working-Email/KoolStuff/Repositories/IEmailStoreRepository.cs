using Microsoft.AspNetCore.Mvc;

namespace KoolStuff.Repositories
{
    public interface IEmailStoreRepository
    {
        Task<int> AddEmail(string userName, string email);
        Task<string> GetByEmail(string email);
        Task<IActionResult> Index();
        Task<IActionResult> SaveEmail(string userName, string email);
        Task<IActionResult> Details(int? id);
    }
}