using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Model
{
    public class NoteModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Remainder { get; set; }
        public string Color { get; set; }
        public string Image { get; set; }
        public bool IsArchive { get; set; }
        public bool IsTrash { get; set; }
        public bool IsPinned { get; set; }
    }
}
