using System;
using System.Threading;

namespace Snake
{
    class Program
    {
        //TONY YU
        //CULMINATING TASK
        //PLEASE REFER TO THE FLOWCHART THAT I HAVE INCUDED TO BETTER UNDERSTAND THE CODE
        //FINAL PROGRAM DEFINITION:
        //SIMPLE SNAKE GAME, WILL ASK THE USER FOR HIS/HER NAME THAT IS LIMITED TO THE SPECIFICATIONS PROVIDED, THEN THE GAME WILL START
        //THE SNAKE WILL BE CONTROLED WITH WSAD, THEN IT WILL GROW IN LEGNTH WHEN IT EATS A FOOD, AND WILL DIE WHEN IT HITS THE WALL
        //IT WILL NOT DIE WHEN IT OVERLAPS ITSELF, I GUESS THE 2D SNAKE GAME IS IN A 3D WORLD (i didnt get that part to work haha)
        //WHEN YOU HIT THE WALL, THE GAME WILL SAY "GAME OVER"; AS WELL, IT WILL ASK THE USER IF THEY WANT TO PLAY AGAIN. (this part is pretty glitchy, please consider this as an extra feature and not include it in the marks)




        //GLOBAL SCOPE ASSIGNMENTS

        int foodX = 5;
        int foodY = 5;

        int segments = 4; //assigns the initial amount of segments in the snake

        char key;

        char[,] a = new char[20, 20] { //creates border that is 20 x 20 //i knew about 2D arrays already but i had to search the syntax of it on https://stackoverflow.com/questions/12567329/multidimensional-array-vs
              { 'c', 'c', 'c', 'c', 'c','c', 'c', 'c', 'c', 'c','c', 'c', 'c', 'c', 'c','c', 'c', 'c', 'c', 'c'}, //this MUST be declared in global scope because or else the head of the snake cant access its elements
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', ' ',' ', ' ', ' ', ' ', 'c',},
            { 'c', 'c', 'c', 'c', 'c','c', 'c', 'c', 'c', 'c','c', 'c', 'c', 'c', 'c','c', 'c', 'c', 'c', 'c'},
        };

        int[] X = new int[100];
        int[] Y = new int[100];

        public void border()
        {
            for (int i = 0; i < 20; i++) //prints the border
            {
                for (int j = 0; j < 20; j++)
                {
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.Write(a[i, j]);
                }
                Console.WriteLine();
            }
        }

        Program()
        {
            X[0] = 2;
            Y[0] = 2;
        }

        private void drawSnake(int x, int y) //draws the snake
        {
            Console.SetCursorPosition(x, y);
            Console.Write("∎");
        }

        int Height = 20;
        int Width = 20; //sets the width and height of the rectange where food can spawn

        private void writeFood(int x, int y)
        {
            Console.SetCursorPosition(x, y); //set cursor position to the coordinates that we want the food to be in (randomized) //learned in a previous c# project
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("$");
        }

        private void refresh()
        {
            //IGNORE
            /*    for (int i = 1; i < segments-3; i++) //this was the part that should kill the snake, but it didnt work out. I tried. please ignore this, i want to make it work after the project so im leaving it here
                {
                    for (int j = 1; j < segments-3; j++)
                    {
                        Console.WriteLine(segments);
                        Console.WriteLine(i);
                        Console.WriteLine(j);
                        if (X[0] == X[i] && Y[0] == Y[j])
                        {
                            Console.WriteLine("Game Over!");
                            System.Threading.Thread.Sleep(1000);
                            Console.WriteLine("Do you want to play again? Type Yes to play again. Any key + enter to end the game.");
                            string bruh;
                            bruh = Console.ReadLine();
                            if (bruh.ToLower() == "yes")
                            {
                                System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName); //https://stackoverflow.com/questions/5706497/how-restart-the-console-app
                            }
                            else
                            {
                                Environment.Exit(-1);//ends the program
                            }
                        }
                    }
                }*/

            userInputKey();

            changingWhereTheHeadIs();

            ifTheHeadHitsTheWall();

            ifTheFoodIsEaten();

            printScore();

            consumeedFood_becomingLonger();

            UpdateSnakeAndFood();

            Thread.Sleep(40); //basically the speed of refresh rate, change this if you like. I made other people test out my code, and the speed isnt the same on every computer apparently
        }

        private void changingWhereTheHeadIs()
        {
            if (key == 'w' || key == 'W')
                Y[0]--;

            else if (key == 's' || key == 'S')
                Y[0]++;

            else if (key == 'd' || key == 'D')
                X[0]++;

            else if (key == 'a' || key == 'A')
                X[0]--;
            else if (key != 'a' && key != 's' && key != 'w' && key != 'd')
                Y[0]++; //will start with going down to begin with. Also, when a key other than WSAD is pressed, it will go down.
        }

        private void ifTheHeadHitsTheWall()
        {
            if (a[X[0], Y[0]] == 'c')
            {
                Console.WriteLine("Game Over!");
                System.Threading.Thread.Sleep(1000);
                Console.WriteLine("Do you want to play again? Type Yes to play again. Any key + enter to end the game.");
                string playagain;
                playagain = Console.ReadLine();
                if (playagain.ToLower() == "yes")
                {
                    Console.Clear();
                    System.Diagnostics.Process.Start(System.AppDomain.CurrentDomain.FriendlyName); //https://stackoverflow.com/questions/5706497/how-restart-the-console-app
                    Console.Clear();
                }
                else
                {
                    Environment.Exit(-1);//ends the program
                }
            }
        }

        private void ifTheFoodIsEaten()
        {
            if (X[0] == foodX && Y[0] == foodY) //if head meets the food
            {
                segments++; //more length for snake
                Random rand2 = new Random((int)DateTime.Now.Ticks); //wouldnt hurt to have a better random

                foodX = rand2.Next(2, Width - 2); //new random food
                foodY = rand2.Next(2, Height - 2);

            }
        }

        private void consumeedFood_becomingLonger()
        {
            for (int i = segments; i > 1; i--) //extends the length of the snake by kinda "pushing" each of the segments of the snake back by one (reordering the array)
            {
                X[i - 1] = X[i - 2];
                Y[i - 1] = Y[i - 2];
            }
        }

        private void printScore()
        {
            Console.SetCursorPosition(0, 25);
            int score = segments - 1;
            Console.WriteLine("Your score is " + score);
        }

        private void userInputKey()
        {
            while (true)
                try
                {
                    if (Console.KeyAvailable) //kinda loops around so that the last key is always the active one, unless there is a new key available //https://www.geeksforgeeks.org/console-keyavailable-property-in-c-sharp/
                    {
                        Console.CursorVisible = false; //already knew this before from another one of my projects in C#
                        key = Console.ReadKey(true).KeyChar; //learned from GeeksforGeerks.com from the same link as above, in the "Console.ReadKey(Boolean)" section of the article ALSO must convert the ReadKey from the user to char
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Please try again");
                }

        }

        private void UpdateSnakeAndFood()
        {
            for (int i = 0; i < segments; i++)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                drawSnake(X[i], Y[i]);
                writeFood(foodX, foodY);
            }
        }

        static void Main(string[] args)
        {
            Console.Clear();

            snakeCaseName();

            System.Threading.Thread.Sleep(1500); //pauses the the game for a second and a half to allow read to finish reading text //

            Program snake1 = new Program(); //accessing object in class oop

            while (true)
            {
                Console.Clear();
                snake1.border();
                snake1.refresh();
            }
        }

        private static void snakeCaseName()
        {
            while (true)
                try
                {
                    string snakeCaseName;
                    bool tf = false;

                    string hello = "Hello, welcome to Tony's snake game!";


                    gameSpeakMethod(hello, 0);
                    string hello2 = "Please use keys WSAD for up, down, left, right, respectively, to eat food with the snake";
                    gameSpeakMethod(hello2, 1);

                    string hello4 = "Please make your name at least 12 characters long and at most 25 characters long.";
                    gameSpeakMethod(hello4, 2);

                    string hello5 = "(snakes have long names because they're are long!)";
                    gameSpeakMethod(hello5, 3);


                    string hello6 = "Also, please include at least one lowercase letter, one uppercase letter, one digit, and one special character!";
                    gameSpeakMethod(hello6, 4);

                    string hello7 = "(snakes have pretty unique names!)";
                    gameSpeakMethod(hello7, 5);

                    string hello3 = "Please enter the name of your snake.";
                    gameSpeakMethod(hello3, 6);

                    while (true)
                    {
                        snakeCaseName = Console.ReadLine();

                        tf = true;

                        if (snakeCaseName.IndexOfAny("ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray()) < 0) tf = false;

                        if (snakeCaseName.IndexOfAny("0123456789".ToCharArray()) < 0) tf = false;  //I learned this method wayyy back in like january when i was solving algorithms in "CODEWARS.COM" while reading an editorial

                        if (snakeCaseName.IndexOfAny("!#$%&\'()*+,-./:;<=>?@[\\]^_`{|}~".ToCharArray()) < 0) tf = false;

                        if (snakeCaseName.IndexOfAny("abcdefghijklmnopqrstuvwxyz".ToCharArray()) < 0) tf = false;

                        if (snakeCaseName.Length < 8 || 20 < snakeCaseName.Length) tf = false;

                        if (tf == true)
                        {
                            Console.WriteLine("Good job, you made a valid snake name!");
                            string hello8 = "Hello ";
                            gameSpeakMethod(hello8, 35);

                            for (int i = 0; i < snakeCaseName.Length; i++)
                            {
                                Console.SetCursorPosition(6 + i, 35);
                                Console.WriteLine(snakeCaseName[i]);
                                System.Threading.Thread.Sleep(35);
                            }
                            break;
                        }
                        else Console.WriteLine("You did not type in a valid snake name, please try again.");
                    }
                    break;
                }
                catch
                {
                    Console.WriteLine("Please try again.");
                }
        }

        private static void gameSpeakMethod(string hello, int b) //creates that special effect with the texts
        {
            for (int i = 0; i < hello.Length; i++)
            {
                Console.SetCursorPosition(i, b);
                Console.WriteLine(hello[i]);
                System.Threading.Thread.Sleep(35);
            }
        }
    }
}
