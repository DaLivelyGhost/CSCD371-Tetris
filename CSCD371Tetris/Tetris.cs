using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Navigation;

namespace CSCD371Tetris
{
    class Tetris
    {
        public int[,] playField;                //array that serves as the playfield
                                                    //Positive numbers in the playfield is the current falling tetromino
                                                    //Negative numbers are blocks that have already fallen
                                                    //0 is empty space

        private Tetromino[] upcoming;           //array to hold the upcoming tetrominos
        private Random upcomingRand;            //randomly selects tetrominos for the upcoming list
        private Tetromino currTetromino;
        public bool active { get; set; }        //simple marker to stop the game from dropping more than one block at a time
        public bool isFalling { get; set; }
        
        int tetrominoTop;                       //Bounding box that will keep track of the coordinates of the falling tetromino.
        int tetrominoLeft;                          //Mainly just for performance purposes.
        int currRotation;                      //Keeps track of which sprite to show.
        int xDisplacement;                      //Because tetrominos are bounded by a 4x4 box, I need to keep track of a tetrominos position within that box.
                                                //This is for when the player rotates the tetromino, I can know where to replace the sprite at.
        int level;                              //I actually don't need level here, it's handled in the main window. It's only here for scoring purposes.

        public int score { get; set; }          //the player's score
        public int linesToClear { get; set; }          //the amount of lines that the player needs to advance.
        public bool moveLeft { get; set; }                  //These bools serve as toggles for player input. This lets me control user input    
        public bool moveRight { get; set; }                 //to operate at the same tick rate the game is moving at.
        public bool rotateLeft { get; set; }
        public bool rotateRight { get; set; }
        public bool playerDrop { get; set; }

        //Constructor
        public Tetris(int lines, int newLevel)
        {
            upcoming = new Tetromino[4];
            playField = new int[10, 18];
            score = 0;
            linesToClear = lines;
            level = newLevel;
        }
        //Getters
        public Tetromino[] getUpcoming()
        {
            return upcoming;
        }
        //private working method. didn't want to rewrite a switch statement
        //so I made it a method.
        public void nextLevel(int lines)
        {
            upcoming = new Tetromino[4];
            playField = new int[10, 18];
            linesToClear = lines;
            level++;
        }
        private Tetromino getRandomTetromino()
        {
            int temp = upcomingRand.Next(0, 7);
            Tetromino toReturn = new Tetromino();
            switch (temp)
            {
                case 0:
                    toReturn = new Luigi_Tetromino();
                    break;
                case 1:
                    toReturn = new Waluigi_Tetromino();
                    break;
                case 2:
                    toReturn = new S_Tetromino();
                    break;
                case 3:
                    toReturn = new Backward_S_Tetromino();
                    break;
                case 4:
                    toReturn = new T_Tetromino();
                    break;
                case 5:
                    toReturn = new Square_Tetromino();
                    break;
                case 6:
                    toReturn = new Line_Tetromino();
                    break;
            }
            return toReturn;
        }
        //Setters
        public void setUpcoming()
        {
            upcomingRand = new Random();

            for (int i = 0; i < upcoming.Length; i++)
            {
                upcoming[i] = getRandomTetromino();
            }
        }
        //-----------------------------------------------------------------------
        public bool dropUpcoming()
        {
            currRotation = 0;
            int obstructionY = 4; //this int will get the relative y value of any blocks that might be in the way of placing a tetromino
                                  //The starting value is 4 because that is out of range of the to be placed tetromino.
                                  //Any obstructing blocks will have a smaller value.

            int obstructionX = 0;
            int toMoveUp = 0;       //the value of which to move the tetromino upward      
            //check for obstructions
            for(int i = 2; i < 6; i++) //horizontal values
            {
                for(int j = 0; j < 4; j++)//vertical values
                {
                    if(playField[i,j] < 0)
                    {
                        
                        if(j < obstructionY)
                        {
                            obstructionY = j;
                        }
                        
                        obstructionX = i - 2;

                        if (upcoming[0].Spritesheet[j, currRotation, i - 2] > 0)
                        {
                            toMoveUp = 4 - obstructionY;
                        }
                    }
                }
            }
         
            //draw the now dropped block
            for(int i = 2; i < 6; i++) //i is horizontal
            {
                for(int j = 0; j < 4; j++)  //j is vertical
                {
                    if (upcoming[0].Spritesheet[j, currRotation, i - 2] > 0)
                    {

                        try
                        {
                            playField[i, j - toMoveUp] = upcoming[0].Spritesheet[j, currRotation, i - 2];

                        }
                        catch (Exception)
                        {
                            return false;
                            
                        }
                    }  
                }
            }

            //set bounding box
            currTetromino = upcoming[0];
            tetrominoTop = 0;
            tetrominoLeft = 2;
            xDisplacement = 0;
            currRotation = 0;

            //advance the queue
            popUpcoming();
            active = true;
            isFalling = true;

            return true;
        }
        private void popUpcoming()
        {
            upcoming[0] = upcoming[1];
            upcoming[1] = upcoming[2];
            upcoming[2] = upcoming[3];
            upcoming[3] = getRandomTetromino();
        }
        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------
        public void moveBlocks()
        {
            if (active == true)
            {
                //the player wants to move left
                if (moveLeft == true)
                {
                    //First we need to check if we can even move left.
                    bool canMoveLeft = true;
                    for(int i = tetrominoLeft; i <= tetrominoLeft + 3; i++)
                    {
                        for(int j = tetrominoTop; j <= tetrominoTop + 3; j++)
                        {
                            if(playField[i,j] > 0)
                            {
                                if(i == 0)
                                {
                                    //tetromino is already on the side of the screen
                                    canMoveLeft = false;
                                }
                                else
                                {
                                    if(playField[i - 1, j] < 0)
                                    {
                                        //there is an obstruction
                                        canMoveLeft = false;
                                    }
                                }
                            }
                        }
                    }
                    if(canMoveLeft) { 
                                 //Look only in the bounding box around the current tetromino.
                                 //Hopefully will help performance in the long run.
                        for (int i = tetrominoLeft; i <= tetrominoLeft + 3; i++)
                        {
                            for (int j = tetrominoTop; j <= tetrominoTop + 3; j++) 
                            {
                                //if the sprite sheet occupies this tile
                                if (playField[i, j] > 0)
                                {
                                    //if the tile to the left is unoccupied
                                    //This may feel like excessive authentication, but I don't want 
                                    //tetrominos collapsing in on themselves
                                    if (playField[i - 1, j] == 0)
                                    {
                                        playField[i - 1, j] = playField[i, j];
                                        playField[i, j] = 0;
                                    }
                                }
                            }

                        }
                        //update the x coordinate of the bounding box
                        if (tetrominoLeft > 0)
                        {
                            tetrominoLeft--;
                        }
                        else
                        {
                            //if the tetromino moved left, but the bounding box was all the way left
                            xDisplacement--;
                        }
                    }
                }
                if (moveRight == true)
                {
                    //First we need to check if we can even move right.
                    bool canMoveRight = true;
                    for (int i = tetrominoLeft; i <= tetrominoLeft + 3; i++)
                    {
                        for (int j = tetrominoTop; j <= tetrominoTop + 3; j++)
                        {
                            if (playField[i, j] > 0)
                            {
                                if (i == 9)
                                {
                                    //tetromino is already on the side of the screen
                                    canMoveRight = false;
                                }
                                else
                                {
                                    if (playField[i + 1, j] < 0)
                                    {
                                        //there is an obstruction
                                        canMoveRight = false;
                                    }
                                }
                            }
                        }
                    }
                    if (canMoveRight)
                    {
                        //Look only in the bounding box around the current tetromino.
                        //Hopefully will help performance in the long run.
                        for (int i = tetrominoLeft + 3; i >= tetrominoLeft; i--)
                        {
                            for (int j = tetrominoTop; j <= tetrominoTop + 3; j++)
                            {
                                //if the sprite sheet occupies this tile
                                if (playField[i, j] > 0)
                                {
                                    //if the tile to the right is unoccupied
                                    //This may feel like excessive authentication, but I don't want 
                                    //tetrominos collapsing in on themselves
                                    if (playField[i + 1, j] == 0)
                                    {
                                        playField[i + 1, j] = playField[i,j];
                                        playField[i, j] = 0;
                                    }
                                }
                            }

                        }
                        //Update the bounding box x coordinate
                        if (tetrominoLeft < 6)
                        {
                            tetrominoLeft++;
                        }
                        else
                        {
                            //if the bounding box is all the way to the right, but the tetromino moved within it
                            xDisplacement++;
                        }

                    }

                }
                moveLeft = false;
                moveRight = false;
            }
           
        }
        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------

        //fun fact: this method used to be twice as long
        public void rotateBlocks()
        {
            if(active == true)
            {
                //since rotateBlocks() is always called, I need to stop the method from running if the player does not want to rotate
                if(rotateLeft || rotateRight)
                {
                    if (rotateLeft == true)
                    {
                        currRotation -= 1;

                        if(currRotation == -1)
                        {
                            currRotation = 3;
                        }
                    }
                    else
                    {
                        currRotation += 1;

                        if (currRotation == 4)
                        {
                            currRotation = 0;
                        }
                    }

                    bool canRotate = true;
                    bool oneHigher = false;
                    
                    //Fist check if there is an obstruction
                    for (int i = tetrominoLeft; i <= tetrominoLeft + 3; i++)
                    {
                        for (int j = tetrominoTop; j <= tetrominoTop + 3; j++)
                        {
                            if (playField[i, j] < 0 && currTetromino.Spritesheet[j - tetrominoTop, currRotation, i - tetrominoLeft] > 0)
                            {
                                canRotate = false;
                            }
                        }
                    }
                    
                    //if there is an obstruction, try 1 block higher.
                    if (!canRotate)
                    {
                        canRotate = true;
                        int newHeight = tetrominoTop - 1;

                        for (int i = tetrominoLeft; i <= tetrominoLeft + 3; i++)
                        {
                            for (int j = newHeight; j <= newHeight + 3; j++)
                            {
                                try
                                {

                                    if (playField[i, j] < 0 && currTetromino.Spritesheet[j - newHeight, currRotation, i - tetrominoLeft] > 0)
                                    {
                                        canRotate = false;
                                    }
                                }
                                catch(Exception e)
                                {

                                }
                            }
                        }
                        if (canRotate)
                        {
                            oneHigher = true;
                        }
                    }
                    //if both test fail, deny the rotation
                    if (canRotate)
                    {
                        //clear the bounding box of the tetromino
                        for (int i = tetrominoLeft; i <= tetrominoLeft + 3; i++)
                        {
                            for (int j = tetrominoTop; j <= tetrominoTop + 3; j++)
                            {

                                if (playField[i, j] > 0)
                                {
                                    playField[i, j] = 0;
                                }
                            }
                        }
                        //hot swap in the rotation sprite
                        for (int i = tetrominoLeft; i <= tetrominoLeft + 3; i++)
                        {
                            for (int j = tetrominoTop; j <= tetrominoTop + 3; j++)
                            {

                                if (currTetromino.Spritesheet[j - tetrominoTop, currRotation, i - tetrominoLeft] > 0)
                                {
                                    try
                                    {
                                        if (oneHigher)
                                        {

                                            playField[i, j - 1] = currTetromino.Spritesheet[j - tetrominoTop, currRotation, i - tetrominoLeft];
                                        }
                                        else
                                        {
                                            playField[i, j] = currTetromino.Spritesheet[j - tetrominoTop, currRotation, i - tetrominoLeft];
                                        }
                                    }
                                    catch (Exception e)
                                    {

                                    }
                                }
                            }
                        }
                    }
                }               
            }
            rotateLeft = false;
            rotateRight = false;
        }
        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------
        //called when the game is in session and the player presses space. Drops the block as far as it can.
        public void superDrop() 
         {
            //This method's gonna search from the bottom up for the lowest possible place to put the active tetromino
            //and then place it.

            int newHeight = tetrominoTop;         //the height of the bounding box of where the block will be placed
            bool canBePlaced = true;              //marks if the current location is able to place the tetromino

            //first find the lowest y value to drop the block
            while(canBePlaced && newHeight < 15)
            {
                for (int i = tetrominoLeft; i <= tetrominoLeft + 3; i++)
                {
                    for (int j = newHeight + 3; j >= newHeight; j--)
                    {
                        //if the tetromino has moved within the bounding box
                        try
                        {
                            if(xDisplacement < 0)
                            {
                                if (playField[i, j] < 0 && currTetromino.Spritesheet[j - newHeight, currRotation, i - tetrominoLeft - xDisplacement] > 0)
                                {
                                    canBePlaced = false;
                                }
                            }
                            else
                            {
                                if (playField[i, j] < 0 && currTetromino.Spritesheet[j - newHeight, currRotation, i - tetrominoLeft - xDisplacement] > 0)
                                {
                                    canBePlaced = false;
                                }
                            }


                        }
                        catch(Exception e)
                        {
                            //weirdly enough just catching the exception makes it work.
                            
                        }

                    }
                }
                //if no obstructions, go down 1 layer
                if(canBePlaced)
                {
                    newHeight++;
                    
                }
                //if there is an obstruction, go up 1 layer and break the while loop
                else
                {
                    canBePlaced = true;
                    newHeight--;
                    break;
                }
            }
            if(newHeight == 15) {
                newHeight--;
            }
            //clear the current tetromino
            for(int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 18; j++)
                {
                    if(playField[i,j] > 0)
                    {
                        playField[i, j] = 0;
                    }
                }
            }
            //place it at the new y coordinate
            if(canBePlaced)
            {
                    for (int i = tetrominoLeft; i <= tetrominoLeft + 3; i++)
                    {
                        for (int j = newHeight + 3; j >= newHeight; j--)
                        {
                            try
                            {
                                if (currTetromino.Spritesheet[j - newHeight, currRotation, i - tetrominoLeft] > 0)
                                    playField[i + xDisplacement, j] = currTetromino.Spritesheet[j - newHeight, currRotation, i - tetrominoLeft];
                            }
                            catch(Exception e)
                            {
                                //don't really need to do anything here.
                            }
                        }
                    }
                    playerDrop = false;
                    return;

                
            }
            playerDrop = false;
            isFalling = false;
            active = false;
        }
        //------------------------------------------------------------------------------------------------------------------------
        //------------------------------------------------------------------------------------------------------------------------
        public void checkIfFalling()
        {
            //checks if the current falling tetromino is on top of anything. If it is, the falling stops.
            isFalling = true;

            for (int i = tetrominoLeft; i <= tetrominoLeft + 3; i++)
            {
                for (int j = tetrominoTop + 3; j >= tetrominoTop; j--) //start from the bottom up
                {
                    if(playField[i,j] > 0)
                    {
                        if(j == 17 || playField[i,j + 1] < 0)
                        {
                            isFalling = false;
                        }
                    }
                }
            }
        }
        public bool checkIfLost()
        {
            for (int i = 0; i < 10; i++)
            {
                if (playField[i, 0] < 0)
                {
                    return true;
                }
            }
            return false;
        }
        public void gravity()
            //the method for making a block fall 1 block per timer tick.
        {
            checkIfFalling();

            if (isFalling == true)
            {
                //Look only in the bounding box around the current falling tetromino.
                //Hopefully will help performance in the long run.
                for (int i = tetrominoLeft; i <= tetrominoLeft + 3; i++)
                {
                    for (int j = tetrominoTop + 3; j >= tetrominoTop; j--) //start from the bottom up
                    {
                        if (playField[i, j] > 0)
                        {
                            if (j < 17 && playField[i, j + 1] == 0)
                            {
                                playField[i, j + 1] = playField[i, j];
                                playField[i, j] = 0;
                                
                            }
                        }
                    }

                }
                //keep the bounding box from falling  off the bottom of the playfield
                if (tetrominoTop < 14)
                {
                    tetrominoTop += 1;
                }
            }
        }
        //Along with deactivating the current active tetromino, this will also clear any lines of blocks for points
        public void deactivateBlocks()
        {
            active = false;
            int lineCounter = 0;    //if this reaches 10, clear the line.
            ArrayList clearedLines = new ArrayList(); //gotta keep track of what lines were cleared.

            for (int j = 0; j < 18; j++)
            {
                for (int i = 0; i < 10; i++)
                {
                    if(playField[i,j] > 0)
                    {
                        playField[i, j] *= -1;
                    }
                    if(playField[i,j] != 0)
                    {
                        lineCounter++;

                        if(lineCounter == 10)
                        {
                            clearLine(j);
                            clearedLines.Add(j);
                            lineCounter = 0;
                            linesToClear--;
                        }
                    }
                }
                
                lineCounter = 0; //reset the linecounter after each line

            }
            //sort the temporary array in ascending order
            clearedLines.Sort();

            //drop the lines above the cleared ones
            replaceClearedLine(clearedLines);

            //inrement score
            switch(clearedLines.Count)
            {
                case 0:
                    break;
                case 1:
                    score += 40 * (level + 1);
                    break;
                case 2:
                    score += 100 * (level + 1);
                    break;
                case 3:
                    score += 300 * (level + 1);
                    break;
                case 4:
                    score += 1200 * (level + 1);
                    break;

            }

        }
        private void clearLine(int lineToClear)
        {
            //clear the line
            for(int i = 0; i < 10; i++)
            {
                playField[i, lineToClear] = 0;
            }

        }
        private void replaceClearedLine(ArrayList clearedLines)
        {
            //move all above lines down 1
            foreach (int row in clearedLines)
            {
                for (int j = row; j > 0; j--)
                {
                    for(int i = 0; i < 10; i++)
                    {
                        playField[i, j] = playField[i, j - 1];
                    }
                }
            }
        }

    }
}
