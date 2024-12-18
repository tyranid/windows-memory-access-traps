Import-Module NtObjectManager

Use-NtObject($f = Get-NtFile "\\localhost\c$\root\file.bin" -Win32Path) {
    Write-Host "Opened File file"
    Use-NtObject($s = New-NtSection -File $f -Protection WriteCopy) {
        Use-NtObject($m = Add-NtSection -Section $s -Protection WriteCopy) {
            $m | Out-Host
            Measure-Command { $m.ReadBytes(512*1024*1024, 4) | Out-Host }
        }
    }
}
