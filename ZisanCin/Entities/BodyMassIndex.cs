using System.ComponentModel.DataAnnotations;

namespace ZisanCin.Entities
{
    public class BodyMassIndex
    {
        public int Id { get; set; }

        [Display(Name = "Adı Soyadı"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        public string Name { get; set; }

        [Display(Name = "Telefon")]
        public string? Phone { get; set; }

        [Display(Name = "Yaş"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [Range(1, 120, ErrorMessage = "Lütfen geçerli bir yaş giriniz.")]
        public int Age { get; set; }

        [Display(Name = "Cinsiyet"), Required(ErrorMessage = "{0} seçimi zorunludur!")]
        public string Gender { get; set; } // "Erkek" veya "Kadın"

        [Display(Name = "Boy (cm)"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [Range(50, 250, ErrorMessage = "Lütfen geçerli bir boy giriniz (50-250 cm).")]
        public double Height { get; set; }

        [Display(Name = "Kilo (kg)"), Required(ErrorMessage = "{0} alanı boş geçilemez!")]
        [Range(10, 500, ErrorMessage = "Lütfen geçerli bir kilo giriniz.")]
        public double Weight { get; set; }

        [Display(Name = "VKI Değeri")]
        public double Result { get; set; }

        [Display(Name = "Durum")]
        public string? Status { get; set; } // "Zayıf", "Normal", "Obez" gibi metin sonucu

        [Display(Name = "Eklenme Tarihi"), ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}