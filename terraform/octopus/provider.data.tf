data aws_secretsmanager_secret octopus_api_key {
  name = "fxp/octopus"
}

data aws_secretsmanager_secret_version octopus_api_key_value {
  secret_id = data.aws_secretsmanager_secret.octopus_api_key.id
}
