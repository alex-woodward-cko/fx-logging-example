module "dlq_number_of_messages" {
  count = var.aws_environment != "dev" ? 1 : 0

  source               = "github.com/cko-core-terraform/terraform-datadog-monitor?ref=3.0.0"
  dd_monitor_name      = "fx-logging-example DLQ Messages"
  app_full_name        = var.datadog_full_name
  app_display_name     = var.datadog_display_name
  team_prefix          = var.team
  type                 = "metric alert"
  sop_code                   = "<TODO>"
  sop_link                   = "<TODO>"
  environment          = var.aws_environment
  friendly_message     = "fx-logging-example has place a message on the DLQ"
  notification_channel = [local.dd_slack_notification_multi_channel]
  dd_tags              = local.datadog_tags

  thresholds = {
    critical = 2
    warning  = 1
  }

  query = "min(last_1m):sum:aws.sqs.approximate_number_of_messages_visible{queuename:fx-logging-example-dlq,environment:${var.aws_environment}} >= 2"
}
