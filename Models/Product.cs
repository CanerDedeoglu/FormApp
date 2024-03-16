using System.ComponentModel.DataAnnotations;

namespace FormApp.Models
{
    public class Product
    {
        [Display(Name ="Urun Id")]
        public int ProductId { get; set; }

        [Display(Name ="Kitap Adı ")]
        [Required(ErrorMessage = "Kitap adı boş geçilemez")]
        public string BookName { get; set; } =null!;
    
        [Display(Name ="Sayfa")]
        [Required(ErrorMessage = "Sayfa sayısı boş geçilemez")]
        [Range(1,1000,ErrorMessage ="Sayfa sayısı 1-1000 arasında olmalıdır")]
        public int PageCount { get; set; }

        [Display(Name ="Resim")]
        public string? Image { get; set; } = string.Empty;

        [Display(Name ="Aktiflik")]
        public bool IsActive { get; set; } 

        [Display(Name ="Kategori Id")]
        [Required(ErrorMessage = "Kategori adı boş geçilemez")]
        public int? CategoryId { get; set; } 

        
    }
    
}