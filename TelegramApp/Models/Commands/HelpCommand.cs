using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramApp.Models.Commands
{
    public class HelpCommand : Command
    {
        public override string Name => "help";

        public override void Execute(Message message, TelegramBotClient client)
        {
            var chatId = message.Chat.Id;
            var messageId = message.MessageId;

            client.SendTextMessageAsync(chatId, "Привет!\nЯ умею делать рифмы-хуифмы.\nЕсли в этот чат напишут сообщение из одного слова - зарифмую.");
        }
    }
}