protoc --csharp_out=../ClientProtobuf/ mapEditor.proto 

cd ../ClientProtobuf
copy * /Y ..\NGUIProj\Assets\ClientProtobuf
pause