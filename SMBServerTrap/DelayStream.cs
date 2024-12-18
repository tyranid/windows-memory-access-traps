//  This file is part of WindowsMemoryAccessTraps.
//  Copyright (C) Google LLC 2021
//
//  WindowsMemoryAccessTraps is free software: you can redistribute it and/or modify
//  it under the terms of the GNU General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  WindowsMemoryAccessTraps is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU General Public License for more details.
//
//  You should have received a copy of the GNU General Public License
//  along with WindowsMemoryAccessTraps.  If not, see <http://www.gnu.org/licenses/>.

using System;
using System.IO;
using System.Reflection;

namespace SMBServerTrap;

class DelayStream : Stream
{
    const long MaxLength = 1024 * 1024 * 1024;

    public override bool CanRead => true;

    public override bool CanSeek => true;

    public override bool CanWrite => true;

    public override long Length => MaxLength;

    public override long Position { get; set; }

    public override void Flush()
    {
        Console.WriteLine("====> {0}", MethodBase.GetCurrentMethod().Name);
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        Console.WriteLine("====> {0} {1} {2}", MethodBase.GetCurrentMethod().Name, offset, count);
        long remaining = MaxLength - (Position + offset);
        const int PageSize = 4096;
        if (count > remaining)
        {
            count = (int)remaining;
        }

        if (count > PageSize)
        {
            count = PageSize;
        }

        if (Position >= (512 * 1024 * 1024) && Position < (768 * 1024 * 1024))
        {
            Console.WriteLine("====> Delaying at Position {0:X}", Position);
            Console.WriteLine("====> Type 'c' and ENTER to continue.");
            Program.ContinueEvent.WaitOne();
            Console.WriteLine("====> Continuing.");
        }

        byte[] page_address = BitConverter.GetBytes(Position / PageSize * PageSize);
        for (int i = 0; i < count; i++)
        {
            buffer[i] = page_address[i % page_address.Length];
        }
        Position += count;

        return count;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        Console.WriteLine("====> {0} {1:X} {2}", MethodBase.GetCurrentMethod().Name, offset, origin);
        switch (origin)
        {
            case SeekOrigin.Begin:
                Position = offset;
                break;
            case SeekOrigin.Current:
                Position += offset;
                break;
            case SeekOrigin.End:
                Position = Length - offset - 1;
                break;
        }
        return Position;
    }

    public override void SetLength(long value)
    {
        Console.WriteLine("====> {0} {1} {2}", MethodBase.GetCurrentMethod().Name, value);
        throw new NotImplementedException();
    }

    public override void Write(byte[] buffer, int offset, int count)
    {
        Position += count;
        Console.WriteLine("====> {0} {1} {2}", MethodBase.GetCurrentMethod().Name, offset, count);
    }
}
