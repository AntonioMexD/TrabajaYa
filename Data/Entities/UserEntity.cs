using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TrabajaYaAPI.Data.Entities
{
    public class UserEntity
    {
        [Key]
        [Required]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Correo { get; set; }
        public string Telefono { get; set; }
        public virtual ICollection<PublishEntity> Publications { get; set; }
    }
}
