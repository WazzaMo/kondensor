#!/usr/bin/env bash
#

echo "Update license text"

SEARCH="Distributed under the Kondensor License."
REPLACE_LGPL="Distributed without warranty, under the GNU Lesser Public License v 3.0 or later."
REPLACE_GPL="Distributed without warranty, under the GNU Public License v3.0 or later."

FILES="*.cs"

for csf in ${FILES}; do
	#ls -l "$csf"
  # -- LGPL Library
	cat "$csf" | sed -e "s/${SEARCH}/${REPLACE_LGPL}/g" > "/tmp/${csf}"

  # -- GPL App code
	# cat "$csf" | sed -e "s/${SEARCH}/${REPLACE_GPL}/g" > "/tmp/${csf}"
	rm $csf
	mv "/tmp/${csf}" .
done

