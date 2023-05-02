Here's the corrected text:

# Spreadsheet Application

## Purpose:
Building an Excel-like spreadsheet application using GRASP design principles. Practice building on and adding to an application over time while keeping up with good design patterns.

## Features:
### Cell Data
**Text:** Users can enter text into a cell which will be saved when they press ENTER. 

**Values:** Numerical values are also stored for the cell. If the text starts with "=", then the value of the cell will be displayed.

**Color:** Background color of each cell can be changed.

### Cell Referencing
Cells that start with "=" and have cell names inside the text string (i.e. A2 or C8) will have their value set to the referenced cell's value.

**Events Updating:** If a cell changes, all the cells that are referencing that cell will also update their values. This is done by subscribing to the cell's property changed event.

**Circular Reference Checking:** Cells that reference themselves or reference a cycle of cells will display an error text message to avoid circular references.

### Cell Equations
**Expression Tree:** For cell texts that have "=" and also include operators such as addition or subtraction, an expression tree is built to calculate the value for those cells.

**Prefix/Postfix Conversion:** Text strings that need expression trees are first converted to prefix notation to ensure proper order of operations in the expression tree.

### Undo/Redo
All text and color changes by the users are saved as commands. This allows the users to undo or redo a command simply by calling the top command on the command stack.

### Saving to XML
Users can click a button that saves all the cell data to an XML file, which can then be loaded to keep progress.

## GRASP patterns
### Information Expert
### Creator
### Controller 
### Polymorphism
### Protected Variations
### Operator Factory

## Unit Testing
