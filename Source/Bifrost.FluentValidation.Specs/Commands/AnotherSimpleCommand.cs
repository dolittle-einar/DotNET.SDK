﻿using System;
using Bifrost.Commands;

namespace Bifrost.FluentValidation.Specs.Commands
{
    public class AnotherSimpleCommand : ICommand
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string SomeString { get; set; }

        public int SomeInt { get; set; }
    }
}