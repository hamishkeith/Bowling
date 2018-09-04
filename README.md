# Bowling

Bowling console app.  

Scores and frames are held in a dictionary of <int>, int[] where the key is the frame number and the values in the array are the number of pins knocked down in each frame.
  
  The game is made up of a class called "Game" which has the methods Roll & Score.  Score called automatically at the end of the game only and calculates the total score.  In addition there is a public method to return the number of pins remaining in the current frame which is used by the console app to ensure that the total roll cannot exceed 10.  An (unhandled) argument out of range error will be thrown if you try and knock down more then 10 pins in any frame ensure you use the check pins in frame method or wrap you roll method in try catch / contiue statement.
  
  There are two private methods to calculate the bonuses.
  
  There is a seperate test project to hold the tests.
