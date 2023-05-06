variable "cidr_base" {
  description = "16 first bits of the IPv4 VPC range, see https://checkout.atlassian.net/wiki/spaces/DevOps/pages/411075192/Amazon+Web+Services+-+CKO"
}

variable "aws_vpc_id" {
  description = "The ID of the VPC where the infrastructure will be created"
}

variable "lambda_name" {
  description = "The name of the Lambda"
}
