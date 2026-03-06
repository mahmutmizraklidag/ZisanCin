using System.ComponentModel.DataAnnotations;

namespace ZisanCin.Entities
{
    public class About
    {
        public int Id { get; set; }
        [Display(Name = "Başlık")]
        public string? Title { get; set; }
        [Display(Name = "Açıklama")]
        [Required(ErrorMessage = "Lütfen açıklama giriniz.")]
        public string Description { get; set; }
        [Display(Name = "Resim")]
        public string? Image { get; set; }
        [Display(Name = "Anasayfa Başlık")]
        public string? HomeTitle { get; set; }
        [Display(Name = "Anasayfa Açıklama")]
        public string? HomeDescription { get; set; }
        [Display(Name = "Anasayfa Resim")]
        public string? HomeImage { get; set; }
        [Display(Name = "Meta-Keywords")]
        public string? Keywords { get; set; }
        [Display(Name = "Meta-Description")]
        public string? MetaDescription { get; set; }
        [Display(Name = "Meta-Title")]
        public string? MetaTitle { get; set; }
    }
}
