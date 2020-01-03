using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;
using Telegram.Bot;
using Telegram.Bot.Types;

namespace TelegramApp.Models.Commands
{
	public class SendRifmaCommand : Command
	{
		public override string Name => "rifma";

		public override void Execute(Message message, TelegramBotClient client)
		{
			// Нас интересует только текст:
			if (message.Type != Telegram.Bot.Types.Enums.MessageType.TextMessage)
				return;

			// Сообщение должно состоять из одного слова
			if (message.Text.Trim().Contains(" "))
				return;

			// Нужна только кириллица
			if (Regex.IsMatch(message.Text, "[а-яА-ЯеЁ]") == false)
				return;

			var chatId = message.Chat.Id;
			var messageId = message.MessageId;
			string word = message.Text.Replace('?', '!');

			// Игнорируем однобуквенные сообщения
			if (word.Length <= 1)
				return;

			//// Игнорируем хуифицированные слова
			//if (word.Length >= 3)
			//{
			//	string first3Letters = word.Substring(0, 3).ToLower();
			//	if (first3Letters == "хуя" || first3Letters == "хую" || first3Letters == "хуё" || first3Letters == "хуи" || first3Letters == "хуе")
			//	{
			//		//client.SendTextMessageAsync(chatId, "Хуифицировал, хуифицировал, да не выхуифицировал!", replyToMessageId: messageId);
			//		return;
			//	}
			//}

			List<char> vowels = new List<char>() { 'а', 'у', 'о', 'ы', 'и', 'э', 'я', 'ю', 'ё', 'е' };
			string resultMessage = string.Empty;

			// Хуифицируем обе части составного слова:
			char delimeter = '-';
			string[] substrings = word.Split(delimeter);

			foreach (string partOfWord in substrings)
			{
				string modifiedSubString = string.Empty;

				foreach (char letter in partOfWord.ToLower().ToCharArray())
				{
					if (vowels.Contains(letter) == false)
						continue;   // Пропускаем согласные

					int position = partOfWord.ToLower().IndexOf(letter) + 1;
					string ending = string.Empty;

					if (position < partOfWord.Length)
						ending = partOfWord.Substring(position);

					bool flag = false; // Флаг для выхода из цикла после switch

					switch (letter)
					{
						case 'а': modifiedSubString = string.Concat("хуя", ending); flag = true; break;
						case 'у': modifiedSubString = string.Concat("хую", ending); flag = true; break;
						case 'о': modifiedSubString = string.Concat("хуё", ending); flag = true; break;
						case 'ы': modifiedSubString = string.Concat("хуи", ending); flag = true; break;
						case 'и': modifiedSubString = string.Concat("хуи", ending); flag = true; break;
						case 'э': modifiedSubString = string.Concat("хуе", ending); flag = true; break;
						case 'я': modifiedSubString = string.Concat("хуя", ending); flag = true; break;
						case 'ю': modifiedSubString = string.Concat("хую", ending); flag = true; break;
						case 'ё': modifiedSubString = string.Concat("хуё", ending); flag = true; break;
						case 'е': modifiedSubString = string.Concat("хуе", ending); flag = true; break;
					}

					if (flag)
						break;
				}

				// Добавляем модифицированную часть слова к результирующей строке
				if (string.IsNullOrWhiteSpace(modifiedSubString) == false)
				{
					// Сохраняем первую заглавную букву, если была
					if (Char.IsUpper(partOfWord, 0))
						modifiedSubString = modifiedSubString.Substring(0, 1).ToUpper() + modifiedSubString.Remove(0, 1);

					if (string.IsNullOrWhiteSpace(resultMessage))
						resultMessage += modifiedSubString;
					else
						resultMessage += (delimeter + modifiedSubString);
				}
			}

			// Последняя проверка на пустой результат:
			if (string.IsNullOrWhiteSpace(resultMessage) == false)
				client.SendTextMessageAsync(chatId, resultMessage);
		}
	}
}