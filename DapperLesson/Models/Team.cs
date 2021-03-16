using System;
using System.Collections.Generic;
using System.Text;

namespace DapperLesson.Models
{
    public class Team
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; }
        public ICollection<Player> Players { get; set; } = new HashSet<Player>();
    }
}
