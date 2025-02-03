using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Domain._01_Entities
{
    public class Task
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreatAt { get; set; }
        public bool IsCompleted { get; set; }
        public int UserId { get; set; }
        public User? User { get; set; }

    }
}
