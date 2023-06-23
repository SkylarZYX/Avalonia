using System;

namespace Avalonia.Input.TextInput
{
    public abstract class TextInputMethodClient
    {
        private Rect _cursorRectangle;
        private string _surroundingText = "";
        private TextSelection _selection;

        /// <summary>
        /// Fires when the text view visual has changed
        /// </summary>
        public event EventHandler? TextViewVisualChanged;

        /// <summary>
        /// Fires when the cursor rectangle has changed
        /// </summary>
        public event EventHandler? CursorRectangleChanged;

        /// <summary>
        /// Fires when the surrounding text has changed
        /// </summary>
        public event EventHandler? SurroundingTextChanged;

        /// <summary>
        /// Fires when the selection has changed
        /// </summary>
        public event EventHandler? SelectionChanged;

        /// <summary>
        /// The visual that's showing the text
        /// </summary>
        public abstract Visual TextViewVisual { get; }

        /// <summary>
        /// Indicates if TextViewVisual is capable of displaying non-committed input on the cursor position
        /// </summary>
        public abstract bool SupportsPreedit { get; }

        /// <summary>
        /// Indicates if text input client is capable of providing the text around the cursor
        /// </summary>
        public abstract bool SupportsSurroundingText { get; }

        /// <summary>
        /// Returns the text around the cursor, usually the current paragraph
        /// </summary>
        public string SurroundingText
        {
            get => _surroundingText;
            set
            {
                var oldValue = _surroundingText;

                if (oldValue == value)
                {
                    return;
                }

                _surroundingText = value;

                OnSurroundingTextChanged(oldValue, value);
            }
        }

        /// <summary>
        /// Gets the cursor rectangle relative to the TextViewVisual
        /// </summary>
        public Rect CursorRectangle
        {
            get => _cursorRectangle;
            protected set
            {
                var oldvalue = _cursorRectangle;

                if (oldvalue == value)
                {
                    return;
                }

                _cursorRectangle = value;

                OnCursorRectangleChanged(oldvalue, value);
            }
        }

        /// <summary>
        /// Gets or sets the curent selection range within current surrounding text.
        /// </summary>
        public TextSelection Selection
        {
            get => _selection;
            set
            {
                var oldValue = _selection;

                if (oldValue == value)
                {
                    return;
                }

                _selection = value;

                OnSelectionChanged(oldValue, value);
            }
        }

        /// <summary>
        /// Sets the non-committed input string
        /// </summary>
        public virtual void SetPreeditText(string? preeditText) { }

        protected virtual void OnCursorRectangleChanged(Rect oldValue, Rect newValue)
        {
            CursorRectangleChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnTextViewVisualChanged(Visual? oldValue, Visual? newValue)
        {
            TextViewVisualChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSurroundingTextChanged(string? oldValue, string? newValue)
        {
            SurroundingTextChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnSelectionChanged(TextSelection oldValue, TextSelection newValue)
        {
            SelectionChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public record struct TextSelection(int Start, int End);
}
