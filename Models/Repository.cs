namespace FormApp.Models
{
    public class Repository
    {
        private static readonly List<Product> _product = new();
        private static readonly List<Category> _category = new();

        static Repository(){
            _category.Add(new Category{CategoryId = 1, CategoryName="Roman"});
            _category.Add(new Category{CategoryId=2, CategoryName="Kişisel Gelişim"});

            _product.Add(new Product{ProductId=1, BookName="Cadı", CategoryId=1, PageCount=225, Image="1.jpeg", IsActive=true});
            _product.Add(new Product{ProductId=1, BookName="Tarık Buğra'nın Roman Dünyası", CategoryId=1, PageCount=275, Image="2.jpeg", IsActive=true});
            _product.Add(new Product{ProductId=2, BookName="Son Ayı", CategoryId=2, PageCount=300, Image="3.jpeg", IsActive=true});

        }

        public static List<Product> Products{get{return _product;}}

        public static void CreateProduct(Product entity){
            _product.Add(entity);
        }
        public static void EditProduct(Product entity){
            var product = _product.FirstOrDefault(p => p.ProductId == entity.ProductId);
            if(product != null){
                product.BookName = entity.BookName;
                product.CategoryId = entity.CategoryId;
                product.PageCount = entity.PageCount;
                product.Image = entity.Image;
                product.IsActive = entity.IsActive;
            }
        }
        public  static List<Category> Categories {get{return _category;}}

    }
}