# Example code for Windows Memory Trap servers.
(c) James Forshaw 2021-2024

This solution contains example code for testing Windows memory traps. To correctly pull the repo
you need to recurse git submodules, e.g. `git clone --recurse-submodules https://github.com/tyranid/windows-memory-access-traps.git`.

The solution contains two .NET 9 projects:
* SMBServerTrap - This is a fake SMB server to trap on memory access.
* CloudFilterTrap - A basic cloud provider which can be used to trap memory access via loopback SMB.

You don't need Visual Studio to build. Instead you can install the [.NET 9 SDK](https://github.com/tyranid/windows-memory-access-traps.git)
from Microsoft at then in the extracted directory run the command:

`C:\src> dotnet build`

You can then run each project with the command:

`C:\src> dotnet run --project SMBServerTrap\SMBServerTrap.csproj [port]`

or 

`C:\src> dotnet run --project CloudFilterTrap\CloudFilterTrap.csproj "c:\root"`

By default to use the SMBServerTrap code you'll need to disable the local SMB server,
or run it on a non-Windows system. However you can also specify a port number and run
the server on a non-standard TCP port. However, only Windows 11 24H2 and above supports
connecting the SMB client to a non-standard TCP port. You can set this up with the following
command replacing `<port>` with the value you chose, and making sure the SMB server is running:

`C:\> net use \\localhost\root password /user:guest /TCPPORT:<port>`

Now when you access the SMB server on `\\localhost\root` it will use the TCP port and connect
to your local server.

The CloudFilterTrap application can only run on Windows 10 greater than 1709. While it shouldn't impact
OneDrive I would not recommend running it on a system where you also rely on the OneDrive provider running
at the same time.

## Usage:

Both examples expose a 1GiB file called file.bin. For the SMBServerTrap code this is directly under the
root share name, so you can access it via `\\HOSTNAME\root\file.bin`. For the CloudFilterTrap it'll put the
file in the directory passed on the command line. For example if you specify the path `c:\root` then you
can access the file via loopback SMB with the path `\\localhost\c$\root\file.bin`.

When you access a position in the file above the 512MiB offset the code will trap and print out to the
console the offset details as shown below:

```
Requesting offset 0x20000000 length 0x00008000
====> Trapping for offset 20000000
```

When this happens you need to manually restart the trap by type 'c' and hitting enter in the console. You
can also exit the application by typing 'x' and enter. The SMB server can limit the trapped data to a single
page (4096 bytes). The cloud filter will return 32KiBs from any read.

An PowerShell script `example_cloud.ps1` is supplied which will map the cloud filter file via local SMB loopback 
and access at the 512MiB boundary. A similar one is provided for SMB `example_smb.ps1`, you'll need to specify
the hostname of the SMB server. You can also specify the port number if you're running locally and you're using
a different port. For example `.\example_smb.ps1 -Hostname localhost -Port 12345`.
