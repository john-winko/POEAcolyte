using System;
using System.Collections.Generic;
using System.Windows.Forms;
using PoeAcolyte.API.Services;

namespace PoeAcolyte.API.Interactions
{
    public class GameClientCommand : ToolStripMenuItem
    {
        private Action<IPoeTradeInteraction> _action;

        public GameClientCommand(GameClientCommandTypeEnum commandTypeEnum, IPoeTradeInteraction tradeInteraction)
        {
            CommandTypeEnum = commandTypeEnum;
            TradeInteraction = tradeInteraction;
            SetAction();
            Click += (_, _) => { _action(TradeInteraction); };
        }

        public IPoeTradeInteraction TradeInteraction { get; set; }
        public GameClientCommandTypeEnum CommandTypeEnum { get; set; }

        /// <summary>
        ///     ToolStripMenuItem[] generator
        /// </summary>
        /// <param name="tradeInteraction">Trade interaction</param>
        /// <returns></returns>
        public static ToolStripMenuItem[] CreateMenuItems(IPoeTradeInteraction tradeInteraction)
        {
            var temp = new List<ToolStripMenuItem>
            {
                new GameClientCommand(GameClientCommandTypeEnum.Wait, tradeInteraction),
                new GameClientCommand(GameClientCommandTypeEnum.Invite, tradeInteraction),
                new GameClientCommand(GameClientCommandTypeEnum.Trade, tradeInteraction),
                new GameClientCommand(GameClientCommandTypeEnum.TYGL, tradeInteraction),
                new GameClientCommand(GameClientCommandTypeEnum.NoStock, tradeInteraction),
                new GameClientCommand(GameClientCommandTypeEnum.Decline, tradeInteraction),
                new GameClientCommand(GameClientCommandTypeEnum.Kick, tradeInteraction),
                new GameClientCommand(GameClientCommandTypeEnum.Whois, tradeInteraction),
                new GameClientCommand(GameClientCommandTypeEnum.Hideout, tradeInteraction)
            };

            return temp.ToArray();
        }

        /// <summary>
        ///     Quick Action button commands
        /// </summary>
        /// <param name="tradeInteraction">Associated trade interaction</param>
        public static void QuickAction(IPoeTradeInteraction tradeInteraction)
        {
            // TODO add state based actions (player in map etc)
            switch (tradeInteraction.LastChatConsoleCommand, tradeInteraction.Entry.Incoming)
            {
                case (GameClientCommandTypeEnum.None, true):
                    Invite(tradeInteraction);
                    break;
                case (GameClientCommandTypeEnum.None, false):
                    Hideout(tradeInteraction);
                    break;
                case (GameClientCommandTypeEnum.Wait, _):
                    Invite(tradeInteraction);
                    break;
                case (GameClientCommandTypeEnum.Invite, _):
                    Trade(tradeInteraction);
                    break;
                case (GameClientCommandTypeEnum.Trade, _):
                    Tygl(tradeInteraction);
                    tradeInteraction.Complete();
                    break;
                default:
                    Invite(tradeInteraction);
                    break;
            }
        }

        #region Actions

        private void SetAction()
        {
            switch (CommandTypeEnum)
            {
                case GameClientCommandTypeEnum.Invite:
                    Text = @"Invite";
                    _action = Invite;
                    break;
                case GameClientCommandTypeEnum.Trade:
                    Text = @"Trade";
                    _action = Trade;
                    break;
                case GameClientCommandTypeEnum.NoStock:
                    Text = @"No Stock";
                    _action = NoStock;
                    break;
                case GameClientCommandTypeEnum.Decline:
                    Text = @"Decline";
                    _action = Decline;
                    break;
                case GameClientCommandTypeEnum.Kick:
                    Text = @"Kick";
                    _action = Kick;
                    break;
                case GameClientCommandTypeEnum.Whois:
                    Text = @"Whois";
                    _action = WhoIs;
                    break;
                case GameClientCommandTypeEnum.Hideout:
                    Text = @"Hideout";
                    _action = Hideout;
                    break;
                case GameClientCommandTypeEnum.Wait:
                    Text = @"Wait";
                    _action = Wait;
                    break;
                case GameClientCommandTypeEnum.TYGL:
                    Text = @"TY GL";
                    _action = Tygl;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }


        public static Action<IPoeTradeInteraction> Invite => interaction =>
        {
            PoeClient.GetInstance().SendChatMessages(new[]
            {
                $@"@{interaction.Entry.Player} Your item is ready for pickup",
                $@"/invite {interaction.Entry.Player}"
            });
            interaction.LastChatConsoleCommand = GameClientCommandTypeEnum.Invite;
        };

        public static Action<IPoeTradeInteraction> Trade => interaction =>
        {
            PoeClient.GetInstance().SendChatMessages(new[]
            {
                $@"/tradewith {interaction.Entry.Player}"
            });
            interaction.LastChatConsoleCommand = GameClientCommandTypeEnum.Trade;
        };

        public static Action<IPoeTradeInteraction> NoStock => interaction =>
        {
            PoeClient.GetInstance().SendChatMessages(new[]
            {
                $@"@{interaction.Entry.Player} Sorry, item is out of stock",
                $@"/kick {interaction.Entry.Player}"
            });
            interaction.LastChatConsoleCommand = GameClientCommandTypeEnum.NoStock;
        };

        public static Action<IPoeTradeInteraction> Decline => interaction =>
        {
            PoeClient.GetInstance().SendChatMessages(new[]
            {
                $@"@{interaction.Entry.Player} No thanks",
                $@"/kick {interaction.Entry.Player}"
            });
            interaction.LastChatConsoleCommand = GameClientCommandTypeEnum.Decline;
        };

        public static Action<IPoeTradeInteraction> Kick => interaction =>
        {
            PoeClient.GetInstance().SendChatMessages(new[]
            {
                $@"/kick {interaction.Entry.Player}"
            });
            interaction.LastChatConsoleCommand = GameClientCommandTypeEnum.Kick;
        };

        public static Action<IPoeTradeInteraction> WhoIs => interaction =>
        {
            PoeClient.GetInstance().SendChatMessages(new[]
            {
                $@"/whois {interaction.Entry.Player}"
            });
            interaction.LastChatConsoleCommand = GameClientCommandTypeEnum.Whois;
        };

        public static Action<IPoeTradeInteraction> Hideout => interaction =>
        {
            PoeClient.GetInstance().SendChatMessages(new[]
            {
                $@"/hideout {interaction.Entry.Player}"
            });
            interaction.LastChatConsoleCommand = GameClientCommandTypeEnum.Hideout;
        };

        public static Action<IPoeTradeInteraction> Wait => interaction =>
        {
            PoeClient.GetInstance().SendChatMessages(new[]
            {
                $@"@{interaction.Entry.Player} Busy at the moment"
            });
            interaction.LastChatConsoleCommand = GameClientCommandTypeEnum.Wait;
        };

        public static Action<IPoeTradeInteraction> Tygl => interaction =>
        {
            PoeClient.GetInstance().SendChatMessages(new[]
            {
                $@"@{interaction.Entry.Player} Thank you, good luck!",
                $@"/kick {interaction.Entry.Player}"
            });
            interaction.LastChatConsoleCommand = GameClientCommandTypeEnum.TYGL;
        };

        #endregion
    }

    public enum GameClientCommandTypeEnum
    {
        None,
        Wait,
        Invite,
        Trade,
        // ReSharper disable once InconsistentNaming
        TYGL,
        NoStock,
        Decline,
        Kick,
        Whois,
        Hideout
    }
}