using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace CoreApplication.Data.Entity
{
    public class BaseEntity
    {
        public DateTime CreationDate { get; set; }
        public DateTime LastModifydate { get; set; }

    }
}
