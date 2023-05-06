data "aws_vpc" "this" {
  id = var.aws_vpc_id
}

data "aws_subnet_ids" "private_subnets" {
  vpc_id = var.aws_vpc_id

  filter {
    name   = "cidr-block"
    values = ["${var.cidr_base}101.0/24", "${var.cidr_base}102.0/24", "${var.cidr_base}103.0/24"]
  }
}

data "aws_sqs_queue" "dlq" {
  name = var.dlq_name
}
