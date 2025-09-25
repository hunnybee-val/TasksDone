using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TasksDone.Domain.Enums;

namespace TasksDone.Domain.Entities
{
    public class User
    {
        /// <summary>
        /// User's ID
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Log-in username
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Work e-mail for notifications
        /// </summary>
        public string Email { get; private set; }

        /// <summary>
        /// Password hash with encryption
        /// </summary>
        public string PasswordHash { get; private set; }

        /// <summary>
        /// Role of the user, defining access rights
        /// </summary>
        public UserRole Role { get; private set; }

        /// <summary>
        /// User's tasks list - navigation
        /// </summary>
        private readonly List<TaskItem> _tasks = new();
        public IReadOnlyCollection<TaskItem> Tasks => _tasks.AsReadOnly();


        public User(string username, string email, string passwordHash, UserRole role = UserRole.Guest)
        {
            Id = Guid.NewGuid();
            Username = !string.IsNullOrWhiteSpace(username) ? username : throw new ArgumentException("Username required");
            Email = !string.IsNullOrWhiteSpace(email) ? email : throw new ArgumentException("Email required");
            PasswordHash = passwordHash ?? throw new ArgumentNullException(nameof(passwordHash));
            Role = role;
        }

        public void ChangeEmail(string newEmail)
        {
            if (string.IsNullOrWhiteSpace(newEmail))
                throw new ArgumentException("Email cannot be empty");
            Email = newEmail;
        }

        public void ChangePassword(string newHash)
        {
            if (string.IsNullOrWhiteSpace(newHash))
                throw new ArgumentException("Password hash cannot be empty");
            PasswordHash = newHash;
        }

        public void AssignTask(TaskItem task)
        {
            if (task == null)
                throw new ArgumentNullException(nameof(task));
            _tasks.Add(task);
        }
    }
}
