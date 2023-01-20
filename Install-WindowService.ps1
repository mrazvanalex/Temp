#Variable setup
$exePath = "PManager"
$exeFolderPath =  '"Pmanager\Pos.Devices.PeripheralManager.exe"'
$username = "Local User"
$description = "Peripheral Manager Windows Service"
$displayName = "Peripheral Manager"
$location = Get-Location

#Create the service
New-Service -Name "PManager2" -BinaryPathName "$location\PManager\Pos.Devices.PeripheralManager.exe" -Description "$description" -StartupType Manual

#Start the service
& "./Start-WindowsService.ps1"
