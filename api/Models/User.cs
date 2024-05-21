﻿using System.ComponentModel.DataAnnotations;

namespace api;

public class User
{
    public int Id { get; set; }
    [Required]
    public string Name { get; set; }
    [EmailAddress]
    [Required]
    public string Email { get; set; }
    [Required]
    public string PasswordHash { get; set; }
    public DateTime AddedAt {get; set; } = DateTime.Now;
}