locals {
  aws_resource_tags = merge(var.aws_resource_tags, { "Environment" = var.aws_environment })
}
