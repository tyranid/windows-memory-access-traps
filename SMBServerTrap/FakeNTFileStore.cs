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

using SMBLibrary;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace SMBServerTrap;

class FakeNTFileStore : INTFileStore
{
    public NTStatus Cancel(object ioRequest)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus CloseFile(object handle)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus CreateFile(out object handle, out FileStatus fileStatus, string path, AccessMask desiredAccess, FileAttributes fileAttributes, ShareAccess shareAccess, CreateDisposition createDisposition, CreateOptions createOptions, SecurityContext securityContext)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus DeviceIOControl(object handle, uint ctlCode, byte[] input, out byte[] output, int maxOutputLength)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus FlushFileBuffers(object handle)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus GetFileInformation(out FileInformation result, object handle, FileInformationClass informationClass)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus GetFileSystemInformation(out FileSystemInformation result, FileSystemInformationClass informationClass)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus GetSecurityInformation(out SecurityDescriptor result, object handle, SecurityInformation securityInformation)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus LockFile(object handle, long byteOffset, long length, bool exclusiveLock)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus NotifyChange(out object ioRequest, object handle, NotifyChangeFilter completionFilter, bool watchTree, int outputBufferSize, OnNotifyChangeCompleted onNotifyChangeCompleted, object context)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus QueryDirectory(out List<QueryDirectoryFileInformation> result, object handle, string fileName, FileInformationClass informationClass)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus ReadFile(out byte[] data, object handle, long offset, int maxCount)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus SetFileInformation(object handle, FileInformation information)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus SetFileSystemInformation(FileSystemInformation information)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus SetSecurityInformation(object handle, SecurityInformation securityInformation, SecurityDescriptor securityDescriptor)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }

    public NTStatus UnlockFile(object handle, long byteOffset, long length)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        return NTStatus.STATUS_SUCCESS;
    }

    public NTStatus WriteFile(out int numberOfBytesWritten, object handle, long offset, byte[] data)
    {
        Console.WriteLine(MethodBase.GetCurrentMethod());
        throw new NotImplementedException();
    }
}
