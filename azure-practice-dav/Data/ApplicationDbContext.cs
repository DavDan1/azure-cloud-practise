using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Models;

public class ApplicationDbContext : DbContext
{
    private readonly string _tableStorageUri;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, string tableStorageUri)
        : base(options)
    {
        _tableStorageUri = tableStorageUri;

    }

    public DbSet<Movie> Movies { get; set; } = default!;
}
