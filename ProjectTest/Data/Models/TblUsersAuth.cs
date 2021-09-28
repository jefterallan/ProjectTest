using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace ProjectTest.Data.Models
{
    [Table("TBL_USERS_AUTH")]
    public partial class TblUsersAuth
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50)]
        public string User { get; set; }
        [Required]
        [StringLength(50)]
        public string Password { get; set; }
    }
}
