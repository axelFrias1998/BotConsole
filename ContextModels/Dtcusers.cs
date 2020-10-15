using System;
using System.Collections.Generic;

namespace TelegramBot.ContextModels
{
    public partial class Dtcusers
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string LastName1 { get; set; }
        public string LastName2 { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public int RollId { get; set; }
        public bool? StatusUser { get; set; }

        public virtual RollsCatalog Roll { get; set; }
    }
}
