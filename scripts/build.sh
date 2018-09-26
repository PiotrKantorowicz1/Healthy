#!/bin/bash

BUILD="dotnet build"
RESTORE="dotnet restore"

cd src
cd Healthy.Api

	 echo ========================================================
	 echo Building application in debug
	 echo ========================================================

$RESTORE
$BUILD
