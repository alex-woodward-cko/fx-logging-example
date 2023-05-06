locals {
  dd_slack_notification_user          = "${var.datadog_slack_user_prefix}${var.aws_environment}"
  dd_slack_notification_channel       = "${var.datadog_slack_channel_prefix}${var.aws_environment}"
  dd_slack_notification_multi_channel = "${var.datadog_slack_user_prefix}{{environment}}-${var.datadog_slack_channel_prefix}{{environment}}"
  datadog_tags                        = ["team:${var.team}", "env:${var.aws_environment}", "product:treasury and fx", "product-group:Finance and Treasury Services"]
}
