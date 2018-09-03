# Bowling

Bowling console app.  

Scores and frames are held in a dictionary of <int>, int[] where the key is the frame number and the values in the array are the number of pins knocked down in each frame.
  
  The game is made up of a class called "Game" which has the methods Roll & Score.  Score is only called at the end of the game and calculates the total score.  In addition there is a public method to return the number of pins remaining in the current frame which is used by the console app to ensure that the total roll cannot exceed 10.
  
  There are two private methods to calculate the bonuses.
  
  There is a seperate test project to hold the tests.
