#!/bin/bash
INIT="git init"
ADD="git add ."
COMMIT="git commit -m InitialCommit"
CHECKOUT="git checkout -b develop"
RESTORE="dotnet restore"
BUILD="dotnet build"
PREFIX=Healthy
REPOSITORIES=($PREFIX.Api $PREFIX.Core $PREFIX.Data $PREFIX.Infrastructure $PREFIX.Services)


for REPOSITORY in ${REPOSITORIES[*]}
do
	 echo ========================================================
	 echo Initialize git 
	 echo ========================================================
     cd src
     cd $REPOSITORY
     $ADD
     $COMMIT
     $CHECKOUT
     $RESTORE
     $BUILD
     cd ..
     cd ..
done