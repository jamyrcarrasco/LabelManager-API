using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MANAGER.MODELS
{
    public class Tenant
    { 
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public required string Id { get; set; }
        public required string Name { get; set; }
    }
}
