using DomainModel.Entity;

namespace DomainModel
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class CMSEntities : DbContext
    {
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public DbSet<Article> articles { get; set; }
        public DbSet<ArticleAuthors> articles_authors { get; set; }
        public DbSet<ArticleCategory> articles_categories { get; set; }
        public DbSet<Author> authors { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Comment> comments { get; set; }
        public DbSet<DocumentGroup> docgroups { get; set; }
        public DbSet<Document> docs { get; set; }
        public DbSet<Image> images { get; set; }
        public DbSet<MenuItem> menuitems { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Page> pages { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Resource> resources { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<RoleResource> roles_resources { get; set; }
        public DbSet<Setting> settings { get; set; }
        public DbSet<Tag> tags { get; set; }
        public DbSet<TagArticle> tags_articles { get; set; }
        public DbSet<Text> texts { get; set; }
        public DbSet<TextValue> texts_values { get; set; }
        public DbSet<User> users { get; set; }
    }
}
