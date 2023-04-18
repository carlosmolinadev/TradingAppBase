﻿using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public decimal RiskPercentage { get; set; }
        public string? Currency { get; set; }
        public Exchange Exchange { get; set; }
        public string? Derivate { get; set; }
        [NotMapped]
        public IList<Trade> Trades { get; set; }  = new List<Trade>();
    }
}
