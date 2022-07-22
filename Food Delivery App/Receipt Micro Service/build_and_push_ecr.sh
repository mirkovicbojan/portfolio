#!/bin/bash
set -e 

aws ecr get-login-password --region us-east-1 --profile foodapp-ecr-agent | docker login --username AWS --password-stdin 120416562914.dkr.ecr.us-east-1.amazonaws.com/food-app-receipts
docker build -f /Dockerfile -t food-app-receipts:latest .
docker tag food-app-receipts:latest 120416562914.dkr.ecr.us-east-1.amazonaws.com/food-app-receipts:latest
docker push 120416562914.dkr.ecr.us-east-1.amazonaws.com/food-app-receipts:latest