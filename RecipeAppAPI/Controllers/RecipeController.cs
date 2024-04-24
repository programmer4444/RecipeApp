using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using RecipeAppAPI.Models;

namespace RecipeAppAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RecipeController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public RecipeController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // GET: api/recipes
        [HttpGet]
        public IActionResult GetAllRecipes()
        {
            var recipes = _dbContext.Recipes.ToList();
            return Ok(recipes);
        }

        // GET: api/recipes/{id}
        [HttpGet("{id}")]
        public IActionResult GetRecipeById(int id)
        {
            var recipe = _dbContext.Recipes.FirstOrDefault(r => r.RecipeId == id);
            if (recipe == null)
            {
                return NotFound();
            }
            return Ok(recipe);
        }
    }
}
