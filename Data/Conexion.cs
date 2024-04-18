using LinqToDB;
using LinqToDB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data
{

    /*public class Conexion : System.Data.Linq.DataContext
    {
        public Conexion() : base("ConnEscuela") { }
        public Table<Estudiante> _Estudiante { get { return GetTable<Estudiante>(); } }
    }*/
    public class Conexion : LinqToDB.Data.DataConnection
    {
        public Conexion() : base("ConnEscuela") { }
        public ITable<Estudiante> _Estudiante => this.GetTable<Estudiante>();


        //public ITable<Estudiante> _Estudiante { get { return GetTable<Estudiante>(); }  }
    }
}
