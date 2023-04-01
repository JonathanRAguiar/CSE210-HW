
using System;
using System.Windows.Forms;

using hash.Game.Dialogs;

namespace hash.Game
{
    class GameLogic
    {
        
        private static Control[] possibleMovements;

        
        public static void EnemyMovement(String symbol, Control[,] bf)
        {
            
            int countPossiblePositions = 0;

            Random random = new Random();
            int randomPosition;

            int i = 0;
            int j = 0;

            
            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    if (bf[i, j].Text.Equals(""))
                    {
                        countPossiblePositions++;
                    }
                }
            }

            if (countPossiblePositions == 0)
            {
				DrawDialog.Create();

                return;
            }

            
            possibleMovements = new Control[countPossiblePositions];

            
            randomPosition = random.Next(possibleMovements.Length);

            countPossiblePositions = 0;

            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    if (bf[i, j].Text.Equals(""))
                    {
                        possibleMovements[countPossiblePositions] = bf[i, j];
                        countPossiblePositions++;
                    }
                }
            }
            
            int countHumanSelection = 0;
            Control freePosIAplay = new Control();
        
            
            if ((!bf[0, 0].Text.Equals(symbol) && (!bf[0, 0].Text.Equals(""))) &&
                (!bf[1, 1].Text.Equals(symbol) && (!bf[1, 1].Text.Equals(""))))
            {
                if (bf[2, 2].Text.Equals(""))
                {
                    bf[2, 2].Text = symbol;
                    bf[2, 2].Enabled = false;
                    return;
                }                
            }

            if ((!bf[0, 0].Text.Equals(symbol) && (!bf[0, 0].Text.Equals(""))) &&
                (!bf[2, 2].Text.Equals(symbol) && (!bf[2, 2].Text.Equals(""))))
            {
                if (bf[1, 1].Text.Equals(""))
                {
                    bf[1, 1].Text = symbol;
                    bf[1, 1].Enabled = false;
                    return;
                }
            }

            if ((!bf[2, 2].Text.Equals(symbol) && (!bf[2, 2].Text.Equals(""))) &&
                (!bf[1, 1].Text.Equals(symbol) && (!bf[1, 1].Text.Equals(""))))
            {
                if (bf[0, 0].Text.Equals(""))
                {
                    bf[0, 0].Text = symbol;
                    bf[0, 0].Enabled = false;
                    return;
                }
            }

            
            if ((!bf[0, 2].Text.Equals(symbol) && (!bf[0, 2].Text.Equals(""))) &&
                (!bf[1, 1].Text.Equals(symbol) && (!bf[1, 1].Text.Equals(""))))
            {
                if (bf[2, 0].Text.Equals(""))
                {
                    bf[2, 0].Text = symbol;
                    bf[2, 0].Enabled = false;
                    return;
                }
            }

            if ((!bf[0, 2].Text.Equals(symbol) && (!bf[0, 2].Text.Equals(""))) &&
                (!bf[2, 0].Text.Equals(symbol) && (!bf[2, 0].Text.Equals(""))))
            {
                if (bf[1, 1].Text.Equals(""))
                {
                    bf[1, 1].Text = symbol;
                    bf[1, 1].Enabled = false;
                    return;
                }
            }

            if ((!bf[2, 0].Text.Equals(symbol) && (!bf[2, 0].Text.Equals(""))) &&
                (!bf[1, 1].Text.Equals(symbol) && (!bf[1, 1].Text.Equals(""))))
            {
                if (bf[0, 2].Text.Equals(""))
                {
                    bf[0, 2].Text = symbol;
                    bf[0, 2].Enabled = false;
                    return;
                }
            }

            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    if (bf[i, j].Text.Equals(""))
                    {
                        freePosIAplay = bf[i, j];
                    }
                    else
                    {
                        if (!bf[i, j].Text.Equals(symbol))
                        {
                            countHumanSelection++;
                        }
                    }
                }

                if (countHumanSelection == 2 && freePosIAplay.Text.Equals("") && !freePosIAplay.Name.Equals(""))
                {
                    freePosIAplay.Text = symbol;
                    freePosIAplay.Enabled = false;
                    return;
                }
                else
                {
                    countHumanSelection = 0;
                    freePosIAplay = new Control();
                }

            }

            countHumanSelection = 0;

            for (i = 0; i < 3; i++)
            {
                for (j = 0; j < 3; j++)
                {
                    if (bf[j, i].Text.Equals(""))
                    {
                        freePosIAplay = bf[j, i];
                    }
                    else
                    {
                        if (!bf[j, i].Text.Equals(symbol))
                        {
                            countHumanSelection++;
                        }
                    }
                }

                if (countHumanSelection == 2 && freePosIAplay.Text.Equals("") && !freePosIAplay.Name.Equals(""))
                {
                    freePosIAplay.Text = symbol;
                    freePosIAplay.Enabled = false;
                    return;
                }
                else
                {
                    countHumanSelection = 0;
                    freePosIAplay = new Control();
                }
            }

            if (bf[1, 1].Text.Equals(""))
            {
                bf[1, 1].Text = symbol;
                bf[1, 1].Enabled = false;
                return;
            }
            else
            {
                possibleMovements[randomPosition].Text = symbol;
                possibleMovements[randomPosition].Enabled = false;
            }
        }



        public static bool CheckVictory(Control[,] bf, String symbol)
        {
            if ((bf[0, 0].Text.Equals(symbol) && bf[0, 1].Text.Equals(symbol) && bf[0, 2].Text.Equals(symbol)) ||
                (bf[1, 0].Text.Equals(symbol) && bf[1, 1].Text.Equals(symbol) && bf[1, 2].Text.Equals(symbol)) ||
                (bf[2, 0].Text.Equals(symbol) && bf[2, 1].Text.Equals(symbol) && bf[2, 2].Text.Equals(symbol)) ||
                (bf[0, 0].Text.Equals(symbol) && bf[1, 0].Text.Equals(symbol) && bf[2, 0].Text.Equals(symbol)) ||
                (bf[0, 1].Text.Equals(symbol) && bf[1, 1].Text.Equals(symbol) && bf[2, 1].Text.Equals(symbol)) ||
                (bf[0, 2].Text.Equals(symbol) && bf[1, 2].Text.Equals(symbol) && bf[2, 2].Text.Equals(symbol)) ||
                (bf[0, 0].Text.Equals(symbol) && bf[1, 1].Text.Equals(symbol) && bf[2, 2].Text.Equals(symbol)) ||
                (bf[0, 2].Text.Equals(symbol) && bf[1, 1].Text.Equals(symbol) && bf[2, 0].Text.Equals(symbol)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}