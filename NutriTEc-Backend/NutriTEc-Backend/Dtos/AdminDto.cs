﻿namespace NutriTEc_Backend.Dtos
{
    public class AdminDto
    {
        public int Id { get; set; }
        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;
    }
}
