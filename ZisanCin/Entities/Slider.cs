using System.ComponentModel.DataAnnotations;

namespace ZisanCin.Entities
{
    public class Slider
    {
        public int Id { get; set; }
        [Display(Name = "Resim")]
        public string? Image { get; set; }
        [Display(Name = "Başlık")]
        public string? Title { get; set; }
        [Display(Name = "Açıklama")]
        public string? Description { get; set; }
    }
}
