###########################
# Secrets Manager
###########################

echo "START Secrets Manager setup"

echo "Seeding secrets"
awslocal secretsmanager create-secret --name fx-logging-example --secret-string "{ }"

echo "END Secrets Manager setup"