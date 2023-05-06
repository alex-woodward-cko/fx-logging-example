variable "aws_environment" {
  description = "The environment the infrastructure is deployed in e.g. prod"
}

variable "aws_region" {
  description = "The AWS region the infrastructure is deployed in e.g. eu-west-1"
}

variable "role_arn" {
  description = "Role based authentication for provider between accounts"
}

variable "mgmt_role_arn" {
  description = "Role to assume in the MGMT account"
}

variable "team" {
  description = "The name of the team"
}

# these will be replaced once we switch to automated pagerduty alerts
variable "datadog_slack_user_prefix" {
  description = "Slack user prefix to notify on Datadog Monitor alerts. Environment is added to this variable as a suffix"
}

variable "datadog_slack_channel_prefix" {
  description = "Slack channel to notify on Datadog Monitor alerts. Environment is added to this variable as a suffix"
  default     = "fx-alerts-"
}

variable "datadog_full_name" {
  description = "The full name of the app as used by datadog"
}

variable "datadog_display_name" {
  description = "The display (short) name of the app as used by datadog"
}
