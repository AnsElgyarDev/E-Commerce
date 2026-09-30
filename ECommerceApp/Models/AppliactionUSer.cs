using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace ECommerceApp.Models;

public class AppliactionUser : IdentityUser<int>
{
    public string FullName { get; set; } = string.Empty;
}