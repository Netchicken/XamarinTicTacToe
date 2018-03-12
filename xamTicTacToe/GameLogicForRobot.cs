using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Android.Widget;
using xamTicTacToe;


namespace TicTacToe
{
    class GameLogicForRobot
    {

        internal ImageView[,] GrabOppositeCorner(ImageView[,] tiles)
        {
            //check the center first....DON'T TAKE THE CENTER, ITS EASILY BEATEN

            // get the opposite diagonal to stop being caught in a trap


            if ((tiles[0, 0].Tag.ToString() == "none") && (tiles[2, 2].Tag.ToString() == "Circle"))
            {
                tiles[0, 0].Tag = string.Format("Cross");
                tiles[0, 0].SetImageResource(Resource.Drawable.xtile);

                return tiles;  //finished this method
            }

            if ((tiles[0, 0].Tag.ToString() == "Circle") && (tiles[2, 2].Tag.ToString() == "none"))
            {
                tiles[2, 2].Tag = string.Format("Cross");
                tiles[2, 2].SetImageResource(Resource.Drawable.xtile);

                return tiles;  //finished this method
            }

            if ((tiles[0, 2].Tag.ToString() == "Circle") && (tiles[2, 0].Tag.ToString() == "none"))
            {
                tiles[2, 0].Tag = string.Format("Cross");
                tiles[2, 0].SetImageResource(Resource.Drawable.xtile);

                return tiles;  //finished this method
            }

            if ((tiles[2, 0].Tag.ToString() == "Circle") && (tiles[0, 2].Tag.ToString() == "none"))
            {
                tiles[0, 2].Tag = string.Format("Cross");
                tiles[0, 2].SetImageResource(Resource.Drawable.xtile);

                return tiles;  //finished this method
            }
            return null;
        }


    }
}
