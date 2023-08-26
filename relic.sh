#!/usr/bin/env bash
#

echo "Update license text"

SEARCH="Distributed under the Kondensor License."
REPLACE_LGPL="Distributed without warranty, under the GNU Lesser Public License v 3.0"
REPLACE_GPL="Distributed without warranty, under the GNU Public License v3.0"

FILES="*.cs"

for csf in ${FILES}; do
	#ls -l "$csf"
	cat "$csf" | sed -e "s/${SEARCH}/${REPLACE_LGPL}/g" > "/tmp/${csf}"
	rm $csf
	mv "/tmp/${csf}" .
done

