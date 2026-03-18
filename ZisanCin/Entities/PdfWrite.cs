using System.ComponentModel.DataAnnotations;

namespace ZisanCin.Entities
{
    public class PdfWrite
    {
        public int Id { get; set; }
        [Display(Name = "Konu Başlığı"), Required(ErrorMessage = "Konu başlığı gereklidir."), StringLength(150, ErrorMessage = "Konu başlığı en fazla 150 karakter olabilir.")]
        public string Name { get; set; }
        [Display(Name="Yazı Dosyası")]
        public string? PdfFİlePath { get; set; }
        [Display(Name = "Slug"), Required(ErrorMessage = "Slug gereklidir.")]
        public string Slug { get; set; }
        [Display(Name = "Meta Başlığı")]
        public string? MetaTitle { get; set; }
        [Display(Name = "Meta Açıklaması")]
        public string? MetaDescription { get; set; }
        [Display(Name = "Meta Anahtar Kelimeler")]
        public string? MetaKeywords { get; set; }
    }
}
