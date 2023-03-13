
#
# (c) Copyright 2022, 2023 Kondensor Contributors
# Written by Warwick Molloy.
# Distributed under the Kondensor License.
#

## Convert spec file into C# files.

def ProcessDecl(action, description, access):
  print("Action: %s; Desc: %s; Access: %s" %(action, description, access))


def FindActionAndDescription(line):
  tab = line.find("\t")
  if tab > 1:
    parts = line.split("\t")
    if len(parts) >= 3:
      [action,description,access] = parts
      ProcessDecl(action, description, access)

def ReadInput():
  x = ""
  more = True
  while more:
    try:
      line = input()
      if len(line) > 0:
        FindActionAndDescription(line)
    except:
      more = False

ReadInput()

