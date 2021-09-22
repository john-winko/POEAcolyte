using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PoeAcolyte.API.Interactions
{
    public class GameClientCommand : ToolStripMenuItem
    {
        public IPoeInteraction Interaction { get; set; }
        public string Message { get; set; }
        public GameClientCommandType CommandType { get; set; }
        private List<string> _commandMessages = new();

        public GameClientCommand(GameClientCommandType commandType, IPoeInteraction interaction, string message = "")
        {
            CommandType = commandType;
            Interaction = interaction;
            Message = message;
            
            Init();
        }

        private void Init()
        {
            switch (CommandType)
            {
                case GameClientCommandType.Invite:
                    Text = @"Invite";
                    _commandMessages.Add($"@{Interaction.Entry.Player} Your item is ready to be picked up");
                    _commandMessages.Add($"/invite {Interaction.Entry.Player}");
                    break;
                case GameClientCommandType.Trade:
                    Text = @"Trade";
                    _commandMessages.Add($"/tradewith {Interaction.Entry.Player}");
                    break;
                case GameClientCommandType.Message:
                    Text = @"Message";
                    break;
                case GameClientCommandType.Kick:
                    Text = @"Kick";
                    break;
                case GameClientCommandType.Whois:
                    Text = @"Whois";
                    break;
                case GameClientCommandType.Hideout:
                    Text = @"Hideout";
                    break;
                case GameClientCommandType.Autoreply:
                    // Nothing
                    break;
                case GameClientCommandType.NoStock:
                    break;
                case GameClientCommandType.Decline:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
            Click += GameClientCommand_Click;
        }

        private void GameClientCommand_Click(object sender, EventArgs e)
        {
            Interaction.LastGameClientCommand = CommandType;

        }

    }

    public enum GameClientCommandType
    {
        Invite,
        Trade,
        Message,
        NoStock,
        Decline,
        Kick,
        Whois,
        Hideout,
        Autoreply
    }
}
