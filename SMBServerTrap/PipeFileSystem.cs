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

using DiskAccessLibrary.FileSystems.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace SMBServerTrap;

class PipeFileSystem : IFileSystem
{
    private static readonly DateTime _curr_time = DateTime.Now;

    public string Name => nameof(PipeFileSystem);

    public long Size => 100L * 1024L * 1024L * 1024L;

    public long FreeSpace => 99L * 1024L * 1024L * 1024L;

    public bool SupportsNamedStreams => false;

    public FileSystemEntry CreateDirectory(string path)
    {
        Console.WriteLine("====> {0}: {1}", MethodBase.GetCurrentMethod().Name, path);
        throw new NotImplementedException();
    }

    public FileSystemEntry CreateFile(string path)
    {
        Console.WriteLine("====> {0}: {1}", MethodBase.GetCurrentMethod().Name, path);
        return CreateEntry(path);
    }

    public void Delete(string path)
    {
        Console.WriteLine("====> {0}: {1}", MethodBase.GetCurrentMethod().Name, path);
    }

    public FileSystemEntry GetEntry(string path)
    {
        Console.WriteLine("====> {0}: {1}", MethodBase.GetCurrentMethod().Name, path);
        return CreateEntry(path);
    }

    public List<KeyValuePair<string, ulong>> ListDataStreams(string path)
    {
        Console.WriteLine("====> {0}: {1}", MethodBase.GetCurrentMethod().Name, path);
        return new List<KeyValuePair<string, ulong>>();
    }

    public List<FileSystemEntry> ListEntriesInDirectory(string path)
    {
        Console.WriteLine("====> {0}: {1}", MethodBase.GetCurrentMethod().Name, path);
        return new List<FileSystemEntry>();
    }

    public void Move(string source, string destination)
    {
        Console.WriteLine("====> {0}: {1} -> {2}", MethodBase.GetCurrentMethod().Name, source, destination);
        throw new NotImplementedException();
    }

    public Stream OpenFile(string path, FileMode mode, FileAccess access, FileShare share, FileOptions options)
    {
        Console.WriteLine("====> {0}: {1} {2} {3} {4} {5}", MethodBase.GetCurrentMethod().Name, path, mode, access, share, options);
        return new DelayStream();
    }

    public void SetAttributes(string path, bool? isHidden, bool? isReadonly, bool? isArchived)
    {
        Console.WriteLine("====> {0}: {1} {2} {3} {4}", MethodBase.GetCurrentMethod().Name, path, isHidden, isReadonly, isArchived);
    }

    public void SetDates(string path, DateTime? creationDT, DateTime? lastWriteDT, DateTime? lastAccessDT)
    {
        Console.WriteLine("====> {0}: {1} {2} {3} {4}", MethodBase.GetCurrentMethod().Name, path, creationDT, lastWriteDT, lastAccessDT);
    }

    private static FileSystemEntry CreateEntry(string path)
    {
        return new FileSystemEntry(path, Path.GetFileName(path), path.EndsWith("\\"), 0,
            _curr_time, _curr_time, _curr_time, false, false, false);
    }
}
