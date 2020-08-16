using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CSCD371Tetris
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        ImageBrush block1Brush;
        ImageBrush block2Brush;
        ImageBrush block3Brush;
        ImageBrush block4Brush;
        ImageBrush block5Brush;
        ImageBrush block6Brush;
        ImageBrush block7Brush;

        private DispatcherTimer gameTimer = new DispatcherTimer();      //this timer handles behind the scenes game logic
        int gravityInterval;                                            //Controls the rate at which blocks fall. (will increase as time goes on)
        int currentRunningTime;                                         //Keeps track of the current running time. This is for making sure the gravityinterval goes off.
        int buffer;                                                     //Tetrominos don't immediately deactivate when they land. There's a buffer of a few frames
                                                                        //that the player can still rotate and move the tetromino.
        int level;
        int[] highScores;
        bool isPaused = false;
        bool isStarted = false;
        //Game Arrays
        private Tetris gameLogic;           //behind the scenes logic for tetris
        
        public MainWindow()
        {
            InitializeComponent();

            highScores = new int[3] { 0, 0, 0 };
            gameTimer.Interval = new TimeSpan(0, 0, 0, 0, 10);
            gameTimer.Tick += GameTimer_Tick;

            block1Brush = new ImageBrush();
            block1Brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/backwards_L_tile.png"));

            block2Brush = new ImageBrush();
            block2Brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/L_tile.png"));

            block3Brush = new ImageBrush();
            block3Brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/backwards_s_tile.png"));

            block4Brush = new ImageBrush();
            block4Brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/s_tile.png"));

            block5Brush = new ImageBrush();
            block5Brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/t_tile.png"));

            block6Brush = new ImageBrush();
            block6Brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/I_tile.png"));

            block7Brush = new ImageBrush();
            block7Brush.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/square_tile.png"));
          

            //will be moved to a keypress event in the future
            MainMenu();            
        }

        //This method is for when the player hasn't begun the game yet, or got a game over.
        //Shows the main method until ESC is pressed
        private void MainMenu()
        {
            //initialize gui elements
            txtblk_title.Visibility = Visibility.Visible;
            txtblk_highscore.Visibility = Visibility.Visible;
            txtblk_pressESC.Visibility = Visibility.Visible;
            
            if(highScores[0] > 0)
            {
                txtblk_highscore01.Visibility = Visibility.Visible;
                txtblk_highscore01.Text = highScores[0].ToString();
                //these if's are nested because there won't be a 2nd place high score if there is no 1st
                if (highScores[1] > 0)
                {
                    txtblk_highscore02.Visibility = Visibility.Visible;
                    txtblk_highscore02.Text = highScores[1].ToString();

                    if (highScores[2] > 0)
                    {
                        txtblk_highscore03.Visibility = Visibility.Visible;
                        txtblk_highscore03.Text = highScores[2].ToString();
                    }
                }
            }


            //set game pieces to their defaults
            isStarted = false;
            gameTimer.IsEnabled = false;
            
        }

        //STARTING A FRESH GAME
        private void GameStart()
        {
            isPaused = false;
            isStarted = true;
            level = 0;      //Official tetris has a 0th level. This was intentional.
            gameLogic = new Tetris(10 * (level + 1), level);
            
       
            //Establish the upcoming tetrominos list (randomly generated)
            gameLogic.setUpcoming();
            
            //establish the timing intervals for falling tetrominos
            currentRunningTime = 0;
            gravityInterval = 15;
            buffer = 50;

            //Adjust GUI
            txtblk_linesInt.Text = gameLogic.linesToClear.ToString();
            txtblk_levelInt.Text = level.ToString();

            txtblk_title.Visibility = Visibility.Hidden;
            txtblk_highscore.Visibility = Visibility.Hidden;
            txtblk_pressESC.Visibility = Visibility.Hidden;
            txtblk_highscore01.Visibility = Visibility.Hidden;
            txtblk_highscore02.Visibility = Visibility.Hidden;
            txtblk_highscore03.Visibility = Visibility.Hidden;
            
            mnuitm_play_pause.Header = "Pause";

            gameTimer.IsEnabled = true; //once the upcoming array is built, start the game.
        }
        private void nextLevel()
        {
            pause("Level up!");
            level++;
            gameLogic.nextLevel((10 * (level + 1)));
            
            txtblk_linesInt.Text = gameLogic.linesToClear.ToString();
            txtblk_levelInt.Text = level.ToString();

            gameLogic.setUpcoming();

            currentRunningTime = 0;
            gravityInterval = 15 - (level);
            buffer = 50;

            printScreen();
            printUpcoming();
            
        }
        private void GameOver()
        {
            updateHighScores(gameLogic.score);
            isStarted = false;
            gameLogic.playField = new int[10, 18];
            printScreen();
            MainMenu();
            
        }
        private void updateHighScores(int toAdd)
        {
            for(int i = 0; i < 3; i++)
            {
                if(toAdd > highScores[i])
                {
                    int temp = highScores[i];
                    highScores[i] = toAdd;
                    toAdd = temp;
                }
            }
        }
        //PRINTS THE PLAYFIELD
        private void printScreen()
        {   
            //gotta clear the playfield so that blocks that have since moved
            //are no longer just chilling in the playspace
            canvas_playField.Children.Clear();
            
            for (int i = 0; i < 10; i++)
            {
                for(int j = 0; j < 18; j++)
                {
                    if(gameLogic.playField[i,j] != 0)
                    {
                        //I'm declaring the rectangle within the nested for loop because each rectangle
                        //is a unique object to the canvas.

                        Rectangle test = new Rectangle();
                        test.Width = canvas_playField.Width / 10;
                        test.Height = canvas_playField.Height / 18;
                        test.StrokeThickness = 4;
                        test.Visibility = Visibility.Visible;

                        //pick which resource to use to fill the block
                        blockPicker(test, gameLogic.playField[i, j]);

                        Canvas.SetTop(test, j * (canvas_playField.Height / 18));
                        Canvas.SetLeft(test, i * (canvas_playField.Width / 10));

                        canvas_playField.Children.Add(test);
                    }
                }
            }
            
        }
        //PRINTS THE UPCOMING LIST
        //Might remove this feature later due to how complex it is. Don't wanna slow down my game too bad.
        private void printUpcoming()
        {
            canvas_upcoming_0.Children.Clear();
            canvas_upcoming_1.Children.Clear();
            canvas_upcoming_2.Children.Clear();
            canvas_upcoming_3.Children.Clear();

            //clearing the children also clears the border on the upcoming canvases, so we gotta re-add em.
            canvas_upcoming_0.Children.Add(rect_upcoming_1);
            canvas_upcoming_1.Children.Add(rect_upcoming_2);
            canvas_upcoming_2.Children.Add(rect_upcoming_3);
            canvas_upcoming_3.Children.Add(rect_upcoming_4);

            Tetromino[] temp = gameLogic.getUpcoming();
            Tetromino t1 = temp[0];
            Tetromino t2 = temp[1];
            Tetromino t3 = temp[2];
            Tetromino t4 = temp[3];
            for (int i = 0; i <= 3; i++)
            {
                for(int j = 0; j <= 3; j++)
                {
                    Rectangle u1 = new Rectangle();
                    u1.Width = canvas_upcoming_0.Width / 4;
                    u1.Height = canvas_upcoming_0.Height / 4;
                    u1.StrokeThickness = 2;
                    u1.Visibility = Visibility.Visible;

                    Rectangle u2 = new Rectangle();
                    u2.Width = canvas_upcoming_1.Width / 4;
                    u2.Height = canvas_upcoming_1.Height / 4;
                    u2.StrokeThickness = 2;
                    u2.Visibility = Visibility.Visible;

                    Rectangle u3 = new Rectangle();
                    u3.Width = canvas_upcoming_2.Width / 4;
                    u3.Height = canvas_upcoming_2.Height / 4;
                    u3.StrokeThickness = 2;
                    u3.Visibility = Visibility.Visible;

                    Rectangle u4 = new Rectangle();
                    u4.Width = canvas_upcoming_3.Width / 4;
                    u4.Height = canvas_upcoming_3.Height / 4;
                    u4.StrokeThickness = 2;
                    u4.Visibility = Visibility.Visible;

                    if (t1.Spritesheet[j,0,i] > 0)
                    {
                        Canvas.SetTop(u1, j * (canvas_upcoming_0.Height / 4));
                        Canvas.SetLeft(u1, i * (canvas_upcoming_0.Width / 4));

                        blockPicker(u1, t1.Spritesheet[j, 0, i]);

                        canvas_upcoming_0.Children.Add(u1);
                    }
                    if (t2.Spritesheet[j, 0, i] > 0)
                    {
                        Canvas.SetTop(u2, j * (canvas_upcoming_1.Height / 4));
                        Canvas.SetLeft(u2, i * (canvas_upcoming_1.Width / 4));

                        blockPicker(u2, t2.Spritesheet[j, 0, i]);

                        canvas_upcoming_1.Children.Add(u2);
                    }
                    if (t3.Spritesheet[j, 0, i] > 0)
                    {
                        Canvas.SetTop(u3, j * (canvas_upcoming_2.Height / 4));
                        Canvas.SetLeft(u3, i * (canvas_upcoming_2.Width / 4));

                        blockPicker(u3, t3.Spritesheet[j, 0, i]);

                        canvas_upcoming_2.Children.Add(u3);
                    }
                    if (t4.Spritesheet[j, 0, i] > 0)
                    {
                        Canvas.SetTop(u4, j * (canvas_upcoming_3.Height / 4));
                        Canvas.SetLeft(u4, i * (canvas_upcoming_3.Width / 4));

                        blockPicker(u4, t4.Spritesheet[j, 0, i]);

                        canvas_upcoming_3.Children.Add(u4);
                    }
                }
            }
        }
        private void blockPicker(Rectangle toFill, int block)
        {
            toFill.Stroke = new SolidColorBrush(Color.FromArgb(255,64,65,68));

            switch (block)
            {
                case 1:
                    toFill.Fill = block1Brush;
                    break;
                case -1:
                    toFill.Fill = block1Brush;
                    break;

                case 2:
                    toFill.Fill = block2Brush;
                    break;
                case -2:
                    toFill.Fill = block2Brush;
                    break;

                case 3:
                    toFill.Fill = block3Brush;
                    break;
                case -3:
                    toFill.Fill = block3Brush;
                    break;

                case 4:
                    toFill.Fill = block4Brush;
                    break;
                case -4:
                    toFill.Fill = block4Brush;
                    break;

                case 5:
                    toFill.Fill = block5Brush;
                    break;
                case -5:
                    toFill.Fill = block5Brush;
                    break;

                case 6:
                    toFill.Fill = block6Brush;
                    break;
                case -6:
                    toFill.Fill = block6Brush;
                    break;

                case 7:
                    toFill.Fill = block7Brush;
                    break;
                case -7:
                    toFill.Fill = block7Brush;
                    break;
            }
        }
            private void updateScore()
        {
            txtblk_scoreInt.Text = gameLogic.score.ToString();
            txtblk_linesInt.Text = gameLogic.linesToClear.ToString();
        }
        //-------------------------------------------------------
        //EVENTS
        //-------------------------------------------------------
        private void GameTimer_Tick(object sender, EventArgs e)
        {

            
            //if there is not currently a block in action,
            //drop one.
            if (gameLogic.active != true)
            {
                //dropping a block return a bool. True if successful, false if not. If unsuccessful,
                //player loses.
                if(!gameLogic.dropUpcoming())
                {
                    GameOver();
                    return;
                }
                //if a tetromino occupies the top row
                if(gameLogic.checkIfLost())
                {
                    GameOver();
                    return;
                }

                printUpcoming();
            }
            else
            {
                //if the player pressed space, drop the block as fast as possible ignoring all other input
                if (gameLogic.playerDrop)
                {
                    gameLogic.superDrop();
                    gameLogic.deactivateBlocks();
                    updateScore();
                    buffer = 50;
                    
                }
                else
                {

                    //process player input (moving left/right and rotations)
                    gameLogic.moveBlocks();
                    gameLogic.rotateBlocks();

                    gameLogic.checkIfFalling(); //this second check for if the tetromino is falling is here because
                                                //the player may have moved the tetromino off a surface and it may be falling again.

                    //first check if the current tetromino has room to fall
                    if (!gameLogic.isFalling)
                    {
                        if (buffer == 0)
                        {
                            gameLogic.deactivateBlocks();

                            updateScore();


                            buffer = 50;
                        }
                        else
                        {
                            buffer--;
                        }
                    }
                    else
                    {
                        if (currentRunningTime % gravityInterval == 0)
                        {
                            //gamelogic.gravity will cause the tetrominos to fall.
                            gameLogic.gravity();
                        }
                    }
                }

            }

            //increment the amount of time running
            currentRunningTime++;
            printScreen();

            if(gameLogic.linesToClear <= 0)
            {
                buffer = 0;
                nextLevel();
                
            }
        }
        private void pause(String status_text)
        {
            txtblk_status_update.Text = status_text;
            txtblk_status_update.Visibility = Visibility.Visible;

            txtblk_pressESC.Visibility = Visibility.Visible;

            isPaused = true;
            gameTimer.IsEnabled = false;
        }
        private void resume()
        {
            txtblk_status_update.Visibility = Visibility.Hidden;
            txtblk_pressESC.Visibility = Visibility.Hidden;
            isPaused = false;
            gameTimer.IsEnabled = true;
        }
        private void window_tetris_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.LeftCtrl)
            {
                if(isStarted)
                {
                    nextLevel();
                }
            }
            if(e.Key  == Key.Right)
            {
                if(isStarted && !isPaused)
                {
                    gameLogic.moveRight = true;
                }
                
                
            }
            if(e.Key == Key.Left)
            {
                if (isStarted && !isPaused)
                {
                    gameLogic.moveLeft = true;
                }
                
            }
            if(e.Key == Key.Up)
            {
                if (isStarted && !isPaused)
                {
                    gameLogic.rotateRight = true;
                }
                
            }
            if(e.Key == Key.Down)
            {
                if (isStarted && !isPaused)
                {
                    gameLogic.rotateLeft = true;
                }
               
            }
            if(e.Key == Key.Space)
            {
                if (isStarted && !isPaused)
                {
                    gameLogic.playerDrop = true;
                }
                
            }
            if(e.Key == Key.Escape)
            {
                if (!isStarted)
                {
                    GameStart();
                }
                else
                {

                    if (isPaused)
                    {
                        resume();
                    }
                    else
                    {
                        pause("Paused");
                    }
                }
            }
        }

        private void mnuitm_play_pause_Click(object sender, RoutedEventArgs e)
        {
            if(isStarted)
            {
                if(isPaused)
                {
                    resume();
                    mnuitm_play_pause.Header = "_Pause";
                }
                else
                {
                    pause("Paused");
                    mnuitm_play_pause.Header = "_Play";
                }
            }
            else
            {
                GameStart();
                mnuitm_play_pause.Header = "_Pause";
            }
        }

        private void mnuitm_about_Click(object sender, RoutedEventArgs e)
        {
            AboutWindow about = new AboutWindow();
            about.Show();
        }
    }
}
