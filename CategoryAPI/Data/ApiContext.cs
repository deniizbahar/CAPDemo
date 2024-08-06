using Microsoft.EntityFrameworkCore;

namespace CategoryAPI.Data;

public class ApiContext : DbContext
{
    public ApiContext(DbContextOptions<ApiContext> options) : base(options) { }
}