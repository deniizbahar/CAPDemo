using Microsoft.EntityFrameworkCore;

namespace ProductAPI.Data;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
}