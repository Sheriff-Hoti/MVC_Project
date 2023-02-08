using Microsoft.AspNetCore.Identity;

namespace LibraryMngSys.Areas.Identity.Data;

// Add profile data for application users by adding properties to the LibraryMngSysUser class
public class LibraryMngSysUser : IdentityUser
{
    public string Name { get; set; }

    public string Surname { get; set; }
}

