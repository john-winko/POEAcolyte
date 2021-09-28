using System.Text.RegularExpressions;

// ReSharper disable CommentTypo

namespace PoeAcolyte.API.Parsers
{
    /// <summary>
    ///     Regex arrays bounced against client.txt file for matches in multiple languages
    /// </summary>
    public static class PoeRegex
    {
        /// <summary>
        ///     REGEX to determine if @From is found (meaning a whisper from someone) <br></br>
        ///     (?:From|De|От кого|จาก|Von|Desde|수신|來自) (?&lt;Player&gt;.*?): (?&lt;Other&gt;.*)
        ///     <value>Player, Other</value>
        /// </summary>
        public static readonly Regex WhisperFrom =
            new(@"@(?:From|De|От кого|จาก|Von|Desde|수신|來自) (?<Player>.*?): (?<Other>.*)");

        /// <summary>
        ///     REGEX to determine if @To is found (meaning a whisper to someone) <br></br>
        ///     (?:To|À|An|Para|Кому|ถึง|발신|向) (?&lt;Player&gt;.*?): (?&lt;Other&gt;.*)
        ///     <value>Player, Other</value>
        /// </summary>
        public static readonly Regex WhisperTo =
            new(@"@(?:To|À|An|Para|Кому|ถึง|발신|向) (?<Player>.*?): (?<Other>.*)");

        /// <summary>
        ///     REGEX for guild and player name <br></br>
        ///     &lt;(?&lt;Guild&gt;.*?)&gt; (?&lt;Player&gt;.*?):
        ///     <value>Guild, Player</value>
        /// </summary>
        public static readonly Regex Guild = new(@"<(?<Guild>.*?)> (?<Player>.*?): ");

        /// <summary>
        ///     Regex used as a catch all <br></br>
        ///     (.*?)] (?&lt;Message&gt;.*)
        ///     <value>Message</value>
        /// </summary>
        public static readonly Regex SystemMessage = new(@"(.*?)] (?<Message>.*)");

        /// <summary>
        ///     REGEX for Regular Priced trades - will need to parse <see cref="StashTabList" /><br></br>
        ///     (.*)Hi, I would like to buy your (?&lt;Item&gt;.*) listed for (?&lt;PriceAmount&gt;\d+) (?&lt;PriceUnit&gt;.*) in
        ///     (?&lt;League&gt;.*?)\(
        ///     <value>Item, PriceAmount, PriceUnit, League</value>
        /// </summary>
        public static readonly Regex[] PricedTradeList =
        {
            /* ENG */
            new(
                @"(.*)Hi, I would like to buy your (?<Item>.*) listed for (?<PriceAmount>\d*\.?\d*) (?<PriceUnit>.*) in (?<League>.*?)\("),
            /* RUS */
            new(
                @"(.*)Здравствуйте, хочу купить у вас (?<Item>.*) за (?<PriceAmount>\d*\.?\d*) (?<PriceUnit>.*) в лиге (?<League>.*?)\("),
            /* POR */
            new(
                @"(.*)Olá, eu gostaria de comprar o seu item (?<Item>.*) listado por (?<PriceAmount>\d*\.?\d*) (?<PriceUnit>.*) na (?<League>.*?)\("),
            /* THA */
            new(
                @"(.*)สวัสดี, เราต้องการจะชื้อของคุณ (?<Item>.*) ใน ราคา (?<PriceAmount>\d*\.?\d*) (?<PriceUnit>.*) ใน (?<League>.*?)\("),
            /* GER */
            new(
                @"(.*)Hi, ich möchte '(?<Item>.*)' zum angebotenen Preis von (?<PriceAmount>\d*\.?\d*) (?<PriceUnit>.*) in der '(?<League>.*?)'-Liga kaufen(.*)"),
            /* FRE */
            new(
                @"(.*)Bonjour, je souhaiterais t'acheter (?<Item>.*) pour (?<PriceAmount>\d*\.?\d*) (?<PriceUnit>.*) dans la ligue (?<League>.*?)"),
            /* SPA */
            new(
                @"(.*)Hola, quisiera comprar tu (?<Item>.*) listado por (?<PriceAmount>\d*\.?\d*) (?<PriceUnit>.*) en (?<League>.*?)"),
            /* KOR */
            new(
                @"(.*)안녕하세요, (?<League>.*?)에 (?<PriceAmount>\d*\.?\d*) (?<PriceUnit>.*)\(으\)로 올려놓은 (?<Item>.*)\(을\)를 구매하고 싶습니다(.*)"),
            /* TWN */
            new(@"(.*)你好，我想購買 (?<Item>.*) 標價 (?<PriceAmount>\d*\.?\d*) (?<PriceUnit>.*) 在 (?<League>.*?)"),
            /* DB  */
            new(@"(.*)您好，我想買在 (?<League>.*?) 的 (?<Item>.*) 價格 (?<PriceAmount>\d*\.?\d*) (?<PriceUnit>.*)") // TWN POEdb
        };

        /// <summary>
        ///     REGEX for stash tab info <br></br>
        ///     \(stash tab &quot;&quot;(?&lt;StashTab&gt;.*)&quot;&quot;; position: left (?&lt;Left&gt;\d+), top (?&lt;Top&gt;
        ///     \d+)\)(?&lt;Other&gt;.*)
        ///     <value>StashTab, Left, Top, Other</value>
        /// </summary>
        public static readonly Regex[] StashTabList =
        {
            /* ENG */
            new(@"\(stash tab ""(?<StashTab>.*)""; position: left (?<Left>\d+), top (?<Top>\d+)\)(?<Other>.*)"),
            /* RUS */
            new(@"\(секция ""(?<StashTab>.*)""; позиция: (?<Left>\d+) столбец, (?<Top>\d+) ряд\)(?<Other>.*)"),
            /* POR */
            new(@"\(aba do baú: ""(?<StashTab>.*)""; posição: esquerda (?<Left>\d+), topo (?<Top>\d+)\)(?<Other>.*)"),
            /* THA */
            // Top position is bugged from GGG side and appears as {{TOP}) for priced items, so we use (.*) instead of (\d+)
            new(@"\(stash tab ""(?<StashTab>.*)""; ตำแหน่ง: ซ้าย (?<Left>\d+), บน (?<Top>.*)\)(?<Other>.*)"),
            /* GER */
            new(
                @"\(Truhenfach ""(?<StashTab>.*)""; Position: (?<Left>\d+). von links, (?<Top>\d+). von oben\)(?<Other>.*)"),
            /* FRE */
            new(
                @"\(onglet de réserve ""(?<StashTab>.*)"" \; (?<Left>\d+)e en partant de la gauche, (?<Top>\d+)e en partant du haut\)(?<Other>.*)"),
            /* SPA */
            new(
                @"\(pestaña de alijo ""(?<StashTab>.*)""; posición: izquierda(?<Left>\d+), arriba (?<Top>\d+)\)(?<Other>.*)"),
            /* KOR */
            new(@"\(보관함 탭 ""(?<StashTab>.*)"", 위치: 왼쪽 (?<Left>\d+), 상단 (?<Top>\d+)\)(?<Other>.*)"),
            /* TWN */
            new(@"\(倉庫頁 ""(?<StashTab>.*)""; 位置: 左 (?<Left>\d+), 上 (?<Top>\d+)\)(?<Other>.*)"),
            /* DB  */ // TWN POEdb
            new(@"\[倉庫:(?<StashTab>.*) 位置: 左(?<Left>\d+), 上 (?<Top>\d+)\)(?<Other>.*)")
        };

        /// <summary>
        ///     Regex for unpriced trades (excluding bulk) - will need to parse <see cref="StashTabList" /><br></br>
        ///     (.*)Hi, I would like to buy your (?&lt;Item&gt;.*) in (?&lt;League&gt;.*?)\(
        ///     <value>Item, League</value>
        /// </summary>
        public static readonly Regex[] UnpricedTradeList =
        {
            /* ENG */ new(@"(.*)Hi, I would like to buy your (?<Item>.*) in (?<League>.*?)\("),
            /* RUS */
            new(@"(.*)Здравствуйте, хочу купить у вас (?<Item>.*) в лиге (?<League>.*?)"),
            /* POR */
            new(@"(.*)Olá, eu gostaria de comprar o seu item (?<Item>.*) na (?<League>.*?)"),
            /* THA */ new(@"(.*)สวัสดี, เราต้องการจะชื้อของคุณ (?<Item>.*) ใน (?<League>.*?)"),
            /* GER */
            new(@"(.*)Hi, ich möchte '(?<Item>.*)' in der '(?<League>.*?)'-Liga kaufen(.*)"),
            /* FRE */
            new(@"(.*)Bonjour, je souhaiterais t'acheter (?<Item>.*) dans la ligue (?<League>.*?)"),
            /* SPA */ new(@"(.*)Hola, quisiera comprar tu (?<Item>.*) en (?<League>.*?)"),
            /* KOR */ new(@"(.*)안녕하세요, (?<League>.*?)에 올려놓은 (?<Item>.*)\(을\)를 구매하고 싶습니다(.*)"),
            /* TWN */ new(@"(.*)你好，我想購買 (?<Item>.*) 在 (?<League>.*?)"),
            /* DB  */ new(@"(.*)您好，我想買在 (?<League>.*?) 的 (?<Item>.*)") // TWN POEdb
        };

        /// <summary>
        ///     REGEX for bulk (currency) trades - will not contain stash info<br></br>
        ///     (.*)Hi, I&apos;d like to buy your (?&lt;SellAmount&gt;\d+) (?&lt;SellUnits&gt;.*?) for my (?&lt;BuyAmount&gt;\d+)
        ///     (?&lt;BuyUnits&gt;.*?) in (?&lt;League&gt;.*).(?&lt;Other&gt;.*)
        ///     <value>(int)SellAmount, SellUnits, (int)BuyAmount, BuyUnits, League, Other</value>
        /// </summary>
        public static readonly Regex[] BulkTradeList =
        {
            /* ENG */
            new(
                @"(.*)Hi, I'd like to buy your (?<SellAmount>\d*\.?\d*) (?<SellUnits>.*?) for my (?<BuyAmount>\d*\.?\d*) (?<BuyUnits>.*?) in (?<League>.*).(?<Other>.*)"),
            /* RUS */
            new(
                @"(.*)Здравствуйте, хочу купить у вас (?<SellAmount>\d*\.?\d*) (?<SellUnits>.*?) за (?<BuyAmount>\d*\.?\d*) (?<BuyUnits>.*?) в лиге (?<League>.*).(?<Other>.*)"),
            /* POR */
            new(
                @"(.*)Olá, eu gostaria de comprar seu\(s\) (?<SellAmount>\d*\.?\d*) (?<SellUnits>.*?) pelo\(s\) meu\(s\) (?<BuyAmount>\d*\.?\d*) (?<BuyUnits>.*?) na (?<League>.*).(?<Other>.*)"),
            /* THA */
            new(
                @"(.*)สวัสดี เรามีความต้องการจะชื้อ (?<SellAmount>\d*\.?\d*) (?<SellUnits>.*?) ของคุณ ฉันมี (?<BuyAmount>\d*\.?\d*) (?<BuyUnits>.*?) ใน (?<League>.*).(?<Other>.*)"),
            /* GER */
            new(
                @"(.*)Hi, ich möchte '(?<SellAmount>\d*\.?\d*) (?<SellUnits>.*?)' zum angebotenen Preis von '(?<BuyAmount>\d*\.?\d*) (?<BuyUnits>.*?)' in der '(?<League>.*).(?<Other>.*)'-Liga kaufen(.*)"),
            /* FRE */
            new(
                @"(.*)Bonjour, je voudrais t'acheter (?<SellAmount>\d*\.?\d*) (?<SellUnits>.*?) contre (?<BuyAmount>\d*\.?\d*) (?<BuyUnits>.*?) dans la ligue (?<League>.*).(?<Other>.*)"),
            /* SPA */
            new(
                @"(.*)Hola, me gustaría comprar tu\(s\) (?<SellAmount>\d*\.?\d*) (?<SellUnits>.*?) por mi (?<BuyAmount>\d*\.?\d*) (?<BuyUnits>.*?) en (?<League>.*).(?<Other>.*)"),
            /* KOR */
            new(
                @"(.*)안녕하세요, (?<League>.*).?에 올려놓은(?<SellAmount>\d*\.?\d*) (?<SellUnits>.*?)\(을\)를 제 (?<BuyAmount>\d*\.?\d*) (?<BuyUnits>.*?)\(으\)로 구매하고 싶습니다(?<Other>.*)"),
            /* TWN */
            new(
                @"(.*)你好，我想用 (?<SellAmount>\d*\.?\d*) (?<SellUnits>.*?) 購買 (?<BuyAmount>\d*\.?\d*) (?<BuyUnits>.*?) in (?<League>.*).(?<Other>.*)"),
            /* DB  */ // POEdb
            new(
                @"(.*)您好，我想買在 (?<League>.*).? 的 (?<SellAmount>\d*\.?\d*) (?<SellUnits>.*?) 個 (.*) 直購價 (?<BuyAmount>\d*\.?\d*) (?<BuyUnits>.*(?<Other>.*))")
        };

        /// <summary>
        ///     Regex if player joins an area <br></br>
        ///     (.*) : (?&lt;Player&gt;.*?) has joined the area.*
        ///     <value>Player</value>
        /// </summary>
        public static readonly Regex[] AreaJoinedList =
        {
            /* ENG */ new(@"(.*) : (?<Player>.*?) has joined the area.*"),
            /* FRE */ new(@"(.*) : (?<Player>.*?) a rejoint la zone.*"),
            /* GER */ new(@"(.*) : (?<Player>.*?) hat das Gebiet betreten.*"),
            /* POR */ new(@"(.*) : (?<Player>.*?) entrou na área.*"),
            /* RUS */ new(@"(.*) : (?<Player>.*?) присоединился.*"),
            /* THA */ new(@"(.*) : (?<Player>.*?) เข้าสู่พื้นที่.*"),
            /* SPA */ new(@"(.*) : (?<Player>.*?) se unió al área.*"),
            /* KOR */ new(@"(.*) : (?<Player>.*?)(이)가 구역에 들어왔습니다.*"),
            /* TWN */ new(@"(.*) : (?<Player>.*?) 進入了此區域.*")
        };

        /// <summary>
        ///     Regex if a player leaves an area <br></br>
        ///     (.*) : (?&lt;Player&gt;.*?) has left the area.*
        ///     <value>Player</value>
        /// </summary>
        public static readonly Regex[] AreaLeftList =
        {
            /* ENG */ new(@"(.*) : (?<Player>.*?) has left the area.*"),
            /* FRE */ new(@"(.*) : (?<Player>.*?) a quitté la zone.*"),
            /* GER */ new(@"(.*) : (?<Player>.*?) hat das Gebiet verlassen.*"),
            /* POR */ new(@"(.*) : (?<Player>.*?) saiu da área.*"),
            /* RUS */ new(@"(.*) : (?<Player>.*?) покинул область.*"),
            /* THA */ new(@"(.*) : (?<Player>.*?) ออกจากพื้นที่.*"),
            /* SPA */ new(@"(.*) : (?<Player>.*?) abandonó el área.*"),
            /* KOR */ new(@"(.*) : (?<Player>.*?)(이)가 구역에서 나갔습니다.*"),
            /* TWN */ new(@"(.*) : (?<Player>.*?) 離開了此區域.*")
        };

        /// <summary>
        ///     Regex if you enter an area <br></br>
        ///     (.*) : You have entered (?&lt;Area&gt;.*?).
        ///     <value>Area</value>
        /// </summary>
        public static readonly Regex[] YouJoinList =
        {
            /* ENG */ new(@"(.*) : You have entered (?<Area>.*).")
            // TODO add language specific regex
        };

        // TODO add afk regex
    }
}