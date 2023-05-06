#!/bin/sh

if [[ $(uname -m) == 'arm64' ]];
then
  dotnet-lambda package \
      -farch arm64 \
      -pl "./../../src/Checkout.FX.LoggingExample.Host.Lambda" \
      --output-package "Checkout.FX.LoggingExample.Host.Lambda.zip" \
      --configuration Debug
else
  dotnet-lambda package \
      -pl "./../../src/Checkout.FX.LoggingExample.Host.Lambda" \
      --output-package "Checkout.FX.LoggingExample.Host.Lambda.zip" \
      --configuration Debug
fi

aws lambda update-function-code \
    --function-name "fx-logging-example" \
    --zip-file "fileb://Checkout.FX.LoggingExample.Host.Lambda.zip" \
    --endpoint-url "http://localhost:4584"