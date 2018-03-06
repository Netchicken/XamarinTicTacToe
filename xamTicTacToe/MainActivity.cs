using System;
using Android.App;
using Android.Widget;
using Android.OS;
using Android.Views;
using System.Collections.Generic;
using Android.Icu.Text;

namespace xamTicTacToe
{
    [Activity(Label = "xamTicTacToe", MainLauncher = true)]
    public class MainActivity : Activity
    {
        private Button btnGo;
        private ImageView iv1, iv2, iv3, iv4, iv5, iv6, iv7, iv8, iv9;
        private int Currentplayer = 1;
        private ImageView[] Tiles;

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

            Tiles = new ImageView[] { iv1, iv2, iv3, iv4, iv5, iv6, iv7, iv8, iv9 };


            btnGo.Click += btnGo_Click;

            //add a touch event to each imageview
            foreach (var tile in Tiles)
            {
                tile.Enabled = false; //don't let the came start till the button has been clicked

                tile.Touch += (sender, args) => { Image_Touch(sender, args); };
            }

            //iv1.Touch += (sender, args) => { Image_Touch(sender, args); };
            //iv2.Touch += (sender, args) => { Image_Touch(sender, args); }; ;
            //iv3.Touch += (sender, args) => { Image_Touch(sender, args); };
            //iv4.Touch += (sender, args) => { Image_Touch(sender, args); };
            //iv5.Touch += (sender, args) => { Image_Touch(sender, args); };
            //iv6.Touch += (sender, args) => { Image_Touch(sender, args); };
            //iv7.Touch += (sender, args) => { Image_Touch(sender, args); };
            //iv8.Touch += (sender, args) => { Image_Touch(sender, args); };
            //iv9.Touch += (sender, args) => { Image_Touch(sender, args); };
            //    init();
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            EnableTiles();
        }


        private EventHandler Image_Touch(object sender, View.TouchEventArgs args)
        {
            var Image = (ImageView)sender;

            string tag = (string)Image.Tag;



            //if there is a blank tile
            if ((tag == "none"))
            {
                switch (Currentplayer)
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
                Player();
                return null;
            }
            return null;
        }


        private void Check()
        {//horozontal

            if (iv1.Tag.ToString() == iv2.Tag.ToString() && iv2.Tag.ToString() == iv3.Tag.ToString() && iv3.Tag.ToString() != "none" ||
                iv4.Tag.ToString() == iv5.Tag.ToString() && iv5.Tag.ToString() == iv6.Tag.ToString() && iv6.Tag.ToString() != "none" ||
                iv7.Tag.ToString() == iv8.Tag.ToString() && iv8.Tag.ToString() == iv9.Tag.ToString() && iv9.Tag.ToString() != "none"
                )
            {
                string Text = "Player " + Currentplayer + " won";
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
                string Text = "Player " + Currentplayer + " won";
                Toast.MakeText(this, Text, ToastLength.Long).Show();
                EndGame();
                return;
            }
            //Diagonal
            if (iv1.Tag.ToString() == iv5.Tag.ToString() && iv5.Tag.ToString() == iv9.Tag.ToString() && iv9.Tag.ToString() != "none" ||
                iv3.Tag.ToString() == iv5.Tag.ToString() && iv5.Tag.ToString() == iv7.Tag.ToString() && iv7.Tag.ToString() != "none"
                )
            {
                string Text = "Player " + Currentplayer + " won";
                Toast.MakeText(this, Text, ToastLength.Long).Show();
                EndGame();
                return;
            }
            //Draw
            Draw();
        }

        private void Draw()
        {
            //DRAW count how many blanks there are if none then its a draw
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

        private void EnableTiles()
        {
            foreach (var iv in Tiles)
            {
                iv.Enabled = true;
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

            Currentplayer = 1;
            Toast.MakeText(this, "Press the button to play a new game", ToastLength.Long).Show();
            btnGo.Text = "Click for New Game";
        }



        private void Player()
        {
            //create a swap on the click to see who is playing
            if (Currentplayer == 1)
            {
                Currentplayer = 2;
                //      this.Text = "Player 2";
            }
            else if (Currentplayer == 2)
            {
                Currentplayer = 1;
                //     this.Text = "Player 1";
            }
        }

    }

}

