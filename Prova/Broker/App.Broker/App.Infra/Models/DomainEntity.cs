using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public abstract class DomainEntity
    {
        public DomainEntity() 
        {
            this.Create = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 0, 0, 0);
        }
        [Key]
        public Guid Code { get; set; }
        public bool IsActive { get; set; }
        public DateTime Create { get; set; }
        public Guid UserCreate { get; set; }
        public DateTime? Update { get; set; }
        public Guid? UserUpdate { get; set; }

    }
}
