using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

    public class Student
    {
        [Key]
        public int Id { get; set; }
        public string stuName { get; set; }
        public string stuSurname { get; set; }
        public string studentId{ get; set; }
     
        public string stuMail { get; set; }
        public string stuPhoneNumber { get; set; }
        
    }
