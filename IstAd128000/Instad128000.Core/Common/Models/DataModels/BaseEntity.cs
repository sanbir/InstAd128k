using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Instad128000.Core.Common.Models.DataModels
{
    public class BaseEntity
    {
        [Required]
        public DateTime CreateDate { get; set; }
        [Required]
        public DateTime ModifyDate { get; set; }
        [Required]
        public Guid ID { get; set; }
        [Required]
        public bool IsDeleted { get; set; }
    }
}
