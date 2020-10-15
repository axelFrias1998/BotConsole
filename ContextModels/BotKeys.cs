
namespace TelegramBot.ContextModels
{
    using System;
    
    public partial class BotKeys
    {
        public string Key { get; set; }
        public bool? InUse { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime? UseDate { get; set; }
        public int? UserId { get; set; }
    }
}
