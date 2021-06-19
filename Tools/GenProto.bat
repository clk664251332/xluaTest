set outPath=..\Assets\Script\Proto

cd ..\Proto

for %%i in (*.proto) do (
	..\Tools\protoc.exe --proto_path=%cd% --csharp_out=%outPath% %%i
)

pause