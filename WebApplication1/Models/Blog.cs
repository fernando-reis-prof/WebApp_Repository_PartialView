namespace WebApplication1.Models
{
    public class Blog 
    {
        public int Id { get; set; }
        public string Url { get; set; }

        public BlogAsset BlogAssets { get; set; } // Propriedade de navegação de RFERÊNCIA

        public IList<Post> Posts { get; set; } // Propriedade de navegação de COLEÇÃO
    }

}
