using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MatchingPairsGame
{
    public partial class MainGameForm : Form
    {
        Label firstClicked = null;
        Label secondClicked = null;
        Random random = new Random(); //to generate icons in random spots
        int counter =25;//number of seconds for the game
        

        //store icons in list
        List<string> icons = new List<string>()
        {
            "!","!", "N","N",",",",","k","k","b","b","v","v","w","w","z","z"
        };//8 pairs for 16 boxes

        public MainGameForm()
        {
            InitializeComponent();
            timerLbl.Text = "Timer: " + counter.ToString();
            gameTimer.Start();
            AssignIconsToSquares();
           
        }

        private void AssignIconsToSquares()
        {
            foreach(Control control in tableLayoutPanel1.Controls)
            {
                Label iconLabel = control as Label;
                if (iconLabel != null)
                {
                    int randomNumber = random.Next(icons.Count);//uses icons.Count for range
                    iconLabel.Text = icons[randomNumber];

                    iconLabel.ForeColor = iconLabel.BackColor;//make all the icons disappear by making text color same as background color
                    icons.RemoveAt(randomNumber);//remove that icon from the list so it doesnt gt used more than once
                }
            }
        }

        private void label_click(object sender, EventArgs e)
        {
            if (timer1.Enabled == true)//ignore clicks if timer is running
                return;

            Label clickedLabel = sender as Label;//uses sender to itdentfy which label was selected
            if(clickedLabel != null)
            {
                if (clickedLabel.ForeColor == Color.Black)//if its already been choosen
                    return;

                if (firstClicked == null)
                {
                    firstClicked = clickedLabel;
                    firstClicked.ForeColor = Color.Black;//if not already choosen change color to black
                    return;
                }

                secondClicked = clickedLabel;
                secondClicked.ForeColor = Color.Black;

                if(firstClicked.Text == secondClicked.Text)//keep the matched pairs on screen
                {
                    firstClicked.BackColor = Color.White;
                    secondClicked.BackColor = Color.White;
                    checkForWinner();
                    firstClicked = null;
                    secondClicked = null;//stop keeping track of labels
                    return;//stop timer
                }

                timer1.Start();
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            firstClicked.ForeColor = firstClicked.BackColor;
            secondClicked.ForeColor = secondClicked.BackColor;
            firstClicked = null;
            secondClicked = null;
        }

        private void checkForWinner()
        {
            foreach (Control control in tableLayoutPanel1.Controls)
            {//loops through all the labels in the layout panel
                Label iconLabel = control as Label;

                if (iconLabel != null)
                {
                    if (iconLabel.BackColor != Color.White)
                        return;
                }
            }

            MessageBox.Show("You matched all the icons!", "Congrats");
            Close();
        }//end of checkforwinner

        private void gameTimer_Tick(object sender, EventArgs e)
        {
            counter--;
            timerLbl.Text = "Timer: "+counter.ToString();
            if (counter == 0)
            {
                gameTimer.Stop();
                MessageBox.Show("Time is up!", "You Lost");
                Close();
            }
        }
    }
}
