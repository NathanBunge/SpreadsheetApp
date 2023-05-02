# Spreadsheet Application

## Purpose:
Building a Excel like spreadsheet application using GRASP design prniciples. Practice building on and adding to an application over time, while keeping up with good design patterns.

## Features:
### Cell Data
***Text:*** users can enter text into a cell which will be saved when they press ENTER. 

**Values:** Numerical Values are also stored for the cell. If the text starts with "=", then the value of the cell will be displayed.

**Color:** Background color of each cell can be changed

### Cell referencing
Cells that start with "=" and have cell names inside the text string (i.e. A2 or C8) will have their value set to the referenced cell's value.

**Events Updating:** If a cell changes, all the cells that are referencing that cell will also update their values. This is done by subscribing to the cell's proterty changed event.

**Circular reference checking:** Cells that reference themselves or reference a cycle of cells will display an error text message to avoid circular references.


### Exspression Tree
For cell texts that have "=" and also include oporators such ad addition or subtraction, an exspression tree is built to calculate the value for those cells.
#### Prefix/Postfix conversion
Text strings that need exspression trees are first converted to prefix notation to ensure proper order of oporations in the expsression tree.

### Undo/Redo
All text anc color changes by the users are saved as commands. This allows the users to under or redo a command simply by calling the top command on the command stack.

### Saving to XML
#### Cell equations
#### Cell colors

## GRASP patterns
### Information exspert
### Creator
### Controllor 
### Polymorphism
### Protected variations
Oporator factory

## Unit Testing
