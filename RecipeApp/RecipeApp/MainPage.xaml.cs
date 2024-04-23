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
            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync("https://localhost:7068/api/Recipe");

                if (response.IsSuccessStatusCode)
                {
                    //var json = await response.Content.ReadAsStringAsync();
                    //var recipes = JsonConvert.DeserializeObject<List<RecipeTable>>(json);
                    await DisplayAlert("Error", "Failed to fetch recipes!", "OK");
                    // Handle the list of recipes as needed (e.g., display in a ListView)
                }
                else
                {
                    await DisplayAlert("Error", "Failed to fetch recipes!", "OK");
                }
            }
        }

    }
}
