# --------------------------------------------------------------------------------------------------------------------
# Global vars
# --------------------------------------------------------------------------------------------------------------------
team = "fx"
aws_resource_tags = {
  "Team"          = "fx"
  "Product-Group" = "Finance and Treasury Services"
  "Product"       = "treasury and fx"
}
aws_region    = "eu-west-1"
mgmt_role_arn = "arn:aws:iam::791259062566:role/spacelift"

octopus_server_url = "https://fxp-octopus.mgmt.ckotech.co/"
octopus_space      = "Spaces-1"

#====================================
# Lambdas
#====================================
lambda_name = "fx-logging-example"

#====================================
# KMS keys
#====================================
kms_alias = "fx-logging-example"

#====================================
# Secrets manager
#====================================
secrets_manager_name = "fx-logging-example"

#====================================
# Dead letter queue
#====================================
dlq_name = "fx-logging-example-dlq"

#====================================
# Datadog settings
#====================================
datadog_slack_user_prefix = "slack-fxp_alerts_"

datadog_slack_channel_prefix = "fx-alerts-"

datadog_full_name    = "Checkout.FX.LoggingExample"
datadog_display_name = "fx-logging-example"
