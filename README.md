# Hearthstone

## Time

Spent time: 7 hours 25 minutes.

## Why .net core

- it is cross-platform (I used mac os for development, you probably will run it on Windows or Linux);
- great integration with command line (allow to use lightweight IDE like VS Code).

## Project structure

1. Library: all about cards. It is the separate module, because of a lot of about cards here.
2. Game: main module contain all game rules and players' interactions.
3. TextUi: Simple text interface.
4. ConsoleRunner: Allow to run the game from console.
5. Test: Unit Tests.

3 and 4 can be replaced with graphical UI and the 1 and 2 still will be the same, it allow me to reach portability (but 3 and are separate modules, because of you can run text game in browser for example, not in console).

## Workflow

The entry point is Battle class. It create 2 players, their decks, hands and randomly select who will be the start first. Then every player select actions at own turn: he allowed to select the card, end turn or surrender.

## Implementation details

- The cards definitions should be in json: it allow add new cards and not rebuild the game! I think, it can be frequent use case.

- For all classes I tried maximally close the visibility of class members for internal security, but I opened some parts to be allow clearly unit tested.

- I covered 100% code with unit tests for core modules: Library and Game, for the rest I simply didn't have time to cover and it is not so significant, because of to test the rest is more about e2e and smoke instead of unit.