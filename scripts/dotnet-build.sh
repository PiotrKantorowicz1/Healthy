#!/bin/bash

RESTORE="dotnet restore"
BUILD="dotnet build"

	 echo ========================================================
	 echo Building application in local environment
	 echo ========================================================

$RESTORE
$BUILD