using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Xamarin.Forms;
using RecipeApp.Models; 
using Newtonsoft.Json;


namespace RecipeApp
{
    public partial class MainPage : ContentPage
    {
        private Xamarin.Forms.Button selectedButton = null;
        private bool isCategorySelected = false;
        private readonly HttpClient httpClient = new HttpClient();

        public MainPage()
        {
            InitializeComponent();

            // Assign event handlers to button click events
            BreakfastButton.Clicked += OnCategorySelected;
            BrunchButton.Clicked += OnCategorySelected;
            LunchButton.Clicked += OnCategorySelected;
            DinnerButton.Clicked += OnCategorySelected;
            AppetizersButton.Clicked += OnCategorySelected;
            SoupsButton.Clicked += OnCategorySelected;

            // Assign event handlers to text changed events
            TitleEntry.TextChanged += OnTextChanged;
            DescriptionEditor.TextChanged += OnTextChanged;
            IngredientsEditor.TextChanged += OnTextChanged;
            InstructionsEditor.TextChanged += OnTextChanged;
            ImageUrlEntry.TextChanged += OnTextChanged;
        }

        private void OnCategorySelected(object sender, EventArgs e)
        {
            var button = sender as Xamarin.Forms.Button;

            if (selectedButton != null)
            {
                // Reset the styling of the previously selected button
                selectedButton.BackgroundColor = Color.White;
                selectedButton.TextColor = Color.FromHex("#495057");
            }

            // Set the styling for the newly selected button
            button.BackgroundColor = Color.FromHex("#007bff");
            button.TextColor = Color.White;

            // Update the selected button
            selectedButton = button;

            // Update category selection flag
            isCategorySelected = true;

            // Check if all fields are valid
            CheckFieldsValidity();
        }

        private void OnTextChanged(object sender, TextChangedEventArgs e)
        {
            // Check if all fields are valid
            CheckFieldsValidity();
        }

        private void CheckFieldsValidity()
        {
            bool areFieldsValid = new[]
            {
        !string.IsNullOrEmpty(TitleEntry.Text),
        !string.IsNullOrEmpty(DescriptionEditor.Text),
        !string.IsNullOrEmpty(IngredientsEditor.Text),
        !string.IsNullOrEmpty(InstructionsEditor.Text),
        !string.IsNullOrEmpty(ImageUrlEntry.Text),
        isCategorySelected
    }.All(condition => condition);

            // Enable or disable the "Add Recipe" button based on validation results
            AddRecipeButton.IsEnabled = areFieldsValid;
        }



        private void OnAddRecipeClicked(object sender, EventArgs e)
        {
            // Add your logic for when the "Add Recipe" button is clicked


        }
        private async void OnGetAllRecipesClicked(object sender, EventArgs e)
        {
            try
            {
                // Make a GET request to your API endpoint to get all recipes
                var response = await httpClient.GetAsync("https://your-api-url/api/Recipe");

                // Check if the response is successful
                if (response.IsSuccessStatusCode)
                {
                    // Read the response content as a string
                    var content = await response.Content.ReadAsStringAsync();

                    // Deserialize the JSON string into a list of recipes
                    var recipes = JsonConvert.DeserializeObject<List<RecipeTable>>(content);

                    // Display the recipes using a DisplayAlert
                    var recipesString = string.Join("\n", recipes.Select(r => $"{r.Title}: {r.Description}"));
                    await DisplayAlert("All Recipes", recipesString, "OK");
                }
                else
                {
                    // Handle unsuccessful response
                    await DisplayAlert("Error", "Failed to retrieve recipes", "OK");
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions
                await DisplayAlert("Error", $"An error occurred: {ex.Message}", "OK");
            }
    }



}
}
