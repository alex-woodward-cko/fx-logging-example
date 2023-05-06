terraform {
  required_version = ">= 1.0.8"
  backend "s3" {}
  required_providers {
    aws = "~> 3.75.1"
    octopusdeploy = {
      version = "~> 0.7.59"
      source  = "octopusdeploylabs/octopusdeploy"
    }
  }
}

provider "aws" {
  region  = var.aws_region
  assume_role {
    role_arn     = var.mgmt_role_arn
    session_name = "terraform"
  }
}

provider "octopusdeploy" {
  address  = var.octopus_server_url
  api_key  = jsondecode(data.aws_secretsmanager_secret_version.octopus_api_key_value.secret_string)["api_key"]
  space_id = var.octopus_space
}
