﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SAGA.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class sagaEntities : DbContext
    {
        public sagaEntities()
            : base("name=sagaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<FactorKUsuario> FactorKUsuario { get; set; }
        public virtual DbSet<FuncionalidadesPorRol> FuncionalidadesPorRol { get; set; }
        public virtual DbSet<LoginLogs> LoginLogs { get; set; }
        public virtual DbSet<NivelSuscripcion> NivelSuscripcion { get; set; }
        public virtual DbSet<Partida> Partida { get; set; }
        public virtual DbSet<Provincia> Provincia { get; set; }
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<RolUsuario> RolUsuario { get; set; }
        public virtual DbSet<sysdiagrams> sysdiagrams { get; set; }
        public virtual DbSet<TipoRitmo> TipoRitmo { get; set; }
        public virtual DbSet<Titulo> Titulo { get; set; }
        public virtual DbSet<TituloUsuario> TituloUsuario { get; set; }
        public virtual DbSet<Torneo> Torneo { get; set; }
        public virtual DbSet<ParticipanteTorneo> ParticipanteTorneo { get; set; }
        public virtual DbSet<Usuario> Usuario { get; set; }
    }
}