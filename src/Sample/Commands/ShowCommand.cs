﻿using Sample.Helpers;
using Yaclops;

namespace Sample.Commands
{
    [Summary("Show various types of objects")]
    public class ShowCommand : ISubCommand
    {
        public void Execute()
        {
            // Execute the command. For demo purposes, just dump out the parameters...
            this.Dump();
        }
    }
}