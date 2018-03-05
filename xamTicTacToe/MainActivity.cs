﻿using System;
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

        private ImageView iv1, iv2, iv3, iv4, iv5, iv6, iv7, iv8, iv9;
        private int Currentplayer = 1;
        private ImageView[] Tiles;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

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

            //add a touch event to each imageview
            foreach (var tile in Tiles)
            {
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


        private void init()
        {


        }

        private void Check()
        {//horozontal

            //for (int i = 0; i < 3; i++)
            //{
            //    int j = i + 1;
            //    int k = j + 1;
            //    if (Tiles[i].Tag.ToString() == Tiles[j].Tag.ToString() && Tiles[j].Tag.ToString() == Tiles[k].Tag.ToString() && Tiles[k].Tag.ToString() != "none")
            //    {
            //        string Text = "Player " + Currentplayer + " won";
            //        Toast.MakeText(this, Text, ToastLength.Long).Show();
            //        Reset();
            //        return;
            //    }
            //}



            if (iv1.Tag.ToString() == iv2.Tag.ToString() && iv2.Tag.ToString() == iv3.Tag.ToString() && iv3.Tag.ToString() != "none" ||
                iv4.Tag.ToString() == iv5.Tag.ToString() && iv5.Tag.ToString() == iv6.Tag.ToString() && iv6.Tag.ToString() != "none" ||
                iv7.Tag.ToString() == iv8.Tag.ToString() && iv8.Tag.ToString() == iv9.Tag.ToString() && iv9.Tag.ToString() != "none"
                )
            {
                string Text = "Player " + Currentplayer + " won";
                Toast.MakeText(this, Text, ToastLength.Long).Show();
                Reset();
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
                Reset();
                return;
            }
            //Diagonal
            if (iv1.Tag.ToString() == iv5.Tag.ToString() && iv5.Tag.ToString() == iv9.Tag.ToString() && iv9.Tag.ToString() != "none" ||
                iv3.Tag.ToString() == iv5.Tag.ToString() && iv5.Tag.ToString() == iv7.Tag.ToString() && iv7.Tag.ToString() != "none"
                )
            {
                string Text = "Player " + Currentplayer + " won";
                Toast.MakeText(this, Text, ToastLength.Long).Show();
                Reset();
                return;
            }
        }


        private void Reset()
        {

            foreach (var iv in Tiles)
            {
                iv.Tag = "none";
                iv.SetImageResource(Resource.Drawable.blanktile);
                iv.Enabled = false;
            }

            Currentplayer = 1;

            Toast.MakeText(this, "New Game!", ToastLength.Long).Show();


            foreach (var iv in Tiles)
            {
               
                iv.Enabled = true;
            }
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

