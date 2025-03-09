﻿namespace SystemDeFacturation_Server.Models
{
    public class Acheteur
    {
        public int AcheteurID { get; set; }
        public required string Nom { get; set; }
        public required string Prenom { get; set; }
        public string? Adresse { get; set; }
        public required string Email { get; set; }
        public required string Telephone { get; set; }
        
    }
}
