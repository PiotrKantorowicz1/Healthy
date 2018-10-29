#!/bin/bash

cd src
cd Healthy.Api

	 echo ========================================================
	 echo Building application in debug
	 echo ========================================================

dotnet build  -c Release --source "https://api.nuget.org/v3/index.json" --source "https://www.myget.org/F/imagesharp/api/v3/index.json" --no-cache

