using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TrabajaYa.Models
{
    public class PublishModel
    {
        public int Id { get; set; }
        public string Empresa { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public string Area { get; set; }
        public string Departamento { get; set; }
        public string Jornada { get; set; }
        public string Requisitos { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public int UserIde { get; set; }
    }
}
