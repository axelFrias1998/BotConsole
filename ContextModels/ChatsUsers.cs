using System;
using System.Collections.Generic;

namespace TelegramBot.ContextModels
{
    public partial class ChatsUsers
    {
        public int ChatId { get; set; }
        public int UserId { get; set; }
        public DateTime? DateStamp { get; set; }

        public virtual ChatsCatalog Chat { get; set; }
        public virtual Dtcusers User { get; set; }
    }
}
