﻿using System;
using Microsoft.EntityFrameworkCore;
using SGR.Models;

namespace SGR.Data
{
    public class SGRContext : DbContext
    {
        public SGRContext(DbContextOptions<SGRContext> options)
            : base(options)
        {
        }

        public DbSet<Funcionário> Funcionario { get; set; }
        public DbSet<Gerente> Gerente { get; set; }

    }   
}
