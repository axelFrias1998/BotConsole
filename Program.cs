namespace TelegramBot
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Telegram.Bot;
    using TelegramBot.Context;
    using TelegramBot.ContextModels;
    using TelegramBot.Models;

    class Program
    {
        private static readonly TelegramBotClient botClient = new TelegramBotClient("1382323760:AAF_TTI8XmB6Eu6gOzEUUkvDbSoBtMWmpKQ");

        static int Main(string[] args)
        {
            switch (args[0])
            {
                //Llave en uso, ChatId, Nombre, Apellido, UserId
                case "UpdateUser":
                    if (!NumberOfArgs(args.Length, 5))
                        return 0;
                    BotSendMessage(args, 0);
                    break;
                case "GenerateKeys":
                    if (!NumberOfArgs(args.Length, 1))
                        return 0;
                    GenerateKeys();
                    break;
                default:
                    return 0;
            }
            return 1;
        }

        private static void GenerateKeys()
        {
            using(var context = new ProsisDTCContext())
            {
                if (context.BotKeys.FirstOrDefault() == null)
                {
                    List<BotKeys> botKeys = new List<BotKeys>();
                    for (int i = 0; i < 50; i++)
                    {
                        botKeys.Add(new BotKeys{
                            Key = Guid.NewGuid().ToString("D"),
                            InUse = false,
                            CreationDate = DateTime.Now,
                        });
                    }
                    context.BotKeys.AddRange(botKeys);
                    context.SaveChanges();
                }
                else
                {   
                    var keysNotUsed = context.BotKeys.Where(x => x.InUse == false);
                    context.BotKeys.RemoveRange(context.BotKeys.Where(x => x.InUse == false));
                    context.SaveChanges();
                    List<BotKeys> botKeys = new List<BotKeys>();
                    for (int i = 0; i < 50; i++)
                    {
                        string key = Guid.NewGuid().ToString("D");
                        if(context.BotKeys.Where(x => x.Key == key) == null)
                        {
                            i--;
                            continue;
                        }
                        botKeys.Add(new BotKeys{
                            Key = Guid.NewGuid().ToString("D"),
                            InUse = false,
                            CreationDate = DateTime.Now,
                        });
                    }
                    context.BotKeys.AddRange(botKeys);
                    context.SaveChanges();
                }
            }
        }

        private static bool NumberOfArgs(int argsLenght, int requiredArgs)
        {
            if(argsLenght != requiredArgs)
            {
                WriteLog(new Event{ Description = "Not enough arguments for this action. Please check your request." });
                return false;
            }
            return true;
        }

        private static void BotSendMessage(string[] args, short operation) 
        {
            var me = botClient.GetMeAsync().Result;
            switch (operation)
            {
                case 0:
                    SendMessage(Convert.ToInt32(args[2]), $"Usuario {args[3]} {args[4]} registrado. Confirmar registro en http://prosisdev.sytes.net:88/api/bot/UpdateStatus/{args[1]}/{args[5]}\nRechazar en: http://prosisdev.sytes.net:88/api/bot/DeleteUser/{args[1]}/{args[5]}");
                    break;
                default:
                    break;
            }
        }

        private static async void SendMessage(int chatId, string message) => await botClient.SendTextMessageAsync(
            chatId: chatId,
            text: message
        );

        private static void WriteLog(Event e)
        {
            string file = @"C:\temporal\BotLog.txt";
            if(File.Exists(file))
            {
                string[] rows = File.ReadAllLines(file);
                rows[0] = e.Information != null ? 
                    $"{DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss")}. {e.Information.Message}. {Convert.ToInt32(e.Information.StackTrace.Substring(e.Information.StackTrace.LastIndexOf(" ") + 1))}.\n{rows[0]}" : $"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}. {e.Description}.\n{rows[0]}";
                File.Delete(file);
                File.WriteAllLines(file, rows); 
            }
            else
            {
                string text;
                text = e.Information != null ? 
                    $"{DateTime.Now.ToString("dd/mm/yyyy hh:mm:ss")}. {e.Information.Message}. {Convert.ToInt32(e.Information.StackTrace.Substring(e.Information.StackTrace.LastIndexOf(" ") + 1))}." : $"{DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss")}. {e.Description}.";
                File.WriteAllText(file, text);
            }
        }
    }
}
