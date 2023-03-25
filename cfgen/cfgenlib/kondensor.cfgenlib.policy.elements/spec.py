
#
# (c) Copyright 2022, 2023 Kondensor Contributors
# Written by Warwick Molloy.
# Distributed under the Kondensor License.
#

import re

## Convert spec file into C# files.

context = {
  "line_buffer": None,
  "end_of_file": False
}

def getLine():
  line_buffer = context["line_buffer"]
  end_of_file = context["end_of_file"]
  fetch = False
  if line_buffer == None and not end_of_file:
    try:
      line = input()
      fetch = True
    except Exception as e:
      end_of_file = True
      line = None
      print("Ex: ", e)
  else:
    line = line_buffer
    fetch = False
    line_buffer = None
  context["end_of_file"] = end_of_file
  context["line_buffer"] = line_buffer
  return line

def ungetLine(line):
  context["line_buffer"] = line

def is_Eof():
  end_of_file = context["end_of_file"]
  return end_of_file

def getWord(line):
  try:
    if line != None and len(line) == 0:
      return None
    else:
      mm = re.search("([:\w \-\*]+)", line)
      if mm == None:
        ungetLine(line)
        return None
      else:
        return mm.group(1)
  except Exception as e:
    return None

def isDeclarationLine(line):
  if line == None:
    return False
  else:
    tab = line.find("\t")
    return (tab > 1)

def getList():
  list = []
  finished = False
  while not finished and not is_Eof():
    line = getLine()
    if isDeclarationLine(line):
      ungetLine(line)
      finished = True
    else:
      entry = getWord(line)
      if entry != None:
        list.append(entry)
      
  return list


def ProcessDecl(line):
  #Actions	->|Description	->|Access level	->|Resource types (*required)	->|Condition keys	->|Dependent actions
  mm = re.search("^(\w+)\s([\w \-]+)[\t]?([\w \-]+)[\t]?([\w \-]*)[\t]?", line)
  if mm != None:
    action = mm.group(1)
    description = mm.group(2)
    access = mm.group(3)
    nextLine = getLine()
    resource_types = getWord(nextLine)
    conditions = getList()
    print("Action: %s" % action)
    print("Desc: %s" % description)
    print("Access: %s" % access)
    print("Res Types: %s" % resource_types)
    print("Conditions: ", conditions)


def FindContent(line):
  if isDeclarationLine(line):
    ProcessDecl(line)
  else:
    raise Exception("Did not handle line:" + line)

def ReadInput():
  x = ""
  while not is_Eof():
    line = getLine()
    if line != None and len(line) > 0:
      FindContent(line)


ReadInput()

