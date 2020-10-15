using System;
using System.Collections.Generic;

namespace TelegramBot.ContextModels
{
    public partial class ChatsCatalog
    {
        public int ChatId { get; set; }
        public string Chat { get; set; }
        public string Description { get; set; }
    }
}
