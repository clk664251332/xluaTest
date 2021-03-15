@echo off
for /r "./Protos" %%v in (*.proto) do protoc.exe --proto_path=./Protos --csharp_out=./ProtoGen %%~nxv
del /s /q "../Assets/Script/protocal/
for /r "./ProtoGen" %%v in (*.cs) do copy %%v "../Assets/Script/protocal/"
echo finish
pause