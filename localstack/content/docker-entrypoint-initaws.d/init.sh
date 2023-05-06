set -x #required for seeding through volumes

source /setup/scripts/init.secrets.sh
source /setup/scripts/init.lambda.sh

{ echo "Localstack seeding completed"; exit 0; }