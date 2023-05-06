# the lambda
module "fx-logging-example" {
  source                         = "github.com/cko-fx-payouts/terraform-processing-lambda?ref=v2.1.0"
  lambda_function_name           = var.lambda_name
  lambda_description             = "fx-logging-example"
  lambda_handler                 = "Checkout.FX.LoggingExample.Host.Lambda::Checkout.FX.LoggingExample.Host.Lambda.Function::HandleAsync"
  lambda_runtime                 = "dotnet6"
  lambda_memory_size             = 512
  lambda_timeout                 = 600
  reserved_concurrent_executions = 1
  iam_role_name                  = "fx-${var.aws_environment}-fx-logging-example-lambda-role"
  iam_role_path                  = "/fx/${var.aws_environment}/"
  iam_policy_name                = "fx-logging-example-policy"
  iam_policy_description         = "fx-logging-example policy"
  iam_policy                     = data.template_file.policy.rendered
  private_subnet_ids             = data.aws_subnet_ids.private_subnets.ids
  log_group_name                 = "/aws/lambda/fx-logging-example"
  log_group_retention_in_days    = 30
  log_forwarder_lambda_arn       = module.lambda_log_forwarder.datadog_forwarder_lambda_arn
  security_group_name            = "fx-logging-example-sg"
  aws_vpc_id                     = var.aws_vpc_id
  aws_environment                = var.aws_environment
  aws_resource_tags              = local.aws_resource_tags
  aws_region                     = var.aws_region
  lambda_dlq_arn                 = data.aws_sqs_queue.dlq.arn
  lambda_insights_arn            = local.lambda_insights_arn
}
