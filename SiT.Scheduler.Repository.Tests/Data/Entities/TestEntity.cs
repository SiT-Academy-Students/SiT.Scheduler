namespace SiT.Scheduler.Repository.Tests.Data.Entities;

using System;
using SiT.Scheduler.Data.Contracts.Models;

public class TestEntity : IEntity
{
    public Guid Id { get; set; }
    public string RandomString { get; set; }
    public int RandomNumber { get; set; }
    public bool RandomBoolean { get; set; }
}
