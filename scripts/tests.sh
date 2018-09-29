#!/bin/bash

TEST="dotnet test"

cd tests
cd Healthy.Tests

	 echo ========================================================
	 echo Testing application in local environment
	 echo ========================================================

$TEST
