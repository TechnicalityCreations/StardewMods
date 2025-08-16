set /p "mod=Which Mod?  "
if exist %mod%\ (
    cd %mod%\
    del ..\%mod%.zip /Q
    "C:\Program Files\7-Zip\7z.exe" a ..\%mod%.zip -r -x!*.cs -x!*.csproj -x!*/obj/* -x!*/bin/* -x!.*.png
) else (echo "FOLDER DOESN'T EXIST")