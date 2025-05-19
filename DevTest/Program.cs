using System;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Diagnostics.Metrics;

public class Calculations
{
    public static int AirScore(string oxygen) 
    {
        return oxygen == "Y" ? 2 : 0;
    }
   

    public static bool SupplementOxygen(int airScore)
    {
        return airScore == 2 ? true : false;
    }

    public static int ConsciousScore(string conscious)
    {
        return conscious == "Y" ? 3 : 0;
    }

    public static int RespirationScore(int respRate)
    {
        if (respRate <= 8 || respRate >= 25)
        {
            return 3;
        }
        else if (respRate >= 21 && respRate <= 24)
        {
            return 2;
        }
        else if (respRate >= 9 && respRate <= 11)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public static int OxygenSatScore(int oxygenSat, bool isOxygen)
    {
        if (oxygenSat <= 83)
        {
            return 3;
        }
        else if (oxygenSat == 84 || oxygenSat == 85)
        {
            return 2;
        }
        else if (oxygenSat == 86 || oxygenSat == 87)
        {
            return 1;
        }
        else if (oxygenSat >= 88 && oxygenSat <= 92)
            return 0;
        else
        {
            if (isOxygen)
            {
                if (oxygenSat == 93 || oxygenSat == 94)
                {
                    return 1;
                }
                else if (oxygenSat == 95 || oxygenSat == 96)
                {
                    return 2;
                }
                else
                {
                    return 3;
                }
            }
            else
            {
                return 0;
            }
        }
    }

    public static int TemperatureScore(double temperature)
    {
        if (temperature <= 35.0)
        {
            return 3;
        }
        else if (temperature >= 39.1)
        {
            return 2;
        }
        else if ((temperature >= 35.1 && temperature <= 36.0) || (temperature >= 38.1 && temperature <= 39.0))
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    public static int CbgScore(bool isFast, double cbg)
    {
        if (isFast)
        {
            if (cbg <= 3.4 || cbg >= 6.0)
            {
                return 3;
            }
            else if ((cbg >= 3.5 && cbg <= 3.9) || (cbg >= 5.5 && cbg <= 5.9))
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
        else
        {
            if (cbg <= 4.5 || cbg >= 9.0)
            {
                return 3;
            }
            else if ((cbg >= 4.5 && cbg <= 5.8) || (cbg >= 7.9 && cbg <= 8.9))
            {
                return 2;
            }
            else
            {
                return 0;
            }
        }
    }

    public static int FinalScore(int score1, int score2)
    {
        return score1 += score2;
    }
}
public class MediScore
{

    static void Main()
    {
        bool isOxygen, isFast, isTesting, isMatch;
        string conscious, oxygen, test, fasting, loop, date;
        int oxygenSat, respRate, airScore, consciousScore, respScore, oxygenScore, tempScore, mediScore=0, oldScore, cbgScore, prevScore;
        double temperature, cbg;
        int[] results = new int[] { };
        DateTime[] dateTimes = new DateTime[] { };
        var list = new List<int>();
        var time = new List<DateTime>();

        Console.WriteLine("Have you completed a Medi Score Test in the past 24 hours? [Y] for yes/[N] for no ");
        test = Console.ReadLine();
        if (test == "Y")
        {
            Console.WriteLine("Can you input your previous Medi Score ");
            oldScore = int.Parse(Console.ReadLine());
            Console.WriteLine("Can you input the date and time of this test in the format DD/MM/YYYY HH:MM:SS ");
            date = Console.ReadLine();
            DateTime result;
            isMatch = DateTime.TryParse(date, out result);
            if (isMatch == false)
            {
                while (isMatch == false)
                {
                    Console.WriteLine("Please input in the correct format DD/MM/YYYY HH:MM:SS ");
                    date = Console.ReadLine();
                    isMatch = DateTime.TryParse(date, out result);
                }
            }
            DateTime currentDateTime = DateTime.Now;
            TimeSpan interval = TimeSpan.FromDays(1);
            if (currentDateTime.Subtract(interval) > result)
            {
                Console.WriteLine("The test was not within 24 hours,starting new test");
            }
            else
            {
                Array.Resize(ref results, results.Length + 1);
                list.Add(oldScore);
                time.Add(result);
                Array.Resize(ref dateTimes, dateTimes.Length + 1);
                dateTimes = time.ToArray();
            }


        }
        else
        {
            oldScore = 18;
        }

        isTesting = true;
        do
        {
            Console.WriteLine("Is the patient on supplementary oxygen? [Y] for yes/[N] for no ");
            oxygen = Console.ReadLine();
            airScore = Calculations.AirScore(oxygen);
            isOxygen = Calculations.SupplementOxygen(airScore);
            mediScore = Calculations.FinalScore(mediScore, airScore);

            Console.WriteLine("Is the patient unconscious or in any confusion? [Y] for yes/[N] for no ");
            conscious = Console.ReadLine();
            consciousScore = Calculations.ConsciousScore(conscious);
            mediScore = Calculations.FinalScore(mediScore, consciousScore);

            Console.WriteLine("What is the patient's respiration rate (per minute) to the nearest whole number? ");
            respRate = int.Parse(Console.ReadLine());
            respScore = Calculations.RespirationScore(respRate);
            mediScore = Calculations.FinalScore(mediScore, respScore);

            Console.WriteLine("What is the patient's oxygen saturation, as a percantage, to the nearest whole number? ");
            oxygenSat = int.Parse(Console.ReadLine());
            oxygenScore = Calculations.OxygenSatScore(oxygenSat, isOxygen);
            mediScore = Calculations.FinalScore(mediScore, oxygenScore);

            Console.WriteLine("What is the patients current temperature to the nearest single decimal point in degrees celsius? ");
            temperature = double.Parse(Console.ReadLine());
            temperature = Math.Round(temperature, 1);
            tempScore = Calculations.TemperatureScore(temperature);
            mediScore = Calculations.FinalScore(mediScore, tempScore);

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
            cbg = Math.Round(cbg, 1);
            cbgScore = Calculations.CbgScore(isFast, cbg);
            mediScore = Calculations.FinalScore(mediScore, cbgScore);

            Console.WriteLine("Your Medi Score is " + mediScore);
            prevScore = mediScore;
            DateTime currentDateTime = DateTime.Now;
            time.Add(currentDateTime);
            Array.Resize(ref dateTimes, dateTimes.Length + 1);
            dateTimes = time.ToArray();
            list.Add(prevScore);
            Array.Resize(ref results, results.Length + 1);
            results = list.ToArray();

            int index = -1;
            DateTime min = time.Min();
            foreach (DateTime dt in time)
            {
                index++;
                if (dt == min)
                {
                    TimeSpan interval = TimeSpan.FromDays(1);
                    if (currentDateTime.Subtract(interval) < dt) {
                        break;
                    }
                }
            }
            int max = results.Max();
            if (mediScore > results[index]) 
            {
                Console.WriteLine("Your Medi Score has raised more than 2 points in the past 24 hours");
            }
            else if (mediScore == 17 && (max == 17))
            {
                Console.WriteLine("Still on the maximum score");
            }

            Console.WriteLine("Would you like to take the test again? [Y] for yes/[N] for no ");
            loop = Console.ReadLine();
            if (loop == "Y")
            {
                isTesting = true;
            }
            else
            {
                isTesting = false;
                Console.WriteLine("All scores are: ");
                foreach (int i in results)
                {
                    Console.WriteLine(i);
                }
                foreach (DateTime i in time)
                {
                    Console.WriteLine(i);
                }
            }
        }
        while (isTesting == true);


    }
}