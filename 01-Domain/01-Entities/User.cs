using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _01_Domain._01_Entities
{
    public class User:IdentityUser<int>
    {
        public List<MyTask> Tasks { get; set; }

    }
}
