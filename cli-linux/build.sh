#!/bin/bash

cd src
cd Healthy.Api

	 echo ========================================================
	 echo Building application in debug
	 echo ========================================================

dotnet restore --source "https://api.nuget.org/v3/index.json"  --source "https://www.myget.org/F/sixlabors/api/v3/index.json" --no-cache
dotnet build --no-restore