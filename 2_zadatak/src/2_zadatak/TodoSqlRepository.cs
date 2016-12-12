using System;
using System.Collections.Generic;
using System.Linq;
using _2_zadatak.Interfaces;
using _2_zadatak.Models;

namespace _2_zadatak
{
    public class TodoSqlRepository : ITodoRepository
    {
        private readonly TodoDbContext _context;
        public TodoSqlRepository(TodoDbContext context)
        {
            _context = context;
        }

        //iz nekog razloga ne funkcionira dobro
        public TodoItem Get(Guid todoId, Guid userId)
        {
            var item = _context.TodoList.FirstOrDefault(p => p.Id == todoId);
            if (item != null)
            {
                if (item.UserId.Equals(userId))
                {
                    return item;
                }
                else
                {
                    throw new TodoAccessDeniedException("Access Denied.");
                }
            }
            return null;
        }

        public void Add(TodoItem todoItem)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }
            var item = Get(todoItem.Id, todoItem.UserId);
            if (item != null)
            {
                throw new DuplicateTodoItemException(todoItem.Id);
            }
            _context.TodoList.Add(todoItem);
            _context.SaveChanges();
        }

        public bool Remove(Guid todoId, Guid userId)
        {
            var item = Get(todoId, userId);
            if (item != null)
            {
                _context.TodoList.Remove(item);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public void Update(TodoItem todoItem, Guid userId)
        {
            if (todoItem == null)
            {
                throw new ArgumentNullException();
            }
            var item = Get(todoItem.Id, userId);
            if (item == null)
            {
                Add(todoItem);
            }
            else
            {
                item.UpdateItem(todoItem);
            }
        }

        public bool MarkAsCompleted(Guid todoId, Guid userId)
        {
            var item = Get(todoId, userId);
            if (item == null)
            {
                return false;
            }
            item.Mark();
            _context.SaveChanges();
            return true;
        }

        public List<TodoItem> GetAll(Guid userId)
        {
            if (!_context.TodoList.Any(a => a.UserId == userId)) return null;
            return _context.TodoList.Where(p => p.UserId.Equals(userId)).OrderByDescending(p => p.DateCreated).ToList();
        }

        public List<TodoItem> GetActive(Guid userId)
        {
            if (_context.TodoList == null) return null;
            if (!_context.TodoList.Any(a => a.UserId == userId)) return null;
            return _context.TodoList.Where(p => p.UserId.Equals(userId) && p.IsCompleted == false).OrderByDescending(p => p.DateCreated).ToList();
        }

        public List<TodoItem> GetCompleted(Guid userId)
        {
            if (!_context.TodoList.Any(a => a.UserId == userId)) return null;
            return _context.TodoList.Where(p => p.UserId.Equals(userId) && p.IsCompleted).OrderByDescending(p => p.DateCreated).ToList();
        }

        public List<TodoItem> GetFiltered(Func<TodoItem, bool> filterFunction, Guid userId)
        {
            if (filterFunction == null)
            {
                throw new ArgumentNullException();
            }
            return _context.TodoList.Where(filterFunction).OrderByDescending(p => p.DateCreated).ToList();
        }
    }
}