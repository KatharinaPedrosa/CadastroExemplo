dotnet publish ./Cadastro/API/Cadastro.API/Cadastro.API.csproj -c release --self-contained true -r win-x64 -o ./release/
Set-Location ./release
invoke-expression 'cmd /c start powershell -Command { ./Cadastro.API.exe }'
Start-Sleep -Seconds 5
Start-Process 'http://localhost:1501'
Start-Process 'http://localhost:1501/swagger/index.html'