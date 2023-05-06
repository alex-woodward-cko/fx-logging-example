module "datadog_monitor_log_exceptions" {
  count = var.aws_environment != "dev" ? 1 : 0

  source                     = "github.com/cko-core-terraform/terraform-datadog-monitor?ref=2.1.0"
  dd_monitor_name            = "Unexpected Exceptions"
  app_full_name              = var.datadog_full_name
  app_display_name           = var.datadog_display_name
  team_prefix                = var.team
  type                       = "log alert"
  sop_code                   = "<TODO>"
  sop_link                   = "<TODO>"
  environment                = var.aws_environment
  friendly_message           = "fx-logging-example has logged a number of exceptions, this may indicate a problem"
  slack_notification_user    = local.dd_slack_notification_user
  slack_notification_channel = local.dd_slack_notification_channel
  dd_tags                    = concat(["fx-logging-example"], local.datadog_tags)

  thresholds = {
    critical = 0
  }

  query = "logs(\"@Properties.Environment:${var.aws_environment} AND service:fx-logging-example AND status:(error OR critical OR emergency)\").index(\"*\").rollup(\"count\").last(\"10m\") > 0"
}

module "datadog_monitor_log_warnings" {
  count = var.aws_environment != "dev" ? 1 : 0

  source                     = "github.com/cko-core-terraform/terraform-datadog-monitor?ref=2.1.0"
  dd_monitor_name            = "Unexpected Warnings"
  app_full_name              = var.datadog_full_name
  app_display_name           = var.datadog_display_name
  team_prefix                = var.team
  type                       = "log alert"
  sop_code                   = "<TODO>"
  sop_link                   = "<TODO>"
  environment                = var.aws_environment
  friendly_message           = "fx-logging-example has logged a number of warnings, this may indicate a problem"
  slack_notification_user    = local.dd_slack_notification_user
  slack_notification_channel = local.dd_slack_notification_channel
  dd_tags                    = concat(["fx-logging-example"], local.datadog_tags)

  thresholds = {
    warning  = 40
    critical = 200
  }

  query = "logs(\"@Properties.Environment:${var.aws_environment} AND service:fx-logging-example AND status:(warn)\").index(\"*\").rollup(\"count\").last(\"10m\") > 200"
}
