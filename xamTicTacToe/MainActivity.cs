using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System.Collections.Generic;
using Android.Content;
using Android.Icu.Text;
using Android.Util;

namespace xamTicTacToe
{
    [Activity(Label = "Tic Tac Toe", MainLauncher = true)]
    public class MainActivity : Activity
    {
        User ThisPlayer = new User();
        private Button btnGo;
        private ImageView iv1, iv2, iv3, iv4, iv5, iv6, iv7, iv8, iv9;
        private ImageView[,] Tiles;
        private TextView TitleText;
        private String LogTag;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);
            btnGo = FindViewById<Button>(Resource.Id.btnGo);
            iv1 = FindViewById<ImageView>(Resource.Id.iv1);
            iv2 = FindViewById<ImageView>(Resource.Id.iv2);
            iv3 = FindViewById<ImageView>(Resource.Id.iv3);
            iv4 = FindViewById<ImageView>(Resource.Id.iv4);
            iv5 = FindViewById<ImageView>(Resource.Id.iv5);
            iv6 = FindViewById<ImageView>(Resource.Id.iv6);
            iv7 = FindViewById<ImageView>(Resource.Id.iv7);
            iv8 = FindViewById<ImageView>(Resource.Id.iv8);
            iv9 = FindViewById<ImageView>(Resource.Id.iv9);
            TitleText = FindViewById<TextView>(Resource.Id.txtTitle);
            Tiles = new ImageView[,] { { iv1, iv2, iv3 }, { iv4, iv5, iv6 }, { iv7, iv8, iv9 } };


            btnGo.Click += btnGo_Click;
            TitleText.Text = "Click the Button to Start";
            //add a touch event to each imageview
            foreach (var tile in Tiles)
            {
                tile.Enabled = false; //don't let the game start till the button has been clicked
                tile.Tag = "none";
                tile.SetImageResource(Resource.Drawable.blanktile);
                tile.Touch += (sender, args) => { Image_Touch(sender, args); };
            }

        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            Log.Info(LogTag, "Button click");

            //reset on button click
            if (btnGo.Text == "Playing ....")
            {
                EndGame();
                return;
            }

            ThisPlayer.CurrentPlayer = 1;
            foreach (var iv in Tiles)
            {
                iv.Enabled = true;
            }
            btnGo.Text = "Playing ....";



        }

        /// <summary>
        /// The touch event for the tiles (similar to click event)
        /// </summary>
        private void Image_Touch(object sender, View.TouchEventArgs touchEventArgs)
        {
            Log.Info(LogTag, "Image click");
            //& MotionEventArgs.Mask THERE ARE MULTIPLE EVENTS TRIGGERED WHEN TOUCHED, NEED TO CATCH THEM   
            //https://developer.xamarin.com/guides/android/application_fundamentals/touch/android_touch_walkthrough/

            switch (touchEventArgs.Event.Action)
            {
                case MotionEventActions.Down:
                    Log.Info(LogTag, "MotionEventActions.Down");

                    ThisPlayer.CurrentPlayer = 1;
                    //get the image clicked
                    var Image = (ImageView)sender;

                    //get the tag - none, Blank, Circle
                    // string tag = (string)Image.Tag;
                    TileClicked(Image);

                    //  TwoHumanPlayersGameplay(tag, Image);  
                    break;
                case MotionEventActions.Up:
                case MotionEventActions.Move:
                    //message = "Touch Ends";
                    break;

                    //   default:
                    //    message = string.Empty;
                    //       break;
            }

        }

        private void TileClicked(ImageView Clickedtile)
        {
            TitleText.Text = "Player = " + ThisPlayer.CurrentPlayer.ToString();

            if (ThisPlayer.CurrentPlayer == 1) //Human
            {
                Clickedtile.SetImageResource(Resource.Drawable.otile);
                Clickedtile.Tag = "Circle";
                ThisPlayer.WinnerCheck(Tiles);
            }
            //human didn't win so Robot gets a chance
            if (ThisPlayer.Winner == false)
            {
                ThisPlayer.SwitchPlayerTurn(); //Robots turn;

                if (ThisPlayer.CurrentPlayer == 2) //extra check its a Robot - it cheats
                {
                    Tiles = Robot.RobotMoveHorozontalAndVertical(Tiles);
                    ThisPlayer.WinnerCheck(Tiles);
                }
            }

            if (ThisPlayer.Winner == true)
            {
                //show if there is a winner
                string wintext = String.Format("Player {0} Wins!", ThisPlayer.CurrentPlayer);
                Log.Info(LogTag, wintext);
                Toast.MakeText(this, wintext, ToastLength.Long).Show();
                EndGame();

            }




        }

        private void ProcessHumanMove()
        {
            ThisPlayer.WinnerCheck(Tiles); //sets Winner to true if won

            //if (ThisPlayer.Winner == true)
            //{
            //    //show if there is a winner
            //    string wintext = String.Format("The Human {0} Wins!", ThisPlayer.CurrentPlayer);
            //    Toast.MakeText(this, wintext, ToastLength.Long).Show();
            //    EndGame();
            //    return true;
            //}
            //return false;
        }

        private void ProcessRobotMove()
        {
            Tiles = Robot.RobotMoveHorozontalAndVertical(Tiles);

            ThisPlayer.WinnerCheck(Tiles);  //sets Winner to true if won

            //if (ThisPlayer.Winner == true)
            //{
            //    //show if there is a winner
            //    string wintext = String.Format("The Computer {0} Wins!", ThisPlayer.CurrentPlayer);
            //    Toast.MakeText(this, wintext, ToastLength.Long).Show();
            //    EndGame();
            //}
        }

        private void TwoHumanPlayersGameplay(string tag, ImageView Image)
        {
            //if there is a none tile
            if ((tag == "none"))
            {
                switch (ThisPlayer.CurrentPlayer)
                {
                    //put in the tile for the player
                    case 1:
                        Image.SetImageResource(Resource.Drawable.otile);
                        Image.Tag = "Circle";
                        break;

                    case 2:
                        Image.SetImageResource(Resource.Drawable.xtile);
                        Image.Tag = "Cross";
                        break;
                }

                Check();
                ThisPlayer.SwitchPlayerTurn();
                return;
            }
        }
        private void Check()
        {//horozontal

            if (iv1.Tag.ToString() == iv2.Tag.ToString() && iv2.Tag.ToString() == iv3.Tag.ToString() && iv3.Tag.ToString() != "none" ||
                iv4.Tag.ToString() == iv5.Tag.ToString() && iv5.Tag.ToString() == iv6.Tag.ToString() && iv6.Tag.ToString() != "none" ||
                iv7.Tag.ToString() == iv8.Tag.ToString() && iv8.Tag.ToString() == iv9.Tag.ToString() && iv9.Tag.ToString() != "none"
                )
            {
                string Text = "Player " + ThisPlayer.CurrentPlayer + " won";
                Toast.MakeText(this, Text, ToastLength.Long).Show();
                EndGame();
                return;

            }
            //vertical
            if (iv1.Tag.ToString() == iv4.Tag.ToString() && iv4.Tag.ToString() == iv7.Tag.ToString() && iv7.Tag.ToString() != "none" ||
                iv2.Tag.ToString() == iv5.Tag.ToString() && iv5.Tag.ToString() == iv8.Tag.ToString() && iv8.Tag.ToString() != "none" ||
                iv3.Tag.ToString() == iv6.Tag.ToString() && iv6.Tag.ToString() == iv9.Tag.ToString() && iv9.Tag.ToString() != "none"
                )
            {
                string Text = "Player " + ThisPlayer.CurrentPlayer + " won";
                Toast.MakeText(this, Text, ToastLength.Long).Show();
                EndGame();
                return;
            }
            //Diagonal
            if (iv1.Tag.ToString() == iv5.Tag.ToString() && iv5.Tag.ToString() == iv9.Tag.ToString() && iv9.Tag.ToString() != "none" ||
                iv3.Tag.ToString() == iv5.Tag.ToString() && iv5.Tag.ToString() == iv7.Tag.ToString() && iv7.Tag.ToString() != "none"
                )
            {
                string Text = "Player " + ThisPlayer.CurrentPlayer + " won";
                Toast.MakeText(this, Text, ToastLength.Long).Show();
                EndGame();
                return;
            }
            //Draw
            Draw();
        }

        private void Draw()
        {
            //DRAW count how many nones there are if none then its a draw
            int countNone = 0;
            foreach (var iv in Tiles)
            {
                if (iv.Tag.ToString() != "none")
                {
                    //this code is ugly, sorry .....
                    countNone++;
                }

                if (countNone == 9)
                {
                    string Text = "Its a Draw! You both lost";
                    Toast.MakeText(this, Text, ToastLength.Long).Show();
                    EndGame();
                }
            }
        }



        private void EndGame()
        {
            foreach (var iv in Tiles)
            {
                iv.Tag = "none";
                iv.SetImageResource(Resource.Drawable.blanktile);
                iv.Enabled = false;
            }

            ThisPlayer.CurrentPlayer = 1;
            Toast.MakeText(this, "Press the button to play a new game", ToastLength.Long).Show();
            btnGo.Text = "Click for New Game";
        }

    }

}

