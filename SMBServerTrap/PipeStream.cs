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

class PipeStream : Stream
{
    public override bool CanRead => true;

    public override bool CanSeek => false;

    public override bool CanWrite => true;

    public override long Length => 1024 * 1024 * 1024;

    public override long Position { get; set; }

    public override void Flush()
    {
        Console.WriteLine("====> {0}", MethodBase.GetCurrentMethod().Name);
    }

    public override int Read(byte[] buffer, int offset, int count)
    {
        Console.WriteLine("====> {0} {1} {2}", MethodBase.GetCurrentMethod().Name, offset, count);
        for (int i = 0; i < count; ++i)
        {
            buffer[offset + i] = 0;
        }
        return count;
    }

    public override long Seek(long offset, SeekOrigin origin)
    {
        Console.WriteLine("====> {0} {1:X} {2}", MethodBase.GetCurrentMethod().Name, offset, origin);
        throw new NotImplementedException();
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
