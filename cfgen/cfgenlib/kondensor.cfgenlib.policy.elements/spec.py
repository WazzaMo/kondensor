
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
  print("GetLn: %s  fetched: %s" % (line,fetch))
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
    if len(line) == 0:
      return None
    else:
      print("GetWord: %s" % line)
      mm = re.search("([\w \-\*]+)", line)
      if mm == None:
        ungetLine(line)
        return None
      else:
        return mm.group(1)
  except Exception as e:
    return None

def getList():
  list = []
  finished = False
  while not finished:
    line = getLine()
    if line == None:
      line = getLine()
    if line != None and line.find("\t") > -1:
      entry = getWord(line)
      list.append(entry)
    else:
      ungetLine(line)
      finished = True
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
  tab = line.find("\t")
  if tab > 1:
    parts = line.split("\t")
    if len(parts) >= 3:
      ProcessDecl(line)
  else:
    parts = line.split("\t")
    print("Small line:", parts)
      # [action,description,access] = parts
      # ProcessDecl(action, description, access)

def ReadInput():
  x = ""
  more = True
  while more:
    try:
      line = getLine() #input()
      if len(line) > 0:
        FindContent(line)
    except Exception as e:
      print(e)
      more = False

ReadInput()

