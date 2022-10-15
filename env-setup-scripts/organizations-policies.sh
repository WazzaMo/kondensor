#/bin/bash

# (c) Copyright 2022 Warwick Molloy, Kondensor
# Published under GPLv3. See license.md

echo "Listing all organization (sub account) service control policies"

aws organizations list-policies --filter SERVICE_CONTROL_POLICY --output text
