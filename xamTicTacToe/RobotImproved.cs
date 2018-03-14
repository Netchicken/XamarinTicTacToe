using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace xamTicTacToe
{
    static class RobotImproved
    {
        public static ImageView[,] HCrossSearch(ImageView[,] tiles)
        {
            for (int i = 0; i < 3; i++) //row
            {
                //each new Row returns the counters back to 0
                int Countnone = 0;
                int CountCross = 0;

                for (int j = 0; j < 3; j++) //column
                {
                    //iterate through all the rows [0,0] [0,1] [0,2] counting up each shape
                    switch (tiles[i, j].Tag.ToString())
                    {
                        case "none":
                            Countnone++;
                            break;
                        case "Cross": //Robot
                            CountCross++;
                            break;
                    }

                    //  Look for 2 and a none first completing robots line or blocking the opponent
                    if (CountCross == 2 && Countnone == 1)
                    {
                        //circle, circle, the one your are on is none
                        //what if its circle, none, circle?
                        //what if its none, circle, circle?
                        for (int k = 0; k < 3; k++)
                        {
                            if (tiles[i, j - k].Tag.ToString() == "none")
                            {
                                tiles[i, j - k].Tag = "Cross";
                                tiles[i, j - k].SetImageResource(Resource.Drawable.xtile);
                                return tiles;
                            }
                        }
                    }
                }
            }

            return null;

        }

        public static ImageView[,] VCrossSearch(ImageView[,] tiles)
        {
            //add new vertical loop here
            for (int i = 0; i < 3; i++)
            {
                //each new Row returns the counters back to 0
                int Countnone = 0;
                int CountCross = 0;

                for (int j = 0; j < 3; j++) //column
                {
                    //iterate through all the columns [0,0] [1,0] [2,0] counting up each shape
                    //just swap i,j to j,i
                    switch (tiles[j, i].Tag.ToString())
                    {
                        case "none":
                            Countnone++;
                            break;
                        case "Cross":
                            CountCross++;
                            break;
                    }

                    //Look for 2 and a none first completing robots line or blocking the opponent
                    //what if its circle, none, circle? 
                    //what if its none, circle, circle?
                    for (int k = 0; k < 3; k++)
                    {
                        if (CountCross == 2 && Countnone == 1)
                        {
                            if (tiles[j - k, i].Tag.ToString() == "none")
                            {
                                tiles[j - k, i].Tag = string.Format("Cross");
                                tiles[j - k, i].SetImageResource(Resource.Drawable.xtile);
                                return tiles;
                            }
                        }

                      return  DCrossSearch(tiles);
                    }
                }
            }
            return null;
        }

        public static ImageView[,] DCrossSearch(ImageView[,] tiles)
        {
            //if you have some none tiles in the diagonal.
            if (Robot.Diagonalnone(tiles) != null)
            {
                string[] data = Robot.Diagonalnone(tiles).Split(',');

                tiles[Convert.ToInt32(data[0]), Convert.ToInt32(data[1])].Tag = string.Format("Cross");
                tiles[Convert.ToInt32(data[0]), Convert.ToInt32(data[1])].SetImageResource(Resource.Drawable.xtile);
                return tiles;
            }

            return null;
        }
    }
}
