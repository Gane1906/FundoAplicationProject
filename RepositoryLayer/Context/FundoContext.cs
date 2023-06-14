using Microsoft.EntityFrameworkCore;
using RepositoryLayer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Context
{
    public class FundoContext : DbContext
    {
        public FundoContext(DbContextOptions dbContextOptions): base(dbContextOptions) 
        {
            
        }
        public DbSet<UserEntity> User { get; set; }
        public DbSet<ProductEntity> Products { get; set; }
        public DbSet<NoteEntity> Notes { get; set; }
        public DbSet<LabelEntity> Label { get; set; }
    }
}
