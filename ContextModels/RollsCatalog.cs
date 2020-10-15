using System;
using System.Collections.Generic;

namespace TelegramBot.ContextModels
{
    public partial class RollsCatalog
    {
        public RollsCatalog()
        {
            Dtcusers = new HashSet<Dtcusers>();
        }

        public int RollId { get; set; }
        public string RollDescription { get; set; }

        public virtual ICollection<Dtcusers> Dtcusers { get; set; }
    }
}
