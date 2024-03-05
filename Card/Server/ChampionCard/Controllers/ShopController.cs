using Microsoft.AspNetCore.Mvc;

namespace ChampionCard.Controllers
{
    public class ShopController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
