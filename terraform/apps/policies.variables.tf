variable "secrets_manager_name" {
  description = "The name of the Secrets Manager securing fx-logging-example"
}

variable "kms_alias" {
  description = "The alias of the KMS Key used to secure the fx-logging-example"
}
