using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class GameTable : MonoBehaviour
{

	private int maxDepth;


	private int[,] gameTable = { 
            { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0, 0 }
                                };
    //1-yellow, 2- red, 0-empty

	public void SetMaxDepth(int d)
    {
		maxDepth = d;
    }

    public bool GetFreeRows(int col)
    {
        if (gameTable[col, 5] != 0)
            return false;
        else
            return true;
    }

	private bool MovesLeft()
	{
		for (int i = 0; i < 7; i++)
		{
			if (GetFreeRows(i)) return true;
		}

		Debug.Log("Nie zostaｳo wi鹹ej ruch");
		return false;

	}

	public void AddToTable(int col, int tok)
    {
        int i = 0;

        while(i <= 5)
        {
            if(gameTable[col, i] == 0)
            {
                gameTable[col, i] = tok;
                break;
            }
            else
                i++;
        }                
    }

	private void RemoveTopCoin(int col)
	{
		for (int i = 5; i >= 0; i--)
		{
			if (gameTable[col, i] != 0)
			{
				gameTable[col, i] = 0;
				break;
			}
		}
	}

	private int MinMax(int depth, bool isMax)
	{
		int score = Evaluate();

		if (score == 1000)
        {
			int sum = score + depth;
			return sum;
        }

		if (score == -1000)
			return score - depth;

		if (!MovesLeft() || depth <=0 )
		{
			return score;
		}

		if (isMax)//ruch maksimizera
		{
			int best = -100000;

			for (int i = 0; i < 7; i++)
			{
				if (GetFreeRows(i))
				{
					AddToTable(i, 2);
					best = Math.Max(best, MinMax(depth - 1, !isMax));
					RemoveTopCoin(i);
				}
			}
			return best;
		}
		else//ruch minimizera
		{
			int best = 100000;

			for (int i = 0; i < 7; i++)
			{
				if (GetFreeRows(i))
				{
					AddToTable(i, 1);
					best = Math.Min(best, MinMax(depth - 1, !isMax));
					RemoveTopCoin(i);
				}
			}
			return best;
		}
	}

	public int BestPossibleMove()
	{
		int bestValue = -100000;
		int bestMove = -1;

		for (int i = 0; i < 7; i++)
		{
			if (GetFreeRows(i))
			{
				AddToTable(i, 2);
				int moveValue = MinMax(maxDepth, false);
				RemoveTopCoin(i);

				Debug.Log("Value for column " + i + ": " + moveValue);

				if (moveValue > bestValue)
				{
					bestValue = moveValue;
					bestMove = i;
				}

			}
		}

		Debug.Log($"Best Move:   {bestMove}");

		return bestMove;

	}

	private int CheckForWin()
	{

		if (!MovesLeft())
		{
			return 3;
		}
		else
		{
			int score;
			//POZIOMO
			for (int j = 0; j < 6; j++)
			{
				score = 1;

				for (int i = 1; i < 7; i++)
				{
					if (gameTable[i, j] == gameTable[i - 1, j] && gameTable[i, j] != 0)
					{
						score++;

						if (score == 4)
						{
							return gameTable[i, j];
						}
					}
					else
					{
						score = 1;
					}
				}
			}
			//PIONOWO
			for (int j = 0; j < 7; j++)
			{
				score = 1;

				for (int i = 1; i < 6; i++)
				{
					if (gameTable[j, i] == gameTable[j, i - 1] && gameTable[j, i] != 0)
					{

						score++;

						if (score == 4)
						{
							return gameTable[j, i];

						}
					}
					else
					{
						score = 1;
					}

				}
			}
			//UKO君IE 1-1
			for (int n = 1; n < 4; n++)
			{
				int i = n;
				int j = 0;

				score = 1;

				while (j < 5 && i < 6)
				{
					if (gameTable[i, j] == gameTable[i + 1, j + 1] && gameTable[i, j] != 0)
					{
						score++;

						if (score == 4)
						{
							return gameTable[i, j];
						}
					}
					else
					{
						score = 1;
					}

					i++;
					j++;
				}

			}
			//UKO君IE 1-2
			for (int m = 0; m < 3; m++)
			{
				int i = 0;
				int j = m;
				score = 1;

				while (i < 6 && j < 5)
				{
					if (gameTable[i, j] == gameTable[i + 1, j + 1] && gameTable[i, j] != 0)
					{
						score++;

						if (score == 4)
						{
							return gameTable[i, j];
						}
					}
					else
					{
						score = 1;
					}

					i++;
					j++;

				}
			}
			//UKOSNIE 2-1
			for (int m = 3; m < 6; m++)
			{
				int i = 0;
				int j = m;

				score = 1;

				while (i < 6 && j > 0)
				{
					if (gameTable[i, j] == gameTable[i + 1, j - 1] && gameTable[i, j] != 0)
					{
						score++;

						if (score == 4)
						{
							return gameTable[i, j];
						}
					}
					else
					{
						score = 1;
					}

					j--;
					i++;
				}

			}
			//UKO君IE 2-2
			for (int n = 1; n < 4; n++)
			{
				int j = 5;
				int i = n;

				score = 1;

				while (i < 6 && j > 0)
				{
					if (gameTable[i, j] == gameTable[i + 1, j - 1] && gameTable[i, j] != 0)
					{
						score++;

						if (score == 4)
						{
							return gameTable[i, j];
						}

					}
					else
					{
						score = 1;
					}

					j--;
					i++;
				}

			}


		}

		return 0;


	}

	public bool IsGameOver()
	{
		if (CheckForWin() == 0) return false;
		else
		{
			GetComponent<NewGame>().GameEnd(CheckForWin());
			return true;
		}

	}

	private int Evaluate()
	{
		if (CheckForWin() == 2) return 1000;
		if (CheckForWin() == 1) return -1000;

		int points = 0;

		int score;
		bool open = false;
		//POZIOMO
		for (int j = 0; j < 6; j++)
		{
			score = 1;
			open = false;

			for (int i = 1; i < 7; i++)
			{
				if (score == 1 && gameTable[i-1, j] == 0)
                {
					open = true;
                }

				if (gameTable[i, j] == gameTable[i - 1, j] && gameTable[i, j] != 0)
				{
					score++;
				}
				else
				{
					if(score != 1)
                    {
						if(gameTable[i, j] == 0 || open)
                        {
							if (gameTable[i - 1, j] == 2) points += (5 * (score - 1));//10 points for 3inRows, 5 points for 2inRows
							else if (gameTable[i - 1, j] == 1) points -= (5 * (score - 1));
                        }
                    }

					open = false;
					score = 1;
				}
			}
		}
		//PIONOWO
		for (int j = 0; j < 7; j++)
		{
			score = 1;

			for (int i = 1; i < 6; i++)
			{
				if (gameTable[j, i] == gameTable[j, i - 1] && gameTable[j, i] != 0)
				{
					score++;
				}
				else
				{
					if(score != 1)
                    {
						if(gameTable[j, i] == 0 && i < ( score + 3))
                        {
							if(gameTable[j, i-1] == 2) points += (5 * (score - 1));
							else if (gameTable[j, i - 1] == 1) points -= (5 * (score - 1));
						}
                    }
					score = 1;
				}

			}
		}
		//UKO君IE 1-1
		for (int n = 1; n < 4; n++)
		{
			int i = n;
			int j = 0;

			score = 1;
			open = false;

			while (j < 5 && i < 6)
			{
				if (gameTable[i, j] == gameTable[i + 1, j + 1] && gameTable[i, j] != 0)
				{
					score++;
				}
				else
				{
					if(gameTable[i, j] == 0 && score == 1)
                    {
						open = true;
                    }
					else
                    {
						if(score != 1  && (gameTable[i+1, j+1] == 0 || open ))
						{
							if(gameTable[i, j] == 2) points += (5 * (score - 1) ); 
							else if(gameTable[i, j] == 1) points -= (5 * (score - 1) );
						}
						score = 1;
						open = false;
                    }

				}

				i++;
				j++;
			}

		}
		//UKO君IE 1-2
		for (int m = 0; m < 3; m++)
		{
			int i = 0;
			int j = m;

			score = 1;
			open = false;

			while (i < 6 && j < 5)
			{
				if (gameTable[i, j] == gameTable[i + 1, j + 1] && gameTable[i, j] != 0)
				{
					score++;
				}
				else
				{
					if (gameTable[i, j] == 0 && score == 1)
					{
						open = true;
					}
					else
					{
						if (score != 1 && (gameTable[i + 1, j + 1] == 0 || open))
						{
							if (gameTable[i, j] == 2) points += (5 * (score - 1) );
							else if (gameTable[i, j] == 1) points -= (5 * (score - 1) );
						}
						score = 1;
						open = false;
					}
				}

				i++;
				j++;

			}
		}
		//UKOSNIE 2-1
		for (int m = 3; m < 6; m++)
		{
			int i = 0;
			int j = m;

			score = 1;
			open = false;

			while (i < 6 && j > 0)
			{
				if (gameTable[i, j] == gameTable[i + 1, j - 1] && gameTable[i, j] != 0)
				{
					score++;
				}
				else
				{
					if (gameTable[i, j] == 0 && score == 1)
					{
						open = true;
					}
					if (score != 1 && (gameTable[i+1, j-1] == 0 || open))
					{
						if (gameTable[i, j] == 2) points += 5 * (score - 1);
						else if (gameTable[i, j] == 1) points -= 5 * (score - 1);
					}
					score = 1;
					open = false;
				}

				j--;
				i++;
			}

		}
		//UKO君IE 2-2
		for (int n = 1; n < 4; n++)
		{
			int j = 5;
			int i = n;

			score = 1;

			while (i < 6 && j > 0)
			{
				if (gameTable[i, j] == gameTable[i + 1, j - 1] && gameTable[i, j] != 0)
				{
					score++;
				}
				else
				{
					if (gameTable[i, j] == 0 && score == 1)
					{
						open = true;
					}
					if (score != 1 && (gameTable[i + 1, j - 1] == 0 || open))
					{
						if (gameTable[i, j] == 2) points += 5 * (score - 1);
						else if (gameTable[i, j] == 1) points -= 5 * (score - 1);
					}
					score = 1;
					open = false;
				}

				j--;
				i++;
			}

		}		

		return points;
	}

}
