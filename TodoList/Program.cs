using Microsoft.VisualBasic;
using NAudio.Wave;
using static System.Net.Mime.MediaTypeNames;

namespace TodoList
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            string text = "████████╗░█████╗░░██████╗██╗░░██╗██████╗░░█████╗░██████╗░\n" +
                           "╚══██╔══╝██╔══██╗██╔════╝██║░██╔╝██╔══██╗██╔══██╗██╔══██╗\n" +
                           "░░░██║░░░███████║╚█████╗░█████═╝░██████╔╝███████║██║░░██║\n" +
                           "░░░██║░░░██╔══██║░╚═══██╗██╔═██╗░██╔═══╝░██╔══██║██║░░██║\n" +
                           "░░░██║░░░██║░░██║██████╔╝██║░╚██╗██║░░░░░██║░░██║██████╔╝\n" +
                           "░░░╚═╝░░░╚═╝░░╚═╝╚═════╝░╚═╝░░╚═╝╚═╝░░░░░╚═╝░░╚═╝╚═════╝░";

            ClearConsole();
            CenterText(text);

            Console.ResetColor();

            string Motivation = "Welcome Buddy! write your Goals and kill it..";
            string Dash1 = "+-+-+-+-+-+-+-+-+-+-+";

            List<String> Menu = new List<string>(new string[] {
                "--------------------------------------------------------------------",
                "▌                       -+-+-+-+-+-+-+-+-                          ▌",
                "▌                       +    Main Menu  +                          ▌",
                "▌                       -+-+-+-+-+-+-+-+-                          ▌",
                "▌------------------------------------------------------------------▌",
                "▌                                                                  ▌",
                "▌        # Press 1 to add a task.                                  ▌",
                "▌        # Press 2 to view all tasks.                              ▌",
                "▌        # Press 3 to view a specific task.                        ▌",
                "▌        # Press 4 to mark a task as completed.                    ▌",
                "▌        # Press 5 to delete a specific task.                      ▌",
                "▌        # Press 6 to update task details.                         ▌",
                "▌        # Press 7 to sort the list according to attributes.       ▌",
                "▌        # Press 8 to filter according user input.                 ▌",
                "▌        # Press 9 to exit.                                        ▌",
                "--------------------------------------------------------------------"
            });

            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Dash1.Length / 2)) + "}", Dash1));
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Motivation.Length / 2)) + "}", Motivation));

            Console.WriteLine();

            //create objects
            TodoManager todoManager = new TodoManager();
            FileHandler fileHandler = new FileHandler();


            //Play welcome audio
            PlayWelcomeAudio();

            //Load tasks from file
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("Loading tasks from file........");
            ConsoleUtility.WriteProgressBar(0);
            for (var i = 0; i <= 100; ++i)
            {
                ConsoleUtility.WriteProgressBar(i, true);
                Thread.Sleep(50);
            }
            Console.WriteLine();
            ConsoleUtility.WriteProgress(0);
            for (var i = 0; i <= 100; ++i)
            {
                ConsoleUtility.WriteProgress(i, true);
                Thread.Sleep(50);
            }
            //Load tasks from file code
            todoManager.SetTasks(fileHandler.ReadFromFile());
            Console.WriteLine("Tasks loaded successfully.");
            Console.ResetColor();


            bool exit = false;
            //bool isupdated = false;
            while (!exit)
            {
                Console.ForegroundColor = ConsoleColor.Cyan;
                for (int i = 0; i < Menu.Count; i++)
                {
                    Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (Menu[i].Length / 2)) + "}", Menu[i]));
                }

                Console.ResetColor();

                string menu = "Select an option : ";
                Console.Write(" " + menu.PadLeft(45));
                string option = Console.ReadLine();

                switch (option)
                {

                    //add a task
                    case "1":
                        try
                        {
                            Console.Write("Enter task title: ");
                            string title = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(title))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                throw new InvalidInputException("Title cannot be empty.");
                            }

                            Console.ResetColor();

                            Console.Write("Enter task description: ");
                            string description = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(description))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                throw new InvalidInputException("Description cannot be empty.");

                            }

                            Console.ResetColor();

                            Console.Write("Enter task priority (1 - High,2 - Medium,3 - Low), give the input in integer : ");
                            string priority = Console.ReadLine();
                            int opt;
                            if (!Int32.TryParse(priority, out opt) || !(priority == "1" || priority == "2" || priority == "3"))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                throw new InvalidInputException("Priority cannot be string and please enter any of these (1 - High,2 - Medium,3 - Low)");
                            }

                            Console.ResetColor();

                            Console.Write("Enter the due date for the task (yyyy-MM-dd) : ");
                            if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
                            {
                                if (dueDate.Date < DateTime.Now.Date )
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Error: Due date cannot be in the past. Task will not be saved.");
                                    Console.ResetColor();
                                }
                                else
                                {
                                    todoManager.AddTask(title, description, priority, dueDate);
                                    Console.WriteLine("Your task has been added successfully!");
                                }
                            }
                            else
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Invalid date format. Task will not be saved.");
                                Console.ResetColor();
                            }

                            //TaskItem updatedTask = new TaskItem
                            //{
                            //    Id = 1,
                            //    Title = title,
                            //    Description = description,
                            //    IsComplete = false,
                            //    Priority = priority,
                            //    DueDate = duedate
                            //};
                            //fileHandler.UpdateTask(updatedTask);
                            //isupdated = true;
                        }
                        catch (InvalidInputException ex)
                        {
                            Console.WriteLine("Invalid input: " + ex.Message);
                        }
                        catch (Exception ex)
                        {
                            ExceptionHandler.HandleException(ex);
                        }
                        Console.WriteLine();
                        break;

                    //view all tasks
                    case "2":
                        todoManager.ViewAllTasks();
                        Console.WriteLine();
                        break;

                    //view a specific task
                    case "3":
                        Console.Write("Enter task id: ");
                        string input = Console.ReadLine();
                        int TaskId;
                        if (!Int32.TryParse(input, out TaskId))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Id cannot be a string, please enter correct Id");
                            Console.ResetColor();
                        }
                        if (TaskId < 0 || TaskId > todoManager.GetMaxId())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("There is no task with this id");
                            Console.ResetColor();
                        }
                        else
                        {
                            todoManager.ViewTask(TaskId);
                        }

                        Console.WriteLine();
                        break;

                    //mark a task as completed
                    case "4":
                        Console.Write("Enter task id: ");
                        string input4 = Console.ReadLine();
                        int TaskId1;
                        if (!Int32.TryParse(input4, out TaskId1))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Id cannot be a string, please enter correct Id");
                            Console.ResetColor();
                        }

                        if (TaskId1 < 0 || TaskId1 > todoManager.GetMaxId())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("There is no task with this id");
                            Console.ResetColor();
                        }
                        else
                        {
                            if (todoManager.GetTaskById(TaskId1).IsComplete == false)
                            {
                                todoManager.CompleteTask(TaskId1);
                            }
                            else
                            {
                                Console.WriteLine("This task has already been completed");
                            }

                        }

                        Console.WriteLine("Task with id: " + TaskId1 + " has been marked completed");
                        Console.WriteLine();
                        break;


                    //delete a specific task
                    case "5":
                        Console.Write("Enter task id: ");
                        string input1 = Console.ReadLine();
                        int TaskId2;
                        if (!Int32.TryParse(input1, out TaskId2))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Id cannot be a string, please enter correct Id");
                            Console.ResetColor();
                            break;
                        }

                        if (TaskId2 < 0 || TaskId2 > todoManager.GetMaxId())
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("There is no task with this id");
                            Console.ResetColor();
                        }
                        else
                        {
                            todoManager.DeleteTask(TaskId2);
                        }

                        Console.WriteLine("Task with id: " + TaskId2 + " has been deleted!");
                        Console.WriteLine();
                        break;

                    //delete all tasks
                    //case "6":
                    //    Console.WriteLine("Are you sure, you want to delete all the task? (NOTE: Once the tasks is deleted it cannot be restored)");
                    //    Console.WriteLine("Enter Yes/No");
                    //    string input = Console.ReadLine();
                    //    if(input == "Yes" || input == "yes")
                    //    {
                    //        todoManager.DeleteAllTasks();
                    //    }
                    //    else
                    //    {
                    //        Console.WriteLine("Task is not deleted!");
                    //    }
                    //    break;


                    //update task details
                    case "6":
                        todoManager.ViewAllTasks();
                        Console.WriteLine("Press 1 for updating Priority");
                        Console.WriteLine("Press 2 for updating Due Date");
                        Console.WriteLine("Press 3 for updating Title");
                        Console.WriteLine("Press 4 for updating Description");
                        Console.WriteLine("Press 5 for updating Status");
                        Console.Write("Enter your choice : ");
                        string input6 = Console.ReadLine();
                        int choice;
                        if (!Int32.TryParse(input6, out choice))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Choice cannot be a string, please enter correct option.");
                            Console.ResetColor();
                            break;
                        }

                        while (choice < 1 || choice > 5)
                        {
                            Console.WriteLine("Invalid choice");
                            Console.Write("Enter your choice : ");
                            choice = Convert.ToInt32(Console.ReadLine());
                        }

                        if (choice == 1)
                        {
                            Console.Write("Enter the id of the task : ");
                            string input3 = Console.ReadLine();
                            int id;
                            if (!Int32.TryParse(input3, out id))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Id cannot be a string, please enter correct Id");
                                Console.ResetColor();
                                break;
                            }

                            if (id < 0 || id > todoManager.GetMaxId())
                            {
                                Console.WriteLine("No task is present with this id");
                            }
                            else
                            {
                                Console.Write("Enter the new priority : ");
                                string priority = Console.ReadLine();
                                todoManager.UpdatePriority(id, priority);
                                Console.WriteLine("Priority updated successfully!");
                            }
                        }
                        if (choice == 2)
                        {
                            Console.Write("Enter the id of the task : ");
                            string input2 = Console.ReadLine();
                            int id;
                            if (!Int32.TryParse(input2, out id))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Id cannot be a string, please enter correct Id");
                                Console.ResetColor();
                                break;
                            }

                            if (id < 0 || id > todoManager.GetMaxId())
                            {
                                Console.WriteLine("No task is present with this id");
                            }
                            else
                            {
                                Console.Write("Enter the due date for the task (yyyy-MM-dd) : ");
                                if (DateTime.TryParse(Console.ReadLine(), out DateTime dueDate))
                                {
                                    if (dueDate.Date < DateTime.Now.Date)
                                    {
                                        Console.WriteLine("Error: Due date cannot be in the past. Task will not be saved.");
                                    }
                                    else
                                    {
                                        todoManager.UpdateDueDate(id, dueDate);
                                    }
                                }
                                else
                                {
                                    Console.WriteLine("Invalid date format. Task will not be saved.");
                                }
                            }
                        }
                        if (choice == 3)
                        {
                            Console.Write("Enter the id of the task : ");
                            string input3 = Console.ReadLine();
                            int id;
                            if (!Int32.TryParse(input3, out id))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Id cannot be a string, please enter correct Id");
                                Console.ResetColor();
                                break;
                            }

                            if(id < 0 || id > todoManager.GetMaxId())
                            {
                                Console.WriteLine("No task is present with this id");
                            }
                            else
                            {
                                Console.Write("Enter the new title : ");
                                string title = Console.ReadLine();
                                todoManager.UpdateTaskTitle(id, title);
                                Console.WriteLine("Title updated successfully!");
                            }
                        }
                        if (choice == 4)
                        {
                            Console.Write("Enter the id of the task : ");
                            string input5 = Console.ReadLine();
                            int id;
                            if (!Int32.TryParse(input5, out id))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Id cannot be a string, please enter correct Id");
                                Console.ResetColor();
                                break;
                            }

                            if (id < 0 || id > todoManager.GetMaxId())
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("No task is with this id");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write("Enter the new description : ");
                                string description = Console.ReadLine();
                                todoManager.UpdateTaskDescription(id, description);
                                Console.WriteLine("Description updated successfully!");
                            }
                        }
                        if (choice == 5)
                        {
                            Console.Write("Enter the id of the task : ");
                            string input5 = Console.ReadLine();
                            int id;
                            if (!Int32.TryParse(input5, out id))
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("Id cannot be a string, please enter correct Id");
                                Console.ResetColor();
                                break;
                            }

                            if (id < 0 || id > todoManager.GetMaxId())
                            {
                                Console.ForegroundColor = ConsoleColor.Red;
                                Console.WriteLine("No task is with this id");
                                Console.ResetColor();
                            }
                            else
                            {
                                Console.Write("Enter the new status(true/false) : ");
                                bool status;
                                if (Boolean.TryParse(Console.ReadLine(), out status))
                                {
                                    todoManager.UpdateTaskStatus(id, status);
                                    Console.WriteLine("Status updated successfully!");
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Invalid status. Task will not be saved.");
                                    Console.ResetColor();
                                }
                            }
                        }

                        Console.WriteLine();
                        break;

                    case "7":
                        todoManager.ViewAllTasks();
                        Console.WriteLine("Press 1 for sorting taskpad according to priority(High to Low)");
                        Console.WriteLine("Press 2 for sorting taskpad according to priority(Low to High");
                        Console.WriteLine("Press 3 for sorting taskpad according to due date");
                        Console.WriteLine("Press 4 for sorting taskpad according to title");
                        Console.WriteLine("Press 5 for sorting taskpad according to status");
                        Console.Write("Enter your choice : ");
                        string input8 = Console.ReadLine();
                        int choice1;
                        if (!Int32.TryParse(input8, out choice1))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Choice cannot be a string, please enter correct option.");
                            Console.ResetColor();
                            break;
                        }

                        while (choice1 < 1 || choice1 > 5)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Invalid choice");
                            Console.ResetColor();
                            Console.Write("Enter your choice : ");
                            choice1 = Convert.ToInt32(Console.ReadLine());
                        }
                        if (choice1 == 1)
                        {
                            todoManager.SortByPriorityHighToLow();
                            Console.WriteLine("Taskpad has been updated according to priority(High to Low)");
                            todoManager.ViewAllTasks();
                        }
                        if (choice1 == 2)
                        {
                            todoManager.SortByPriorityLowToHigh();
                            Console.WriteLine("Taskpad has been updated according to priority(Low to High)");
                            todoManager.ViewAllTasks();
                        }
                        if (choice1 == 3)
                        {
                            todoManager.SortByDueDate();
                            Console.WriteLine("Taskpad has been updated according to due date");
                            todoManager.ViewAllTasks();
                        }
                        if (choice1 == 4)
                        {
                            todoManager.SortByTitle();
                            Console.WriteLine("Taskpad has been updated according to Title");
                            todoManager.ViewAllTasks();
                        }
                        if (choice1 == 5)
                        {
                            todoManager.SortByStatus();
                            Console.WriteLine("Taskpad has been updated according to due status");
                            todoManager.ViewAllTasks();
                        }

                        Console.WriteLine();
                        break;

                    
                    case "8":
                        //filter the task and display table based on iscompleted or not
                        Console.WriteLine("Press 1 for filtering taskpad according to status");
                        Console.WriteLine("Press 2 for filtering taskpad according to priority");
                        Console.Write("Enter your Choice : ");
                        string input9 = Console.ReadLine();
                        int choice3;
                        if (!Int32.TryParse(input9, out choice3))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Choice cannot be a string, please enter correct option.");
                            Console.ResetColor();
                            break;
                        }

                        if(choice3 < 0 && choice3 > 2)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("Please enter correct choice.");
                            Console.ResetColor();
                        }
                        else
                        {
                            if(choice3 == 1)
                            {
                                Console.Write("Do you want to see completed tasks?[Y/N]");
                                string inpt = Console.ReadLine();
                                if(inpt == "Y" || inpt == "N" || inpt == "y" || inpt == "n")
                                {
                                    todoManager.FilterTasksByIsComplete(true);
                                }
                                else
                                {
                                    Console.ForegroundColor = ConsoleColor.Red;
                                    Console.WriteLine("Please enter correct choice.");
                                    Console.ResetColor();
                                }
                            }
                        }





                        //Console.WriteLine("This is the future scope, this console section is under construction!");
                        break;

                    case "9":
                        exit = true;
                        todoManager.SortById();
                        string exitMessage = "Thank you for using the Taskpad, your tasks have been saved in file. Have a nice day!";
                        Console.WriteLine(" "+exitMessage.PadLeft(20));
                        PlayExitAudio();
                        break;

                    default:
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please give correct input between 1 to 9.");
                        Console.ResetColor();
                        Console.WriteLine();
                        break;
                }

                // Save tasks to file before exiting
                fileHandler.SaveToFile(todoManager.GetTasks());
            }
        }

        static async void PlayWelcomeAudio()
        {
            try
            {
                using (var audioFile = new AudioFileReader(@"C:\Shibom\C# Module Project\TodoList\welcome.mp3"))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    // Wait for the audio to finish playing
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while playing the welcome audio: " + ex.Message);
            }
        }

        static void PlayExitAudio()
        {
            try
            {
                using (var audioFile = new AudioFileReader(@"C:\Shibom\C# Module Project\TodoList\exit.mp3"))
                using (var outputDevice = new WaveOutEvent())
                {
                    outputDevice.Init(audioFile);
                    outputDevice.Play();

                    // Wait for the audio to finish playing
                    while (outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        System.Threading.Thread.Sleep(100);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred while playing the exit audio: " + ex.Message);
            }
        }

        static void ClearConsole()
        {
            Console.Clear();
            Console.SetCursorPosition(0, 0);
        }

        static void CenterText(string text)
        {
            int consoleWidth = Console.WindowWidth;
            int consoleHeight = Console.WindowHeight;

            string[] lines = text.Split('\n');
            int linesCount = lines.Length;

            int startY = Math.Max(0, (consoleHeight - linesCount) / 2);

            foreach (string line in lines)
            {
                int startX = Math.Max(0, (consoleWidth - line.Length) / 2);
                Console.SetCursorPosition(startX, startY);
                Console.WriteLine(line);
                startY++;
            }
        }

        //static string SearchTask(string query)
        //{
        //    List<TaskItem> tasks = TodoManager.GetTasks();
        //}


    }



    //progressbar class
    static class ConsoleUtility
    {
        const char _block = '■';
        const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
        const string _twirl = "-\\|/";
        public static void WriteProgressBar(int percent, bool update = false)
        {
            if (update)
                Console.Write(_back);
            Console.Write("[");
            var p = (int)((percent / 10f) + .5f);
            for (var i = 0; i < 10; ++i)
            {
                if (i >= p)
                    Console.Write(' ');
                else
                    Console.Write(_block);
            }
            Console.Write("] {0,3:##0}%", percent);
        }
        public static void WriteProgress(int progress, bool update = false)
        {
            if (update)
                Console.Write("\b");
            Console.Write(_twirl[progress % _twirl.Length]);
        }
    }
}