namespace MVC_Project.Models.Shop
{
    public class ShopUtility
    {
        public Dictionary<string, Func<Shop, string>> ShopUtils;
        Func<Shop, string> Name = x => x.Name;
        Func<Shop, string> Id = x => x.Id.ToString();
        Func<Shop, string> Address = x => x.Address;
        Func<Shop, string> OpeningDate = x => x.OpeningDate.ToString();
        Func<Shop, string> Location = x => x.Location;

        public ShopUtility()
        {
            ShopUtils = new Dictionary<string, Func<Shop, string>>();
            ShopUtils.Add("Name", Name);
            ShopUtils.Add("Id", Id);
            ShopUtils.Add("Address", Address);
            ShopUtils.Add("OpeningDate", OpeningDate);
            ShopUtils.Add("Location", Location);
        }
    }
}
