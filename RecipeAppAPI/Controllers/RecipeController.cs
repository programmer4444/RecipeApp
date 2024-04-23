using Microsoft.AspNetCore.Mvc;
using RecipeAppAPI.Models;
using System.Linq;

namespace RecipeAppAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecipeController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public RecipeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/Recipe
        [HttpGet]
        public IActionResult GetAllRecipes()
        {
            var recipes = _dbContext.Recipes.ToList();
            return Ok(recipes);
        }
    }
}
