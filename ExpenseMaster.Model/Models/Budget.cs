﻿
namespace ExpenseMaster.Model.Models
{
    public class Budget
    {
        public int Id { get; set; }
        int UserId {get; set;}
        int CategoryId { get; set; }
        public decimal Limit { get; set; }
        public decimal WarningThreshold { get; set; }

        public User? User { get; set; }
        public Category? Category { get; set; }
    }
}
