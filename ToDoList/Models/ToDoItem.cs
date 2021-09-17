using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace ToDoList.Models
{

    public partial class ToDoItem
    {
        
        public int IdTask { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Details { get; set; }
        public bool IsDone { get; set; }
        [Required]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
    }
}
