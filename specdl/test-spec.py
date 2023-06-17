#!/usr/bin/python3

import yaml
# import load, dump

# try:
#   from yaml import CLoader as Loader, CDumper as Dumper
# except ImportError:

stream = open('actions.yaml','rt')

text = stream.read()


data = yaml.load(text,Loader=yaml.Loader)

print( yaml.dump(data) )