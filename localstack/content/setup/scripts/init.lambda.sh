#!/bin/sh

awslocal lambda create-function \
    --region eu-north-1 \
    --function-name fx-logging-example \
    --runtime dotnet6 \
    --handler Checkout.FX.LoggingExample.Host.Lambda::Checkout.FX.LoggingExample.Host.Lambda.Function::HandleAsync \
    --memory-size 512 \
    --zip-file fileb:///setup/data/lambda/lambda-initial-setup.zip \
    --role arn:aws:iam::123456:role/irrelevant \
    --environment "Variables={DOTNET_ENVIRONMENT=localdocker}" \
    --timeout 900 # Default is 3 seconds, we want more time