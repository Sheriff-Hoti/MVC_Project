namespace MVC_Project.Models.Category
{
    public class CategoryUtility
    {
        public Dictionary<string, Func<Category, string>> CategoryUtils;
        Func<Category, string> Id = x => x.Id.ToString();
        Func<Category, string> Name = x => x.Name;
        Func<Category, string> Description = x => x.Description;
        Func<Category, string> CreatedAt = x => x.createdAt.ToString();
        Func<Category, string> UpdatedAt = x => x.updatedAt.ToString();


        public CategoryUtility()
        {
            CategoryUtils = new Dictionary<string, Func<Category, string>>();
            CategoryUtils.Add("Id", Id);
            CategoryUtils.Add("Name", Name);
            CategoryUtils.Add("Description", Description);
            CategoryUtils.Add("createdAt", CreatedAt);
            CategoryUtils.Add("updatedAt", UpdatedAt);
        }


        public Dictionary<string, string> userAttrs(Category category)
        {
            return new Dictionary<string, string>()
            {
                {"Name",category.Name },
                {"Description",category.Description },

            };
        }
    }



}
