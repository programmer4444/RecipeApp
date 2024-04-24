using Microsoft.VisualStudio.TestTools.UnitTesting;
using RecipeApp.Views;
using System.Threading.Tasks;

namespace RecipeApp.Tests
{
    [TestClass]
    public class AboutPageTests
    {
        [TestMethod]
        public async Task GetRecipesByCategoryAsync_ValidCategory_ReturnsRecipes()
        {
            // Arrange
            var aboutPage = new AboutPage();
            string category = "Dinner";

            // Act
            var recipes = await aboutPage.GetRecipesByCategoryAsync(category);

            // Assert
            Assert.IsNotNull(recipes);
            Assert.IsTrue(recipes.Count > 0);
        }

    }
}
