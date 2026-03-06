using System.ComponentModel.DataAnnotations;

namespace ZisanCin.Entities
{
    public class Faq
    {
        public int Id { get; set; }
        [Display(Name = "Soru"), Required(ErrorMessage = "Lütfen bir soru giriniz.")]
        public string Question { get; set; }
        [Display(Name = "Cevap"), Required(ErrorMessage = "Lütfen bir cevap giriniz.")]
        public string Answer { get; set; }
    }
}
