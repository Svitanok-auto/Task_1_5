using System;
using System.Collections.Generic;
using System.Linq;

namespace Task_1_5
{
    public class Program
    {
        public const int WORKING_HOURS_OF_DAY = 8;
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

        private static List<Task> GetTheInitialListOfTasks()
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
                    if ((taskName != null) && (taskName != ""))
                    {
                        tasks.Insert(i, new Task { TaskName = taskName, Priority = Helper.VerifyPriorityInput(), Complexity = Helper.VerifyComplexityInput() });
                        Console.WriteLine("Press Escape (Esc) to complete the list OR any other character to continue");
                        keyPressed = Console.ReadKey();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Wrong input");
                        Console.WriteLine("Press Escape (Esc) to complete input OR any other character to continue");
                        keyPressed = Console.ReadKey();
                        break;
                    }
                }
            } while (keyPressed.Key != ConsoleKey.Escape);
        return tasks;
        }

        private static void PrintTheListOfTasks(List<Task> tasks)
        {
            foreach (Task task in tasks)
            {
                Console.WriteLine($"---Task Description- Task: {task.TaskName}, Priority:{task.Priority}, Complexity:{task.Complexity}");
            }
        }

        private static void GetTimeRequiredToPerformAllTheTasksFromTheList(List<Task> tasks)
        {
            int hoursRequiredToPerformAllTasks = tasks.FindAll(comp => comp.Complexity == Complexity.High).Count * (int)Complexity.High + tasks.FindAll(comp => comp.Complexity == Complexity.Medium).Count * (int)Complexity.Medium + tasks.FindAll(comp => comp.Complexity == Complexity.Low).Count * (int)Complexity.Low;
            Console.WriteLine("\nTo perform all the tasks you need " + hoursRequiredToPerformAllTasks + " hour(s)");
        }

        private static void PrintTheListOfTasksFilteredByPriority(List<Task> tasks)
        {   
            if (tasks.FindAll(comp => comp.Priority == Priority.High).Count > 0)
            {
                Console.WriteLine($"\nTasks of Priority = {Priority.High}");
                PrintTheListOfTasks(tasks.FindAll(comp => comp.Priority == Priority.High));
            }
            if (tasks.FindAll(comp => comp.Priority == Priority.Medium).Count > 0)
            {
                Console.WriteLine($"\nTasks of Priority = {Priority.Medium}");
                PrintTheListOfTasks(tasks.FindAll(comp => comp.Priority == Priority.Medium));
            }
            if (tasks.FindAll(comp => comp.Priority == Priority.Low).Count > 0)
            {
                Console.WriteLine($"\nTasks of Priority = {Priority.Low}");
                PrintTheListOfTasks(tasks.FindAll(comp => comp.Priority == Priority.Low));
            }
        }

        private static void GetTheListOfTasksThatCanBeDoneDuringNDays(List<Task> tasks)
        {
            int daysAvailableForTaskExcecution = Helper.GetDaysAvailableToPerformTasks();
            int daysConvertedToWorkingHours = daysAvailableForTaskExcecution * WORKING_HOURS_OF_DAY;
            Console.WriteLine("\nAmount of available hours to perform the tasks is: " + daysConvertedToWorkingHours);

            List <Task> sortedTaskList = GetPrioritySortedList(tasks, Priority.High).Concat(GetPrioritySortedList(tasks, Priority.Medium)).Concat(GetPrioritySortedList(tasks, Priority.Low)).ToList();
            Console.WriteLine("\nList of Tasks that can be done based on Priority and available working hours is below:");
            PrintTheListOfTasks(GetTasksResultList(sortedTaskList, daysConvertedToWorkingHours));
        }

        private static List<Task> GetTasksResultList(List<Task> tasks, int workingHours)
        {
            List<Task> tasksResult = new List<Task>();
            for (int i=0; i < tasks.Count; i++)
            {
                if ((tasks[i].Complexity == Complexity.High) && (0 <= (workingHours - (int)Complexity.High)))
                {
                    tasksResult.Add(tasks[i]);
                    workingHours = workingHours - (int)Complexity.High;
                }
                if ((tasks[i].Complexity == Complexity.Medium) && (0 <= (workingHours - (int)Complexity.Medium)))
                {
                    tasksResult.Add(tasks[i]);
                    workingHours = workingHours - (int)Complexity.Medium;
                }
                if ((tasks[i].Complexity == Complexity.Low) && (0 <= (workingHours - (int)Complexity.Low)))
                {
                    tasksResult.Add(tasks[i]);
                    workingHours = workingHours - (int)Complexity.Low;
                }
            }
            return tasksResult;
        }

        private static List <Task> GetPrioritySortedList(List<Task> tasks, Priority priority)
        {
            IEnumerable<Task> queryPriorityTasks =
            from task in tasks
            where task.Priority == priority
            orderby task.Complexity descending
            select task;
            return queryPriorityTasks.ToList();
        }
    }
}

