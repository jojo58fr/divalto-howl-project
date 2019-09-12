///-----------------------------------------------------------------
///   Class:          Score
///   Description:    Scoring of a game (not specificly. Get & Set the scoring and string format)
///   Author:         Joachim Miens                   Date: 06/07/2017
///   E-mail :        joachim.miens@gmail.com
///   Notes:          <Notes>
///   Revision History:
///   Name:           Date & Time:        Description:
///-----------------------------------------------------------------

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score
{

    ///------------------------------------------------------------------------------------------------------------
    /// Class Variables
    ///------------------------------------------------------------------------------------------------------------
    
    private static int score = 0;
    private static int bestScore;

    #region Singleton
    private static Score instance;

    public static Score Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new Score();
            }
            return instance;
        }

    }

    private Score() { }

    #endregion

    public int Nbscore
    {
        get
        {
            return score;
        }

        set
        {
            score = value;
        }
    }

    public int BestScore
    {
        get
        {
            return bestScore;
        }

        set
        {
            bestScore = value;
        }
    }

    ///------------------------------------------------------------------------------------------------------------
    /// Beginning
    ///------------------------------------------------------------------------------------------------------------

    //Reset the scoring
    public void Reset()
	{
		score = 0;
	}

	//Adding the scoring with the older value
	public void AddScore(int value)
	{
		score += value;
		normalize ();

	}

	//Adding the scoring without the older value
	public void SetScore(int value)
	{
		score = value;
		normalize ();
	}

	//Substract the scoring with the value in parameter
	public void SubstractScore(int value)
	{
		score -= value;
		normalize ();
	}

	void normalize()
	{
		if (score < 0) 
		{
			score = 0;
		}
	}

    public void SaveScore(int OldScore)
    {
        if(OldScore < score)
        {
            //Save New Score
        }
    }

    public void LoadScore()
    {
        //LoadScore
    }

	// Format Score to a XXXX number with parameters
	public string ToString(int score,int nb_format = 4)
	{
		int nbmax = 10;
		int i = nb_format;
		bool quit = false;

		while (!quit)
		{
			if (score < nbmax) 
			{
				quit = true;

			} 
			else
			{
				i--;
				nbmax = nbmax * 10;
			}
		
		
		}

		string res = "";

		while (i > 1) 
		{
			res += "0";
			i--;
		}

		return res + score;
	}
		
}
