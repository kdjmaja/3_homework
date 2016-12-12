using System;

namespace _1_zadatak.Models
{
    public class TodoItem
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime? DateCompleted { get; set; }
        public DateTime DateCreated { get; set; }
        public Guid UserId { get; set; }

        public TodoItem(string text, Guid userId)
        {
            Id = Guid.NewGuid(); // Generates new unique identifier
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now; // Set creation date as current time
            UserId = userId;
        }
        public TodoItem()
        {
            // entity framework needs this one
            // not for use :)
        }

        public void Mark()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                DateCreated = DateTime.Now;
            }
        }

        public void UpdateItem(TodoItem todoItem)
        {
            Text = todoItem.Text;
            IsCompleted = todoItem.IsCompleted;
            DateCreated = DateTime.Now;
        }
    }
}
