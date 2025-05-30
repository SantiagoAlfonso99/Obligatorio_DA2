﻿using Domain.Models;

namespace WebApi.DTOs.In;

public class CategoryCreateModel
{
    public string Name { get; set; }

    public Category ToEntity()
    {
        return new Category() { Name = Name };
    }
}