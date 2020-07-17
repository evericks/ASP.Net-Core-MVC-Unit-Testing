using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        public int Id { get; set; }

        [StringLength(200)]
        public string Content { get; set; }

        public DateTime? Date { get; }

        public int PlayerId { get; set; }

        [StringLength(256)]
        public string Email { get; set; }
    }
}
