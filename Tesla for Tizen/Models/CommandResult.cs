using Tesla.NET.Models;

namespace TeslaTizen.Models
{
    public class CommandResult
    {
        public string Reason { get; set; }
        public bool Result { get; set; }

        public static CommandResult From(ICommandResult commandResult)
        {
            return new CommandResult
            {
                Reason = commandResult.Reason,
                Result = commandResult.Result,
            };
        }
    }
}
