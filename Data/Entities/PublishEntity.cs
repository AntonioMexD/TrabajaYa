using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace TrabajaYaAPI.Data.Entities
{
    public class PublishEntity
    {
        [Key]
        [Required]
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
        [ForeignKey("UserIde")]
        public virtual UserEntity User { get; set; }
    }
}
