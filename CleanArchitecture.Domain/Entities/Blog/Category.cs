using CleanArchitecture.Domain.Generics;

namespace CleanArchitecture.Domain.Entities.Blog;

public sealed class Category : EntityBase<CategoryId>
{
    public Category() { }
    private Category(CategoryId id,string name, string description)
    {
        Id = id;
        Name = name;
        Description = description;
    } 

    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<Post> Posts { get; set; } = new List<Post>();

    public static Category Create(string name, string description)
    {
        var category = new Category(new CategoryId(Guid.NewGuid()), name, description);
        return category;
    }
}
