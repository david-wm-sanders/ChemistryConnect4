using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Cryptography;
using System.Collections;
using System.Diagnostics;

namespace ChemistryConnect4
{
    public partial class mainForm : Form
    {
        public mainForm()
        {
            InitializeComponent();
        }

        // Defining oxidation states, players and molecules.

        int[] oxidStates = {  0 , -2 ,  1 ,  4 ,  2 , -3 ,  6 ,
                              6 ,  5 , -1 ,  5 ,  6 ,  0 ,  0 ,
                              0 ,  3 ,  2 ,  0 ,  1 , -4 ,  2 ,
                              5 ,  2 ,  5 ,  3 ,  1 ,  2 , -3 ,
                              4 ,  0 , -3 ,  0 ,  6 ,  0 ,  4 ,
                              5 ,  1 , -1 ,  2 ,  4 ,  5 ,  1 ,
                              2 ,  4 ,  0 ,  3 , -4 , -2 ,  4 };

        Color[,] gridSquareColor = new Color[7, 7];
        //Button[,] correspondingButtons = new Button[7, 7];

        //ArrayList winningGridCombo = new ArrayList();
        //Button[] winningButtons;

        Player p1 = new Player();
        Player p2 = new Player();
        Player activePlayer;

        Molecule[] currentMolecules;
        Molecule currentMolecule;

        Molecule Al_PLUS3 = new Molecule();
        Molecule BaCl2 = new Molecule();
        Molecule BF3 = new Molecule();
        Molecule BrO3_MINUS = new Molecule();
        Molecule C2H4 = new Molecule();
        Molecule Ca_OH_2 = new Molecule();
        Molecule CCl4 = new Molecule();
        Molecule CH4 = new Molecule();
        Molecule Cl2 = new Molecule();
        Molecule ClO2_MINUS = new Molecule();
        Molecule ClO_MINUS = new Molecule();
        Molecule CO = new Molecule();
        Molecule CO2 = new Molecule();
        Molecule Cu = new Molecule();
        Molecule F2 = new Molecule();
        Molecule Fe = new Molecule();
        Molecule Fe2O3 = new Molecule();
        Molecule HNO3 = new Molecule();
        Molecule KBrO3 = new Molecule();
        Molecule MgO = new Molecule();
        Molecule N2O = new Molecule();
        Molecule Na = new Molecule();
        Molecule NaCl = new Molecule();
        Molecule NaClO = new Molecule();
        Molecule NaHCO3 = new Molecule();
        Molecule NaNO2 = new Molecule();
        Molecule NH3 = new Molecule();
        Molecule N_MINUS3 = new Molecule();
        Molecule NO = new Molecule();
        Molecule PF5 = new Molecule();
        Molecule PO4_MINUS3 = new Molecule();
        Molecule SF6 = new Molecule();
        Molecule SO2 = new Molecule();
        Molecule SO3 = new Molecule();
        Molecule SO4_MINUS2 = new Molecule();
        Molecule TiO2 = new Molecule();

        ArrayList al = new ArrayList();

        // -----------------------------------------------------------------------------------------------

        public void labelButtons()
        {
            Button[] buttonGrid = { button1  , button2  , button3  , button4  , button5  , button6  , button7  ,
                                    button8  , button9  , button10 , button11 , button12 , button13 , button14 ,
                                    button15 , button16 , button17 , button18 , button19 , button20 , button21 ,
                                    button22 , button23 , button24 , button25 , button26 , button27 , button28 ,
                                    button29 , button30 , button31 , button32 , button33 , button34 , button35 ,
                                    button36 , button37 , button38 , button39 , button40 , button41 , button42 ,
                                    button43 , button44 , button45 , button46 , button47 , button48 , button49 };

            int x = 0;
            foreach (Button btn in buttonGrid)
            {
                btn.Text = oxidStates[x].ToString();
                x++;
            }
        }

        public void nextTurn()
        {
            if (activePlayer == p1)
            {
                activePlayer = p2;
                pictureBox3.Visible = true;
                pictureBox2.Visible = false;
            }
            else if (activePlayer == p2)
            {
                activePlayer = p1;
                pictureBox2.Visible = true;
                pictureBox3.Visible = false;
            }

            currentMolecule = randomMolecule();
            pictureBox1.Image = currentMolecule.molecularFormula;
        }

        private bool buttonXClicked(Button x)
        {
            if (currentMolecule.oxidNumber == Convert.ToInt32(x.Text))
            {
                x.BackColor = activePlayer.playerColor;
                x.Enabled = false;
                button50.Select();
                return true;
            }
            else
            {
                MessageBox.Show(String.Format("Sorry {0}. Your answer is incorrect.", activePlayer.name), "Incorrect!");
                button50.Select();
                return false;
            }
        }

        private int GetRandomSeed()
        {
            byte[] randomBytes = new byte[4];
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            rng.GetBytes(randomBytes);
            Int32 randomInt = BitConverter.ToInt32(randomBytes, 0);
            return randomInt;
        }

        private Player randomPlayer()
        {
            Player[] players = { p1, p2 };

            int randInt32 = GetRandomSeed();
            Random r = new Random(randInt32);
            int randNum = r.Next(players.Length);

            return players[randNum];
        }

        private Molecule randomMolecule()
        {
            Molecule[] allMolecules = { Al_PLUS3 , BaCl2 , BF3 , BrO3_MINUS , C2H4 , Ca_OH_2 , CCl4 ,
                                       CH4 , Cl2 , ClO2_MINUS , ClO_MINUS , CO , CO2 , Cu , F2 , Fe ,
                                       Fe2O3 , HNO3 , KBrO3 , MgO , N2O , NaCl , NaClO , NaHCO3 , NaNO2 ,
                                       NH3 , N_MINUS3 , NO , PF5 , PO4_MINUS3 , SF6 , SO2 , SO3 ,
                                       SO4_MINUS2 , TiO2 };

            // If the currentMolecules array is empty, then restore it so that it contains all the
            // molecules again.
            if (currentMolecules.Length == 0)
            {
                currentMolecules = allMolecules;
            }

            int randInt32 = GetRandomSeed();
            Random r = new Random(randInt32);
            int randNum = r.Next(currentMolecules.Length);

            Molecule molForm =  currentMolecules[randNum];

            // Set up an ArrayList and add the currentMolecules to it, remove the Molecule that was just
            // chosen from the ArrayList before letting currentMolecules = ArrayList.
            // This ensures that all Molecules are used before repetition due the random function occurs.
            // ArrayList al = new ArrayList();

            al.Clear();

            foreach (Molecule m in currentMolecules)
            {
                al.Add(m);
            }

            al.RemoveAt(randNum);
            currentMolecules = al.ToArray(System.Type.GetType("ChemistryConnect4.Molecule")) as Molecule[];

            return molForm;
        }

        private bool isThereAWinner(int y, int x)
        {
            bool win = false;
            //winningGridCombo.Clear();

            // Check squares vertically above
            if ((y - 1) >= 0)
            {
                if (gridSquareColor[(y - 1), x] == activePlayer.playerColor)
                {
                    if ((y - 2) >= 0)
                    {
                        if (gridSquareColor[(y - 2), x] == activePlayer.playerColor)
                        {
                            if ((y - 3) >= 0)
                            {
                                if (gridSquareColor[(y - 3), x] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[(y - 1), x]);
                                    //winningGridCombo.Add(correspondingButtons[(y - 2), x]);
                                    //winningGridCombo.Add(correspondingButtons[(y - 3), x]);
                                }
                            }

                            if ((y + 1) <= 6)
                            {
                                if (gridSquareColor[(y + 1), x] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[(y - 1), x]);
                                    //winningGridCombo.Add(correspondingButtons[(y - 2), x]);
                                    //winningGridCombo.Add(correspondingButtons[(y + 1), x]);
                                }
                            }
                        }
                    }
                }
            }

            // Check squares vertically below
            if ((y + 1) <= 6)
            {
                if (gridSquareColor[(y + 1), x] == activePlayer.playerColor)
                {
                    if ((y + 2) <= 6)
                    {
                        if (gridSquareColor[(y + 2), x] == activePlayer.playerColor)
                        {
                            if ((y + 3) <= 6)
                            {
                                if (gridSquareColor[(y + 3), x] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[(y + 1), x]);
                                    //winningGridCombo.Add(correspondingButtons[(y + 2), x]);
                                    //winningGridCombo.Add(correspondingButtons[(y + 3), x]);
                                }
                            }

                            if ((y - 1) >= 0)
                            {
                                if (gridSquareColor[(y - 1), x] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[(y + 1), x]);
                                    //winningGridCombo.Add(correspondingButtons[(y + 2), x]);
                                    //winningGridCombo.Add(correspondingButtons[(y - 1), x]);
                                }
                            }
                        }
                    }
                }
            }

            // Check squares to the left
            if ((x - 1) >= 0)
            {
                if (gridSquareColor[y, (x - 1)] == activePlayer.playerColor)
                {
                    if ((x - 2) >= 0)
                    {
                        if (gridSquareColor[y, (x - 2)] == activePlayer.playerColor)
                        {
                            if ((x - 3) >= 0)
                            {
                                if (gridSquareColor[y, (x - 3)] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[y, (x - 1)]);
                                    //winningGridCombo.Add(correspondingButtons[y, (x - 2)]);
                                    //winningGridCombo.Add(correspondingButtons[y, (x - 3)]);
                                }
                            }

                            if ((x + 1) <= 6)
                            {
                                if (gridSquareColor[y, (x + 1)] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[y, (x - 1)]);
                                    //winningGridCombo.Add(correspondingButtons[y, (x - 2)]);
                                    //winningGridCombo.Add(correspondingButtons[y, (x + 1)]);
                                }
                            }
                        }
                    }
                }
            }

            // Check squares to the right
            if ((x + 1) <= 6)
            {
                if (gridSquareColor[y, (x + 1)] == activePlayer.playerColor)
                {
                    if ((x + 2) <= 6)
                    {
                        if (gridSquareColor[y, (x + 2)] == activePlayer.playerColor)
                        {
                            if ((x + 3) <= 6)
                            {
                                if (gridSquareColor[y, (x + 3)] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[y, (x + 1)]);
                                    //winningGridCombo.Add(correspondingButtons[y, (x + 2)]);
                                    //winningGridCombo.Add(correspondingButtons[y, (x + 3)]);
                                }
                            }

                            if ((x - 1) >= 0)
                            {
                                if (gridSquareColor[y, (x - 1)] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[y, (x + 1)]);
                                    //winningGridCombo.Add(correspondingButtons[y, (x + 2)]);
                                    //winningGridCombo.Add(correspondingButtons[y, (x - 1)]);
                                }
                            }
                        }
                    }
                }
            }

            // Check the squares diagonally above-left
            if ((y - 1) >= 0 && (x - 1) >= 0)
            {
                if (gridSquareColor[(y - 1), (x - 1)] == activePlayer.playerColor)
                {
                    if ((y - 2) >= 0 && (x - 2) >= 0)
                    {
                        if (gridSquareColor[(y - 2), (x - 2)] == activePlayer.playerColor)
                        {
                            if ((y - 3) >= 0 && (x - 3) >= 0)
                            {
                                if (gridSquareColor[(y - 3), (x - 3)] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[(y - 1), (x - 1)]);
                                    //winningGridCombo.Add(correspondingButtons[(y - 2), (x - 2)]);
                                    //winningGridCombo.Add(correspondingButtons[(y - 3), (x - 3)]);
                                }
                            }

                            if ((y + 1) <= 6 && (x + 1) <= 6)
                            {
                                if (gridSquareColor[(y + 1), (x + 1)] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[(y - 1), (x - 1)]);
                                    //winningGridCombo.Add(correspondingButtons[(y - 2), (x - 2)]);
                                    //winningGridCombo.Add(correspondingButtons[(y + 1), (x + 1)]);
                                }
                            }
                        }
                    }
                }
            }

            // Check the squares diagonally below-right
            if ((y + 1) <= 6 && (x + 1) <= 6)
            {
                if (gridSquareColor[(y + 1), (x + 1)] == activePlayer.playerColor)
                {
                    if ((y + 2) <= 6 && (x + 2) <= 6)
                    {
                        if (gridSquareColor[(y + 2), (x + 2)] == activePlayer.playerColor)
                        {
                            if ((y + 3) <= 6 && (x + 3) <= 6)
                            {
                                if (gridSquareColor[(y + 3), (x + 3)] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[(y + 1), (x + 1)]);
                                    //winningGridCombo.Add(correspondingButtons[(y + 2), (x + 2)]);
                                    //winningGridCombo.Add(correspondingButtons[(y + 3), (x + 3)]);
                                }
                            }

                            if ((y - 1) >= 0 && (x - 1) >= 0)
                            {
                                if (gridSquareColor[(y - 1), (x - 1)] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[(y + 1), (x + 1)]);
                                    //winningGridCombo.Add(correspondingButtons[(y + 2), (x + 2)]);
                                    //winningGridCombo.Add(correspondingButtons[(y - 1), (x - 1)]);
                                }
                            }
                        }
                    }
                }
            }

            // Check the squares above-right
            if ((y - 1) >= 0 && (x + 1) <= 6)
            {
                if (gridSquareColor[(y - 1), (x + 1)] == activePlayer.playerColor)
                {
                    if ((y - 2) >= 0 && (x + 2) <= 6)
                    {
                        if (gridSquareColor[(y - 2), (x + 2)] == activePlayer.playerColor)
                        {
                            if ((y - 3) >= 0 && (x + 3) <= 6)
                            {
                                if (gridSquareColor[(y - 3), (x + 3)] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[(y - 1), (x + 1)]);
                                    //winningGridCombo.Add(correspondingButtons[(y - 2), (x + 2)]);
                                    //winningGridCombo.Add(correspondingButtons[(y - 3), (x + 3)]);
                                }
                            }

                            if ((y + 1) <= 6 && (x - 1) >= 0)
                            {
                                if (gridSquareColor[(y + 1), (x - 1)] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[(y - 1), (x + 1)]);
                                    //winningGridCombo.Add(correspondingButtons[(y - 2), (x + 2)]);
                                    //winningGridCombo.Add(correspondingButtons[(y + 1), (x - 1)]);
                                }
                            }
                        }
                    }
                }
            }

            // Check the squares below-left
            if ((y + 1) <= 6 && (x - 1) >= 0)
            {
                if (gridSquareColor[(y + 1), (x - 1)] == activePlayer.playerColor)
                {
                    if ((y + 2) <= 6 && (x - 2) >= 0)
                    {
                        if (gridSquareColor[(y + 2), (x - 2)] == activePlayer.playerColor)
                        {
                            if ((y + 3) <= 6 && (x - 3) >= 0)
                            {
                                if (gridSquareColor[(y + 3), (x - 3)] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[(y + 1), (x - 1)]);
                                    //winningGridCombo.Add(correspondingButtons[(y + 2), (x - 2)]);
                                    //winningGridCombo.Add(correspondingButtons[(y + 3), (x - 3)]);
                                }
                            }

                            if ((y - 1) >= 0 && (x + 1) <= 6)
                            {
                                if (gridSquareColor[(y - 1), (x + 1)] == activePlayer.playerColor)
                                {
                                    win = true;
                                    //winningGridCombo.Add(correspondingButtons[(y + 1), (x - 1)]);
                                    //winningGridCombo.Add(correspondingButtons[(y + 2), (x - 2)]);
                                    //winningGridCombo.Add(correspondingButtons[(y - 1), (x + 1)]);
                                }
                            }
                        }
                    }
                }
            }

            if (win) 
            {
                //winningGridCombo.Add(correspondingButtons[y, x]);
                //winningButtons = winningGridCombo.ToArray(Type.GetType("System.Windows.Forms.Button")) as Button[];
                return true; 
            }
            else { return false; }
        }

        private void winner()
        {
            Button[] buttonGrid = { button1  , button2  , button3  , button4  , button5  , button6  , button7  ,
                                    button8  , button9  , button10 , button11 , button12 , button13 , button14 ,
                                    button15 , button16 , button17 , button18 , button19 , button20 , button21 ,
                                    button22 , button23 , button24 , button25 , button26 , button27 , button28 ,
                                    button29 , button30 , button31 , button32 , button33 , button34 , button35 ,
                                    button36 , button37 , button38 , button39 , button40 , button41 , button42 ,
                                    button43 , button44 , button45 , button46 , button47 , button48 , button49 };

            foreach (Button btn in buttonGrid)
            {
                btn.Enabled = false;
            }

            button51.Enabled = false;

            //whiteFlagAllButtons();

            //timer1.Enabled = true;

            if (MessageBox.Show(String.Format("Congratulations {0}. You have won the game!\nWould you like to start a new game?", activePlayer.name),
                                              "Winner!",
                                              MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                newGameToolStripMenuItem_Click(newGameToolStripMenuItem, new EventArgs());
            }
        }

        private void mainForm_Load(object sender, EventArgs e)
        {
            // Setting up Players and Molecules.

            // Setup
            Al_PLUS3.Setup(ChemistryConnect4.Properties.Resources.AlPLUS3, 3);
            BaCl2.Setup(ChemistryConnect4.Properties.Resources.BaCl2, 2); 
            BF3.Setup(ChemistryConnect4.Properties.Resources.BF3, 3);
            BrO3_MINUS.Setup(ChemistryConnect4.Properties.Resources.BrO3MINUS, 5);
            C2H4.Setup(ChemistryConnect4.Properties.Resources.C2H4, -2);
            Ca_OH_2.Setup(ChemistryConnect4.Properties.Resources.Ca_OH_2, 2);
            CCl4.Setup(ChemistryConnect4.Properties.Resources.CCl4, 4);
            CH4.Setup(ChemistryConnect4.Properties.Resources.CH4, -4);
            Cl2.Setup(ChemistryConnect4.Properties.Resources.CL2, 0);
            ClO2_MINUS.Setup(ChemistryConnect4.Properties.Resources.ClO2MINUS, 3);
            ClO_MINUS.Setup(ChemistryConnect4.Properties.Resources.ClOMINUS, 1);
            CO.Setup(ChemistryConnect4.Properties.Resources.CO, 2);
            CO2.Setup(ChemistryConnect4.Properties.Resources.CO2, 4);
            Cu.Setup(ChemistryConnect4.Properties.Resources.Cu, 0);
            F2.Setup(ChemistryConnect4.Properties.Resources.F2, 0);
            Fe.Setup(ChemistryConnect4.Properties.Resources.Fe, 0);
            Fe2O3.Setup(ChemistryConnect4.Properties.Resources.Fe2O3, 3);
            HNO3.Setup(ChemistryConnect4.Properties.Resources.HNO3, 5);
            KBrO3.Setup(ChemistryConnect4.Properties.Resources.KBrO3, 5);
            MgO.Setup(ChemistryConnect4.Properties.Resources.MgO, -2);
            N2O.Setup(ChemistryConnect4.Properties.Resources.N2O, 1);
            Na.Setup(ChemistryConnect4.Properties.Resources.Na, 0);
            NaCl.Setup(ChemistryConnect4.Properties.Resources.NaCl, -1);
            NaClO.Setup(ChemistryConnect4.Properties.Resources.NaClO, 1);
            NaHCO3.Setup(ChemistryConnect4.Properties.Resources.NaHCO3, 4);
            NaNO2.Setup(ChemistryConnect4.Properties.Resources.NaNO2, 1);
            NH3.Setup(ChemistryConnect4.Properties.Resources.NH3, -3);
            N_MINUS3.Setup(ChemistryConnect4.Properties.Resources.NMINUS3, -3);
            NO.Setup(ChemistryConnect4.Properties.Resources.NO, 2);
            PF5.Setup(ChemistryConnect4.Properties.Resources.PF5, 5);
            PO4_MINUS3.Setup(ChemistryConnect4.Properties.Resources.PO4MINUS3, 5);
            SF6.Setup(ChemistryConnect4.Properties.Resources.SF6, 6);
            SO2.Setup(ChemistryConnect4.Properties.Resources.SO2, 4);
            SO3.Setup(ChemistryConnect4.Properties.Resources.SO3, 6);
            SO4_MINUS2.Setup(ChemistryConnect4.Properties.Resources.SO4MINUS2, 6);
            TiO2.Setup(ChemistryConnect4.Properties.Resources.TiO2, 4);

            Molecule[] allMolecules = { Al_PLUS3 , BaCl2 , BF3 , BrO3_MINUS , C2H4 , Ca_OH_2 , CCl4 ,
                                       CH4 , Cl2 , ClO2_MINUS , ClO_MINUS , CO , CO2 , Cu , F2 , Fe ,
                                       Fe2O3 , HNO3 , KBrO3 , MgO , N2O , NaCl , NaClO , NaHCO3 , NaNO2 ,
                                       NH3 , N_MINUS3 , NO , PF5 , PO4_MINUS3 , SF6 , SO2 , SO3 ,
                                       SO4_MINUS2 , TiO2 };

            currentMolecules = allMolecules;

            p1.playerColor = Color.Blue;
            p1.highlightColor = Color.DarkBlue;
            p1.name = "Player 1";
            p2.playerColor = Color.Red;
            p2.highlightColor = Color.DarkRed;
            p2.name = "Player 2";

            p1Label.ForeColor = p1.playerColor;
            p2Label.ForeColor = p2.playerColor;

            Button[] buttonGrid = { button1  , button2  , button3  , button4  , button5  , button6  , button7  ,
                                    button8  , button9  , button10 , button11 , button12 , button13 , button14 ,
                                    button15 , button16 , button17 , button18 , button19 , button20 , button21 ,
                                    button22 , button23 , button24 , button25 , button26 , button27 , button28 ,
                                    button29 , button30 , button31 , button32 , button33 , button34 , button35 ,
                                    button36 , button37 , button38 , button39 , button40 , button41 , button42 ,
                                    button43 , button44 , button45 , button46 , button47 , button48 , button49 };

            /*
            int posy;
            int posx;
            int posz = 0;
            for (posy = 0; posy == 6; posy++)
            {
                for (posx = 0; posx == 6; posx++)
                {
                    correspondingButtons[posy, posx] = buttonGrid[posz];
                    posz++;
                }
            }
             */

            //-------------------------------------------------------------------------------------------

            whiteFlagAllButtons();
            labelButtons();

            // Pick a random player to start.
            activePlayer = randomPlayer();
            if (activePlayer == p1)
            {
                pictureBox3.Visible = false;
                pictureBox2.Visible = true;
            }
            else
            {
                pictureBox3.Visible = true;
                pictureBox2.Visible = false;
            }

            currentMolecule = randomMolecule();
            pictureBox1.Image = currentMolecule.molecularFormula;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button1))
            {
                gridSquareColor[0, 0] = activePlayer.playerColor;
                if (isThereAWinner(0, 0))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button2))
            {
                gridSquareColor[0, 1] = activePlayer.playerColor;
                if (isThereAWinner(0, 1))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button3))
            {
                gridSquareColor[0, 2] = activePlayer.playerColor;
                if (isThereAWinner(0, 2))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button4))
            {
                gridSquareColor[0, 3] = activePlayer.playerColor;
                if (isThereAWinner(0, 3))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button5))
            {
                gridSquareColor[0, 4] = activePlayer.playerColor;
                if (isThereAWinner(0, 4))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button6))
            {
                gridSquareColor[0, 5] = activePlayer.playerColor;
                if (isThereAWinner(0, 5))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button7))
            {
                gridSquareColor[0, 6] = activePlayer.playerColor;
                if (isThereAWinner(0, 6))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button8))
            {
                gridSquareColor[1, 0] = activePlayer.playerColor;
                if (isThereAWinner(1, 0))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button9))
            {
                gridSquareColor[1, 1] = activePlayer.playerColor;
                if (isThereAWinner(1, 1))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button10))
            {
                gridSquareColor[1, 2] = activePlayer.playerColor;
                if (isThereAWinner(1, 2))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button11))
            {
                gridSquareColor[1, 3] = activePlayer.playerColor;
                if (isThereAWinner(1, 3))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button12))
            {
                gridSquareColor[1, 4] = activePlayer.playerColor;
                if (isThereAWinner(1, 4))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button13))
            {
                gridSquareColor[1, 5] = activePlayer.playerColor;
                if (isThereAWinner(1, 5))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button14))
            {
                gridSquareColor[1, 6] = activePlayer.playerColor;
                if (isThereAWinner(1, 6))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button15))
            {
                gridSquareColor[2, 0] = activePlayer.playerColor;
                if (isThereAWinner(2, 0))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button16))
            {
                gridSquareColor[2, 1] = activePlayer.playerColor;
                if (isThereAWinner(2, 1))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button17))
            {
                gridSquareColor[2, 2] = activePlayer.playerColor;
                if (isThereAWinner(2, 2))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button18))
            {
                gridSquareColor[2, 3] = activePlayer.playerColor;
                if (isThereAWinner(2, 3))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button19_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button19))
            {
                gridSquareColor[2, 4] = activePlayer.playerColor;
                if (isThereAWinner(2, 4))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button20))
            {
                gridSquareColor[2, 5] = activePlayer.playerColor;
                if (isThereAWinner(2, 5))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button21_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button21))
            {
                gridSquareColor[2, 6] = activePlayer.playerColor;
                if (isThereAWinner(2, 6))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button22_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button22))
            {
                gridSquareColor[3, 0] = activePlayer.playerColor;
                if (isThereAWinner(3, 0))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button23))
            {
                gridSquareColor[3, 1] = activePlayer.playerColor;
                if (isThereAWinner(3, 1))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button24))
            {
                gridSquareColor[3, 2] = activePlayer.playerColor;
                if (isThereAWinner(3, 2))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button25_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button25))
            {
                gridSquareColor[3, 3] = activePlayer.playerColor;
                if (isThereAWinner(3, 3))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button26_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button26))
            {
                gridSquareColor[3, 4] = activePlayer.playerColor;
                if (isThereAWinner(3, 4))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button27))
            {
                gridSquareColor[3, 5] = activePlayer.playerColor;
                if (isThereAWinner(3, 5))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button28_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button28))
            {
                gridSquareColor[3, 6] = activePlayer.playerColor;
                if (isThereAWinner(3, 6))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button29_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button29))
            {
                gridSquareColor[4, 0] = activePlayer.playerColor;
                if (isThereAWinner(4, 0))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button30_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button30))
            {
                gridSquareColor[4, 1] = activePlayer.playerColor;
                if (isThereAWinner(4, 1))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button31_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button31))
            {
                gridSquareColor[4, 2] = activePlayer.playerColor;
                if (isThereAWinner(4, 2))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button32_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button32))
            {
                gridSquareColor[4, 3] = activePlayer.playerColor;
                if (isThereAWinner(4, 3))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button33_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button33))
            {
                gridSquareColor[4, 4] = activePlayer.playerColor;
                if (isThereAWinner(4, 4))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button34_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button34))
            {
                gridSquareColor[4, 5] = activePlayer.playerColor;
                if (isThereAWinner(4, 5))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button35_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button35))
            {
                gridSquareColor[4, 6] = activePlayer.playerColor;
                if (isThereAWinner(4, 6))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button36))
            {
                gridSquareColor[5, 0] = activePlayer.playerColor;
                if (isThereAWinner(5, 0))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button37_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button37))
            {
                gridSquareColor[5, 1] = activePlayer.playerColor;
                if (isThereAWinner(5, 1))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button38_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button38))
            {
                gridSquareColor[5, 2] = activePlayer.playerColor;
                if (isThereAWinner(5, 2))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button39_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button39))
            {
                gridSquareColor[5, 3] = activePlayer.playerColor;
                if (isThereAWinner(5, 3))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button40_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button40))
            {
                gridSquareColor[5, 4] = activePlayer.playerColor;
                if (isThereAWinner(5, 4))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button41_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button41))
            {
                gridSquareColor[5, 5] = activePlayer.playerColor;
                if (isThereAWinner(5, 5))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button42_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button42))
            {
                gridSquareColor[5, 6] = activePlayer.playerColor;
                if (isThereAWinner(5, 6))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button43_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button43))
            {
                gridSquareColor[6, 0] = activePlayer.playerColor;
                if (isThereAWinner(6, 0))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button44_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button44))
            {
                gridSquareColor[6, 1] = activePlayer.playerColor;
                if (isThereAWinner(6, 1))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button45_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button45))
            {
                gridSquareColor[6, 2] = activePlayer.playerColor;
                if (isThereAWinner(6, 2))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button46_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button46))
            {
                gridSquareColor[6, 3] = activePlayer.playerColor;
                if (isThereAWinner(6, 3))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button47_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button47))
            {
                gridSquareColor[6, 4] = activePlayer.playerColor;
                if (isThereAWinner(6, 4))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button48_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button48))
            {
                gridSquareColor[6, 5] = activePlayer.playerColor;
                if (isThereAWinner(6, 5))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void button49_Click(object sender, EventArgs e)
        {
            if (buttonXClicked(button49))
            {
                gridSquareColor[6, 6] = activePlayer.playerColor;
                if (isThereAWinner(6 ,6))
                {
                    winner();
                }
                else { nextTurn(); }
            }

            else { nextTurn(); }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //timer1.Enabled = false;
            whiteFlagAllButtons();

            Button[] buttonGrid = { button1  , button2  , button3  , button4  , button5  , button6  , button7  ,
                                    button8  , button9  , button10 , button11 , button12 , button13 , button14 ,
                                    button15 , button16 , button17 , button18 , button19 , button20 , button21 ,
                                    button22 , button23 , button24 , button25 , button26 , button27 , button28 ,
                                    button29 , button30 , button31 , button32 , button33 , button34 , button35 ,
                                    button36 , button37 , button38 , button39 , button40 , button41 , button42 ,
                                    button43 , button44 , button45 , button46 , button47 , button48 , button49 };

            foreach (Button btn in buttonGrid)
            {
                // Reset all the buttons to their original states.
                btn.BackColor = Color.White;
                btn.Enabled = true;
            }

            button51.Enabled = true;

            activePlayer = randomPlayer();
            if (activePlayer == p1)
            {
                pictureBox3.Visible = false;
                pictureBox2.Visible = true;
            }
            else
            {
                pictureBox3.Visible = true;
                pictureBox2.Visible = false;
            }

            Molecule[] allMolecules = { Al_PLUS3 , BaCl2 , BF3 , BrO3_MINUS , C2H4 , Ca_OH_2 , CCl4 ,
                                       CH4 , Cl2 , ClO2_MINUS , ClO_MINUS , CO , CO2 , Cu , F2 , Fe ,
                                       Fe2O3 , HNO3 , KBrO3 , MgO , N2O , NaCl , NaClO , NaHCO3 , NaNO2 ,
                                       NH3 , N_MINUS3 , NO , PF5 , PO4_MINUS3 , SF6 , SO2 , SO3 ,
                                       SO4_MINUS2 , TiO2 };

            // Reset to using all molecules upon the starting of a new game.
            currentMolecules = allMolecules;

            currentMolecule = randomMolecule();
            pictureBox1.Image = currentMolecule.molecularFormula;

            // Select the hidden button, so that the last button selected isn't highlighted, even after
            // a game has been won.
            button50.Select();
        }

        private void whiteFlagAllButtons()
        {
            int y;
            int x;
            for (y = 0; y < 7; y++)
            {
                for (x = 0; x < 7; x++)
                {
                    gridSquareColor[y, x] = Color.White;
                }
            }
        }

        private void chemConnect4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AboutBox1().ShowDialog(this);
        }

        private void hDRToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Process.Start("http://www.halcyondr.co.uk/");
        }

        private void button51_Click(object sender, EventArgs e)
        {
            button50.Select();
            nextTurn();
        }

        /*
        private void timer1_Tick(object sender, EventArgs e)
        {
            if (winningButtons[0].BackColor == activePlayer.playerColor)
            {
                foreach (Button btn in winningButtons)
                {
                    btn.BackColor = activePlayer.highlightColor;
                }
            }
            else if (winningButtons[0].BackColor == activePlayer.highlightColor)
            {
                foreach (Button btn in winningButtons)
                {
                    btn.BackColor = activePlayer.playerColor;
                }
            }
        }
         */

    }

    public class Player
    {
        private Color m_playerColor;

        public Color playerColor
        {
            get { return m_playerColor; }
            set { m_playerColor = value; }
        }

        private string m_name;

        public string name
        {
            get { return m_name; }
            set { m_name = value; }
        }

        private Color m_highlightColor;

        public Color highlightColor
        {
            get { return m_highlightColor; }
            set { m_highlightColor = value; }
        }
    }

    public class Molecule
    {
        private Image m_molecularFormula;

        public Image molecularFormula
        {
            get { return m_molecularFormula; }
            set { m_molecularFormula = value; }
        }

        private int m_oxidNumber;

        public int oxidNumber
        {
            get { return m_oxidNumber; }
            set { m_oxidNumber = value; }
        }

        public void Setup(Image MF, int ON)
        {
            molecularFormula = MF;
            oxidNumber = ON;
        }
    }
}
