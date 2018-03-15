﻿using System;
using Dolittle.Commands;

namespace Dolittle.FluentValidation.Specs.Commands
{
    public class SimpleCommand : ICommand
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string SomeString { get; set; }

        public int SomeInt { get; set; }
    }
}