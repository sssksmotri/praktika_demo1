﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace praktika_demo
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class Entities1 : DbContext
    {
        public Entities1()
            : base("name=Entities1")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<auth> auth { get; set; }
        public virtual DbSet<comments> comments { get; set; }
        public virtual DbSet<homeTechType> homeTechType { get; set; }
        public virtual DbSet<request> request { get; set; }
        public virtual DbSet<request_client> request_client { get; set; }
        public virtual DbSet<status> status { get; set; }
        public virtual DbSet<type> type { get; set; }
        public virtual DbSet<user> user { get; set; }
    }
}
