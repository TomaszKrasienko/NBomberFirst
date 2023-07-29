using System.ComponentModel.DataAnnotations;

namespace NBomberFirst.Entities;

public class Movie
{
    [Key]
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public DateTime PremiereDate { get; set; }
}