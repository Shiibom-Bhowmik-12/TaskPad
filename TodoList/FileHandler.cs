using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace TodoList
{
    public class FileHandler
    {
        private const string FileName = @"C:\Shibom\C#-Module-Project\TodoList\Tasks.json";

        // Save tasks to file
        public void SaveToFile(List<TaskItem> tasks)
        {
            JArray jArray = new JArray();
            foreach (var task in tasks)
            {
                JObject jObject = new JObject();
                jObject["Id"] = task.Id;
                jObject["Title"] = task.Title;
                jObject["Description"] = task.Description;
                jObject["IsComplete"] = task.IsComplete;
                jObject["Priority"] = task.Priority;
                jObject["DueDate"] = task.DueDate;
                jArray.Add(jObject);
            }
            string json = JsonConvert.SerializeObject(jArray, Formatting.Indented);
            System.IO.File.WriteAllText(FileName, json);
        }

        // Retrieve tasks from file
        public List<TaskItem> ReadFromFile()
        {
            if (File.Exists(FileName))
            {
                string json = File.ReadAllText(FileName);
                if (!string.IsNullOrWhiteSpace(json))
                {
                    JArray jArray = JArray.Parse(json);
                    List<TaskItem> tasks = new List<TaskItem>();
                    foreach (var jObject in jArray)
                    {
                        TaskItem task = new TaskItem();
                        task.Id = (int)jObject["Id"];
                        task.Title = (string)jObject["Title"];
                        task.Description = (string)jObject["Description"];
                        task.IsComplete = (bool)jObject["IsComplete"];
                        task.Priority = (int)jObject["Priority"];
                        task.DueDate = (DateTime)jObject["DueDate"];
                        tasks.Add(task);
                    }
                    return tasks;
                }
            }
            return new List<TaskItem>(); // Return an empty list if file doesn't exist or is empty
        }


        public void UpdateTask(TaskItem updatedTask)
        {
            List<TaskItem> tasks = ReadFromFile();

            TaskItem existingTask = tasks.FirstOrDefault(task => task.Id == updatedTask.Id);
            if (existingTask != null)
            {
                existingTask.Title = updatedTask.Title;
                existingTask.Description = updatedTask.Description;
                existingTask.IsComplete = updatedTask.IsComplete;
                existingTask.Priority = updatedTask.Priority;
                existingTask.DueDate = updatedTask.DueDate;
                SaveToFile(tasks);
            }
        }
    }
}

