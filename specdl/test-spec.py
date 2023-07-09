#!/usr/bin/python3

import yaml
# import load, dump

# try:
#   from yaml import CLoader as Loader, CDumper as Dumper
# except ImportError:

# test_file = 'actions.yaml'
test_file = 'n-actions.yaml'

stream = open(test_file,'rt')

text = stream.read()


data = yaml.load(text,Loader=yaml.Loader)

print( yaml.dump(data) )