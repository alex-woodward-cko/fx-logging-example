terraform {
  backend "s3" {}
  required_version = ">= 1.0.8"
  required_providers {
    aws = "~> 3.75.1"
  }
}

provider "aws" {
  region = var.aws_region
  assume_role {
    role_arn     = var.role_arn
    session_name = "terraform"
  }
}
