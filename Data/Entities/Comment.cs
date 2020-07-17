using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities
{
    [Table("Comment")]
    public partial class Comment
    {
        [Key]
        [Column(Order = 0)]
        public int Id { get; set; }

        [Column(Order = 1)]
        [StringLength(200)]
        public string Content { get; set; }

        [Column(Order = 2)]
        public DateTime Date { get; set; }

        [Column(Order = 3)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int PlayerId { get; set; }

        [Column(Order = 4)]
        [StringLength(256)]
        public string Email { get; set; }
    }
}
