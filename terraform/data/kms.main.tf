module "kms_key" {
  source              = "github.com/cko-fx-payouts/aws-core-modules//kms?ref=v5.0.1"
  aws_account_id      = var.aws_account_id
  aws_resource_tags   = local.aws_resource_tags
  aws_environment     = var.aws_environment
  kms_name            = var.kms_alias
  kms_key_description = "Used to secure the fx-logging-example"
}
