module "lambda_log_forwarder" {
  source                       = "github.com/cko-core-terraform/terraform-aws-datadog-forwarder.git?ref=4.0.0"
  lambda_function_name         = "fx-logging-example" # '-datadog-forwarder' is appended to the name by cko-core-terraform module
  tags_lambda_log_group        = merge(local.aws_resource_tags, { "Name" : "fx-logging-example-datadog-forwarder-log-group" })
  dd_ignore_aws_generated_logs = "true"

  tags_lambda = {
    "Name" : "fx-cloudwatch-datadog-forwarder-log-group",
    "env" : var.aws_environment
  }
}
