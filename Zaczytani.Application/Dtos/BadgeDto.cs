﻿namespace Zaczytani.Application.Dtos;

public class BadgeDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string IconUrl { get; set; } = string.Empty;
    public string Type { get; set; } = string.Empty;
}
