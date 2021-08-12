echo Downloading...
try{

Remove-Item -LiteralPath MonoBleedingEdge -Force -Recurse
Remove-Item -LiteralPath NinjasKnightsNecromancers_Data -Force -Recurse
Remove-Item NinjasKnightsNecromancers.exe -Force
Remove-Item UnityCrashHandler64.exe -Force
Remove-Item CurrVer.txt -Force
Remove-Item UnityPlayer.dll -Force
}
catch{

}
try{
Invoke-WebRequest https://github.com/xxxMEMESCOEPxxx/NinjasKnightsNecromancers/raw/main/Build/A_0.0.15.zip -OutFile .\A_0.0.15.zip
}catch{
echo download failed!
}

echo Unzipping Download...
try{


Expand-Archive A_0.0.15.zip -DestinationPath .\






del A_0.0.15.zip


}catch{

}


echo done
NinjasKnightsNecromancers.exe