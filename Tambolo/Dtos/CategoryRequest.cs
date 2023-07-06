using System.ComponentModel.DataAnnotations;

namespace Tambolo.Dtos
{
    public class CategoryRequest
    {
        [Required] public string Title { get; set; }
        [Required] public string Slug { get; set; }
    }
}
