Steps to run:
1. Run Install-WindowsService.ps1

This will automatically also start the service.

------------------------------------------------------
Stopping the PManager
1. Run Stop-WindowsService.ps1

This will only stop the service and not remove it. 

-----------------------------------------------------
Starting the PManager when it has been stopped.
1. Run Start-WindowsService.ps1

-----------------------------------------------------
Removing the PManager
1. Run Stop-WindowsService.ps1
2. Run Remove-WindowsService.ps1