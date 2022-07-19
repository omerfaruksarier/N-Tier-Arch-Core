using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concrete
{
    public class OperationClaim: IEntity
    {
        [Key]
        public int Id { get; set; }

        [StringLength(250)]
        public string Name { get; set; }
    }
}
