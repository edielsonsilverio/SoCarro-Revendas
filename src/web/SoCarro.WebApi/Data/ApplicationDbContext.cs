using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace SoCarro.WebApi.Data;

public class ApplicationDbContext : IdentityDbContext 
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
}

/*
    
    Add-Migration TabelasUsuario -StartupProject Web\SoCarro.WebApi -Project Web\SoCarro.WebApi
   
    Update-Database -StartupProject Web\SoCarro.WebApi -Project Web\SoCarro.WebApi
*/