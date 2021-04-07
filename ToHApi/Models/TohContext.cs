using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ToHApi.Models
{
    public class TohContext : DbContext
    {
        public TohContext(DbContextOptions<TohContext> options)
            : base(options)
        {
        }

        public DbSet<TohHero> TohHeroes { get; set; }
    }
}
