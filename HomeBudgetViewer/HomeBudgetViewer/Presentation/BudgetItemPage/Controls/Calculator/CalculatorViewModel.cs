using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight.Command;
using HomeBudgetViewer.Models.Enum;

namespace HomeBudgetViewer.Presentation.BudgetItemPage.Controls.Calculator
{
    public class CalculatorViewModel : AppViewModelBase
    {
        private AppViewModelBase _parentViewModel;
        private RelayCommand _clearOutputCommand;
        private RelayCommand<object> _handleNumberCommand;
        private RelayCommand<object> _handleArithmeticalOperationCommand;
        private RelayCommand _calculateResultCommand;
        private RelayCommand _appendCommaCommand;
        private string _currentArithmeticalNumber;
        private string _memoryArithmeticalNumber;
        private string _arithmeticalSignText;
        private ArithmeticOperation _lastOperation;

        public CalculatorViewModel(AppViewModelBase parentViewModel)
        {
            _parentViewModel = parentViewModel;
            _lastOperation = ArithmeticOperation.None;
        }

        public string CurrentArithmeticalNumber
        {
            get { return _currentArithmeticalNumber; }
            set
            {
                if (_currentArithmeticalNumber == value)
                    return;
                _currentArithmeticalNumber = value;
                this.RaisePropertyChanged();
            }
        }

        public string ArithmeticalSignText
        {
            get { return _arithmeticalSignText; }
            set
            {
                if (_arithmeticalSignText == value)
                    return;
                _arithmeticalSignText = value;
                this.RaisePropertyChanged();
            }
        }

        public string MemoryArithmeticalNumber
        {
            get { return _memoryArithmeticalNumber; }
            set
            {
                if (_memoryArithmeticalNumber == value)
                    return;
                _memoryArithmeticalNumber = value;
                this.RaisePropertyChanged();
            }
        }


        public RelayCommand ClearOutputCommand
        {
            get
            {
                return _clearOutputCommand ?? (_clearOutputCommand = new RelayCommand(() =>
                {
                    this.CurrentArithmeticalNumber = string.Empty;
                    this.MemoryArithmeticalNumber = string.Empty;
                    this.ArithmeticalSignText = string.Empty;
                    this._lastOperation = ArithmeticOperation.None;
                }));
            }
        }

        public RelayCommand<object> HandleNumberCommand
        {
            get
            {
                return _handleNumberCommand ?? (_handleNumberCommand = new RelayCommand<object>(param =>
                {
                    if (param != null && (param as string) != null )
                    {
                        var number = (string) param;
                        if (_lastOperation == ArithmeticOperation.Result)
                        {
                            this.CurrentArithmeticalNumber = string.Empty;
                            this._lastOperation = ArithmeticOperation.None;
                        }
                        CurrentArithmeticalNumber += number;
                    }
                }));
            }
        }

        public RelayCommand<object> HandleArithmeticalOperationCommand
        {
            get
            {
                return
                    _handleArithmeticalOperationCommand ?? (_handleArithmeticalOperationCommand = new RelayCommand<object>(
                        param =>
                        {
                            if (param != null)
                            {
                                if (this._lastOperation != ArithmeticOperation.None ||
                                    this._lastOperation != ArithmeticOperation.Result)
                                {
                                    this.CalculateResult();
                                }

                                var operation = (ArithmeticOperation)param;
                                this._lastOperation = operation;
                                this.MemoryArithmeticalNumber = this.CurrentArithmeticalNumber;
                                switch (operation)
                                {
                                    case ArithmeticOperation.Addition:
                                        this.ArithmeticalSignText = "+";
                                        break;
                                    case ArithmeticOperation.Substraction:
                                        this.ArithmeticalSignText = "-";
                                        break;
                                    case ArithmeticOperation.Multiplication:
                                        this.ArithmeticalSignText = "*";
                                        break;
                                    case ArithmeticOperation.Division:
                                        this.ArithmeticalSignText = "/";
                                        break;
                                }
                                this.CurrentArithmeticalNumber = string.Empty;
                            }
                        }));
            }
        }

        public RelayCommand CalculateResultCommand
        {
            get { return _calculateResultCommand ?? (_calculateResultCommand = new RelayCommand(CalculateResult)); }
        }

        private void CalculateResult()
        {
            switch (this._lastOperation)
            {
                case ArithmeticOperation.None:
                case ArithmeticOperation.Result:
                    return;
            }
            if (string.IsNullOrEmpty(this.CurrentArithmeticalNumber))
                this.CurrentArithmeticalNumber = "0";

            double currentNumber = double.Parse(this.CurrentArithmeticalNumber);
            double memoryNumber = double.Parse(this.MemoryArithmeticalNumber);
            switch (this._lastOperation)
            {
                case ArithmeticOperation.Addition:
                    this.CurrentArithmeticalNumber = (memoryNumber + currentNumber).ToString();
                    break;
                case ArithmeticOperation.Substraction:
                    this.CurrentArithmeticalNumber = (memoryNumber - currentNumber).ToString();
                    break;
                case ArithmeticOperation.Multiplication:
                    this.CurrentArithmeticalNumber = (memoryNumber * currentNumber).ToString();
                    break;
                case ArithmeticOperation.Division:
                    this.CurrentArithmeticalNumber = (memoryNumber / currentNumber).ToString();
                    break;
            }
            this._lastOperation = ArithmeticOperation.Result;
            this.MemoryArithmeticalNumber = string.Empty;
            this.ArithmeticalSignText = string.Empty;
        }

        public RelayCommand AppendCommaCommand
        {
            get
            {
                return _appendCommaCommand ?? (_appendCommaCommand = new RelayCommand(() =>
                {
                    if (this._lastOperation == ArithmeticOperation.Result)
                    {
                        this.CurrentArithmeticalNumber = string.Empty;
                        this._lastOperation = ArithmeticOperation.None;
                    }
                    if (this.CurrentArithmeticalNumber.Contains(',') || string.IsNullOrEmpty(this.CurrentArithmeticalNumber))
                        return;

                    this.CurrentArithmeticalNumber += ",";
                }));
            }
        }

        private RelayCommand _undoLastNumberCommand;
        public RelayCommand UndoLastNumberCommand
        {
            get
            {
                return _undoLastNumberCommand ?? (_undoLastNumberCommand = new RelayCommand(() =>
                {
                    if (string.IsNullOrEmpty(this.CurrentArithmeticalNumber))
                        return;
                    if (this.CurrentArithmeticalNumber.Length == 1)
                        this.CurrentArithmeticalNumber = "0";
                    else
                    {
                        this.CurrentArithmeticalNumber = this.CurrentArithmeticalNumber.Remove(this.CurrentArithmeticalNumber.Length - 1);
                    }
                }));
            }
        }

    }
}
