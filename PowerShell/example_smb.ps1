param(
  [param(Mandatory)]
  [string]$Hostname,
  [int]$Port = 0
)
Import-Module NtObjectManager

$root_path = "\\$Hostname\root"
$map_cmd = "net use $root_path password /user:guest"
if ($Port -ne 0) {
  $map_cmd += " /TCPPORT:$Port"
}

Invoke-Expression $map_cmd

Use-NtObject($f = Get-NtFile "$root_path\file.bin" -Win32Path) {
    Write-Host "Opened File $root_path\file.bin"
    Use-NtObject($s = New-NtSection -File $f -Protection WriteCopy) {
        Use-NtObject($m = Add-NtSection -Section $s -Protection WriteCopy) {
            $m | Out-Host
            Measure-Command { $m.ReadBytes(512*1024*1024, 4) | Out-Host }
        }
    }
}

net use $root_path /DELETE
