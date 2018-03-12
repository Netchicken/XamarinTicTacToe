using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Widget;


namespace xamTicTacToe
{
    public class User
    {
        public int CurrentPlayer { get; set; }

        public bool Winner { get; set; }
        public int TurnCounter { get; set; }
        public string Shape { get; set; }

        public User()
        {//default settings constructor
            CurrentPlayer = 1;
            Winner = false;
            TurnCounter = 0;
        }

        public void SwitchPlayerTurn()
        {
            //create a swap on the click to see who is playing
            if (CurrentPlayer == 1)
            {
                CurrentPlayer = 2;
            }
            else if (CurrentPlayer == 2)
            {
                CurrentPlayer = 1;
            }
        }



        public string ShowWinner()
        {
            if (Winner == true)
                return String.Format("Player {0} Wins!", CurrentPlayer);
            else if (Winner == false && TurnCounter == 9)
                return "Draw!";
            else
                //show on the title bar
                return string.Format("Player {0}'s {1} Turn", CurrentPlayer, TurnCounter);

            // CurrentPlayer = CurrentPlayer == 1 ? 2 : 1;
        }
        public void WinnerCheck(ImageView[,] tiles)
        {
            for (int i = 0; i < 3; i++)
            {
                //horozontal and vertical

                if (tiles[i, 0].Tag.ToString() != "none" && tiles[i, 0].Tag == tiles[i, 1].Tag &&
                    tiles[i, 1].Tag == tiles[i, 2].Tag
                    ||
                    tiles[0, i].Tag.ToString() != "none" && tiles[0, i].Tag == tiles[1, i].Tag &&
                    tiles[1, i].Tag == tiles[2, i].Tag
                    )
                {
                    Winner = true; //Winner = 1
                    return;
                }
            }

            if ( //Check for diagonals
                tiles[0, 0].Tag.ToString() != "none" && tiles[0, 0].Tag.Equals(tiles[1, 1].Tag) &&
                tiles[1, 1].Tag.Equals(tiles[2, 2].Tag)
                ||
                tiles[0, 2].Tag.ToString() != "none" && tiles[0, 2].Tag.Equals(tiles[1, 1].Tag) &&
                tiles[1, 1].Tag.Equals(tiles[2, 0].Tag))
            {
                Winner = true; //Winner = 1
            }

        }

    }
}
