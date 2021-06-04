using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1_5
{
    public static class Helper<T>
    {
        public const int MAX_ATTEMPTS = 30;
        public static T VerifyPriorityInput()
        {
            Console.WriteLine("Populate Priority 1; 2 or 3 (1=High, 2=Medium, 3=Low)");
            return MatchToPriorityOrComplexity(Convert.ToInt32(GetValidatePriorityOrComplexity()));
        }

        public static T VerifyComplexityInput()
        {
            Console.WriteLine("Populate Complexity 1; 2 or 3 (1=High, 2=Medium, 3=Low)");
            return MatchToPriorityOrComplexity(Convert.ToInt32(GetValidatePriorityOrComplexity()));
        }

        public static T MatchToPriorityOrComplexity(int priorityOrComplexity)
        {
            List<T> listPriority = Enum.GetValues(typeof(T)).OfType<T>().ToList();
            T priorityOrComplexityValue = listPriority[priorityOrComplexity - 1];
            return priorityOrComplexityValue;
        }

        public static int GetValidatePriorityOrComplexity()
        {
            int attempt = 0;
            int validComplexityOrPriority;
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
                            validComplexityOrPriority = Convert.ToInt32(inputString);
                            Console.WriteLine("\n Input value is= " + validComplexityOrPriority);
                            return validComplexityOrPriority;
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
            int daysAvailable;
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
