# ConsoleDungeonCrawler


This is an attempt to make a Dos style Console Dungeon game.  It is designed for Windows Terminal, uses extended (24 bit) color, and is designed for a screen resolution of 1920x1080.

The licensing on this is MIT, as it is to be used as a learning tool, and is open for others to use as they desire, with appropriate attribution.  Portions of this code are AI Generated using Copilot.

The Idea is to create a map using a text file template (included) and to load the map from file.

It uses 2 grids from dictionaries with an outer x key and an inner y key.  the Map grid controls the map layout (physical structure)  the Overlay grid is for placing objects that can be or will move around the map.

It uses a basic turn system driven by the player keystroke choices.

It has an armor system, inventory system class types and weapon types.  it includes melee and ranged attacks.  there is a legend that shows the visible overlay objects and a status section that displays armour, items, spells and player stats.

Finally there is a notification system that will scroll, logging all activity in text form.  it is color coded for ease of reading.

Suggestions are welcome!
