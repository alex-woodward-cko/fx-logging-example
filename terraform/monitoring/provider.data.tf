

data "aws_secretsmanager_secret" "datadog_api_key" {
  name = "cko-${var.aws_environment}/ApiKey/DataDog"
}

data "aws_secretsmanager_secret_version" "datadog_api_key_value" {
  secret_id = data.aws_secretsmanager_secret.datadog_api_key.id
}

data "aws_secretsmanager_secret" "datadog_app_key" {
  provider = aws.mgmt
  name     = "fxp/datadog"
}

data "aws_secretsmanager_secret_version" "datadog_app_key_value" {
  provider  = aws.mgmt
  secret_id = data.aws_secretsmanager_secret.datadog_app_key.id
}
