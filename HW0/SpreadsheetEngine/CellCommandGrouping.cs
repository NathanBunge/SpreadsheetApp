// <copyright file="Spreadsheet.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace SpreadsheetEngine
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Text;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using System.Windows.Input;


        /// <summary>
    /// Custom ICommand Class.
    /// </summary>
    public interface ICellCommand : ICommand
    {
        /// <summary>
        /// Execute the command.
        /// </summary>
        void Execute();

        /// <summary>
        /// Undo the command.
        /// </summary>
        void Undo();

    }

    /// <summary>
    /// List of cell commands.
    /// </summary>
    public class CellCommandGrouping : ICellCommand
    {
        private readonly List<ICellCommand> commands;
        public CellCommandGrouping()
        {
            this.commands = new List<ICellCommand>();
        }

        public event EventHandler CanExecuteChanged;

        public void AddCommand(ICellCommand newCommand)
        {
            this.commands.Add(newCommand);
        }
        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public Type GetType()
        {
            ICellCommand temp = this.commands.First();
            return temp.GetType();
        }

        public void Execute()
        {
            foreach (ICellCommand command in this.commands)
            {
                command.Execute();
            }
        }

        public void Execute(object parameter)
        {
            foreach (ICellCommand command in this.commands)
            {
                command.Execute();
            }
        }

        public void Undo()
        {
            foreach (ICellCommand command in this.commands)
            {
                command.Undo();
            }
        }
    }

    public class CommandStack
    {
        private readonly Stack<ICellCommand> commandStack;
        private readonly Stack<ICellCommand> undoStack;

        public CommandStack()
        {
            this.commandStack = new Stack<ICellCommand>();
            this.undoStack = new Stack<ICellCommand>();
        }

        public void ExecuteCommand(ICellCommand newCommand)
        {
            this.commandStack.Push(newCommand);
            newCommand.Execute(null);
            this.undoStack.Clear();
        }

        public void UndoCommand()
        {
            if (this.commandStack.Count > 0)
            {
                ICellCommand command = this.commandStack.Pop();
                command.Undo();
                this.undoStack.Push(command);
            }
        }

        public void RedoCommand()
        {
            if (this.undoStack.Count > 0)
            {
                ICellCommand command = this.undoStack.Pop();
                command.Execute(null);
                this.commandStack.Push(command);
            }
        }

        public void ClearAllCommands()
        {
            this.commandStack.Clear();
            this.undoStack.Clear();
        }

        public string GetUndoCommandType()
        {
            if (this.commandStack.Count > 0)
            {
                Type commandType = this.commandStack.Peek().GetType();

                if (commandType == typeof(CellCommandGrouping))
                {
                    CellCommandGrouping topCommand = (CellCommandGrouping)this.commandStack.Peek();

                    commandType = topCommand.GetType();
                }

                if (commandType == typeof(CellTextChangeCommand))
                {
                    return "Cell Text Changed";
                }
                else if (commandType == typeof(CellColorChangeCommand))
                {
                    return "Cell Color Changed";
                }
                // add more command types here as needed
            }
            return "No Command";
        }

        public string GetRedoCommandType()
        {
            if (this.undoStack.Count > 0)
            {
                Type commandType = this.undoStack.Peek().GetType();

                if (commandType == typeof(CellCommandGrouping))
                {
                    CellCommandGrouping topCommand = (CellCommandGrouping)this.undoStack.Peek();

                    commandType = topCommand.GetType();
                }

                if (commandType == typeof(CellTextChangeCommand))
                {
                    return "Cell Text Changed";
                }
                else if (commandType == typeof(CellColorChangeCommand))
                {
                    return "Cell Color Changed";
                }
                // add more command types here as needed
            }
            return "No Command";
        }

    }

    public class CellTextChangeCommand : ICellCommand
    {
        private Cell cell;
        private string previousText;
        private string newText;

        public CellTextChangeCommand(Cell cell, string newText)
        {
            this.cell = cell;
            this.previousText = cell.Text;
            this.newText = newText;
        }

        // required for icommand
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            this.cell.Text = this.newText;
        }

        public void Execute(object parameter)
        {
            this.cell.Text = this.newText;
        }

        public void Undo()
        {
            this.cell.Text = this.previousText;
        }
    }

    public class CellColorChangeCommand : ICellCommand
    {
        private Cell cell;
        private uint previousBGColor;
        private uint newBGColor;

        public CellColorChangeCommand(Cell cell, uint newBGColor)
        {
            this.cell = cell;
            this.previousBGColor = cell.BGColor;
            this.newBGColor = newBGColor;
        }

        // required for ICommand
        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute()
        {
            this.cell.BGColor = this.newBGColor;
        }

        public void Execute(object parameter)
        {
            this.cell.BGColor = this.newBGColor;
        }

        public void Undo()
        {
            this.cell.BGColor = this.previousBGColor;
        }
    }
}
