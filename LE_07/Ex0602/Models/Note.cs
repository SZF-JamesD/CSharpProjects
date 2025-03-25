using System;

namespace Ex0602.Models
{
    public class Note
    {
        public int NoteID { get; set; }
        public string Content {  get; set; }
        public DateTime DateCreated { get; set; }
        public int EmployeeID { get; set; }
    }
}
