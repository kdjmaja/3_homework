using System;

namespace _2_zadatak.Models
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
            Id = Guid.NewGuid();
            Text = text;
            IsCompleted = false;
            DateCreated = DateTime.Now;
            UserId = userId;
        }
        public TodoItem()
        {
         
        }

        public void Mark()
        {
            if (!IsCompleted)
            {
                IsCompleted = true;
                DateCompleted = DateTime.Now;
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
