using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("categoria")]
    public class Categoria
    {
        [Key]
        public int pkCategoria { get; set; }
        public string Nombre { get; set; }
        public int Costo { get; set; }

        public bool Estatus {  get; set; } 
    }
}
