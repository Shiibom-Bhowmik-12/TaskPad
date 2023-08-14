using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading.Tasks.Sources;

namespace TodoList
{
    public class TodoManager
    {
        private List<TaskItem> tasks;

        public TodoManager()
        {
            tasks = new List<TaskItem>();
        }

        public List<TaskItem> GetTasks()
        {
            return tasks;
        }

        public void SetTasks(List<TaskItem> loadedTasks)
        {
            tasks = loadedTasks;
        }

        public void AddTask(string title,string description, int priority, DateTime duedate)
        { 
            var maxId = tasks.Max(t => t.Id);
            TaskItem task = new TaskItem()
            {
                Id = maxId + 1,
                Title = title,
                Description = description,
                IsComplete = false,
                Priority = priority,
                DueDate = duedate
            };
            tasks.Add(task);
        }


        //return maxid
        public int GetMaxId()
        {
            return tasks.Max(t => t.Id);
        }

        //view all tasks
        public void ViewAllTasks()
        {
            Console.WriteLine();
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Yellow;
            string namePad = "Your TaskPad";
            string dashes = "-------------";
            Console.WriteLine(namePad.PadLeft(40));
            Console.WriteLine(dashes.PadLeft(40));
            Console.ResetColor();
            string table = TablePrinter.DisplayTasksInTableFormat(tasks);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(table);
            Console.ResetColor();
        }

        //view a single task using id
        public void ViewTask(int taskId)
        {
            Console.WriteLine(taskId);
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if (task != null)
            {
                string table = TablePrinter.DisplayTasksInTableFormat(new List<TaskItem>() { task });
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine(table);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("Task not found");
            }
        }

        //Filter tasks by Iscomplete and display the table
        public void FilterTasksByIsComplete(bool isComplete)
        {
            var filteredTasks = tasks.Where(t => t.IsComplete == isComplete).ToList();
            string table = TablePrinter.DisplayTasksInTableFormat(filteredTasks);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(table);
            Console.ResetColor();
        }

        //Filter tasks by priority and display the table
        public void FilterTasksByPriority(int priority)
        {
            var filteredTasks = tasks.Where(t => t.Priority == priority).ToList();
            string table = TablePrinter.DisplayTasksInTableFormat(filteredTasks);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(table);
            Console.ResetColor();
        }

        //Filter tasks by duedate and display the table
        public void FilterTasksByDueDate(DateTime dueDate)
        {
            var filteredTasks = tasks.Where(t => t.DueDate == dueDate).ToList();
            string table = TablePrinter.DisplayTasksInTableFormat(filteredTasks);
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine(table);
            Console.ResetColor();
        }


        //complete a task using id
        public void CompleteTask(int taskId)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if(task != null)
            {
                task.IsComplete = true;
            }
            else
            {
                Console.WriteLine("Task not found");
            }
        }


        //delete a task using id
        public void DeleteTask(int taskId)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if(task != null)
            {
                tasks.Remove(task);
            }
            else
            {
                Console.WriteLine("Task not found");
            }
        }

        //delete all tasks from the list
        public void DeleteAllTasks()
        {
            tasks.Clear();
        }

        //update priority
        public void UpdatePriority(int taskId, int priority)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if(task != null)
            {
                task.Priority = priority;
            }
            else
            {
                Console.WriteLine("Task not found");
            }
        }

        //update due date
        public void UpdateDueDate(int taskId, DateTime dueDate)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if(task != null)
            {
                task.DueDate = dueDate;
            }
            else
            {
                Console.WriteLine("Task not found");
            }
        }

        //update title of the task
        public void UpdateTaskTitle(int taskId, string title)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if(task != null)
            {
                task.Title = title;
            }
            else
            {
                Console.WriteLine("Task not found");
            }
        }

        //update task description
        public void UpdateTaskDescription(int taskId, string description)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if(task != null)
            {
                task.Description = description;
            }
            else
            {
                Console.WriteLine("Task not found");
            }
        }

        //update task status
        public void UpdateTaskStatus(int taskId, bool isComplete)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            if(task != null)
            {
                task.IsComplete = isComplete;
            }
            else
            {
                Console.WriteLine("Task not found");
            }
        }

        //sort by priority high to low
        public void SortByPriorityHighToLow()
        {
            tasks = tasks.OrderBy(t => t.Priority).ToList();
        }

        //sort by priority low to high
        public void SortByPriorityLowToHigh()
        {
            tasks = tasks.OrderByDescending(t => t.Priority).ToList();
        }

        //sort by due date
        public void SortByDueDate()
        {
            tasks = tasks.OrderBy(t => t.DueDate).ToList();
        }

        //sort by title
        public void SortByTitle()
        {
            tasks = tasks.OrderBy(t => t.Title).ToList();
        }

        //sort by status
        public void SortByStatus()
        {
            tasks = tasks.OrderBy(t => t.IsComplete).ToList();
        }

        //sort by id
        public void SortById()
        {
            tasks = tasks.OrderBy(t => t.Id).ToList();
        }


        //return all List of tasks having same alphabetical order
        //search function

        //static List<TaskItem> searchedTasks = new List<TaskItem>();
        ////searchedTasks.Add(tasks);
        //static List<TaskItem> SearchByTitle(string title)
        //{
        //    //var searchedTasks = tasks.Where(t => t.Title.Contains(title)).ToList();
        //    return searchedTasks;
        //}

        //return particular task by id
        public TaskItem GetTaskById(int taskId)
        {
            var task = tasks.FirstOrDefault(t => t.Id == taskId);
            return task;
        }

        //return particular task by title
        public TaskItem GetTaskByTitle(string title)
        {
            var task = tasks.FirstOrDefault(t => t.Title == title);
            return task;
        }


    }
}
