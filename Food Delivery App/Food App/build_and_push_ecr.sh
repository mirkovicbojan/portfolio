#!/bin/bash
set -e 

aws ecr get-login-password --region us-east-1 --profile foodapp-ecr-agent | docker login --username AWS --password-stdin 120416562914.dkr.ecr.us-east-1.amazonaws.com/food-app-main
docker build -f ./Dockerfile -t food-app-main:latest .
docker tag food-app-main:latest 120416562914.dkr.ecr.us-east-1.amazonaws.com/food-app-main:latest
docker push 120416562914.dkr.ecr.us-east-1.amazonaws.com/food-app-main:latest