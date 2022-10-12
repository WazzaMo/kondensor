# UNIX philosophy and clean code

Like 'em or hate 'em, Robert Martin
and the original authors of UNIX
Ken Thompson, Dennis Ritchie, Doug McIlroy have expressed some useful
ideas about software and systems
design that are worth investigating
for use in this project.

## Reasoning

UNIX tools do one thing well and this
 can be likened to Bob Martin's
 Single Responsibility Principle.
These two things are different in scale
 being that Clean Code is about software
design and coding conventions, while
the UNIX philosophy is for systems
design.



### UNIX philosophy

The philosophy is summarised by
Eric S Raymond, of the Open Source Initiative and author of "The Cathedral and the Bazaar," in (2) and (3) and
he makes reference to McIlroy, who
pushed the idea of UNIX pipes & pipelines. Dennis Ritchie also mentions
Doug McIlroy's concepts of pipelines in (5).
According to (4), McIlroy and Pinson
summarised the UNIX characteristics as:

1. Make each program do one thing well. To do a new job, build afresh rather than
complicate old programs
by adding new "features"
2. Expect the output of every program
to become he inut to another, as yet
unknown, program. Don't clutter output
with extraneous information. Avoid
stringently coluymnar or binary input formats. Don't insist on interactive
input.
3. Design and uild software, even
operating systems, to be
tried early, ideally within weeks. Don't
hesitate to throw
away the clumsy parts and rebuild them.
4. Use tools in preference to unskilled
help to lighten a programming task,
even if you have to detour to build
the tools and expect to throw
some of them out after you've finished
using them.


### Clean code

Wojtek Lukaszuk summarised points
from Robert Martin's book in (6) about
how to keep source code simple and small
and the signs, or smells, of complexity.
Separate to that, some of the most famous
points of Clean Code are the 5 principles
with the acronym S.O.L.I.D:

- Single responsibility principle - module has one reason for change; it does one thing
- Open-closed principle - Open for extension but closed for modification.
- Liskov substitution principle
- Interface segregation principle
- Dependency inversion principle

These ideas are about software
design and coding conventions.

# References

[1 - Robert Martin, Uncle Bob Martin](http://cleancoder.com/products)

[2 - Eric S Raymond, Basics of UNIX philosophy](http://www.catb.org/~esr/writings/taoup/html/ch01s06.html)

[3 - Eric S Raymond, What UNIX gets right](http://www.catb.org/~esr/writings/taoup/html/ch01s05.html)

[4 - McIlroy, M.d. and Pinson, E.N., UNIX Time-sharing System](https://archive.org/details/bstj57-6-1899/page/n3/mode/1up)

[5 - Ritchie, Dennis M, The Evolution of the Unix Time-sharing System](http://webarchive.loc.gov/all/20100506231949/http://cm.bell-labs.com/cm/cs/who/dmr/hist.html)

[6 - Wojtek, Lukaszuk, Summary of 
'Clean Code' by Robert Martin](https://gist.github.com/wojteklu/73c6914cc446146b8b533c0988cf8d29)

