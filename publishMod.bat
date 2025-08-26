set /p "mod=Which Mod?  "
if exist %mod%\ (
    del %mod%.zip /Q
    "C:\Program Files\7-Zip\7z.exe" a %mod%.zip -r %mod%/* -x!*.cs -x!*.csproj -x!*obj* -x!*bin* -x!.*.png -x!*.zip -x!bin -x!obj -x!*.sln
) else (echo "FOLDER DOESN'T EXIST")