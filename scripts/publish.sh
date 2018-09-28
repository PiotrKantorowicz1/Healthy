#!/bin/bash
dotnet publish --no-restore ./src/Healthy.Api -c Release -o ./bin/docker

