module "octopus_deployment_fx_s3_rate_file_processor" {
  source = "github.com/cko-fx-payouts/octopus-lambda-deployment?ref=v1.0.0"
  project = {
    name                    = "fx-logging-example"
    description             = "fx-logging-example Lambda Deploys Pipeline"
    lifecycle_name          = "Default Lifecycle"
    group_name              = "FX"
    application_name        = "Checkout.FX.LoggingExample"
    function                = var.lambda_name
    function_name           = var.lambda_name
    bucket                  = "ckotech-fxp-fx-lambda-repository"
    package_directory       = "fx-logging-example"
    connected_variable_sets = ["AWS", "Common"]
  }

  slack = {
    notification_channel = "fx-releases"
    hook_url_variable    = "Slack.FX.WebHook"
    username             = "Octopus Deploy"
  }
}
