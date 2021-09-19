using System;
using System.Collections.Generic;
using System.Linq;

namespace PoeAcolyte.API.Parsers
{
    public interface IPoeLogEntry
    {
        /// <summary>
        /// What type of log entry is detected in the client.txt
        /// <para>Based on the <see cref="Service.PoeLogReader"/> event</para>
        /// </summary>
        public enum PoeLogEntryTypeEnum
        {
            Whisper,
            PricedTrade,
            UnpricedTrade,
            BulkTrade,
            AreaJoined,
            AreaLeft,
            YouJoin,
            SystemMessage
        }

        /// <summary>
        /// Anything found after a standard trade message or any non trade message with @From:
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.PricedTradeList"/>
        /// <seealso cref="PoeRegex.UnpricedTradeList"/>
        /// <seealso cref="PoeRegex.BulkTradeList"/>
        /// <seealso cref="PoeRegex.WhisperTo"/>
        /// <seealso cref="PoeRegex.WhisperFrom"/>
        /// </summary>
        public string Other { get; set; }
        /// <summary>
        /// Player enters your area <br></br>
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.AreaJoinedList"/>
        /// </summary>
        public string Area { get; set; }
        /// <summary>
        /// Item being traded for
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.PricedTradeList"/>
        /// <seealso cref="PoeRegex.UnpricedTradeList"/>
        /// <seealso cref="PoeRegex.BulkTradeList"/>
        /// </summary>
        public string Item { get; set; }
        /// <summary>
        /// Top (Y) location of stash tab (1,1) being (left, top) most item
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.PricedTradeList"/>
        /// </summary>
        public int Top { get; set; }
        /// <summary>
        /// Left (X) location of stash tab (1,1) being (left, top) most item
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.PricedTradeList"/>
        /// </summary>
        public int Left { get; set; }
        /// <summary>
        /// [Optional] Guild name if specified
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.Guild"/>
        /// </summary>
        public string Guild { get; set; }
        /// <summary>
        /// Player Name
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.WhisperFrom"/>
        /// <seealso cref="PoeRegex.WhisperTo"/>
        /// <seealso cref="PoeRegex.Guild"/>
        /// <seealso cref="PoeRegex.AreaJoinedList"/>
        /// <seealso cref="PoeRegex.AreaLeftList"/>
        /// </summary>
        public string Player { get; set; }
        /// <summary>
        /// Name of stash tab item is located
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.StashTabList"/>
        /// </summary>
        public string StashTab { get; set; }
        /// <summary>
        /// Number of <see cref="PriceUnits"/> that player is paying
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.PricedTradeList"/>
        /// <seealso cref="PoeRegex.UnpricedTradeList"/>
        /// <seealso cref="PoeRegex.BulkTradeList"/>
        /// </summary>
        public int PriceAmount { get; set; }
        /// <summary>
        /// Unit of currency <see cref="PriceUnits"/>player is paying
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.PricedTradeList"/>
        /// <seealso cref="PoeRegex.UnpricedTradeList"/>
        /// <seealso cref="PoeRegex.BulkTradeList"/>
        /// </summary>
        public string PriceUnits { get; set; }
        /// <summary>
        /// Bulk trade - Number of <see cref="BuyPriceUnits"/> that player want
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.BulkTradeList"/>
        /// </summary>
        public int BuyPriceAmount { get; set; }
        /// <summary>
        /// Bulk trade - Unit of currency <see cref="BuyPriceAmount"/> that player wants
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.BulkTradeList"/>
        /// </summary>
        public string BuyPriceUnits { get; set; }
        /// <summary>
        /// League trade is requested on
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.PricedTradeList"/>
        /// <seealso cref="PoeRegex.UnpricedTradeList"/>
        /// <seealso cref="PoeRegex.BulkTradeList"/>
        /// </summary>
        public string League { get; set; }
        /// <summary>
        /// Incoming message (@From)
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.WhisperFrom"/>
        /// </summary>
        public bool Incoming { get; set; }
        /// <summary>
        /// Outgoing message (@To)
        /// <seealso cref="PoeRegex"/>
        /// <seealso cref="PoeRegex.WhisperTo"/>
        /// </summary>
        public bool Outgoing { get; set; }
        /// <summary>
        /// Date/Time of log entry as recorded in the client file (Parsed in the constructor of
        /// <see cref="PoeLogEntry"/>)
        /// </summary>
        public DateTime TimeStamp { get; set; }
        /// <summary>
        /// Raw string from log entry
        /// </summary>
        public string Raw { get; set; }
        /// <summary>
        /// Was log entry able to be parsed?
        /// </summary>
        public bool IsValid { get; set; }
        /// <summary>
        /// What type of log entry it is
        /// </summary>
        public PoeLogEntryTypeEnum PoeLogEntryType { get; set; }
        
        public static IEnumerable<IPoeLogEntry> ParseStrings(string input)
        {
            var inputArray = input.Split("\r\n");
            var ret = inputArray
                .Select(entry => new PoeLogEntry(entry))
                .Where(logEntry => logEntry.IsValid)
                .Cast<IPoeLogEntry>()
                .ToList();
            return ret;
        }
    }
}
