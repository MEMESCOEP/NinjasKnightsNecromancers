cls
echo Downloading...
try{
(taskkill /IM NinjasKnightsNecromancers.exe) | Out-Null
Start-Sleep -s 5
Remove-Item -LiteralPath MonoBleedingEdge -Force -Recurse
Remove-Item -LiteralPath NinjasKnightsNecromancers_Data -Force -Recurse
Remove-Item NinjasKnightsNecromancers.exe -Force
Remove-Item UnityCrashHandler64.exe -Force
Remove-Item CurrVer.txt -Force
Remove-Item UnityPlayer.dll -Force
}
catch{

}

$ProgressPreference = 'SilentlyContinue'
try{
(Invoke-WebRequest https://github.com/xxxMEMESCOEPxxx/NinjasKnightsNecromancers/raw/main/Build/LatestVersion.zip -OutFile .\LatestVersion.zip) | Out-Null
}catch{
echo download failed!
} > $null
$ProgressPreference = 'Continue'


echo Unzipping Download...
try{


Expand-Archive LatestVersion.zip -DestinationPath .\
echo Showing Metadata for DEBUGING PURPOSES...

$folder=".\"

 Get-ChildItem $folder -Recurse | Measure-Object


Get-ChildItem -Path $folder -Recurse   | Format-List *



cls
echo Deleting Temporary Files...


del LatestVersion.zip


}catch{

}


echo done
echo Starting Game...
start .\NinjasKnightsNecromancers.exe