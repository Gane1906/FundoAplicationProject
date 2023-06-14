using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Text.Json.Serialization;

namespace RepositoryLayer.Entity
{
    public class LabelEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int LabelId { get; set; }
        public string LabelName { get; set; }
        [ForeignKey("Notes")]
        public long NoteId { get; set; }
        [JsonIgnore]
        public virtual NoteEntity Notes { get; set; }
        [ForeignKey("User")]
        public int UserId { get; set; }
        [JsonIgnore]
        public virtual UserEntity User { get; set; }
    }
}
