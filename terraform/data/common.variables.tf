variable "aws_account_id" {
  description = "The account id that this resource is created with"
}

variable "aws_resource_tags" {
  description = "Tags that apply to all aws resources"
}

variable "aws_environment" {
  description = "The environment the infrastructure is deployed in e.g. prod"
}

variable "aws_region" {
  description = "The AWS region the infrastructure is deployed in e.g. eu-west-1"
}

variable "role_arn" {
  description = "Role based authentication for provider between accounts"
}
