using System;
using System.ComponentModel.Design;

class MediScore
{
    static void Main()
    {
        bool isOxygen, isFast;
        string conscious, oxygen, test, fasting;
        int oxygenSat, respRate, airScore, consciousScore, respScore, oxygenScore, tempScore, mediScore, oldScore, cbgScore;
        float cbg;
        double temperature;

        Console.WriteLine("Have you completed a Medi Score Test in the past 24 hours? [Y] for yes/[N] for no ");
        test = Console.ReadLine();
        if (test == "Y")
        {
            Console.WriteLine("Can you input your previous Medi Score ");
            oldScore = int.Parse(Console.ReadLine());
        }
        else
        {
            oldScore = 18;
        }

        Console.WriteLine("Is the patient on supplementary oxygen? [Y] for yes/[N] for no ");
        oxygen = Console.ReadLine();
        if (oxygen == "Y")
        {
            airScore = 2;
            isOxygen = true;

        }
        else
        {
            airScore = 0;
            isOxygen = false;
        }

        Console.WriteLine("Is the patient unconscious or in any confusion? [Y] for yes/[N] for no ");
        conscious = Console.ReadLine();
        if (conscious == "Y")
        {
            consciousScore = 3;
        }
        else
        {
            consciousScore = 0;
        }

        Console.WriteLine("What is the patient's respiration rate (per minute) to the nearest whole number? ");
        respRate = int.Parse(Console.ReadLine());
        if (respRate <= 8 || respRate >= 25) {
            respScore = 3;
        }
        else if (respRate >= 21 && respRate <= 24) { 
            respScore = 2;
        }
        else if (respRate >= 9 && respRate <= 11)
        {
            respScore = 1;
        }
        else
        {
            respScore = 0;
        }

        Console.WriteLine("What is the patient's oxygen saturation, as a percantage, to the nearest whole number? ");
        oxygenSat = int.Parse(Console.ReadLine());
        if (oxygenSat <= 83) {
            oxygenScore = 3;
        }
        else if (oxygenSat == 84 || oxygenSat == 85)
        {
            oxygenScore = 2;
        }
        else if (oxygenSat == 86 || oxygenSat == 87)
        {
            oxygenScore = 1;
        }
        else if (oxygenSat >= 88 && oxygenSat <= 92)
            oxygenScore = 0;
        else
        {
            if (isOxygen == true)
            {
                if (oxygenSat == 93 || oxygenSat == 94)
                {
                    oxygenScore = 1;
                }
                else if (oxygenSat == 95 || oxygenSat == 96)
                {
                    oxygenScore = 2;
                }
                else
                {
                    oxygenScore = 3;
                }
            }
            else
            {
                oxygenScore = 0;
            }
        }

        Console.WriteLine("What is the patients current temperature to the nearest single decimal point in degrees celsius? ");
        temperature = double.Parse(Console.ReadLine());
        temperature = Math.Round(temperature, 1);
        if (temperature <= 35.0)
        {
            tempScore = 3;
        }
        else if (temperature >= 39.1)
        {
            tempScore = 2;
        }
        else if ((temperature >= 35.1 && temperature <= 36.0) || (temperature >= 38.1 && temperature <= 39.0))
        {
            tempScore = 1;
        }
        else
        {
            tempScore = 0;
        }

        Console.WriteLine("Have you eaten in the past 2 hours? [Y] for yes/[N] for no ");
        fasting = Console.ReadLine();
        if (fasting == "Y")
        {
            isFast = false;
        }
        else
        {
            isFast = true;
        }

        Console.WriteLine("What is your cbg to the nearest one decimal point in mmol/L? ");
        cbg = float.Parse(Console.ReadLine());
        if (isFast == true)
        {
            if (cbg <= 3.4 || cbg >= 6.0)
            {
                cbgScore = 3;
            }
            else if ((cbg >= 3.5 && cbg <= 3.9) || (cbg >= 5.5 && cbg <= 5.9))
            {
                cbgScore = 2;
            }
            else
            {
                cbgScore = 0;
            }
        }
        else
        {
            if (cbg <= 4.5 || cbg >= 9.0)
            {
                cbgScore = 3;
            }
            else if ((cbg >= 4.5 && cbg <= 5.8) || (cbg >= 7.9 && cbg <= 8.9))
            {
                cbgScore = 2;
            }
            else
            {
                cbgScore = 0;
            }
        }
        

        mediScore = airScore + consciousScore + respScore + oxygenScore + tempScore + cbgScore;
        Console.WriteLine("Your Medi Score is " + mediScore);
        if (mediScore > (oldScore + 2)) {
            Console.WriteLine("Your Medi Score has raised more than 2 points in the past 24 hours");
        }
    }
}