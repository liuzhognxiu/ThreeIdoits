@echo off

::-------------------------------------
:: 转换所有数据
::-------------------------------------

echo.
echo =========Compilation  All xls=========


::---------------------------------------------------
::第一步，将xls经过xls_deploy_tool转成bin和proto
::---------------------------------------------------
set STEP1_XLS2PROTO_PATH=xls2proto

@echo on
cd %STEP1_XLS2PROTO_PATH%

@echo off
echo TRY TO DELETE TEMP FILES:
del *_pb2.py
del *_pb2.pyc
del *.proto
del *.bin
del *.log
del *.txt

@echo on

for /f "delims=" %%i in (..\ConvertList.txt) do   python ..\xls2protobuf_v3.py %%i


::---------------------------------------------------
::第二步：把proto翻译成cs
::---------------------------------------------------
cd ..

set STEP2_PROTO2CS_PATH=.\proto2cs
set PROTO_DESC=proto.protodesc
set SRC_OUT=.\


::for cpp
set STEP2_PROTO2CPP_PATH=.\proto2cpp

cd %STEP2_PROTO2CS_PATH%

@echo off
echo TRY TO DELETE TEMP FILES:
del *.cs
del *.protodesc
del *.txt


@echo on
dir ..\%STEP1_XLS2PROTO_PATH%\*.proto /b  > protolist.txt

@echo on
for /f "delims=." %%i in (protolist.txt) do protoc --descriptor_set_out=%%i.protodesc --proto_path=..\%STEP1_XLS2PROTO_PATH% ..\%STEP1_XLS2PROTO_PATH%\%%i.proto
::for /f "delims=." %%i in (protolist.txt) do ProtoGen\protogen -i:%PROTO_DESC% -o:%%i.cs
for /f "delims=." %%i in (protolist.txt) do protoc --proto_path=..\%STEP1_XLS2PROTO_PATH% ..\%STEP1_XLS2PROTO_PATH%\%%i.proto --csharp_out=%SRC_OUT%

::for cpp
for /f "delims=." %%i in (protolist.txt) do protoc --proto_path=..\%STEP1_XLS2PROTO_PATH% ..\%STEP1_XLS2PROTO_PATH%\%%i.proto --cpp_out=%SRC_OUT%

cd ..

move /y %STEP2_PROTO2CS_PATH%\*pb.cc %STEP2_PROTO2CPP_PATH%
move /y %STEP2_PROTO2CS_PATH%\*pb.h %STEP2_PROTO2CPP_PATH%

::---------------------------------------------------
::第三步：将bin和cs拷到Assets里
::---------------------------------------------------

@echo off
set OUT_PATH=..\..\..\Client\MODWorkspace\MODUnityProject\Assets
set DATA_DEST=StreamingAssets\DataConfig
set CS_DEST=Plugins\ResData
  

@echo on
copy %STEP1_XLS2PROTO_PATH%\*.bin %OUT_PATH%\%DATA_DEST%
copy %STEP2_PROTO2CS_PATH%\*.cs %OUT_PATH%\%CS_DEST%

::---------------------------------------------------
::第四步：清除中间文件
::---------------------------------------------------
@echo off
echo TRY TO DELETE TEMP FILES:
cd %STEP1_XLS2PROTO_PATH%
del *_pb2.py
del *_pb2.pyc
:: del *.proto
:: del *.bin
:: del *.log
:: del *.txt
:: cd ..
:: cd %STEP2_PROTO2CS_PATH%
:: del *.cs
:: del *.protodesc
:: del *.txt
:: cd ..

::---------------------------------------------------
::第五步：结束
::---------------------------------------------------
cd ..

@echo on

pause
