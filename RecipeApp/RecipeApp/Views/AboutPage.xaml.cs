using System;
using Xamarin.Forms;

namespace RecipeApp.Views
{
    public partial class AboutPage : ContentPage
    {
        public AboutPage()
        {
            InitializeComponent();
        }

        private void OnCategoryButtonClicked(object sender, EventArgs e)
        {
            // Reset all buttons to default style
            foreach (var child in ((StackLayout)((ScrollView)Content).Content).Children)
            {
                if (child is Button button)
                {
                    button.BackgroundColor = Color.White;
                    button.TextColor = Color.Black;
                }
            }

            // Highlight the clicked button
            if (sender is Button clickedButton)
            {
                clickedButton.BackgroundColor = Color.Blue;
                clickedButton.TextColor = Color.White;
            }
        }
    }
}
