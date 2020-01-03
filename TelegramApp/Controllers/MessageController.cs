using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using Telegram.Bot.Types;
using TelegramApp.Models;
using TelegramApp.Models.Commands;

namespace TelegramApp.Controllers
{
	public class MessageController : ApiController
	{
		[Route(@"api/message/update")] //webhook uri part
		public async Task<OkResult> Update([FromBody]Update update)
		{
			var commands = Bot.Commands;
			Message message = update.Message;
			var client = await Bot.Get();

			foreach (var command in commands)
			{
				if (command.Contains(message.Text))
				{
					command.Execute(message, client);
					return Ok();
				}
			}

			SendRifmaCommand rifma = new SendRifmaCommand();
			rifma.Execute(message, client);
			return Ok();
		}
	}
}