#====================================
# KMS Data
#====================================

data "aws_kms_key" "key" {
  key_id = "alias/${var.kms_alias}"
}

#====================================
# Secrets Manager Data
#====================================

data "aws_secretsmanager_secret" "secretsmanager" {
  name = var.secrets_manager_name
}

#====================================
# Policy Data
#====================================

data "template_file" "policy" {
  template = file("./policies/fx-logging-example.json")

  vars = {
    secrets_arn = data.aws_secretsmanager_secret.secretsmanager.arn
    kms_arn     = data.aws_kms_key.key.arn
    dlq_arn     = data.aws_sqs_queue.dlq.arn
  }
}
