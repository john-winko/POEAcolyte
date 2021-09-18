# Planned Features / Specs

* Client.txt log monitoring
  * Captures game messages (whispers) which triggers trade interface/overlay in game
  * Only monitor while game client is open
* Game Client monitoring
  * Game client is open/closed
  * Bounds and Location of game client while opened
* In-Game trade interface/overlay
  * Displays incoming/outgoing trade requests
  * Trade interface or the interface elements will differ depending on
    * Trade is incoming/outgoing
    * Previous messages sent
    * Item is available
  * Zone based state information for single click functionality
    * (Inviting for trade if in hideout/town vs Asking to wait if not)
  * Quick action list - predefined
    * Initiate trade
    * Invite to party
    * Kick from party
    * Thank for trade
    * Check player status (/whois)
* In-Game item overlay
  * Click-through shaded area overlay where item should be located in game

# Future Features / Specs

* Integration with online APIs
  * OAuth authentication
  * Client caching of item database
  * Cataloging of trades
    * ML of trade properties for item evaluation
* Efficiency metrics
  * Time spent per map
  * Currency / Valuables per time spent
* Custom quick actions