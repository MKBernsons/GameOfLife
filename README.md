# [Conway's game of life](https://en.wikipedia.org/wiki/Conway%27s_Game_of_Life)
## A console application project

### Iteration #1
1. The game is a console application
2. Simple navigation which allows you to select the size of the field
3. Game field needs to be updated by iteration each second

### Iteration #2
1. Informative representation on count of iterations (cycles) and count of live cells
2. Possibility to save information to file and restore it on application start
3. Possibility to stop the application at any moment

### Iteration #3
1. Possibility to execute 1000 games in parallel
2. Possibility to show 8 selected games on screen
3. Possibility to change which games are shown on screen
4. Possibility to save all games at once
5. Present real-time statistics on how many games and how many live cells we have in total

## Status
Iteration 1 | Iteration 2 | Iteration 3
------------|-------------|------------
1 done      |1 done       |1 done      
2 done      |2 done       |2 done      
3 done      |3 done       |3 done      
____________|____________ |4 done      
____________|____________ |5 done      

### Additional things to do (a reminder)
1. Redo the menu system -> Moved it to a seperate static User class and now the whole game can be started with a single "Start();"
2. clean up everything (Du uh) -> slowly but surely
3. configure gitignore -> ignores the saved games
4. maybe - delete all previous files when saving multiple games -> implemented so that if you play a thousand games and you save twice you dont get 2000 games saved...
5. Think about making the visualizer and gamemanager classes static and move the saving funcionality into a seperate class -> for now changed the gamemanager class to static
