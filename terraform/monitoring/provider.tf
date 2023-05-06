

terraform {
  backend "s3" {}
  required_version = ">= 1.0.8"
  required_providers {
    aws = "~> 3.75.1"
    datadog = {
      version = "~> 3.4.0"
      source  = "DataDog/datadog"
    }
  }
}

provider "aws" {
  region = var.aws_region
  assume_role {
    role_arn     = var.role_arn
    session_name = "terraform"
  }
}

provider "aws" {
  alias  = "mgmt"
  region = var.aws_region

  assume_role {
    role_arn     = var.mgmt_role_arn
    session_name = "terraform"
  }
}

provider "datadog" {
  api_key = data.aws_secretsmanager_secret_version.datadog_api_key_value.secret_string
  app_key = data.aws_secretsmanager_secret_version.datadog_app_key_value.secret_string
}
