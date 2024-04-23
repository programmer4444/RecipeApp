using System.ComponentModel.DataAnnotations;

public class RecipeTable
{
    [Key]
    public int RecipeId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    public string Description { get; set; }

    [Required]
    public string Ingredients { get; set; }

    [Required]
    public string Instructions { get; set; }

    [MaxLength(50)]
    public string Category { get; set; }

    [MaxLength(255)]
    public string ImageUrl { get; set; }
}
