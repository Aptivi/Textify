//
// Textify  Copyright (C) 2023-2025  Aptivi
//
// This file is part of Textify
//
// Textify is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// Textify is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY, without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with this program.  If not, see <https://www.gnu.org/licenses/>.
//

// Originally found in https://github.com/rawsonm88/Cowsay

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Textify.Tests.Cowsay.Stubs
{
    internal class SlowStream : Stream
    {
        private readonly Stream _baseStream;
        private int _threadsReadingCount = 0;

        public SlowStream(Stream baseStream)
        {
            _baseStream = baseStream;
        }

        public override bool CanRead => _baseStream.CanRead;

        public override bool CanSeek => _baseStream.CanSeek;

        public override bool CanWrite => _baseStream.CanWrite;

        public override long Length => _baseStream.Length;

        public override long Position { get => _baseStream.Position; set => _baseStream.Position = value; }

        public int ThreadsReadingCount => _threadsReadingCount;

        public override void Flush()
        {
            _baseStream.Flush();
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            Interlocked.Increment(ref _threadsReadingCount);

            int output;

            try
            {
                Delay().Wait();
                output = _baseStream.Read(buffer, offset, count);
            }
            finally
            {
                Interlocked.Decrement(ref _threadsReadingCount);
            }

            return output;
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            return _baseStream.Seek(offset, origin);
        }

        public override void SetLength(long value)
        {
            _baseStream.SetLength(value);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            _baseStream.Write(buffer, offset, count);
        }

        public override async Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken)
        {
            Interlocked.Increment(ref _threadsReadingCount);

            int output;

            try
            {
                await Delay();
                output = await _baseStream.ReadAsync(buffer, offset, count);
            }
            finally
            {
                Interlocked.Decrement(ref _threadsReadingCount);
            }

            return output;
        }

        private Task Delay()
        {
            return Task.Delay(TimeSpan.FromMilliseconds(500));
        }
    }
}
