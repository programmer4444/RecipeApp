namespace RecipeApp.Models
{
    public class Recipes
    {
        public int RecipeId { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public string Ingredients { get; set; }

        public string Instructions { get; set; }

        public string Category { get; set; }

        public string ImageUrl { get; set; }
    }

}