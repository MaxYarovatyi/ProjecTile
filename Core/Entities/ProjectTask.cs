using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public enum TaskStatus
    {
        ToDo,      // Завдання потрібно виконати
        InProgress,// Завдання в процесі виконання
        Done       // Завдання виконано
    }
    public class ProjectTask : BaseEntity
    {
        public string TaskId { get; set; } // Унікальний ідентифікатор завдання
        public string Title { get; set; } // Назва завдання
        public string Description { get; set; } // Опис завдання
        public DateTime CreatedAt { get; set; } // Дата створення
        public DateTime? UpdatedAt { get; set; } // Дата останнього оновлення
        public DateTime? DueDate { get; set; } // Дедлайн
        public TaskStatus? Status { get; set; } // Статус завдання (enum)
        public string? AssignedToId { get; set; } // Ідентифікатор користувача-виконавця
        public AccountUser? AssignedTo { get; set; } // Призначений користувач (зв'язок з User)

        public string? ProjectId { get; set; } // Ідентифікатор проєкту
        public Project? Project { get; set; } // Проєкт, до якого прив'язане завдання (зв'язок з Project)
        public List<string> Comments { get; set; }


    }
}