resource "aws_sqs_queue" "dlq" {
  name                      = var.dlq_name
  max_message_size          = 2048
  message_retention_seconds = 1209600 # 14 days
  receive_wait_time_seconds = 20
  kms_master_key_id         = module.kms_key.id

  tags = local.aws_resource_tags
}
