@echo off

set CUDA_PATH=%1
set COPY_DEST=%2
set CUDA_BIN_DIR="%~dp0..\vcpkg\downloads\%CUDA_PATH%\bin"
if exist "%VCPKG_DOWNLOADS%\%CUDA_PATH%\bin" (
set CUDA_BIN_DIR="%VCPKG_DOWNLOADS%\%CUDA_PATH%\bin"
)

copy %CUDA_BIN_DIR%\cudart*.dll %COPY_DEST%
copy %CUDA_BIN_DIR%\cublas*.dll %COPY_DEST%
