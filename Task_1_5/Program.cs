using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1_5
{
    public class Program
    {
        public const string PRIORITY_1 = "1";
        public const string PRIORITY_2 = "2";
        public const string PRIORITY_3 = "3";
        public const string COMPLEXITY_1 = "1";
        public const string COMPLEXITY_2 = "2";
        public const string COMPLEXITY_3 = "3";
        public const int WORKING_HOURS_OF_DAY = 8;
        public const int HOURS_TO_DO_COMPLEXITY_1_TASK= 4;
        public const int HOURS_TO_DO_COMPLEXITY_2_TASK = 2;
        public const int HOURS_TO_DO_COMPLEXITY_3_TASK = 1;
        public const int MAX_ATTEMPTS = 10;

        public static void Main(string[] args)
        {
            List<Task> taskList = GetTheInitialListOfTasks();
            if (taskList.Count > 0)
            {
                GetTimeRequiredToPerformAllTheTasksFromTheList(taskList);
                PrintTheListOfTasksFilteredByPriority(taskList);
                GetTheListOfTasksThatCanBeDoneDuringNDays(taskList);
            }
            Console.WriteLine("\nExit, press any key");
            Console.ReadKey();
        }

        public static List<Task> GetTheInitialListOfTasks()
        {
            Console.WriteLine("Populate Tasks in Format (press some key to Start): \nTask Name \nPriority (1=High, 2=Medium, 3=Low) \nComplexity (1=High, 2=Medium, 3=Low)");          
            var keyPressed = Console.ReadKey();
            List<Task> tasks = new List<Task>();
            do
            {
                Console.Write("\n--- You pressed " + keyPressed.Key.ToString());
                for (int i = 0; i < MAX_ATTEMPTS; i++)
                {
                    Console.WriteLine("\n Task Name is");
                    string taskName = Console.ReadLine();
                    Console.WriteLine("Populate Priority 1; 2 or 3 (1=High, 2=Medium, 3=Low)");
                    string taskPriority = Console.ReadLine();
                    Console.WriteLine("Populate Complexity 1; 2 or 3 (1=High, 2=Medium, 3=Low)");
                    string taskComplexity = Console.ReadLine();
                    if ((taskName != null) && (taskName != "") && (taskPriority != null) && ((taskPriority == PRIORITY_1) || (taskPriority == PRIORITY_2) || (taskPriority == PRIORITY_3)) && (taskComplexity != null) && ((taskComplexity == COMPLEXITY_1) || (taskComplexity == COMPLEXITY_2) || (taskComplexity == COMPLEXITY_3)) && (keyPressed.Key != ConsoleKey.Escape))
                    {
                        tasks.Insert(i, new Task { TaskName = taskName, Priority = taskPriority, Complexity = taskComplexity });
                        Console.WriteLine("Press Escape (Esc) to complete the list OR any other character to continue");
                        keyPressed = Console.ReadKey();
                        break;
                    }
                    else if ((taskName == null)||(taskName == "") || (taskPriority == null) || ((taskPriority != PRIORITY_1) && (taskPriority != PRIORITY_2) && (taskPriority != PRIORITY_3)) || (taskComplexity == null) || ((taskComplexity != COMPLEXITY_1) && (taskComplexity != COMPLEXITY_2) && (taskComplexity != COMPLEXITY_3)) && (keyPressed.Key != ConsoleKey.Escape))
                    {
                        Console.WriteLine("Wrong input");
                        Console.WriteLine("Press Escape (Esc) to complete the list OR any other character to continue");
                        keyPressed = Console.ReadKey();
                        break;
                    }
                }
            } while (keyPressed.Key != ConsoleKey.Escape);
        return tasks;
        }

        public static void PrintTheListOfTasks(List<Task> tasks)
        {
            foreach (Task task in tasks)
            {
                Console.WriteLine($"---Task Description- Task: {task.TaskName}, Priority:{task.Priority}, Complexity:{task.Complexity}");
            }
        }

        public static void GetTimeRequiredToPerformAllTheTasksFromTheList(List<Task> tasks)
        {
            int hoursRequiredToPerformAllTasks = tasks.FindAll(comp => comp.Complexity == COMPLEXITY_1).Count * 4 + tasks.FindAll(comp => comp.Complexity == COMPLEXITY_2).Count * 2 + tasks.FindAll(comp => comp.Complexity == COMPLEXITY_3).Count;
            Console.WriteLine("\nTo perform all the tasks you need " + hoursRequiredToPerformAllTasks + " hour(s)");
        }

        public static void PrintTheListOfTasksFilteredByPriority(List<Task> tasks)
        {   
            if (tasks.FindAll(comp => comp.Priority == PRIORITY_1).Count > 0)
            {
                Console.WriteLine($"\nTasks of Priority = {PRIORITY_1}");
                PrintTheListOfTasks(tasks.FindAll(comp => comp.Priority == PRIORITY_1));
            }
            if (tasks.FindAll(comp => comp.Priority == PRIORITY_2).Count > 0)
            {
                Console.WriteLine($"\nTasks of Priority = {PRIORITY_2}");
                PrintTheListOfTasks(tasks.FindAll(comp => comp.Priority == PRIORITY_2));
            }
            if (tasks.FindAll(comp => comp.Priority == PRIORITY_3).Count > 0)
            {
                Console.WriteLine($"\nTasks of Priority = {PRIORITY_3}");
                PrintTheListOfTasks(tasks.FindAll(comp => comp.Priority == PRIORITY_3));
            }
        }

        public static void GetTheListOfTasksThatCanBeDoneDuringNDays(List<Task> tasks)
        {
            int daysAvailableForTaskExcecution = GetDaysAvailableToPerformTasks();
            int daysConvertedToWorkingHours = daysAvailableForTaskExcecution * WORKING_HOURS_OF_DAY;
            Console.WriteLine("\nAmount of available hours to perform the tasks is: " + daysConvertedToWorkingHours);

            List <Task> sortedTaskList = GetPrioritySortedList(tasks, PRIORITY_1).Concat(GetPrioritySortedList(tasks, PRIORITY_2)).Concat(GetPrioritySortedList(tasks, PRIORITY_3)).ToList();
            Console.WriteLine("\nList of Tasks that can be done based on Priority and available working hours is below:");
            PrintTheListOfTasks(GetTasksResultList(sortedTaskList, daysConvertedToWorkingHours));
        }

        public static List<Task> GetTasksResultList(List<Task> tasks, int workingHours)
        {
            List<Task> tasksResult = new List<Task>();
            for (int i=0; i < tasks.Count; i++)
            {
                if ((tasks[i].Complexity == COMPLEXITY_1) && (0 <= (workingHours - HOURS_TO_DO_COMPLEXITY_1_TASK)))
                {
                    tasksResult.Add(tasks[i]);
                    workingHours = workingHours - HOURS_TO_DO_COMPLEXITY_1_TASK;
                }
                if ((tasks[i].Complexity == COMPLEXITY_2) && (0 <= (workingHours - HOURS_TO_DO_COMPLEXITY_2_TASK)))
                {
                    tasksResult.Add(tasks[i]);
                    workingHours = workingHours - HOURS_TO_DO_COMPLEXITY_2_TASK;
                }
                if ((tasks[i].Complexity == COMPLEXITY_3) && (0 <= (workingHours - HOURS_TO_DO_COMPLEXITY_3_TASK)))
                {
                    tasksResult.Add(tasks[i]);
                    workingHours = workingHours - HOURS_TO_DO_COMPLEXITY_3_TASK;
                }
            }
            return tasksResult;
        }

        public static List <Task> GetPrioritySortedList(List<Task> tasks, string priority)
        {
            IEnumerable<Task> queryPriorityTasks =
            from task in tasks
            where task.Priority == priority
            orderby task.Complexity descending
            select task;
            return queryPriorityTasks.ToList();
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

