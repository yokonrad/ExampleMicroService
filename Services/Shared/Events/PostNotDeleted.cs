﻿namespace Shared.Events
{
    public class PostNotDeleted
    {
        public int Id { get; set; }
        public string Message { get; set; } = null!;
    }
}