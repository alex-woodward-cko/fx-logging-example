module "secrets_manager" {
  source                         = "github.com/cko-fx-payouts/aws-core-modules//secrets-manager?ref=v8.1.0"
  aws_resource_tags              = local.aws_resource_tags
  secret_name                    = var.secrets_manager_name
  secret_description             = "Used to secure fx-logging-example secrets"
  kms_key_id                     = module.kms_key.arn
  secret_recovery_window_in_days = var.secret_recovery_window
  include_empty_json             = true
}
