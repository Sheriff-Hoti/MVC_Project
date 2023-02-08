namespace MVC_Project.Models.Supplier
{
    public class SupplierUtility
    {
        public Dictionary<string, Func<Supplier, string>> SupplierUtils;
        Func<Supplier, string> Name = x => x.Name;
        Func<Supplier, string> Id = x => x.Id.ToString();
        Func<Supplier, string> Address = x => x.Address;
        Func<Supplier, string> Email = x => x.Email;
        Func<Supplier, string> ContactNumber = x => x.ContactNumber;
        Func<Supplier, string> CreatedAt = x => x.createdAt.ToString();
        Func<Supplier, string> UpdatedAt = x => x.updatedAt.ToString();

        public SupplierUtility()
        {
            SupplierUtils = new Dictionary<string, Func<Supplier, string>>();
            SupplierUtils.Add("Name", Name);
            SupplierUtils.Add("Id", Id);
            SupplierUtils.Add("Address", Address);
            SupplierUtils.Add("Email", Email);
            SupplierUtils.Add("ContactNumber", ContactNumber);
            SupplierUtils.Add("createdAt", CreatedAt);
            SupplierUtils.Add("updatedAt", UpdatedAt);
        }

        public Dictionary<string, string> userAttrs(Supplier supplier)
        {
            return new Dictionary<string, string>()
            {
                {"Name",supplier.Name },
                {"Address",supplier.Address },
                {"Email",supplier.Email },
                {"ContactNumber",supplier.ContactNumber }

            };
        }
    }
}
