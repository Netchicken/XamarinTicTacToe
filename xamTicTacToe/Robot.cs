using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using Android.Widget;
using TicTacToe;
using xamTicTacToe;


namespace xamTicTacToe
{
    public static class Robot
    {
        private static GameLogicForRobot RobotLogic = new GameLogicForRobot();
        public static int turncounter = 0;

        // private User player = new User();
        //check what tiles are free
        //pick the best tile 
        //start with corners, and center
        //check if user or computer has 2 in a row
        //block user two in a row
        //add tile to computer two in a row

        //user = circle
        //robot = cross

        //[row,column]
        //eye moves through the screen showing how the program works.
        private static void SauronEye(ImageView[,] tiles, int i, int j, string Direction)
        {
            //Commented out until the actual code works. 

            //if (Direction == "v")
            //{

            //    iv.SetImageResource(tiles[j, i].Drawable.Current);
            //    tiles[j, i].SetImageResource(Resource.Drawable.sauron);
            //    //  Application.DoEvents();
            //    System.Threading.Thread.Sleep(300);
            //    tiles[j, i].SetImageResource(iv.Image);
            //}
            //if (Direction == "h")
            //{

            //    iv.SetImageResource(tiles[i, j].Image);
            //    tiles[i, j].SetImageResource(Resource.Drawable.sauron);
            //    //   Application.DoEvents();
            //    System.Threading.Thread.Sleep(300);
            //    tiles[i, j].Image = iv.Image;
            //}
        }

        public static ImageView[,] RobotMoveHorozontalAndVertical(ImageView[,] tiles)
        {
            for (int i = 0; i < 3; i++) //row
            {
                // tiles = RotateMatrix(tiles, 3);
                //each new Row returns the counters back to 0
                int CountCircle = 0;
                int Countnone = 0;
                int CountCross = 0;

                for (int j = 0; j < 3; j++) //column
                {
                    // SauronEye(tiles, i, j, "h");
                    //iterate through all the rows [0,0] [0,1] [0,2] counting up each shape

                    switch (tiles[i, j].Tag.ToString())
                    {
                        case "Circle":
                            CountCircle++;
                            break;
                        case "none":
                            Countnone++;
                            break;
                        case "Cross":
                            CountCross++;
                            break;
                    }

                    //  Look for 2 and a none first completing robots line or blocking the opponent
                    if (CountCross == 2 && Countnone == 1 || CountCircle == 2 && Countnone == 1)
                    {
                        //circle, circle, none if the last tile, the one your are on, is none
                        if (tiles[i, j].Tag.ToString() == "none")
                        {
                            tiles[i, j].Tag = "Cross";
                            tiles[i, j].SetImageResource(Resource.Drawable.xtile); //.Image = Resource1.x_tile;
                            return tiles;
                        }

                        //what if its circle, none, circle?
                        if (tiles[i, j - 1].Tag.ToString() == "none")
                        {
                            tiles[i, j - 1].Tag = "Cross";
                            tiles[i, j - 1].SetImageResource(Resource.Drawable.xtile); // Resource1.x_tile;
                            return tiles;
                        }
                        //what if its none, circle, circle?
                        if (tiles[i, j - 2].Tag.ToString() == "none")
                        {
                            tiles[i, j - 2].Tag = "Cross";
                            tiles[i, j - 2].SetImageResource(Resource.Drawable.xtile);// Resource1.x_tile;
                            return tiles;
                        }
                    }
                }
            }
            //add new vertical loop here
            for (int i = 0; i < 3; i++)
            {
                //row

                //each new Row returns the counters back to 0
                int CountCircle = 0;
                int Countnone = 0;
                int CountCross = 0;

                for (int j = 0; j < 3; j++) //column
                {
                    // SauronEye(tiles, i, j, "v");
                    //iterate through all the columns [0,0] [1,0] [2,0] counting up each shape
                    //just swap i,j to j,i
                    switch (tiles[j, i].Tag.ToString())
                    {
                        case "Circle":
                            CountCircle++;
                            break;
                        case "none":
                            Countnone++;
                            break;
                        case "Cross":
                            CountCross++;
                            break;
                    }

                    //Look for 2 and a none first completing robots line or blocking the opponent
                    if (CountCross == 2 && Countnone == 1 || CountCircle == 2 && Countnone == 1)
                    {
                        if (tiles[j, i].Tag.ToString() == "none")
                        {
                            tiles[j, i].Tag = string.Format("Cross");
                            tiles[j, i].SetImageResource(Resource.Drawable.xtile);// = Resource1.x_tile;
                            return tiles;
                        } //what if its circle, none, circle?
                        if (tiles[j - 1, i].Tag.ToString() == "none")
                        {
                            tiles[j - 1, i].Tag = string.Format("Cross");
                            tiles[j - 1, i].SetImageResource(Resource.Drawable.xtile); // Resource1.x_tile;
                            return tiles;
                        }
                        //what if its none, circle, circle?
                        if (tiles[j - 2, i].Tag.ToString() == "none")
                        {
                            tiles[j - 2, i].Tag = string.Format("Cross");
                            tiles[j - 2, i].SetImageResource(Resource.Drawable.xtile);  //Resource1.x_tile;
                            return tiles;
                        }
                    }
                    //if you have some none tiles in the diagonal.
                    if (Diagonalnone(tiles) != null)
                    {
                        string[] data = Diagonalnone(tiles).Split(',');

                        tiles[Convert.ToInt32(data[0]), Convert.ToInt32(data[1])].Tag = string.Format("Cross");
                        tiles[Convert.ToInt32(data[0]), Convert.ToInt32(data[1])].SetImageResource(Resource.Drawable.xtile); // .Image = Resource1.x_tile;
                        return tiles;
                    }
                }
            }


            tiles = RandomPosition(tiles);
            return tiles;
        }


        private static ImageView[,] CheckForTwoCrossOrnone(int CountCross, int Countnone, int CountCircle,
            ImageView[,] tiles, int i, int j)
        {

            //Look for 2 and a none first completing robots line or blocking the opponent
            if (CountCross == 2 && Countnone == 1 || CountCircle == 2 && Countnone == 1)
            {
                //if the last tile, the one your are on, is none
                if (tiles[i, j].Tag.ToString() == "none")
                {
                    tiles[i, j].Tag = string.Format("Cross");
                    tiles[i, j].SetImageResource(Resource.Drawable.xtile);// Resource1.x_tile;
                    return tiles;
                } //what if its circle, none, circle?
                if (tiles[i, j - 1].Tag.ToString() == "none")
                {
                    tiles[i, j - 1].Tag = string.Format("Cross");
                    tiles[i, j - 1].SetImageResource(Resource.Drawable.xtile);// Resource1.x_tile;
                    return tiles;
                }
                //what if its none, circle, circle?
                if (tiles[i, j - 2].Tag.ToString() == "none")
                {
                    tiles[i, j - 2].Tag = string.Format("Cross");
                    tiles[i, j - 2].SetImageResource(Resource.Drawable.xtile); //= Resource1.x_tile;
                    return tiles;
                }
            }
            return null;
        }


        //  introduces some randomness to the program to stop it from being so predictable, if it doesn't match the other criteria then place it randommly in the next best places
        private static ImageView[,] RandomPosition(ImageView[,] tiles)
        {
            Random myrandom = new Random();

            int x = 0, y = 0;
            bool FoundASpace = false;

            //check the center first....DON'T TAKE THE CENTER FIRST, ITS EASILY BEATEN
            if ((tiles[1, 1].Tag.ToString() == "none") && turncounter >= 1)
            {
                tiles[1, 1].Tag = string.Format("Cross");
                tiles[1, 1].SetImageResource(Resource.Drawable.xtile);//= Resource1.x_tile;

                return tiles;  //finished this method
            }


            // get the opposite diagonal to stop being caught in a trap

            //if (RobotLogic.GrabOppositeCorner(tiles) != null && RobotLogic.GrabOppositeCorner(tiles).Length >0) {
            //    return RobotLogic.GrabOppositeCorner(tiles);
            //    }

            if ((tiles[0, 0].Tag.ToString() == "none") && (tiles[2, 2].Tag.ToString() == "Circle"))
            {
                tiles[0, 0].Tag = string.Format("Cross");
                tiles[0, 0].SetImageResource(Resource.Drawable.xtile);// Resource1.x_tile;

                return tiles;  //finished this method
            }

            if ((tiles[0, 0].Tag.ToString() == "Circle") && (tiles[2, 2].Tag.ToString() == "none"))
            {
                tiles[2, 2].Tag = string.Format("Cross");
                tiles[2, 2].SetImageResource(Resource.Drawable.xtile);// Resource1.x_tile;

                return tiles;  //finished this method
            }

            if ((tiles[0, 2].Tag.ToString() == "Circle") && (tiles[2, 0].Tag.ToString() == "none"))
            {
                tiles[2, 0].Tag = string.Format("Cross");
                tiles[2, 0].SetImageResource(Resource.Drawable.xtile); //Resource1.x_tile;

                return tiles;  //finished this method
            }

            if ((tiles[2, 0].Tag.ToString() == "Circle") && (tiles[0, 2].Tag.ToString() == "none"))
            {
                tiles[0, 2].Tag = string.Format("Cross");
                tiles[0, 2].SetImageResource(Resource.Drawable.xtile);// Resource1.x_tile;

                return tiles;  //finished this method
            }




            //otherwise randomly choose another square, FoundAspace is redundant, but what is a loop without an end point?
            while (FoundASpace == false)
            {

                switch (myrandom.Next(1, 4))
                {
                    //case 0:
                    //    x = 1;
                    //    y = 1;
                    //    //center //    //if the middle is free grab it
                    //    break;
                    case 1:
                        x = 0;
                        y = 0;
                        //lefttop
                        break;
                    case 2:
                        x = 0;
                        y = 2;
                        //righttop
                        break;
                    case 3:
                        x = 2;
                        y = 0;
                        //leftbottom
                        break;
                    case 4:
                        x = 2;
                        y = 2;
                        //right bottom
                        break;
                    case 5:
                        //centerleft
                        x = 1;
                        y = 0;
                        break;
                    case 6:
                        x = 2;
                        y = 1;
                        //centerright
                        break;
                    case 7:
                        x = 1;
                        y = 2;
                        break;
                }

                //this gets out of the loop if its a none square make it a cross
                if (tiles[x, y].Tag.ToString() == "none")
                {
                    tiles[x, y].Tag = "Cross";
                    tiles[x, y].SetImageResource(Resource.Drawable.xtile);// = Resource1.x_tile;

                    FoundASpace = true;
                    return tiles;
                }
            } //end while loop
            return tiles;
        }


        public static ImageView[,] RotateMatrix(ImageView[,] matrix, int n)
        {
            ImageView[,] Rotated = new ImageView[n, n];  //3,3
            for (int i = 0; i < n; ++i)
            {
                for (int j = 0; j < n; ++j)
                {
                    Rotated[i, j] = matrix[n - j - 1, i];
                }
            }
            return Rotated;
        }


        /// <summary>
        /// Seach for a none in the Diagonals
        /// </summary>
        /// <param name="tiles"></param>
        /// <returns> returns 0,1,2,3,4,5 or 10 which is none </returns>
        private static int DiagonalSearch(ImageView[,] tiles)
        {

            //this array holds the 3 tiles from left to right
            //we need to check if there are 2 circles and a none to start with. Put the tiles into a string array
            string[] LToRCount = { tiles[0, 0].Tag.ToString(), tiles[1, 1].Tag.ToString(), tiles[2, 2].Tag.ToString() };
            //this array holds the 3 tiles from right to left
            string[] RToLCount = { tiles[0, 2].Tag.ToString(), tiles[1, 1].Tag.ToString(), tiles[2, 0].Tag.ToString() };

            //just to count how many nones and circles using linq
            int countCircleLToR = LToRCount.Count(t => t.Equals("Circle"));
            int countnonesLToR = LToRCount.Count(t => t.Equals("none"));
            int countCrossLToR = LToRCount.Count(t => t.Equals("Cross"));

            //just to count how many nones and circles
            int countCircleRToL = RToLCount.Count(t => t.Equals("Circle"));
            int countnonesRToL = RToLCount.Count(t => t.Equals("none"));
            int countCrossRToL = RToLCount.Count(t => t.Equals("Cross"));

            //if there are 2 nones in both diagonals then return because there is no threat 
            if (countnonesLToR == 2 && countnonesRToL == 2)
            {
                return 10; //there are two nones in the line so its not important to carry on
            }
            //if there is are two crosses and a none then find the none and fill it. =WIN 
            if (countCrossLToR == 2 && countnonesLToR == 1)
            { //Left to Right
                // tiles[0, 0].Tag.ToString(), tiles[1, 1].Tag.ToString(), tiles[2, 2].Tag.ToString() 
                return Array.FindIndex(LToRCount, l => l.Contains("none"));
                //returns 0, 1, 2
            }
            //-----------   WIN WITH 1 none AND 2 CROSSES  ------------------------------

            //if there is are two crosses and a none then find the none and fill it. =WIN 
            if (countCrossRToL == 2 && countnonesRToL == 1)
            {
                //Right To left
                // tiles[0, 2].Tag.ToString(), tiles[1, 1].Tag.ToString(), tiles[2, 0].Tag.ToString()
                //This returns back the number of the cell (0,1,2) that holds the none, so you can put your cross in it. When this gets passed to the next array you need to call it 3,4,5 as the left to right has 1,2,3

                //this is the whole point of the this method to return back the number of a none cell in the diagonal. 1,2,3,4,5,6
                int none = Array.FindIndex(RToLCount, l => l.Contains("none"));

                switch (none)
                {
                    case 0:
                        return 3;
                    case 1:
                        return 4;
                    case 2:
                        return 5;
                }
            }
            //-------------- DEFEND WHEN 2 CIRCLES AND 1 none ---------------------------
            //if there is are two circles and a none then find the none and fill it. = DEFEND

            if (countCircleLToR == 2 && countnonesLToR == 1)
            {//Left To Right
                //tiles[0, 0].Tag.ToString(), tiles[1, 1].Tag.ToString(), tiles[2, 2].Tag.ToString() 
                return Array.FindIndex(LToRCount, t => t.Contains("none"));
                //returns 0, 1, 2
            }
            //if there is are two circles and a none then find the none and fill it. = DEFEND
            if ((countCircleRToL == 2 && countnonesRToL == 1))
            {//Right to left
                //tiles[0, 2].Tag.ToString(), tiles[1, 1].Tag.ToString(), tiles[2, 0].Tag.ToString()
                int none = Array.FindIndex(RToLCount, t => t.Contains("none"));

                switch (none)
                {
                    case 0:
                        return 3;
                    case 1:
                        return 4;
                    case 2:
                        return 5;
                }
                return 10;
            }
            return 10;
        }

        /// <summary>
        /// return the coords of the ImageView that holds a diagonal none to enter the cross in
        /// </summary>
        /// <param name="tiles"></param>
        /// <returns>ImageView coords</returns>
        private static string Diagonalnone(ImageView[,] tiles)
        {

            int none = DiagonalSearch(tiles);
            if (none != 10)
            {

                switch (none)
                {
                    //tiles[0, 0].Tag.ToString(), tiles[1, 1].Tag.ToString(), tiles[2, 2].Tag.ToString()
                    case 0:
                        return "0,0";

                    case 1:
                        return "1,1";

                    case 2:
                        return "2,2";


                    // tiles[0, 2].Tag.ToString(), tiles[1, 1].Tag.ToString(), tiles[2, 0].Tag.ToString()
                    case 3:
                        return "0,2";

                    case 4:
                        return "1,1";

                    case 5:
                        return "2,0";
                }
            }
            return null;
        }





        /// <summary>
        /// 
        /// 
        /// </summary>
        /// <param name="tiles"></param>
        /// <returns></returns>

        private static ImageView DiagonalSearchrebuild(ImageView[,] tiles)
        {
            //left to right tiles
            string[] LToRCount = { tiles[0, 0].Tag.ToString(), tiles[1, 1].Tag.ToString(), tiles[2, 2].Tag.ToString() };
            if (GetnoneToWin("LToR", LToRCount, tiles) != null)
            {
                return (ImageView)GetnoneToWin("LToR", LToRCount, tiles);//0, 1, 2 --- 10, 11, 12
            }
            // right to left tiles   //3, 4, 5 --- 13, 14, 15
            string[] RToLCount = { tiles[0, 2].Tag.ToString(), tiles[1, 1].Tag.ToString(), tiles[2, 0].Tag.ToString() };
            if (GetnoneToWin("RToL", RToLCount, tiles) != null)
            {
                return (ImageView)GetnoneToWin("RToL", RToLCount, tiles);

            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Direction"> Left to right or right to left</param>
        /// <param name="DiagArray">The string array for each diagonal</param>
        /// <param name="tiles">The main tile array of ImageViewes</param>
        /// <returns>Returns back the ImageView with the none in it</returns>
        private static ImageView[] GetnoneToWin(string Direction, string[] DiagArray, ImageView[,] tiles)
        {
            //this array holds the 3 tiles from left to right
            //we need to check if there are 2 circles and a none to start with. Put the tiles into a string array

            //just to count how many nones and circles using linq
            int countCircle = DiagArray.Count(t => t.Equals("Circle"));
            int countnones = DiagArray.Count(t => t.Equals("none"));
            int countCross = DiagArray.Count(t => t.Equals("Cross"));

            //if there is are two crosses and a none then find the none and fill it. =WIN 
            if (countCross == 2 && countnones == 1 || countCircle == 2 && countnones == 1)
            {

                if (Direction == "LToR")
                {//tiles[0, 0].Tag.ToString(), tiles[1, 1].Tag.ToString(), tiles[2, 2].Tag.ToString()
                    if (tiles[0, 0].Tag.ToString() == "none")
                    {
                        return (ImageView[])tiles[0, 0];
                    }
                    else if (tiles[1, 1].Tag.ToString() == "none")
                    {
                        return (ImageView[])tiles[1, 1];
                    }
                    else if (tiles[2, 2].Tag.ToString() == "none")
                    {
                        return (ImageView[])tiles[2, 2];
                    }
                }

                if (Direction == "RToL")
                {//tiles[0, 2].Tag.ToString(), tiles[1, 1].Tag.ToString(), tiles[2, 0].Tag.ToString()
                    if (tiles[0, 2].Tag.ToString() == "none")
                    {
                        return (ImageView[])tiles[0, 2];
                    }
                    else if (tiles[1, 1].Tag.ToString() == "none")
                    {
                        return (ImageView[])tiles[1, 1];
                    }
                    else if (tiles[2, 0].Tag.ToString() == "none")
                    {
                        return (ImageView[])tiles[2, 0];
                    }
                }
            }
            return null;
        }
    }
}



