﻿using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using Shouldly;
using Yaclops.Attributes;
using Yaclops.Injecting;
using Yaclops.Model;
using Yaclops.Parsing;

namespace Yaclops.Tests.Injecting
{
    [TestFixture]
    public class PropertyInjectorTests
    {


        [Test]
        public void CanInjectLongNamedParameter()
        {
            var result = new ResultMaker().NamedParam("Num", "316").Result;
            var target = Populate<LongNamed>(result);

            target.Num.ShouldBe(316);
        }


        [Test]
        public void CanInjectLongPositionalParameter()
        {
            var result = new ResultMaker().PositionalParam("Num", "806").Result;
            var target = Populate<LongPositional>(result);

            target.Num.ShouldBe(806);
        }


        [Test]
        public void CanInjectOneItemIntoInitializedStringList()
        {
            var result = new ResultMaker().PositionalParam("Things", "Fred", true).Result;
            var target = Populate<InitializedStringList>(result);

            target.Things.ShouldBe(new[] { "Fred" });
        }


        [Test]
        public void CanInjectOneItemIntoUninitializedStringList()
        {
            var result = new ResultMaker().PositionalParam("Things", "Barney", true).Result;
            var target = Populate<UninitializedStringList>(result);

            target.Things.ShouldBe(new[] { "Barney" });
        }


        [Test]
        public void CanInjectMultipleItemsIntoUninitializedStringList()
        {
            var result = new ResultMaker().PositionalParam("Things", "Fred", "Barney").Result;
            var target = Populate<UninitializedStringList>(result);

            target.Things.ShouldBe(new[] { "Fred", "Barney" });
        }


        [Test]
        public void CanInjectMultipleItemsIntoUninitializedLongList()
        {
            var result = new ResultMaker().PositionalParam("Nums", "1025", "408").Result;
            var target = Populate<UninitializedLongList>(result);

            target.Nums.ShouldBe(new[] { 1025L, 408L });
        }


        [Test]
        public void CanInjectEnumPositionalParameter()
        {
            var result = new ResultMaker().PositionalParam("Num", "One").Result;
            var target = Populate<EnumPositional>(result);

            target.Num.ShouldBe(TestEnum.One);
        }


        [Test]
        public void CanInjectMultipleItemsIntoUninitializedEnumList()
        {
            var result = new ResultMaker().PositionalParam("Nums", "Two", "Three").Result;
            var target = Populate<UninitializedEnumList>(result);

            target.Nums.ShouldBe(new[] { TestEnum.Two, TestEnum.Three });
        }


        [Test]
        public void CanInjectMultipleItemsIntoUninitializedEnumSet()
        {
            var result = new ResultMaker().PositionalParam("Nums", "Two", "Three").Result;
            var target = Populate<UninitializedEnumSet>(result);

            target.Nums.ShouldContain(TestEnum.Two);
            target.Nums.ShouldContain(TestEnum.Three);
            target.Nums.ShouldNotContain(TestEnum.One);
        }



        #region Helpers

        private T Populate<T>(ParseResult result) where T : new()
        {
            var injector = new PropertyInjector(result);
            var target = new T();

            injector.Populate(target);

            return target;
        }



        private class ResultMaker
        {
            private readonly ParseResult _result;

            public ResultMaker()
            {
                _result = new ParseResult
                {
                    NamedParameters = new List<ParserNamedParameterResult>(),
                    PositionalParameters = new List<ParserPositionalParameterResult>()
                };
            }


            public ResultMaker NamedParam(string propertyName, string value, bool isBool = false)
            {
                _result.NamedParameters.Add(new ParserNamedParameterResult(new CommandNamedParameter(propertyName, isBool, x => x), value));
                return this;
            }


            public ResultMaker PositionalParam(string propertyName, string value, bool isList = false)
            {
                _result.PositionalParameters.Add(new ParserPositionalParameterResult(new CommandPositionalParameter(propertyName, isList, false, x => x), value));
                return this;
            }


            public ResultMaker PositionalParam(string propertyName, params string[] values)
            {
                var thing = new ParserPositionalParameterResult(new CommandPositionalParameter(propertyName, true, false, x => x), values.First());
                thing.Values.AddRange(values.Skip(1));
                _result.PositionalParameters.Add(thing);
                return this;
            }


            public ParseResult Result { get { return _result; } }
        }

        #endregion

        #region Target Commands

        public class LongNamed
        {
            [NamedParameter]
            public long Num { get; set; }
        }


        public class LongPositional
        {
            [PositionalParameter]
            public long Num { get; set; }
        }


        public class InitializedStringList
        {
            public InitializedStringList()
            {
                Things = new List<string>();
            }

            [PositionalParameter]
            public List<string> Things { get; private set; } 
        }


        public class UninitializedStringList
        {
            [PositionalParameter]
            public List<string> Things { get; set; } 
        }


        public class UninitializedLongList
        {
            [PositionalParameter]
            public List<long> Nums { get; set; } 
        }


        public enum TestEnum
        {
            One,
            Two,
            Three
        };

        public class EnumPositional
        {
            [PositionalParameter]
            public TestEnum Num { get; set; }
        }

        public class UninitializedEnumList
        {
            [PositionalParameter]
            public List<TestEnum> Nums { get; set; }
        }

        public class UninitializedEnumSet
        {
            [PositionalParameter]
            public HashSet<TestEnum> Nums { get; set; }
        }

        #endregion
    }
}
