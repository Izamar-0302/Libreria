using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace LibreriaApi.Models
{
    public class DBContextProyect:DbContext
    {
        public DBContextProyect():base("MyConnectionString") 
        { 
        }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Bonificaciones> Bonificaciones { get; set; }
        public DbSet<Deducciones> Deducciones { get; set; }
        public DbSet<Detalle_pedido> Detalles_pedidos { get; set; }
        public DbSet<Detalle_Venta> Detalles_ventas { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Empleado_planilla> Empleados_Planillas { get; set; }
        public DbSet<Lector> Lectores { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<MetodoPago> MetodosPagos { get; set; }
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Planilla> Planillas { get; set; }
        public DbSet<Proveedor> Proveedores { get; set; }
        public DbSet<Sucursal> Sucursales { get; set; }
        public DbSet<Turno> Turnos { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Venta> Ventas { get; set; }
        public DbSet<Cargo> Cargo { get; set; }
    }
}