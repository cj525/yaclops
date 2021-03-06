﻿
using Yaclops.Parsing.Configuration;

namespace Yaclops.Parsing
{
    internal class ParserContext
    {
        public ParserContext(ParserConfiguration configuration, string text)
        {
            Configuration = configuration;
            Result = new ParseResult();
            Lexer = new Lexer(text);
            Mapper = new CommandMapper(Configuration.Commands);
        }

        public ParserConfiguration Configuration { get; private set; }
        public ParseResult Result { get; private set; }
        public Lexer Lexer { get; private set; }
        public CommandMapper Mapper { get; private set; }

        public ParserCommand Command { get; set; }
    }
}
