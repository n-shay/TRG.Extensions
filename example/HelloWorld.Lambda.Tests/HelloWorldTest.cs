using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using Amazon.Lambda.TestUtilities;
using HelloWorld.Lambda.Models;
using Xunit;
using Xunit.Abstractions;

namespace HelloWorld.Lambda.Tests
{
    public class HelloWorldTest
    {
        private readonly ITestOutputHelper _output;

        public HelloWorldTest(ITestOutputHelper output)
        {
            _output = output;
        }

        [Fact]
        public void InstantiatedAndExecutedOnce_Success()
        {
            Output response;
            var context = new TestLambdaContext();

            using (var bt = new BlockTelemeter("Test", _output.WriteLine, false))
            {
                var function = new Startup();
                bt.Snap("Startup");

                response = function.Handle(null, context);
            }

            Assert.NotNull(response);
            Assert.Equal("Hello World!", response.Message);
        }
    }

    public class BlockTelemeter : IDisposable
    {
        private readonly string _blockName;
        private readonly Action<string> _writeAction;
        private readonly bool _writeNewLine;
        private readonly List<(string Name, long Time)> _collection;
        private readonly Stopwatch _stopwatch;

        public BlockTelemeter(string blockName, TextWriter textWriter)
            : this(blockName, textWriter.WriteLine, false)
        {
            if (textWriter == null) throw new ArgumentNullException(nameof(textWriter));
        }

        public BlockTelemeter(string blockName, Action<string> writeAction, bool writeNewLine = true)
        {
            _blockName = blockName;
            _writeAction = writeAction ?? throw new ArgumentNullException(nameof(writeAction));
            _writeNewLine = writeNewLine;
            _collection = new List<(string Name, long Time)>();
            _stopwatch = Stopwatch.StartNew();
        }

        public void Snap(string checkpointName)
        {
            RecordTime(checkpointName);

            _stopwatch.Restart();
        }

        public void Dispose()
        {
            RecordTime("[End]");
            _stopwatch.Stop();

            var average = _collection.Average(i => i.Time);
            var total = _collection.Sum(i => i.Time);

            var newLine = _writeNewLine ? Environment.NewLine : null;
            _writeAction($"[{_blockName}] {_collection.Count} checkpoints in {total:N0}ms (Average: {average:N0}ms).{newLine}");

            for (var i = 0; i < _collection.Count; i++)
            {
                var (name, time) = _collection[i];

                _writeAction($"[{_blockName}] {name}:{i + 1} - {time:N0}ms{newLine}");
            }
        }

        private void RecordTime(string name)
        {
            var time = _stopwatch.ElapsedMilliseconds;
            _collection.Add((name, time));
        }
    }
}
