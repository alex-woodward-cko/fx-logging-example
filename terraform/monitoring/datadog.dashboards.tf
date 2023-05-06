resource "datadog_dashboard_json" "dashboard" {
  dashboard = templatefile("dashboards/fx-logging-example.json", {
    aws_environment = var.aws_environment
  })
}
