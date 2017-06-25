using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.Spatial;

namespace EntityFrameWork
{  
    public partial class Users
    {
        public int Id { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        public int Age { get; set; }

        public virtual ICollection<Books> books { get; set; }

        public Users()
        {
            books =new  List<Books>();
        }
    }
}
