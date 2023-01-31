using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Diagnostics;
namespace Battleship
{
    public partial class Form1 : Form
    {

        List<Button> playerPositionButtons;
        List<Button> enemyPositionButtons;

        Random rand = new Random();
        int totalShips = 15;
        int round = 100;
        int playerScore;
        int enemyScore;


        public Form1()
        {
            InitializeComponent();
            RestartGame();
        }

        private void EnemyPlayTimerEvent(object sender, EventArgs e)
        {
            if (playerPositionButtons.Count > 0 && round > 0)
            {
                round = round - 1;

                TxtRounds.Text = "Round left: " + round;

                int index = rand.Next(playerPositionButtons.Count);
            
                if ((string)playerPositionButtons[index].Tag == "playerShip")
                {
                    playerPositionButtons[index].BackgroundImage = Properties.Resources.fireIcon1;
                    enemyMove.Text = playerPositionButtons[index].Text;
                    playerPositionButtons[index].Enabled = false;
                    playerPositionButtons[index].BackColor = Color.Brown;
                    playerPositionButtons.RemoveAt(index);
                    enemyScore = enemyScore + 1;
                    TxtEnemy.Text = enemyScore.ToString();
                    EnemyPlayTimer.Stop();
                }
                else
                {
                    playerPositionButtons[index].BackgroundImage = Properties.Resources.missIcon1;
                    enemyMove.Text = playerPositionButtons[index].Name;
                    playerPositionButtons[index].Enabled = false;
                    playerPositionButtons[index].BackColor = Color.Brown;
                    playerPositionButtons.RemoveAt(index);
                    EnemyPlayTimer.Stop();
                }
            }

            if(round < 1 || enemyScore > 14 || playerScore > 14)
            {

                if(playerScore > enemyScore)
                {
                    MessageBox.Show("You win the battle!", "Win!");
                    RestartGame();
                }
                else if (enemyScore > playerScore)
                {
                    MessageBox.Show("You lose the battle", "Lose!");
                    RestartGame();
                }
                else if (enemyScore == playerScore)
                {
                    MessageBox.Show("No one wins this game", "Draw");
                    RestartGame();
                }
            }


        }

        private void AttackButtonEvent(object sender, EventArgs e)
        {
            if (EnemyLocationListBox.Text != "")
            {
                var attackPosition = EnemyLocationListBox.Text.ToLower();
                
                int index = enemyPositionButtons.FindIndex(a => a.Name == attackPosition + "_");
                
                if (round >= 0 && enemyPositionButtons[index].Enabled)
                {
                    round -= 1;
                    TxtRounds.Text = "Rounds left: " + round;

                    if ((string)enemyPositionButtons[index].Tag == "enemyShip")
                    {
                        enemyPositionButtons[index].Enabled = false;
                        enemyPositionButtons[index].BackgroundImage = Properties.Resources.fireIcon1;
                        enemyPositionButtons[index].BackColor = Color.Brown;
                        playerScore = playerScore + 1;
                        TxtPlayer.Text = playerScore.ToString();
                        EnemyPlayTimer.Start();

                    }
                    else
                    {
                        enemyPositionButtons[index].Enabled = false;
                        enemyPositionButtons[index].BackgroundImage = Properties.Resources.missIcon1;
                        enemyPositionButtons[index].BackColor = Color.Brown;
                        EnemyPlayTimer.Start();
                    }
                }

            }
            else
            {
                MessageBox.Show("Select a location from the drop down first, then press the Attack button", "Information");

            }
        }

        private void PlayerPositionButtonsEvent(object sender, EventArgs e)
        {
            if(totalShips > 0)
            {
                var button = (Button)sender;

                button.Enabled = false;
                button.Tag = "playerShip";
                button.BackColor = Color.Orange;
                totalShips = totalShips - 1;
            }

            if(totalShips == 0) {
                btnAttack.Enabled = true;
                btnAttack.BackColor = Color.Red;
                btnAttack.ForeColor = Color.White;

                TxtHelp.Text = "2) Now pick the attack position from the drop down";
                
            }

        }

        private void RestartGame()
        {
            playerPositionButtons = new List<Button> { a1, a2, a3, a4, a5, a6, a7, a8, a9, a10, b1, b2, b3, b4, b5, b6, b7, b8, b9, b10, c1, c2, c3, c4, c5, c6, c7, c8, c9, c10, d1, d2, d3, d4, d5, d6, d7, d8, d9, d10, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, f1, f2, f3, f4, f5, f6, f7, f8, f9, f10, g1, g2, g3, g4, g5, g6, g7, g8, g9, g10, h1, h2, h3, h4, h5, h6, h7, h8, h9, h10, i1, i2, i3, i4, i5, i6, i7, i8, i9, i10, j1, j2, j3, j4, j5, j6, j7, j8, j9, j10 };
            enemyPositionButtons = new List<Button> { a1_, a2_, a3_, a4_, a5_, a6_, a7_, a8_, a9_, a10_, b1_, b2_, b3_, b4_, b5_, b6_, b7_, b8_, b9_, b10_, c1_, c2_, c3_, c4_, c5_, c6_, c7_, c8_, c9_, c10_, d1_, d2_, d3_, d4_, d5_, d6_, d7_, d8_, d9_, d10_, e1_, e2_, e3_, e4_, e5_, e6_, e7_, e8_, e9_, e10_, f1_, f2_, f3_, f4_, f5_, f6_, f7_, f8_, f9_, f10_, g1_, g2_, g3_, g4_, g5_, g6_, g7_, g8_, g9_, g10_, h1_, h2_, h3_, h4_, h5_, h6_, h7_, h8_, h9_, h10_, i1_, i2_, i3_, i4_, i5_, i6_, i7_, i8_, i9_, i10_, j1_, j2_, j3_, j4_, j5_, j6_, j7_, j8_, j9_, j10_ };

            EnemyLocationListBox.Items.Clear();
            EnemyLocationListBox.Text = null;

            TxtHelp.Text = "1) Place on 15 diffrent locations battleships to start!!";

            for (int i = 0; i < enemyPositionButtons.Count; i++)
            {
                enemyPositionButtons[i].Enabled = true;
                enemyPositionButtons[i].Tag = null;
                enemyPositionButtons[i].BackColor = Color.White;
                enemyPositionButtons[i].BackgroundImage = null;
                
                string originalString = enemyPositionButtons[i].Name;
                enemyPositionButtons[i].Name = enemyPositionButtons[i].Name.Remove(enemyPositionButtons[i].Name.Length - 1);
                EnemyLocationListBox.Items.Add(enemyPositionButtons[i].Name);
                enemyPositionButtons[i].Name = originalString;

            }

            for (int i = 0; i < playerPositionButtons.Count; i++)
            {
                playerPositionButtons[i].Enabled = true;
                playerPositionButtons[i].Tag = null;
                playerPositionButtons[i].BackColor = Color.White;
                playerPositionButtons[i].BackgroundImage = null;

            }

            playerScore = 0;
            enemyScore = 0;
            round = 100;
            totalShips = 15;

            TxtPlayer.Text = playerScore.ToString();
            TxtEnemy.Text = enemyScore.ToString();
            enemyMove.Text = "  ";

            btnAttack.Enabled = false;

            enemyLocationPicker();
        }

        private void enemyLocationPicker() 
        {

            for (int i = 0; i < 15; i++)
            {
                int index = rand.Next(enemyPositionButtons.Count);

                if (enemyPositionButtons[index].Enabled == true && (String)enemyPositionButtons[index].Tag == null)
                {
                    enemyPositionButtons[index].Tag = "enemyShip";

                    string originalString = enemyPositionButtons[index].Name;
                    string debugName =  enemyPositionButtons[index].Name.Remove(enemyPositionButtons[index].Name.Length - 1);

                    Debug.WriteLine("Enemy Position: " + debugName);

                    enemyPositionButtons[index].Name = originalString;
                }
                else
                {
                    index = rand.Next(enemyPositionButtons.Count);
                }
            }
        }

        private void j7__Click(object sender, EventArgs e)
        {

        }

        private void j1__Click(object sender, EventArgs e)
        {

        }

        private void i2__Click(object sender, EventArgs e)
        {

        }

        private void i4__Click(object sender, EventArgs e)
        {

        }

        private void h5__Click(object sender, EventArgs e)
        {

        }
    }
}
