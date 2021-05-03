using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Domain.Models
{
    public interface IDomainEntity
    {
        bool IsActive { get; set; }
        DateTime Create { get; set; }
        Guid UserCreate { get; set; }
        DateTime? Update { get; set; }
        Guid? UserUpdate { get; set; }
    }
}
