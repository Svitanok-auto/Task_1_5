using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1_5
{
    public static class Helper
    {
        public const int MAX_ATTEMPTS = 30;
        public static Priority VerifyPriorityInput()
        {
            Console.WriteLine("Populate Priority 1; 2 or 3 (1=High, 2=Medium, 3=Low)");
            return MatchToPriority(Convert.ToInt32(GetValidatePriorityOrComplexity()));  
        }

        public static Complexity VerifyComplexityInput()
        {
            Console.WriteLine("Populate Complexity 1; 2 or 3 (1=High, 2=Medium, 3=Low)");
            return MatchToComplexity(Convert.ToInt32(GetValidatePriorityOrComplexity()));
        }

        public static Priority MatchToPriority(int priority)
        {
            List<Priority> listPriority = Enum.GetValues(typeof(Priority)).OfType<Priority>().ToList();
            Priority priorityValue = listPriority[priority - 1];
            return priorityValue;
        }

        public static Complexity MatchToComplexity(int complexity)
        {
            List<Complexity> listComplexity = Enum.GetValues(typeof(Complexity)).OfType<Complexity>().ToList();
            Complexity complexityValue = listComplexity[complexity - 1];
            return complexityValue;
        }

        public static int GetValidatePriorityOrComplexity()
        {
            int attempt = 0;
            int choiceToSeeGarland = 0;
            while (attempt < MAX_ATTEMPTS)
            {
                string inputString = Console.ReadLine();
                if ((inputString.ToString() != string.Empty) && ((inputString.ToString() == "1") || (inputString.ToString() == "2") || (inputString.ToString() == "3")))
                {
                    try
                    {
                        if ((inputString.IndexOf('0') != -1) && (inputString.Length == 1))
                        {
                            Console.WriteLine("\nIncorrect Input: Zero is not allowed ");
                            attempt++;
                            continue;
                        }
                        else
                        {
                            choiceToSeeGarland = Convert.ToInt32(inputString);
                            Console.WriteLine("\n Input value is= " + choiceToSeeGarland);
                            return choiceToSeeGarland;
                        }
                    }
                    catch (SystemException ex)
                    {
                        Console.WriteLine("\nIncorrect Input, error: " + ex.Message);
                        Console.WriteLine("\nShould 1, 2 or 3");
                        attempt++;
                    }
                }
                else
                {
                    Console.WriteLine("\nIncorrect Input: Such value is not allowed");
                }
            }
            return 0;
        }

        public static int GetDaysAvailableToPerformTasks()
        {
            Console.WriteLine("\nPopulate how many days (integer) are available for tasks performing e.g. 1 or 2, etc. ");
            int attempt = 0;
            int daysAvailable = 0;
            while (attempt < MAX_ATTEMPTS)
            {
                string inputString = Console.ReadLine();
                if (inputString.ToString() != string.Empty)
                {
                    try
                    {
                        if ((inputString.IndexOf('0') != -1) && (inputString.Length == 1))
                        {
                            Console.WriteLine("\nIncorrect Input: Zero is not allowed");
                            attempt++;
                            continue;
                        }
                        else if (Convert.ToInt32(inputString) < 0)
                        {
                            Console.WriteLine("\nIncorrect Input: Negative values are not allowed");
                            attempt++;
                            continue;
                        }
                        else
                        {
                            daysAvailable = Convert.ToInt32(inputString);
                            Console.WriteLine("\n Day(s)= " + daysAvailable);
                            return daysAvailable;
                        }
                    }
                    catch (SystemException ex)
                    {
                        Console.WriteLine("\nIncorrect Input, error: " + ex.Message);
                        Console.WriteLine("\nTry again to populate correct Days Available");
                        attempt++;
                    }
                }
                else
                {
                    Console.WriteLine("\nIncorrect Input: Empty value is not allowed");
                }
            }
            return 0;
        }
    }
}
