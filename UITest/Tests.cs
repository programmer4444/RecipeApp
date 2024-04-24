using System;
using NUnit.Framework;
using UITest;
using Xamarin.UITest;
using Xamarin.UITest.Queries;

namespace RecipeApp.UITests
{
    [TestFixture(Platform.Android)]
    [TestFixture(Platform.iOS)]
    public class AboutPageTests
    {
        IApp app;
        Platform platform;

        public AboutPageTests(Platform platform)
        {
            this.platform = platform;
        }

        [SetUp]
        public void BeforeEachTest()
        {
            app = AppInitializer.StartApp(platform);
        }

        [Test]
        public void VerifyAboutPageTitle()
        {
            // Arrange
            app.WaitForElement("AboutPageTitle");

            // Act
            string pageTitle = app.Query("AboutPageTitle")[0].Text;

            // Assert
            Assert.AreEqual("About Page", pageTitle);
        }

        [Test]
        public void TapOnCategoryButton()
        {
            // Arrange
            app.WaitForElement("BreakfastButton");

            // Act
            app.Tap("BreakfastButton");

            // Assert
            app.WaitForElement("RecipesCollectionView");
        }

        [Test]
        public void TapOnRecipeImage()
        {
            // Arrange
            app.WaitForElement("RecipeImage");

            // Act
            app.Tap("RecipeImage");

            // Assert
            app.WaitForElement("RecipeDetailsPage");
        }
    }
}
